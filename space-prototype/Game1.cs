﻿using System;
using Microsoft.Xna.Framework;
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
        Vector3 cameraPosition = new Vector3(0.0f, -300.0f, 10.0f);
        Matrix projectionMatrix;
        Matrix viewMatrix;

        SpriteFont motorwerk;

        //Visual components
        //Ship ship = new Ship();
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
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                GraphicsDevice.DisplayMode.AspectRatio, 1.0f, 1000.0f);
            viewMatrix = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
            asteroid.Position = new Vector3(0.1f, 0f, 0f);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mainSong = Content.Load<Song>("Audio/mainTheme");
            motorwerk = Content.Load<SpriteFont>("Fonts/motorwerk");
            //ship.Model = Content.Load<Model>("Models/torusknot");
            //ship.Transforms = SetupEffectDefaults(ship.Model);
            asteroid.Model = Content.Load<Model>("Models/asteroid");
            asteroid.Transforms = SetupEffectDefaults(asteroid.Model);
            MediaPlayer.Play(mainSong);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                asteroid.Position = asteroid.Position + new Vector3(0f, 0f, -5f);
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                asteroid.Position = asteroid.Position + new Vector3(0f, 0f, 5f);
            if(Keyboard.GetState().IsKeyDown(Keys.A))
                asteroid.Position = asteroid.Position + new Vector3(-5f, 0f, 0f);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                asteroid.Position = asteroid.Position + new Vector3(5f, 0f, 0f);
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                asteroid.Position = asteroid.Position + new Vector3(0f, 5f, 0f);
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                asteroid.Position = asteroid.Position + new Vector3(0f, -5f, 0f);

            //ship.Rotation += (float) gameTime.ElapsedGameTime.TotalMilliseconds*MathHelper.ToRadians(0.2f);
            asteroid.Rotation += (float) gameTime.ElapsedGameTime.TotalMilliseconds*MathHelper.ToRadians(0.15f);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSalmon);
            spriteBatch.Begin();
            spriteBatch.DrawString(motorwerk, "Best Game 2016! Make Games great again!", Vector2.Zero, Color.Black);
            spriteBatch.End();
            //Matrix shipTransformMatrix = ship.RotationMatrix*Matrix.CreateTranslation(ship.Position);
            Matrix asteroidTransformMatrix = asteroid.RotationMatrix*Matrix.CreateTranslation(asteroid.Position);
            //DrawModel(ship.Model, shipTransformMatrix, ship.Transforms);
            DrawModel(asteroid.Model, asteroidTransformMatrix, asteroid.Transforms);

            base.Draw(gameTime);
        }

        private Matrix[] SetupEffectDefaults(Model myModel)
        {
            Matrix[] absoluteTransforms = new Matrix[myModel.Bones.Count];
            myModel.CopyAbsoluteBoneTransformsTo(absoluteTransforms);

            foreach (ModelMesh mesh in myModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.Projection = projectionMatrix;
                    effect.View = viewMatrix;
                }
            }

            return absoluteTransforms;
        }

        void DrawModel(Model model, Matrix modelTransform, Matrix[] absoluteBoneTransform)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = absoluteBoneTransform[mesh.ParentBone.Index]*modelTransform;
                }
                mesh.Draw();
            }
        }
    }
}