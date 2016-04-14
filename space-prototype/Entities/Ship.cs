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
                Position = CheckWithinBounds(Position + new Vector3(0f, 0f, 1f));
                if (Position.Z > 10)
                {
                    RotationMatrix = Matrix.CreateRotationZ(MathHelper.ToRadians(-10f));
                }
                else
                {
                    RotationMatrix = Matrix.Identity;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Position = CheckWithinBounds(Position + new Vector3(0f, 0f, -1f));
                if (Position.Z < -10)
                {
                    RotationMatrix = Matrix.CreateRotationZ(MathHelper.ToRadians(10f));
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
            if ((v.Z < 26) && (v.Z > -26))
            {
                return v;
            }
            return Position;
        }
    }
}