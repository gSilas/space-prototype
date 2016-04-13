﻿using Microsoft.Xna.Framework;
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
                Position = CheckWithinBounds(Position + new Vector3(0f, 1f, 0f));
                RotationMatrix = Matrix.CreateRotationZ(MathHelper.ToRadians(10f));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Position = CheckWithinBounds(Position + new Vector3(0f, -1f, 0f));
                RotationMatrix = Matrix.CreateRotationZ(MathHelper.ToRadians(-10f));
            }
            //this.RotationMatrix = Matrix.Identity;
        }

        private Vector3 CheckWithinBounds(Vector3 v)
        {
            if ((v.Y < 26) && (v.Y > -26))
            {
                return v;
            }
            return Position;
        }
    }
}