using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace space_prototype.Tools
{
    public class Camera
    {
        private readonly GraphicsDevice _graphicsDevice;

        public Vector3 Position;
        public Vector3 Target;
        public Vector3 UpVector;
        public float FarClipPlane;
        public float NearClipPlane;
        public float FieldOfView;
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
                return Matrix.CreateLookAt(Position, Target, UpVector);
            }
        }

        public Matrix ProjectionMatrix
        {
            get
            {
                float aspectRatio = _graphicsDevice.DisplayMode.AspectRatio;

                return Matrix.CreatePerspectiveFieldOfView(FieldOfView, aspectRatio, NearClipPlane, FarClipPlane);
            }
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
