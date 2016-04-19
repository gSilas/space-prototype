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
        private readonly ContentManager _content;
        private readonly Random _random = new Random();
        private readonly int _size;

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
                var rx = _random.Next(-200, -100);
                var rz = _random.Next(-100, 100);

                var vec = new Vector3(rx, 20, rz);
                var a = new Asteroid(vec);
                _asteroidList.Add(a);
            }
        }

        private void InitializeChunk()
        {
            var addList = new List<Asteroid>();
            var removeList = new List<Asteroid>();

            for (var i = 0; i < 20; i++)
            {
                var rx = _random.Next(-200, -150);
                var rz = _random.Next(-100, 100);
                var r = _random.Next(-2, 1);

                var vec = new Vector3(rx, 20, rz);
                var a = new Asteroid(vec);

                if (r < 0)
                {
                    a.Model = _content.Load<Model>("Models/asteroid");
                }
                else if (r >= 0)
                {
                    a.Model = _content.Load<Model>("Models/asteroid2");
                }
                addList.Add(a);
            }

            foreach (var asteroid in _asteroidList)
            {
                foreach (var ast in _asteroidList)
                {
                    if (!ast.Equals(asteroid) && Collider3D.Intersection(asteroid, ast))
                    {
                        removeList.Add(ast);
                    }
                }
            }
            foreach (var asteroid in removeList)
            {
                addList.Remove(asteroid);
            }
            _asteroidList.AddRange(addList);
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
                    if (!ast.Equals(asteroid) && Collider3D.Intersection(asteroid, ast))
                    {
                        removeList.Add(ast);
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
            foreach (var asteroid in _asteroidList)
            {
                asteroid.Update(gameTime);
            }
            foreach (var asteroid in _asteroidList)
            {
                if (asteroid.Position.X > 200)
                {
                    removeList.Add(asteroid);
                }
                foreach (var ast in _asteroidList)
                {
                    if (Collider3D.Intersection(asteroid, ast) && !ast.Equals(asteroid))
                    {
                        removeList.Add(ast);
                    }
                }
            }
            foreach (var asteroid in removeList)
            {
                _asteroidList.Remove(asteroid);
            }
            if (gameTime.TotalGameTime.Milliseconds%60000 == 0)
                InitializeChunk();
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