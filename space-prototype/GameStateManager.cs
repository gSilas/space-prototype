using System.Collections.Generic;
using Microsoft.Xna.Framework;
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
            GameOver,
            Win,
            Credits
        }

        public static GameState CurrentGameState;

        public static GraphicsDeviceManager Graphics;
        public static SpriteBatch SpriteBatch;
        public static ContentManager Content;

        //MainGame
        private AsteroidField _asteroidField;

        private SpriteFont _bebasNeue;

        //MainMenu
        private Button _button1;
        private Button _button2;
        private Button _button3;
        private List<Button> _buttonList;
        private Camera _camera;
        private List<Entity> _entityList;
        private Song _mainSong;
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

            //Audio
            //_mainSong = Content.Load<Song>("Audio/n-Dimensions");

            //MainMenu
            _button1 = new Button(Content.Load<Texture2D>("UI/red_button01"), Content.Load<Texture2D>("UI/red_button02"));
            _button2 = new Button(Content.Load<Texture2D>("UI/red_button01"), Content.Load<Texture2D>("UI/red_button02"));
            _button3 = new Button(Content.Load<Texture2D>("UI/red_button01"), Content.Load<Texture2D>("UI/red_button02"));
            _button1.Position = new Vector2(300, 100);
            _button2.Position = new Vector2(300, 200);
            _button3.Position = new Vector2(300, 300);
            _button1.ButtonText = "Play";
            _button2.ButtonText = "Credits";
            _button3.ButtonText = "Restart";
            _buttonList.Add(_button1);
            _buttonList.Add(_button2);
            _buttonList.Add(_button3);

            //MainGame
            _asteroidField.LoadContent();
            _plane.LoadContent(Content, "models/bgplane");
            _ship.LoadContent(Content, "models/spaceship");
            _entityList.Add(_ship);
            _entityList.Add(_asteroidField);
            _entityList.Add(_plane);

            //Start Audio
            //MediaPlayer.Play(_mainSong);
            //MediaPlayer.Volume = 0.1f

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

            //MainGame
            _entityList = new List<Entity>();
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
            _asteroidField = new AsteroidField(60,Content);
            _asteroidField.Initialize();
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
            _asteroidField.Initialize();
            _asteroidField.LoadContent();
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
                    CurrentGameState = new MainGame(this, _bebasNeue, _entityList, _camera);
                    break;
                case GameStates.GameOver:
                    CurrentGameState = new GameOverScreen(this);
                    break;
                case GameStates.Win:
                    CurrentGameState = new WinScreen(this);
                    break;
                case GameStates.Credits:
                    CurrentGameState = new Credits(this, _bebasNeue);
                    break;
            }
        }
    }
}