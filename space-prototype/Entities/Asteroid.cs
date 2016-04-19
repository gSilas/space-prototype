using System;
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
            /* if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                this.Position = this.Position + new Vector3(0f, -1f, 0f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                this.Position = this.Position + new Vector3(0f, 1f, 0f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.Position = this.Position + new Vector3(-1f, 0f, 0f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.Position = this.Position + new Vector3(1f, 0f, 0f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                this.Position = this.Position + new Vector3(0f, 0f, 1f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                this.Position = this.Position + new Vector3(0f, 0f, -1f);
            }*/

            Rotation += (float) gameTime.ElapsedGameTime.TotalMilliseconds*MathHelper.ToRadians(0.25f);
            Position = Position + new Vector3(0.5f, 0, 0);
            RotateY();
        }
    }
}