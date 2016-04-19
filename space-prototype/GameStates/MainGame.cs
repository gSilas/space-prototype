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

        private readonly List<Entity> _entityList;

        private readonly SpriteFont _font;
        private readonly GameStateManager _manager;

        private readonly Ship _ship;

        public MainGame(GameStateManager manager, SpriteFont font, List<Entity> entityList, Camera camera, Ship ship)
        {
            _bulletList = new List<Projectile>();
            _manager = manager;
            _font = font;
            _entityList = entityList;
            _camera = camera;
            _ship = ship;
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
            BulletCollide(gameTime);
            _camera.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameStateManager.Graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            GameStateManager.Graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            //3D stuff
            foreach (var entity in _entityList)
            {
                entity.Draw(_camera);
            }
            foreach (var bullet in _bulletList)
            {
                bullet.Draw(_camera);
            }

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
            var eremoveList = new List<Entity>();
            var bremoveList = new List<Projectile>();

            foreach (var entity in _entityList)
            {
                entity.Update(gameTime);
                foreach (var bullet in _bulletList)
                {
                    bullet.Update(gameTime);

                    if (bullet.Position.X > 200)
                    {
                        bremoveList.Add(bullet);
                    }

                    if (entity != _ship && entity.Model != null)
                    {
                        if (Collider3D.Intersection(bullet, entity))
                        {
                            eremoveList.Add(entity);
                            bremoveList.Add(bullet);
                        }
                    }
                }
            }
            foreach (var entity in eremoveList)
            {
                _entityList.Remove(entity);
            }
            foreach (var bullet in bremoveList)
            {
                _bulletList.Remove(bullet);
            }
        }
    }
}