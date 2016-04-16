using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace space_prototype.Entities
{
    internal class AsteroidField
    {
        private readonly Random _random = new Random();
        private readonly List<Asteroid> _asteroidList;
        private int _size;
        private readonly Collider3D _collider = new Collider3D();

        public AsteroidField(int size)
        {
            //TODO make them stay true to size
            _asteroidList = new List<Asteroid>();
            _size = size;
            var rx = _random.Next(-100, 100);
            var ry = _random.Next(-100, 100);
            var rz = _random.Next(-100, 100);

            var vec = new Vector3(rx, ry, rz);
            var a = new Asteroid(vec);

        }

        public void Initialize()
        {
            for (var i = 0; i < _size; i++)
            {
                var rx = _random.Next(-100, 100);
                var rz = _random.Next(-100, 100);

                var vec = new Vector3(rx, 20, rz);
                var a = new Asteroid(vec);
                _asteroidList.Add(a);
            }
        }

        public void LoadContent(ContentManager content)
        {
            //TODO maybe clean this up maybe not

            List<Asteroid> removeList = new List<Asteroid>();

            foreach (var asteroid in _asteroidList)
            {
                asteroid.LoadContent(content, "models/asteroid2");
            }
            foreach (var asteroid in _asteroidList)
            {
                foreach (var ast in _asteroidList)
                {

                    if (_collider.Intersection(asteroid, ast) && !ast.Equals(asteroid))
                    {
                        removeList.Add(ast);
                        Console.WriteLine("Success!");
                    }
                }
            }
            foreach (var asteroid in removeList)
            {
                _asteroidList.Remove(asteroid);
            }
            removeList = null;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var asteroid in _asteroidList)
            {
                asteroid.Update(gameTime);
            }
        }

        public void Draw(Camera camera)
        {
            foreach (var asteroid in _asteroidList)
            {
                asteroid.Draw(camera);
            }
        }
    }
}