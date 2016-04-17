using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using space_prototype.UI;

namespace space_prototype.GameStates
{
    public class MainMenu : Game
    {
        //Buttons
        private Button _button1;
        private Button _button2;
        private Button _button3;

        private List<Button> _buttonList;

        //Render
        private GraphicsDeviceManager _graphics;

        private Vector2 _mouseposition;
        private SpriteBatch _spriteBatch;

        //Visual components
        private SpriteFont BebasNeue;

        public MainMenu()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        //TODO UI
        protected override void Initialize()
        {
            //2D spritebatch
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Window
            Window.Title = "3D Space Prototype";
            Window.AllowAltF4 = true;
            IsMouseVisible = true;

            _buttonList = new List<Button>();

            base.Initialize();
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void LoadContent()
        {
            //Fonts
            BebasNeue = Content.Load<SpriteFont>("Fonts/bebasneue");
            _button1 = new Button(Content.Load<Texture2D>("UI/red_button01"), Content.Load<Texture2D>("UI/red_button02"));
            _button2 = new Button(Content.Load<Texture2D>("UI/red_button01"), Content.Load<Texture2D>("UI/red_button02"));
            _button3 = new Button(Content.Load<Texture2D>("UI/red_button01"), Content.Load<Texture2D>("UI/red_button02"));
            _button1.Position = new Vector2(300, 100);
            _button2.Position = new Vector2(300, 200);
            _button3.Position = new Vector2(300, 300);
            _button1.ButtonText = "Play";
            _button2.ButtonText = "Credits";
            _button3.ButtonText = "End";
            _buttonList.Add(_button1);
            _buttonList.Add(_button2);
            _buttonList.Add(_button3);
        }

        protected override void Update(GameTime gameTime)
        {
            var state = Mouse.GetState();
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
                            Program.NextGameState(Program.Gamestates.Game);
                        }
                        else if (button.ButtonText == "Credits")
                        {
                            Program.NextGameState(Program.Gamestates.Credits);
                        }
                        else if (button.ButtonText == "End")
                        {
                            Exit();
                        }
                    }
                    else
                    {
                        button.DeSelect();
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            foreach (var button in _buttonList)
            {
                _spriteBatch.Draw(button.TButton, button.Position);
                _spriteBatch.DrawString(BebasNeue, button.ButtonText, new Vector2(button.Position.X, button.Position.Y),
                    Color.LightGoldenrodYellow);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}