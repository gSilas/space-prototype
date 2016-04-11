using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace space_prototype.Entities
{
    abstract class Entity
    {
        private Model _model;

        public Matrix[] Transforms;

        public Model Model
        {
            get { return _model; }
            set
            {
                _model = value;
                Matrix[] absoluteTransforms = new Matrix[_model.Bones.Count];
                _model.CopyAbsoluteBoneTransformsTo(absoluteTransforms);

                foreach (ModelMesh mesh in _model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();
                        effect.Projection = Game1.ProjectionMatrix;
                        effect.View = Game1.ViewMatrix;
                    }
                }

                Transforms = absoluteTransforms;
            }
        }

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

        public void DrawEntity()
        {
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = Transforms[mesh.ParentBone.Index] * RotationMatrix * Matrix.CreateTranslation(Position);
                }
                mesh.Draw();
            }
        }

        public abstract void Move();
    }
}
