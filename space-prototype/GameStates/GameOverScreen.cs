using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace space_prototype.GameStates
{
    public class GameOverScreen : GameState
    {
        private readonly SpriteFont _font;
        private readonly GameStateManager _manager;

        public GameOverScreen(GameStateManager manager, SpriteFont font)
        {
            _font = font;
            _manager = manager;
        }

        public override void Draw(GameTime gameTime)
        {
            GameStateManager.SpriteBatch.DrawString(_font, "You died!", new Vector2(200, 100),
                Color.LightGoldenrodYellow);
            GameStateManager.SpriteBatch.DrawString(_font, "Press Escape to return to Menu!", new Vector2(200, 200),
                Color.LightGoldenrodYellow);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _manager.Restart();
                _manager.NextGameState(GameStateManager.GameStates.MainMenu);
            }
        }
    }
}