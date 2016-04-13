﻿using System;
using System.Collections.Generic;
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
        private Asteroid asteroid2;
        private List<Asteroid> asteroidList;

        //Camera/View information
        private Camera camera;

        //Audio components
        private Song mainSong;

        //Visual components
        private SpriteFont motorwerk;
        private Gameboard plane;

        //Generators
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
            camera.Position = new Vector3(0, 0, 100);
            camera.Target = Vector3.Zero;
            camera.UpVector = Vector3.UnitY;
            camera.FieldOfView = MathHelper.PiOver4;
            camera.NearClipPlane = 0.1f;
            camera.FarClipPlane = 10000f;
            camera.Angle = 0f;

            //Generators
            rand = new Random();

            //Containers
            asteroidList = new List<Asteroid>();

            //Plane
            plane = new Gameboard();

            //Entites
            ship = new Ship();
            ship.Position = new Vector3(-52, 0, 20);

            for (var i = 0; i < 11; i++)
            {
                var r = rand.Next(21);
                var vec = new Vector3(50 - r, r - 7*i, 20);
                asteroidList.Add(new Asteroid(vec));
            }


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
            plane.Initialize(Content, "models/bgplane");
            foreach (var asteroid in asteroidList)
            {
                asteroid.Initialize(Content, "models/asteroid");
            }
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

            foreach (var asteroid in asteroidList)
            {
                asteroid.Update(gameTime);
            }
            ship.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //2D SpriteBatch stuff
            spriteBatch.Begin();
            spriteBatch.DrawString(motorwerk, "Move the ship around with WS!", Vector2.Zero, Color.Black);
            spriteBatch.End();

            camera.Update(gameTime);

            //3D stuff
            plane.Draw(camera);

            foreach (var asteroid in asteroidList)
            {
                asteroid.Draw(camera);
            }

            ship.Draw(camera);

            base.Draw(gameTime);
        }
    }
}