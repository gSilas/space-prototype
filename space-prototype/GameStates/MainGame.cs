using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using space_prototype.Entities;

namespace space_prototype.GameStates
{
    public class MainGame : GameState
    {
        private readonly AsteroidField _asteroids;
        private readonly List<Projectile> _bulletList;
        private readonly Camera _camera;
        private readonly SpriteFont _font;
        private readonly SoundEffect _hit;
        private readonly SoundEffect _laser;
        private readonly GameStateManager _manager;
        private readonly Gameboard _plane;

        private readonly Ship _ship;
        private int _ammo = 20;
        private int _health = 100;
        private TimeSpan _reloadTime;
        private int _score;
        private TimeSpan _shootTime;
        private bool _collide;

        public MainGame(GameStateManager manager, SpriteFont font, Camera camera, Ship ship, Gameboard plane,
            AsteroidField asteroids, SoundEffect laser, SoundEffect hit)
        {
            _bulletList = new List<Projectile>();
            _manager = manager;
            _font = font;
            _camera = camera;
            _ship = ship;
            _plane = plane;
            _asteroids = asteroids;
            _laser = laser;
            _hit = hit;
            _collide = true;
            _reloadTime = TimeSpan.FromSeconds(3);
            _shootTime = TimeSpan.FromMilliseconds(150);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _manager.NextGameState(GameStateManager.GameStates.MainMenu);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F1))
            {
                _collide = !_collide;
                _hit.Play(0.5f, 1, 1);
            }

            _ship.Update(gameTime);
            _camera.Update(gameTime);
            _plane.Update(gameTime);
            _asteroids.Update(gameTime);

            if (_collide)
            {
                Collide(gameTime);
            }
         

            if (_ammo > 0)
            {
                _shootTime = _shootTime.Subtract(gameTime.ElapsedGameTime);
                if (_shootTime.TotalSeconds <= 0)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        SpawnBullet(gameTime);
                        _ammo--;
                        _laser.Play(0.2f, 0f, 0);
                        _shootTime = TimeSpan.FromMilliseconds(150);
                    }
                }
            }

            if (_ammo <= 0)
            {
                _reloadTime = _reloadTime.Subtract(gameTime.ElapsedGameTime);
                if (_reloadTime.TotalSeconds <= 0)
                {
                    _reloadTime = TimeSpan.FromSeconds(3);
                    _ammo = 20;
                }
            }

            foreach (var bullet in _bulletList)
            {
                bullet.Update(gameTime);
            }

            if (_health <= 0)
                _manager.NextGameState(GameStateManager.GameStates.GameOver);
        }

        public override void Draw(GameTime gameTime)
        {
            GameStateManager.Graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            //3D stuff
            _plane.Draw(_camera);
            _ship.Draw(_camera);
            foreach (var bullet in _bulletList)
            {
                bullet.Draw(_camera);
            }
            _asteroids.Draw(_camera);

            //2D SpriteBatch stuff
            GameStateManager.SpriteBatch.DrawString(_font, "Bullets: " + _ammo, new Vector2(10, 100),
                Color.LightGoldenrodYellow);
            GameStateManager.SpriteBatch.DrawString(_font, "Health: " + _health, new Vector2(10, 0),
                Color.LightGoldenrodYellow);
            GameStateManager.SpriteBatch.DrawString(_font, "Score: " + _score, new Vector2(10, 50),
                Color.LightGoldenrodYellow);
            if (_ammo <= 0)
            {
                GameStateManager.SpriteBatch.DrawString(_font, "Reloadtime: " + (float) _reloadTime.TotalSeconds,
                    new Vector2(10, 450), Color.LightGoldenrodYellow);
            }
            if (_score >= 00)
            {
                    GameStateManager.SpriteBatch.DrawString(_font, "[E]xchange 100 Score for 50 Health?", new Vector2(200, 450), Color.LightGoldenrodYellow);
                    
                if (Keyboard.GetState().IsKeyDown(Keys.E))
                {
                    _score -= 100;
                    _health += 50;       
                }
            }

            if (!_collide)
            {

                GameStateManager.SpriteBatch.DrawString(_font,"No Collison",new Vector2(0, 200), Color.LightGoldenrodYellow);
            }

            GameStateManager.SpriteBatch.DrawString(_font,
                "Move around with W and S\nand fire your Laser with Space!\nDisable Collison with F1 !",
                new Vector2(520, 0), Color.LightGoldenrodYellow);
        }

        private void SpawnBullet(GameTime gTime)
        {
            var bullet = new Projectile(_ship.Position);
            bullet.Model = _manager.BulletModel;
            _bulletList.Add(bullet);
        }

        private void Collide(GameTime gameTime)
        {
            var aremoveList = new List<Asteroid>();
            var bremoveList = new List<Projectile>();
            foreach (var asteroid in _asteroids.AsteroidList)
            {
                foreach (var bullet in _bulletList)
                {
                    if (bullet.Position.X < -200)
                    {
                        bremoveList.Add(bullet);
                        continue;
                    }

                    if (asteroid.Model != null)
                    {
                        if (Collider3D.Intersection(bullet, asteroid))
                        {
                            aremoveList.Add(asteroid);
                            bremoveList.Add(bullet);
                            _score += 1;
                        }
                    }
                }
            }
            foreach (var asteroid in _asteroids.AsteroidList)
            {
                if (Collider3D.Intersection(_ship, asteroid))
                {
                    aremoveList.Add(asteroid);
                    _health -= 5;
                    _hit.Play(0.2f, 0f, 0);
                }
            }

            foreach (var ast in aremoveList)
            {
                _asteroids.AsteroidList.Remove(ast);
            }
            foreach (var bullet in bremoveList)
            {
                _bulletList.Remove(bullet);
            }
        }
    }
}