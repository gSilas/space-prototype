using Microsoft.Xna.Framework;

namespace space_prototype.Entities
{
    public class Asteroid : Entity
    {
        public Asteroid(Vector3 position)
        {
            Position = position;
        }

        public override void Update(GameTime gameTime)
        {
            Rotation += (float) gameTime.ElapsedGameTime.TotalMilliseconds*MathHelper.ToRadians(0.25f);
            Position = Position + new Vector3(0.8f, 0, 0);
            RotateY();
        }
    }
}