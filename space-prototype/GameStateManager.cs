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
            GameOver,
            Win,
            Credits,
            Options
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
        private Button _button4;
        private Button _button5;
        private List<Button> _buttonList;
        private List<Button> _buttonOptionsList;
        private Camera _camera;
        private SoundEffect _click;
        private SoundEffect _hit;
        private SoundEffect _laser;
        private Song _mainSong;
        private Gameboard _plane;
        private Ship _ship;
        public Model BulletModel;
        public Model BulletModel2;

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
            _mainSong = Content.Load<Song>("Audio/n-Dimensions");
            _laser = Content.Load<SoundEffect>("Audio/Effect/laser");
            _click = Content.Load<SoundEffect>("Audio/Effect/click");
            _hit = Content.Load<SoundEffect>("Audio/Effect/hit");

            //MainMenu
            _button1 = new Button(Content.Load<Texture2D>("UI/red_button01"), Content.Load<Texture2D>("UI/red_button02"));
            _button2 = new Button(Content.Load<Texture2D>("UI/red_button01"), Content.Load<Texture2D>("UI/red_button02"));
            _button3 = new Button(Content.Load<Texture2D>("UI/red_button01"), Content.Load<Texture2D>("UI/red_button02"));
            _button4 = new Button(Content.Load<Texture2D>("UI/red_button01"), Content.Load<Texture2D>("UI/red_button02"));
            _button5 = new Button(Content.Load<Texture2D>("UI/red_button01"), Content.Load<Texture2D>("UI/red_button02"));
            _button1.Position = new Vector2(300, 100);
            _button2.Position = new Vector2(300, 200);
            _button3.Position = new Vector2(300, 300);
            _button4.Position = new Vector2(300, 400);
            _button5.Position = new Vector2(300, 100);
            _button1.ButtonText = "Play";
            _button2.ButtonText = "Credits";
            _button3.ButtonText = "Restart";
            _button4.ButtonText = "Options";
            _button5.ButtonText = "Load Special Projectile (non reversable)";
            _buttonList.Add(_button1);
            _buttonList.Add(_button2);
            _buttonList.Add(_button3);
            _buttonList.Add(_button4);
            _buttonOptionsList.Add(_button5);

            //MainGame
            BulletModel = Content.Load<Model>("Models/projectile");
            BulletModel2 = Content.Load<Model>("Models/special_projectile");
            _asteroidField.LoadContent();
            _plane.LoadContent(Content, "models/bgplane");
            _ship.LoadContent(Content, "models/spaceship");
            //Start Audio
            MediaPlayer.Play(_mainSong);
            MediaPlayer.Volume = 0.1f;

            CurrentGameState = new MainMenu(this, _buttonList, _bebasNeue, _click);
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
            _asteroidField = new AsteroidField(50, Content);
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
            CurrentGameState = new MainMenu(this, _buttonList, _bebasNeue, _click);
        }

        public void NextGameState(GameStates target)
        {
            switch (target)
            {
                case GameStates.MainMenu:
                    CurrentGameState = new MainMenu(this, _buttonList, _bebasNeue, _click);
                    break;
                case GameStates.Game:
                    CurrentGameState = new MainGame(this, _bebasNeue, _camera, _ship, _plane, _asteroidField, _laser,
                        _hit);
                    break;
                case GameStates.GameOver:
                    CurrentGameState = new GameOverScreen(this, _bebasNeue);
                    break;
                case GameStates.Win:
                    CurrentGameState = new WinScreen(this);
                    break;
                case GameStates.Credits:
                    CurrentGameState = new Credits(this, _bebasNeue);
                    break;
                case GameStates.Options:
                    CurrentGameState = new Options(this, _buttonOptionsList, _bebasNeue);
                    break;
            }
        }
    }
}