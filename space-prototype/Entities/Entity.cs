using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using space_prototype.Tools;

namespace space_prototype.Entities
{
    public abstract class Entity
    {
        private Model _model;

        public Vector3 Position = Vector3.Zero;

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
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    effect.World = Matrix.CreateWorld(Position, Vector3.UnitX, Vector3.UnitZ);
                    effect.View = camera.ViewMatrix;
                    effect.Projection = camera.ProjectionMatrix;
                }

                mesh.Draw();
            }
        }
    }
}
