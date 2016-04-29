using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using space_prototype.Entities;

namespace space_prototype.GameStates
{
    public class MainGame : GameState
    {
        private readonly Camera _camera;
        private readonly SpriteFont _font;
        private readonly GameStateManager _manager;
        private readonly Gameboard _plane;

        private readonly Ship _ship;

        public MainGame(GameStateManager manager, SpriteFont font, Camera camera, Ship ship, Gameboard plane)
        {
            _manager = manager;
            _font = font;
            _camera = camera;
            _ship = ship;
            _plane = plane;
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _manager.NextGameState(GameStateManager.GameStates.MainMenu);
            }


            _ship.Update(gameTime);
            _camera.Update(gameTime);
            _plane.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameStateManager.Graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            //3D stuff
            _plane.Draw(_camera);
            _ship.Draw(_camera);


            //2D SpriteBatch stuff
        }
    }
}