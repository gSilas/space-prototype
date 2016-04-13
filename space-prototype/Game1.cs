using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using space_prototype.Entities;

namespace space_prototype
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Render stuff
        private static GraphicsDeviceManager graphics;

        //3D Models
        private AsteroidField asteroidField;

        //Camera/View information
        private Camera camera;

        //Audio components
        private Song mainSong;

        //Visual components
        private SpriteFont motorwerk;
        private Gameboard plane;
        private Random rand;
        private Ship ship;
        private SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        ///     Allows the game to perform any initialization it needs to before starting to run.
        ///     This is where it can query for any required services and load any non-graphic
        ///     related content.  Calling base.Initialize will enumerate through any components
        ///     and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Window
            Window.Title = "3D Space Prototype";
            Window.AllowAltF4 = true;

            //Camera
            camera = new Camera(graphics.GraphicsDevice);
            camera.Position = new Vector3(0, 100, 0.0001f);
            camera.Target = Vector3.Zero;
            camera.UpVector = Vector3.UnitY;
            camera.FieldOfView = MathHelper.PiOver4;
            camera.NearClipPlane = 0.1f;
            camera.FarClipPlane = 10000f;
            camera.Angle = 0f;

            //Generators
            rand = new Random();

            //Plane
            plane = new Gameboard();

            //Entites
            asteroidField = new AsteroidField(10);
            ship = new Ship();
            ship.Position = new Vector3(0, -52, 20);

            base.Initialize();
        }

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Audio
            mainSong = Content.Load<Song>("Audio/mainTheme");

            //Fonts
            motorwerk = Content.Load<SpriteFont>("Fonts/motorwerk");

            //Models
            asteroidField.Initialize(Content);
            plane.Initialize(Content, "models/bgplane");
            ship.Initialize(Content, "models/spaceship");

            //Start Audio
            MediaPlayer.Play(mainSong);
            MediaPlayer.Volume = 0.1f;
        }

        /// <summary>
        ///     UnloadContent will be called once per game and is the place to unload
        ///     game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Controls
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            camera.Update(gameTime);
            asteroidField.Update(gameTime);
            ship.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkBlue);

            //3D stuff
            plane.Draw(camera);
            ship.Draw(camera);
            asteroidField.Draw(camera);

            //2D SpriteBatch stuff
            spriteBatch.Begin();
            spriteBatch.DrawString(motorwerk, "Move around with W (Up) and S (Down)!", new Vector2(50, 0),
                Color.LightGoldenrodYellow);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}