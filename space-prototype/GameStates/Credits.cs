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
            GameStateManager.SpriteBatch.DrawString(_font, "Credits to:", new Vector2(05, 0), Color.LightGoldenrodYellow);
            GameStateManager.SpriteBatch.DrawString(_font, "Monogame        Framework", new Vector2(10, 50),
                Color.LightGoldenrodYellow);
            GameStateManager.SpriteBatch.DrawString(_font, "kenney.nl       2D Art", new Vector2(10, 100),
                Color.LightGoldenrodYellow);
            GameStateManager.SpriteBatch.DrawString(_font, "Matthew Pablo   Music", new Vector2(10, 150),
                Color.LightGoldenrodYellow);
            GameStateManager.SpriteBatch.DrawString(_font, "dharmatype.com  Font", new Vector2(10, 200),
                Color.LightGoldenrodYellow);
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