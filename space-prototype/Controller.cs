using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace space_prototype
{
    internal class Controller : Game
    {
        //Render
        private readonly GraphicsDeviceManager _graphics;
        private GameStateManager _gameStateManager;
        private SpriteBatch _spriteBatch;

        public Controller()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //2D spritebatch
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Window
            Window.Title = "3D Space Prototype";
            Window.AllowAltF4 = true;
            IsMouseVisible = true;

            _gameStateManager = new GameStateManager(_graphics, _spriteBatch, Content);
            _gameStateManager.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _gameStateManager.LoadContent();
        }

        protected override void UnloadContent()
        {
            _gameStateManager.UnloadContent();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _gameStateManager.Draw(gameTime);

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            _gameStateManager.Update(gameTime);

            base.Update(gameTime);
        }
    }
}