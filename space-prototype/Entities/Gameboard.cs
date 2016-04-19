using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace space_prototype.Entities
{
    public class Gameboard : Entity
    {

        public override void Draw(Camera camera)
        {
            foreach (var mesh in Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.Alpha = 1f;
                    effect.World = Matrix.CreateWorld(Position, Vector3.UnitX, Vector3.UnitY);
                    effect.View = camera.ViewMatrix;
                    effect.Projection = camera.ProjectionMatrix;
                }

                mesh.Draw();
            }
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}