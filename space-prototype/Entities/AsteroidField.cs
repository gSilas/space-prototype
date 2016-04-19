using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace space_prototype.Entities
{
    internal class AsteroidField : Entity
    {
        private readonly List<Asteroid> _asteroidList;
        private readonly Random _random = new Random();
        private readonly int _size;
        private ContentManager _content;

        public AsteroidField(int size, ContentManager content)
        {
            //TODO make them stay true to size
            _asteroidList = new List<Asteroid>();
            _size = size;
            _content = content;
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
                var rx = _random.Next(-300, -200);
                var rz = _random.Next(-100, 100);

                var vec = new Vector3(rx, 20, rz);
                var a = new Asteroid(vec);
                _asteroidList.Add(a);
            }
        }

        private void InitializeOne()
        {
            var rx = _random.Next(-150, -100);
            var rz = _random.Next(-100, 100);
            var r = _random.Next(-100, 100);

            var vec = new Vector3(rx, 20, rz);
            var a = new Asteroid(vec);
            if (r < 0)
            {
                a.Model = _content.Load<Model>("Models/asteroid");
            }
            else if(r >= 0)
            {
                a.Model = _content.Load<Model>("Models/asteroid2");
            }
            _asteroidList.Add(a);
        }

        public void LoadContent()
        {
            //TODO maybe clean this up maybe not

            var removeList = new List<Asteroid>();

            for (var i = 0; i < _asteroidList.Count; i++)
            {
                if (i%2 == 0)
                {
                    _asteroidList[i].Model = _content.Load<Model>("Models/asteroid");
                }
                else
                    _asteroidList[i].Model = _content.Load<Model>("Models/asteroid2");
            }

            foreach (var asteroid in _asteroidList)
            {
                foreach (var ast in _asteroidList)
                {
                    if (Collider3D.Intersection(asteroid, ast) && !ast.Equals(asteroid))
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
        }

        public override void Update(GameTime gameTime)
        {
            var removeList = new List<Asteroid>();
            var count = 0;
            foreach (var asteroid in _asteroidList)
            {
                asteroid.Update(gameTime);
            }
            foreach (var asteroid in _asteroidList)
            {
                if (asteroid.Position.X > 100)
                {
                    removeList.Add(asteroid);
                }
                if (asteroid.Position.X > -90)
                {
                    count++;
                }
            }
            foreach (var asteroid in removeList)
            {
                _asteroidList.Remove(asteroid);
            }
            for ( int i = 0; i < count; i++)
            {
                    InitializeOne();
                }
        }

        public override void Draw(Camera camera)
        {
            foreach (var asteroid in _asteroidList)
            {
                asteroid.Draw(camera);
            }
        }
    }
}