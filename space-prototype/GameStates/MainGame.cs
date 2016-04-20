using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using space_prototype.Entities;

namespace space_prototype.GameStates
{
    public class MainGame : GameState
    {
        private readonly List<Projectile> _bulletList;
        private readonly Camera _camera;
        private readonly Gameboard _plane;
        private readonly AsteroidField _asteroids;
        private readonly SpriteFont _font;
        private readonly GameStateManager _manager;

        private readonly Ship _ship;

        public MainGame(GameStateManager manager, SpriteFont font,  Camera camera, Ship ship, Gameboard plane, AsteroidField asteroids)
        {
            _bulletList = new List<Projectile>();
            _manager = manager;
            _font = font;
            _camera = camera;
            _ship = ship;
            _plane = plane;
            _asteroids = asteroids;
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                SpawnBullet();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _manager.NextGameState(GameStateManager.GameStates.MainMenu);
            }
            _ship.Update(gameTime);
            _camera.Update(gameTime);
            _plane.Update(gameTime);
            _asteroids.Update(gameTime);
            foreach (var bullet in _bulletList)
            {
                bullet.Update(gameTime);
            }
            if (gameTime.TotalGameTime.Milliseconds % 200 == 0)
                BulletCollide(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameStateManager.Graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            //3D stuff
            _plane.Draw(_camera);
            _ship.Draw(_camera);
            foreach (var bullet in _bulletList)
            {
                bullet.Draw(_camera);
            }
            _asteroids.Draw(_camera);

            //2D SpriteBatch stuff
            GameStateManager.SpriteBatch.DrawString(_font, "Move around with W (Up) and S (Down) and JKLIUO for Camera!",
                new Vector2(50, 0), Color.LightGoldenrodYellow);
        }

        private void SpawnBullet()
        {
            var bullet = new Projectile(_ship.Position);
            bullet.Model = _manager.BulletModel;
            _bulletList.Add(bullet);
        }
        //TODO rework this
        private void BulletCollide(GameTime gameTime)
        {
            var aremoveList = new List<Asteroid>();
            var bremoveList = new List<Projectile>();
            foreach (var asteroid in _asteroids.AsteroidList)
            {              
                foreach (var bullet in _bulletList)
                {
                    if (bullet.Position.X < -200)
                    {
                        bremoveList.Add(bullet);
                    }

                    if (asteroid.Model != null)
                    {
                        if (Collider3D.Intersection(bullet, asteroid))
                        {
                            aremoveList.Add(asteroid);
                            bremoveList.Add(bullet);
                        }
                    }
                }
            }
            foreach (var ast in aremoveList)
            {
                _asteroids.AsteroidList.Remove(ast);
            }
            foreach (var bullet in bremoveList)
            {
                _bulletList.Remove(bullet);
            }
        }
    }
}