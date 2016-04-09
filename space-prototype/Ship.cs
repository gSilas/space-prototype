using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace space_prototype
{
    class Ship
    {
        public Model Model;
        public Matrix[] Transforms;

        public Vector3 Position = Vector3.Zero;

        public Vector3 Velocity = Vector3.Zero;

        public Matrix RotationMatrix = Matrix.Identity;

        private float _rotation;

        public float Rotation
        {
            get { return _rotation; }
            set
            {
                float newVal = value;

                while (newVal < 0)
                {
                    newVal += MathHelper.TwoPi;
                }
                while (newVal >= MathHelper.TwoPi)
                {
                    newVal -= MathHelper.TwoPi;
                }
                if (_rotation != newVal)
                {
                    _rotation = newVal;
                    RotationMatrix = Matrix.CreateRotationX(MathHelper.PiOver2) * Matrix.CreateRotationZ(_rotation);
                }
            }
        }

    }
}
