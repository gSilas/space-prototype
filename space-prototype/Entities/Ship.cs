using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace space_prototype.Entities
{
    public class Ship : Entity
    {
        //TODO
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Position = Position + new Vector3(0f, 1f, 0f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Position = Position + new Vector3(0f, -1f, 0f);
            }
        }
    }
}