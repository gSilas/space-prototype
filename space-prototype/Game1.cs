using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using space_prototype.Tools;

namespace space_prototype
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Render stuff
        static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Camera/View information
        Camera camera;

        SpriteFont motorwerk;

        //Visual components
        Asteroid asteroid = new Asteroid();

        //Audio components
        Song mainSong;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Window
            Window.Title = "3D Space Prototype";
            Window.AllowAltF4 = true;

            //Camera
            camera = new Camera(graphics.GraphicsDevice);
            camera.Position = new Vector3(0, 1000, 0);
            camera.Target = Vector3.Zero;
            camera.Angle = 0f;

            //Entites
            asteroid.Position = Vector3.Zero;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Audio
            mainSong = Content.Load<Song>("Audio/mainTheme");

            //Fonts
            motorwerk = Content.Load<SpriteFont>("Fonts/motorwerk");

            //Models
            asteroid.Initialize(Content, "models/asteroid");

            //Start Audio
            MediaPlayer.Play(mainSong);
            MediaPlayer.Volume = 0.1f;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Controls
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                asteroid.Position = asteroid.Position + new Vector3(0f, -5f, 0f);
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                asteroid.Position = asteroid.Position + new Vector3(0f, 5f, 0f);
            if(Keyboard.GetState().IsKeyDown(Keys.A))
                asteroid.Position = asteroid.Position + new Vector3(-5f, 0f, 0f);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                asteroid.Position = asteroid.Position + new Vector3(5f, 0f, 0f);
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                asteroid.Position = asteroid.Position + new Vector3(0f, 0f, 5f);
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                asteroid.Position = asteroid.Position + new Vector3(0f, 0f, -5f);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //2D SpriteBatch stuff
            spriteBatch.Begin();
            spriteBatch.DrawString(motorwerk, "Move the asteroid around with WASDQE!", Vector2.Zero, Color.Black);
            spriteBatch.End();

            //3D stuff
            asteroid.Draw(camera);
            
            base.Draw(gameTime);
        }
    }
}