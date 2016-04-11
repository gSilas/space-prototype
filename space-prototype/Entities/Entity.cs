using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using space_prototype.Tools;

namespace space_prototype.Entities
{
    public abstract class Entity
    {
        private Model _model;
        private float _rotation;

        public Vector3 Position = Vector3.Zero;
        public Matrix RotationMatrix = Matrix.Identity;

        public void Initialize(ContentManager contentManager, string name)
        {
            _model = contentManager.Load<Model>(name);

        }

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

        public void Draw(Camera camera)
        {
            foreach (var mesh in _model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    effect.World = effect.World;
                    effect.View = camera.ViewMatrix;
                    effect.Projection = camera.ProjectionMatrix;
                }

                mesh.Draw();
            }
        }

        public abstract void Move();
    }
}
