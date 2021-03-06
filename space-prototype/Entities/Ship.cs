﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace space_prototype.Entities
{
    public class Ship : Entity
    {
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Position = CheckWithinBounds(Position + new Vector3(0f, 0f, 2.5f));
                if (Position.Z > 10 && Position.Z < 30)
                {
                    RotationMatrix = Matrix.CreateRotationZ(MathHelper.ToRadians(-10f));
                }
                else if (Position.Z > 30)
                {
                    RotationMatrix = Matrix.CreateRotationZ(MathHelper.ToRadians(-20f));
                }
                else
                {
                    RotationMatrix = Matrix.Identity;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Position = CheckWithinBounds(Position + new Vector3(0f, 0f, -2.5f));
                if (Position.Z < -10 && Position.Z > -30)
                {
                    RotationMatrix = Matrix.CreateRotationZ(MathHelper.ToRadians(10f));
                    ;
                }
                else if (Position.Z < -30)
                {
                    RotationMatrix = Matrix.CreateRotationZ(MathHelper.ToRadians(20f));
                    ;
                }
                else
                {
                    RotationMatrix = Matrix.Identity;
                }
            }
        }

        private Vector3 CheckWithinBounds(Vector3 v)
        {
            if ((v.Z < 70) && (v.Z > -70))
            {
                return v;
            }
            return Position;
        }
    }
}