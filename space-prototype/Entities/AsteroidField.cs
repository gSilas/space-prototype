using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace space_prototype.Entities
{
    internal class AsteroidField
    {
        private readonly Random _random = new Random();
        private readonly List<Asteroid> asteroidList;

        public AsteroidField(int size)
        {
            asteroidList = new List<Asteroid>();

            for (var i = 0; i < size; i++)
            {
                var r1 = _random.Next(-24, 24);
                var r2 = _random.Next(0, 20);

                var vec = new Vector3(-50 + r2,20 ,r1);
                asteroidList.Add(new Asteroid(vec));
            }
        }

        public void Initialize(ContentManager content)
        {
            foreach (var asteroid in asteroidList)
            {
                asteroid.Initialize(content, "models/asteroid");
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var asteroid in asteroidList)
            {
                asteroid.Update(gameTime);
            }
        }

        public void Draw(Camera camera)
        {
            foreach (var asteroid in asteroidList)
            {
                asteroid.Draw(camera);
            }
        }
    }
}