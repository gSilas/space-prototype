using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace space_prototype.Entities
{
    public class Camera
    {
        private readonly GraphicsDevice _graphicsDevice;
        public float Angle;
        public float FarClipPlane;
        public float FieldOfView;
        public float NearClipPlane;

        public Vector3 Position;
        public Vector3 Target;
        public Vector3 UpVector;

        public Camera(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public Matrix ViewMatrix
        {
            get
            {
                return Matrix.CreateLookAt(Position, Target, UpVector);
            }
        }

        public Matrix ProjectionMatrix
        {
            get
            {
                var aspectRatio = _graphicsDevice.DisplayMode.AspectRatio;
                return Matrix.CreatePerspectiveFieldOfView(FieldOfView, aspectRatio, NearClipPlane, FarClipPlane);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                Position = Position + new Vector3(0, 100, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                Position = Position - new Vector3(0, 100, 0);
        }
    }
}