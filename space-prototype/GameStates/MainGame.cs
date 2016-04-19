using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using space_prototype.Entities;

namespace space_prototype.GameStates
{
    public class MainGame : GameState
    {
        private readonly Camera _camera;

        private readonly List<Entity> _entityList;

        private readonly SpriteFont _font;
        private readonly GameStateManager _manager;

        public MainGame(GameStateManager manager, SpriteFont font, List<Entity> entityList, Camera camera)
        {
            _manager = manager;
            _font = font;
            _entityList = entityList;
            _camera = camera;
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _manager.NextGameState(GameStateManager.GameStates.MainMenu);
            }

            foreach (var entity in _entityList)
            {
                entity.Update(gameTime);
            }

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

            //2D SpriteBatch stuff
            GameStateManager.SpriteBatch.DrawString(_font, "Move around with W (Up) and S (Down) and JKLIUO for Camera!",
                new Vector2(50, 0), Color.LightGoldenrodYellow);
        }
    }
}