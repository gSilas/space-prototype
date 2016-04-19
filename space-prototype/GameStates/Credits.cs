using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace space_prototype.GameStates
{
    public class Credits : GameState
    {
        private readonly SpriteFont _font;
        private readonly GameStateManager _manager;

        public Credits(GameStateManager manager, SpriteFont font)
        {
            _font = font;
            _manager = manager;
        }

        public override void Draw(GameTime gameTime)
        {
            GameStateManager.SpriteBatch.DrawString(_font, "Dangi!", new Vector2(50, 0), Color.LightGoldenrodYellow);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _manager.NextGameState(GameStateManager.GameStates.MainMenu);
            }
        }
    }
}