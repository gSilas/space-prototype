using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using space_prototype.Entities;

namespace space_prototype.GameStates
{
    public class MainGame : Game
    {
        //Render
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //3D Models
        private AsteroidField asteroidField;

        //Visual components
        private SpriteFont BebasNeue;

        //Camera/View information
        private Camera camera;

        //Audio components
        private Song mainSong;
        private Gameboard plane;
        private Ship ship;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Initialize()
        {
            //2D spritebatch
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Camera
            camera = new Camera();
            camera.Position = new Vector3(0, 100, 0);
            camera.Target = Vector3.Zero;
            camera.UpVector = Vector3.UnitZ;
            camera.FieldOfView = MathHelper.PiOver2;
            camera.NearClipPlane = 0.1f;
            camera.FarClipPlane = 10000f;
            camera.AspectRatio = _graphics.GraphicsDevice.DisplayMode.AspectRatio;

            //Plane
            plane = new Gameboard();

            //Entites
            asteroidField = new AsteroidField(100);
            asteroidField.Initialize();
            ship = new Ship();
            ship.Position = new Vector3(52, 20, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Audio
            mainSong = Content.Load<Song>("Audio/n-Dimensions");

            //Fonts
            BebasNeue = Content.Load<SpriteFont>("Fonts/bebasneue");

            //Models
            asteroidField.LoadContent(Content);
            plane.LoadContent(Content, "models/bgplane");
            ship.LoadContent(Content, "models/spaceship");

            //Start Audio
            MediaPlayer.Play(mainSong);
            MediaPlayer.Volume = 0.1f;
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
                Program.NextGameState(Program.Gamestates.MainMenu);
            }

            camera.Update(gameTime);
            asteroidField.Update(gameTime);
            ship.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            //3D stuff
            plane.Draw(camera);
            ship.Draw(camera);
            asteroidField.Draw(camera);

            //2D SpriteBatch stuff
            _spriteBatch.Begin();
            _spriteBatch.DrawString(BebasNeue, "Move around with W (Up) and S (Down) and JKLIUO for Camera!",
                new Vector2(50, 0), Color.LightGoldenrodYellow);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}