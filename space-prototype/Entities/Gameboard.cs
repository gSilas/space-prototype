using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace space_prototype.Entities
{
    public class Gameboard
    {
        private readonly Vector3 _position = new Vector3(0, 0, 0);
        private Model _model;

        public void Initialize(ContentManager contentManager, string name)
        {
            _model = contentManager.Load<Model>(name);
        }

        public void Draw(Camera camera)
        {
            foreach (var mesh in _model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.Alpha = 1f;
                    effect.World = Matrix.CreateWorld(_position, Vector3.UnitX, Vector3.UnitY);
                    effect.View = camera.ViewMatrix;
                    effect.Projection = camera.ProjectionMatrix;
                }

                mesh.Draw();
            }
        }
    }
}