using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using space_prototype.UI;

namespace space_prototype.GameStates
{
    public class MainMenu : GameState
    {
        //Buttons
        private Button _button1;
        private Button _button2;
        private Button _button3;

        private List<Button> _buttonList; 

        private Vector2 _mouseposition;

        //Visual components
        private SpriteFont BebasNeue;

        //TODO UI
        public override void Initialize()
        {
            _buttonList = new List<Button>();
            
        }

        public override void LoadContent(ContentManager content)
        {
            //Fonts
            BebasNeue = content.Load<SpriteFont>("Fonts/bebasneue");
            _button1 = new Button(content.Load<Texture2D>("UI/red_button02"),content.Load<Texture2D>("UI/red_button01"));
            _button2 = new Button(content.Load<Texture2D>("UI/red_button02"),content.Load<Texture2D>("UI/red_button01"));
            _button1.Position = new Vector2(300, 100);
            _button2.Position = new Vector2(300, 200);
            _button1.ButtonText = "Play";
            _button2.ButtonText = "Credits";
            _buttonList.Add(_button1);
            _buttonList.Add(_button2);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            _mouseposition.X = state.X;
            _mouseposition.Y = state.Y;

            if (state.LeftButton == ButtonState.Pressed)
            {
                foreach (var button in _buttonList)
                {
                    if (button.CursorOnButton(_mouseposition))
                    {
                        button.Select();
                        if (button.ButtonText == "Play")
                        {
                            Game1.NextGameState(Game1.Gamestates.Game);
                        }
                        else if (button.ButtonText == "Credits")
                        {
                            Game1.NextGameState(Game1.Gamestates.Credits);
                        }
                    }
                    else
                    {
                        button.DeSelect();
                    }
                }
            }
        }

        public override void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var button in _buttonList)
            {
                spriteBatch.Draw(button.TButton, button.Position);
                spriteBatch.DrawString(BebasNeue, button.ButtonText, new Vector2(button.Position.X, button.Position.Y), Color.LightGoldenrodYellow);
            }
            spriteBatch.End();
        }
    }
}