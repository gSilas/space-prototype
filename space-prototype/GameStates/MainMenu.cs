using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using space_prototype.UI;

namespace space_prototype.GameStates
{
    public class MainMenu : GameState
    {
        private readonly List<Button> _buttonList;
        private readonly SpriteFont _font;
        private readonly GameStateManager _manager;
        private Vector2 _mouseposition;

        public MainMenu(GameStateManager manager, List<Button> buttonList, SpriteFont font)
        {
            _buttonList = buttonList;
            _font = font;
            _manager = manager;
        }

        public override void Update(GameTime gameTime)
        {
            var state = Mouse.GetState();
            _mouseposition.X = state.X;
            _mouseposition.Y = state.Y;
            foreach (var button in _buttonList)
            {
                if (button.Selected && !button.CursorOnButton(_mouseposition))
                {
                    button.DeSelect();
                }
                //TODO replace string matching with Enum/ID
                if (state.LeftButton == ButtonState.Pressed)
                {
                    if (button.CursorOnButton(_mouseposition))
                    {
                        button.Select();
                        if (button.ButtonText == "Play")
                        {
                            _manager.NextGameState(GameStateManager.GameStates.Game);
                        }
                     
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var button in _buttonList)
            {
                GameStateManager.SpriteBatch.Draw(button.TButton, button.Position);
                GameStateManager.SpriteBatch.DrawString(_font, button.ButtonText,
                    new Vector2(button.Position.X, button.Position.Y), Color.LightGoldenrodYellow);
            }
        }
    }
}