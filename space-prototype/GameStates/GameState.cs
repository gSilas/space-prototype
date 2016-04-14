using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace space_prototype.GameStates
{
    public abstract class GameState
    {
        public abstract void Initialize();
        public abstract void LoadContent(ContentManager content);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch);

        public void Next(int StateID)
        {
        }
    }
}