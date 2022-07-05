using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace StarComet.Content.src
{
    internal class Player : GameItem, Interfaces.IUpdatable
    {
        public ShieldBelt _shieldBelt;
        protected KeyboardState _currentKey;
        protected KeyboardState _previousKey;
        private float _timer;

        private Limit _limit;
        public Bullet BulletType { get; set; }
        public int CountOfBullet { get; set; }
        public int Health { get; set; }
        public int MaxSpeed { get; }
        public int Score { get; set; }

        public bool Limit = false;

        public bool IsDead = false;

  

        public Player(Texture2D Sprite, Vector2 Position, Vector2 Velocity, float Rotation, float Scale, int MaxSpeed, int Health, ContentManager Content)
          : base(Sprite, Rotation, Scale, Position, Velocity)
        {
            _shieldBelt = new ShieldBelt(Content);
            this.MaxSpeed = MaxSpeed;
            this.Health = Health;
            Children = new List<GameItem>();
            BulletType = new Bullet(Content.Load<Texture2D>("Sprites/Bullet"));
            _limit = new Limit();
        }

        public override void OnCollide(GameItem sprite)
        {
            if (sprite == this.Parent)
                return;

            if (sprite is Bullet)
                return;
            if (sprite.GetType().Name == "RamEnemy")
            {
                sprite.IsRemoved = true;
                Health -= 2;
            }
            if (sprite.GetType().Name == "DefaultEnemy")
            {
                sprite.IsRemoved = true;
                Health--;
            }
            if (sprite.GetType().Name == "RingEnemy")
            {
                sprite.IsRemoved = true;
                Health--;
            }
            if (sprite is AmmoSupply)
            {
                sprite.IsRemoved = true;
                CountOfBullet += 15;
            }
        }

        public void Update(GameTime gameTime)
        {
            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            if (Health < 0)
            {

                Health = 0;
            }

            if (_acceleration > MaxSpeed)
            {
                _acceleration = MaxSpeed;
            }

            if (!Limit)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    VelocityX = VelocityY = 0;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W) && _acceleration < MaxSpeed)
                {
                    _acceleration += 0.02f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S) && _acceleration > 0)
                {
                    _acceleration -= 0.01f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (_acceleration < 0)
                    {
                        _acceleration = 0;
                    }
                }
            }
            else
            {
                _acceleration -= 0.1f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (_acceleration < 5)
                {
                    _acceleration = 5;
                }
            }

            if (!IsDead)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    this.Rotation += 0.08f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    this.Rotation -= 0.08f;
                }

                Direction = new Vector2((float)Math.Sin(this.Rotation), (float)-Math.Cos(this.Rotation));

                this.VelocityX = Direction.X * (float)gameTime.ElapsedGameTime.TotalMilliseconds * _acceleration / 30;
                this.VelocityY = Direction.Y * (float)gameTime.ElapsedGameTime.TotalMilliseconds * _acceleration / 30;

                this.Position = new Vector2(this.Position.X + this.VelocityX, this.Position.Y + this.VelocityY);
            }

            if (_currentKey.IsKeyDown(Keys.V) && _previousKey.IsKeyUp(Keys.V) && CountOfBullet > 0)
            {
                Shoot();
            }
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_currentKey.IsKeyDown(Keys.B) && _previousKey.IsKeyUp(Keys.B) && _timer > 3)
            {
                _shieldBelt.InitialiseShield();
                _timer = 0;
            }
            _shieldBelt.Update(this);
            UpdateBullets(gameTime);
            _limit.CheckPlayerLimit(this, 100000);
            
        }

        private void UpdateBullets(GameTime gameTime)
        {
            foreach (Bullet B in this.Children)
            {
                B.Update(gameTime);
            }
            DeleteBullets();
        }

        private void DeleteBullets()
        {
            for (int i = 0; i < this.Children.Count; i++)
            {
                if ((this.Children[i] as Bullet).IsRemoved)
                {
                    this.Children.RemoveAt(i);
                    i--;
                }
            }
        }

        private void Shoot()
        {
            Bullet bullet = BulletType.Clone() as Bullet;
            bullet.Direction = this.Direction;
            bullet.Position = this.Position;
            bullet.Speed = 6f;
            bullet.StartSpeed = new Vector2(this.VelocityX, this.VelocityY);
            bullet.LifeSpan = 3f;
            bullet.Parent = this;
            this.CountOfBullet--;

            Children.Add(bullet);
        }
    }
}