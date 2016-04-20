using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace space_prototype.Entities
{
    public class AsteroidField
    {
        public List<Asteroid> AsteroidList;
        private readonly ContentManager _content;
        private readonly Random _random = new Random();
        private readonly int _size;

        public AsteroidField(int size, ContentManager content)
        {
            //TODO make them stay true to size
            AsteroidList = new List<Asteroid>();
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
                AsteroidList.Add(a);
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

            foreach (var asteroid in AsteroidList)
            {
                foreach (var ast in AsteroidList)
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
            AsteroidList.AddRange(addList);
        }

        public void LoadContent()
        {
            //TODO maybe clean this up maybe not

            var removeList = new List<Asteroid>();

            for (var i = 0; i < AsteroidList.Count; i++)
            {
                if (i%2 == 0)
                {
                    AsteroidList[i].Model = _content.Load<Model>("Models/asteroid");
                }
                else
                    AsteroidList[i].Model = _content.Load<Model>("Models/asteroid2");
            }

            foreach (var asteroid in AsteroidList)
            {
                foreach (var ast in AsteroidList)
                {
                    if (!ast.Equals(asteroid) && Collider3D.Intersection(asteroid, ast))
                    {
                        removeList.Add(ast);
                    }
                }
            }
            foreach (var asteroid in removeList)
            {
                AsteroidList.Remove(asteroid);
            }
        }

        public void Update(GameTime gameTime)
        {
            var removeList = new List<Asteroid>();
            foreach (var asteroid in AsteroidList)
            {
                asteroid.Update(gameTime);
            }
            foreach (var asteroid in AsteroidList)
            {
                if (asteroid.Position.X > 200)
                {
                    removeList.Add(asteroid);
                }
                foreach (var ast in AsteroidList)
                {
                    if (Collider3D.Intersection(asteroid, ast) && !ast.Equals(asteroid))
                    {
                        removeList.Add(ast);
                    }
                }
            }
            foreach (var asteroid in removeList)
            {
                AsteroidList.Remove(asteroid);
            }
            if (gameTime.TotalGameTime.Milliseconds%60000 == 0)
                InitializeChunk();
        }

        public void Draw(Camera camera)
        {
            foreach (var asteroid in AsteroidList)
            {
                asteroid.Draw(camera);
            }
        }
    }
}