using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace space_prototype.GameStates
{
    public class WinScreen : Game
    {
        //Render
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public WinScreen()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();

            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Initialize()
        {
            //2D spritebatch
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Window
            Window.Title = "3D Space Prototype";
            Window.AllowAltF4 = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            throw new NotImplementedException();
        }

        protected override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();

            base.Update(gameTime);
        }
    }
}