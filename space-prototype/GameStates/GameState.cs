using Microsoft.Xna.Framework;

namespace space_prototype.GameStates
{
    public abstract class GameState
    {
        private GameStateManager manager;

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);
    }
}