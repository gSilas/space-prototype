using Microsoft.Xna.Framework;

namespace space_prototype.Entities
{
    public class Projectile : Entity
    {
        public Projectile(Vector3 position)
        {
            Position = position;
        }

        public override void Update(GameTime gameTime)
        {
            Position = Position + new Vector3(-2f, 0, 0);
        }
    }
}