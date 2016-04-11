﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace space_prototype
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Camera/View information
        public static Vector3 CameraPosition = new Vector3(0.0f, -1000.0f, 0.1f);
        public static Matrix ProjectionMatrix;
        public static Matrix ViewMatrix;

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
            Window.Title = "3D Space Prototype!";
            Window.AllowAltF4 = true;

            //Camera
            ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),GraphicsDevice.DisplayMode.AspectRatio, 1.0f, 1000.0f);
            ViewMatrix = Matrix.CreateLookAt(CameraPosition, Vector3.Zero, Vector3.Up);

            //Entites
            asteroid.Position = new Vector3(0f, 0f, 0f);

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
            asteroid.Model = Content.Load<Model>("Models/asteroid");

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

            //Movement
            asteroid.Rotation += (float) gameTime.ElapsedGameTime.TotalMilliseconds*MathHelper.ToRadians(0.05f);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.PaleGoldenrod);

            //2D SpriteBatch stuff
            spriteBatch.Begin();
            spriteBatch.DrawString(motorwerk, "Move around with WASDQE!", Vector2.Zero, Color.Black);
            spriteBatch.End();

            //3D stuff
            asteroid.DrawEntity();
            
            base.Draw(gameTime);
        }       
    }
}