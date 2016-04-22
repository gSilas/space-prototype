using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using space_prototype.UI;

namespace space_prototype.GameStates
{
    internal class Options : GameState
    {
        private readonly List<Button> _buttonList;
        private readonly SpriteFont _font;
        private readonly GameStateManager _manager;
        private readonly SoundEffect _click;
        private Vector2 _mouseposition;


        public Options(GameStateManager manager, List<Button> buttonList, SpriteFont font, SoundEffect click)
        {
            _buttonList = buttonList;
            _font = font;
            _manager = manager;
            _click = click;
        }

        public override void Update(GameTime gameTime)
        {
            var state = Mouse.GetState();
            _mouseposition.X = state.X;
            _mouseposition.Y = state.Y;
            foreach (var button in _buttonList)
            {
                if (button.CursorOnButton(_mouseposition))
                {
                    button.DeSelect();
                }
            }
            if (state.LeftButton == ButtonState.Pressed)
            {
                foreach (var button in _buttonList)
                {
                    if (button.CursorOnButton(_mouseposition))
                    {
                        button.Select();
                        _click.Play(0.07f, 0, 0);
                        if (button.ButtonText == "Load Special Projectile (non reversable)")
                        {
                            _manager.BulletModel = _manager.BulletModel2;
                        }
                    }
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _manager.NextGameState(GameStateManager.GameStates.MainMenu);
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