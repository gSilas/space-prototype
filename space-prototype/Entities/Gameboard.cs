using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using space_prototype.Tools;

namespace space_prototype.Entities
{
    public class Gameboard
    {
        private VertexPositionNormalTexture[] _floorVerts;
        private BasicEffect _effect;
        private Texture2D _texture;
        private GraphicsDeviceManager _graphics;

        public void LoadTexture(Texture2D tex)
        {
            _texture = tex;
        }

        //TODO add 3D plane for orientation
        public void Initialize(GraphicsDeviceManager gra)
        {
            _graphics = gra;
            _floorVerts = new VertexPositionNormalTexture[6];

            _floorVerts[0].Position = new Vector3(-20, -20, 0);
            _floorVerts[1].Position = new Vector3(-20, 20, 0);
            _floorVerts[2].Position = new Vector3(20, -20, 0);

            _floorVerts[3].Position = _floorVerts[1].Position;
            _floorVerts[4].Position = new Vector3(20, 20, 0);
            _floorVerts[5].Position = _floorVerts[2].Position;

            int repetitions = 20;

            _floorVerts[0].TextureCoordinate = new Vector2(0, 0);
            _floorVerts[1].TextureCoordinate = new Vector2(0, repetitions);
            _floorVerts[2].TextureCoordinate = new Vector2(repetitions, 0);

            _floorVerts[3].TextureCoordinate = _floorVerts[1].TextureCoordinate;
            _floorVerts[4].TextureCoordinate = new Vector2(repetitions, repetitions);
            _floorVerts[5].TextureCoordinate = _floorVerts[2].TextureCoordinate;


            _effect = new BasicEffect(_graphics.GraphicsDevice);
        }

        public void Draw(Camera cam)
        {
            _effect.View = Matrix.CreateLookAt(cam.Position, cam.Target, cam.UpVector);

            float aspectRatio = _graphics.GraphicsDevice.DisplayMode.AspectRatio;

            _effect.Projection = Matrix.CreatePerspectiveFieldOfView(cam.FieldOfView, aspectRatio, cam.NearClipPlane, cam.FarClipPlane);
            _effect.TextureEnabled = true;
            _effect.Texture = _texture;

            foreach (var pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList,_floorVerts,0,2);
            }
        }
    }
}
