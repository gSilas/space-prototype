using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using space_prototype.Entities;

namespace space_prototype.GameStates
{
    public class MainGame : GameState
    {
        //3D Models
        private AsteroidField asteroidField;

        //Visual components
        private SpriteFont BebasNeue;

        //Camera/View information
        private Camera camera;

        //Audio components
        private Song mainSong;
        private Gameboard plane;
        private Ship ship;

        public override void Initialize()
        {
            //Camera
            camera = new Camera();
            camera.Position = new Vector3(0, 100, 0);
            camera.Target = Vector3.Zero;
            camera.UpVector = Vector3.UnitZ;
            camera.FieldOfView = MathHelper.PiOver2;
            camera.NearClipPlane = 0.1f;
            camera.FarClipPlane = 10000f;

            //Plane
            plane = new Gameboard();

            //Entites
            asteroidField = new AsteroidField(10);
            ship = new Ship();
            ship.Position = new Vector3(52, 20, 0);
        }

        public override void LoadContent(ContentManager Content)
        {
            //Audio
            mainSong = Content.Load<Song>("Audio/n-Dimensions");

            //Fonts
            BebasNeue = Content.Load<SpriteFont>("Fonts/bebasneue");

            //Models
            asteroidField.Initialize(Content);
            plane.Initialize(Content, "models/bgplane");
            ship.Initialize(Content, "models/spaceship");

            //Start Audio
            MediaPlayer.Play(mainSong);
            MediaPlayer.Volume = 0.1f;
        }

        public override void Update(GameTime gameTime)
        {
            camera.Update(gameTime);
            asteroidField.Update(gameTime);
            ship.Update(gameTime);
        }

        public override void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            //3D stuff
            plane.Draw(camera);
            ship.Draw(camera);
            asteroidField.Draw(camera);

            //2D SpriteBatch stuff
            spriteBatch.Begin();
            spriteBatch.DrawString(BebasNeue, "Move around with W (Up) and S (Down) and JKLIUO for Camera!",
                new Vector2(50, 0), Color.LightGoldenrodYellow);
            spriteBatch.End();
        }
    }
}