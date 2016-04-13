using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace space_prototype.Entities
{
    public abstract class Entity
    {
        private Model _model;
        private float _rotation;
        public Vector3 Position = Vector3.Zero;

        public Matrix RotationMatrix = Matrix.Identity;

        public float Rotation
        {
            get { return _rotation; }
            set
            {
                var newVal = value;

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
                }
            }
        }

        public void Initialize(ContentManager contentManager, string name)
        {
            _model = contentManager.Load<Model>(name);
        }

        public abstract void Update(GameTime gameTime);

        public void Draw(Camera camera)
        {
            foreach (var mesh in _model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    //TODO hacked upvector
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    effect.World = RotationMatrix*Matrix.CreateWorld(Position, Vector3.UnitX, Vector3.UnitY);
                    effect.View = camera.ViewMatrix;
                    effect.Projection = camera.ProjectionMatrix;
                }

                mesh.Draw();
            }
        }

        public void RotateY()
        {
            RotationMatrix = Matrix.CreateFromAxisAngle(Vector3.UnitY, _rotation);
            
        }
    }
}