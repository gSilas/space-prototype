using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace space_prototype.Entities
{
    public class Camera
    {
        public float AspectRatio;
        public float FarClipPlane;
        public float FieldOfView;
        public float NearClipPlane;

        public Vector3 Position;
        public Vector3 Target;
        public Vector3 UpVector;

        public Matrix ViewMatrix
        {
            get { return Matrix.CreateLookAt(Position, Target, UpVector); }
        }

        public Matrix ProjectionMatrix
        {
            get { return Matrix.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearClipPlane, FarClipPlane); }
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.J))
                Position = Position + new Vector3(1, 0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.L))
                Position = Position - new Vector3(1, 0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.I))
                Position = Position + new Vector3(0, 0, 1);
            if (Keyboard.GetState().IsKeyDown(Keys.K))
                Position = Position - new Vector3(0, 0, 1);
            if (Keyboard.GetState().IsKeyDown(Keys.U))
                Position = Position + new Vector3(0, 1, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.O))
                Position = Position - new Vector3(0, 1, 0);
        }
    }
}