using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace space_prototype.Entities
{
    public abstract class Entity
    {
        private float _rotation;
        public Vector3 Position = Vector3.Zero;

        public Matrix RotationMatrix = Matrix.Identity;

        public float Rotation
        {
            get { return _rotation; }
            set
            {
                while (value < 0)
                {
                    value += MathHelper.TwoPi;
                }
                while (value >= MathHelper.TwoPi)
                {
                    value -= MathHelper.TwoPi;
                }
                _rotation = value;
            }
        }

        public Model Model { get; set; }

        public void LoadContent(ContentManager contentManager, string name)
        {
            Model = contentManager.Load<Model>(name);
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(Camera camera)
        {
            foreach (var mesh in Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
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