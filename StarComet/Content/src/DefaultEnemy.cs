using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StarComet.Content.src
{
    internal class DefaultEnemy : GameItem, Interfaces.IUpdatable, Interfaces.IShootable
    {
        public Player FollowTarget;
        public float FollowDistance;
        private float _timer;
        public Bullet BulletType;
        public Queue<Bullet> Bullets;
       

        public DefaultEnemy(Texture2D Sprite, Vector2 Position, Vector2 Velocity, float Rotation, float Scale)
         : base(Sprite, Rotation, Scale, Position, Velocity) { Bullets = new Queue<Bullet>(); }
   
        public virtual void Follow()
        {
            if (FollowTarget == null)
                return;

            var distance = FollowTarget.Position - this.Position;
            Rotation = (float)Math.Atan2(distance.Y, distance.X);

            Direction = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation));

            var currentDistance = Vector2.Distance(this.Position, FollowTarget.Position);

            if (currentDistance > FollowDistance)
            {
                var t = MathHelper.Min((float)Math.Abs(currentDistance - FollowDistance), 2f);
                var velocity = Direction * t;

                Position += velocity;
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer > 1)
            {
                Shoot();
                _timer = 0;
            }
            
        }

        protected virtual void Shoot()
        {
            Bullet bullet = BulletType.Clone() as Bullet;
            bullet.Direction = this.Direction;
            bullet.Position = this.Position;
            bullet.Speed = 3f;
            bullet.StartSpeed = new Vector2(this.VelocityX, this.VelocityY);
            bullet.LifeSpan = 10f;
            bullet.Parent = this;


            Bullets.Enqueue(bullet);
        }
     
    }
}