using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using space_prototype.Entities;
using space_prototype.GameStates;
using space_prototype.UI;

namespace space_prototype
{
    public class GameStateManager
    {
        public enum GameStates
        {
            MainMenu,
            Game,
            GameOver
        }

        public static GameState CurrentGameState;

        public static GraphicsDeviceManager Graphics;
        public static SpriteBatch SpriteBatch;
        public static ContentManager Content;

        //MainGame
        private SpriteFont _bebasNeue;

        //MainMenu
        private Button _button1;
        private Button _button2;
        private Button _button3;
        private Button _button4;
        private Button _button5;
        private List<Button> _buttonList;
        private List<Button> _buttonOptionsList;
        private Camera _camera;
  
        private Gameboard _plane;
        private Ship _ship;

        public GameStateManager(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, ContentManager content)
        {
            Graphics = graphics;
            SpriteBatch = spriteBatch;
            Content = content;
        }

        public void LoadContent()
        {
            //Font
            _bebasNeue = Content.Load<SpriteFont>("Fonts/bebasneue");

         

            //MainMenu //TODO add nice function
            _button1 = new Button(Content.Load<Texture2D>("UI/red_button02"), Content.Load<Texture2D>("UI/red_button01"));
          
            _button1.Position = new Vector2(300, 100);

            _button1.ButtonText = "Play";
   
            _buttonList.Add(_button1);


            //MainGame
            _plane.LoadContent(Content, "models/bgplane");
            _ship.LoadContent(Content, "models/spaceship");

            CurrentGameState = new MainMenu(this, _buttonList, _bebasNeue);
        }

        public void UnloadContent()
        {
            Content.Unload();
        }

        public void Initialize()
        {
            //MainMenu
            _buttonList = new List<Button>();

            //Options
            _buttonOptionsList = new List<Button>();

            //MainGame
            _camera = new Camera();
            _camera.Position = new Vector3(0, 100, 0);
            _camera.Target = Vector3.Zero;
            _camera.UpVector = Vector3.UnitZ;
            _camera.FieldOfView = MathHelper.PiOver2;
            _camera.NearClipPlane = 0.1f;
            _camera.FarClipPlane = 10000f;
            _camera.AspectRatio = Graphics.GraphicsDevice.DisplayMode.AspectRatio;
            _plane = new Gameboard();
            _plane.Position = Vector3.Zero;
            _ship = new Ship();
            _ship.Position = new Vector3(95, 20, 0);
            //
        }

        public void Update(GameTime gameTime)
        {
            CurrentGameState.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            CurrentGameState.Draw(gameTime);
            SpriteBatch.End();
        }

        public void Restart()
        {
            _camera.Position = new Vector3(0, 100, 0);
            _camera.Target = Vector3.Zero;
            _camera.UpVector = Vector3.UnitZ;
            _camera.FieldOfView = MathHelper.PiOver2;
            _camera.NearClipPlane = 0.1f;
            _camera.FarClipPlane = 10000f;
            _camera.AspectRatio = Graphics.GraphicsDevice.DisplayMode.AspectRatio;
            _plane.Position = Vector3.Zero;
            _ship.Position = new Vector3(95, 20, 0);
            CurrentGameState = new MainMenu(this, _buttonList, _bebasNeue);
        }

        public void NextGameState(GameStates target)
        {
            switch (target)
            {
                case GameStates.MainMenu:
                    CurrentGameState = new MainMenu(this, _buttonList, _bebasNeue);
                    break;
                case GameStates.Game:
                    CurrentGameState = new MainGame(this, _bebasNeue, _camera, _ship, _plane);
                    break;
                case GameStates.GameOver:
                    CurrentGameState = new GameOverScreen(this, _bebasNeue);
                    break;
            }
        }
    }
}