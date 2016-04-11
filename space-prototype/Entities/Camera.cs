using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace space_prototype.Tools
{
    public class Camera
    {
        GraphicsDevice graphicsDevice;

        public Vector3 Position;
        public Vector3 Target;
        public float Angle;

        public Camera(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public Matrix ViewMatrix
        {
            get
            {
                Target = Vector3.Transform(Target, Matrix.CreateRotationZ(Angle));
                Target += Position;
                return Matrix.CreateLookAt(Position, Target, Vector3.Up);
            }
        }

        public Matrix ProjectionMatrix
        {
            get
            {
                float fieldOfView = MathHelper.PiOver4;
                float nearClipPlane = 1;
                float farClipPlane = 200;
                float aspectRatio = graphicsDevice.DisplayMode.AspectRatio;

                return Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
            }
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
