using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace space_prototype.Tools
{
    public class Camera
    {
        private readonly GraphicsDevice _graphicsDevice;

        public Vector3 Position;
        public Vector3 Target;
        public float Angle;

        public Camera(GraphicsDevice graphicsDevice)
        {
            this._graphicsDevice = graphicsDevice;
        }

        public Matrix ViewMatrix
        {
            get
            {
                //TODO Add rotation
                //Target = Vector3.Transform(Target, Matrix.CreateRotationZ(Angle));
                //Target += Position;
                return Matrix.CreateLookAt(Position, Target, Vector3.UnitZ);
            }
        }

        public Matrix ProjectionMatrix
        {
            get
            {
                float fieldOfView = MathHelper.PiOver4;
                float nearClipPlane = 1f;
                float farClipPlane = 10000;
                float aspectRatio = _graphicsDevice.DisplayMode.AspectRatio;

                return Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
            }
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
