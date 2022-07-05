using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarComet.Content.src
{
    class RingEnemy : DefaultEnemy
    {
        private float _timer;
        private float theta;
        private int numberBullets = 50;
        private float rotateCircle;
        public int Health;

        public RingEnemy(Texture2D Sprite, Vector2 Position, Vector2 Velocity, float Rotation, float Scale) : base(Sprite, Position, Velocity, Rotation, Scale)
        {
            Health = 3;
        }
        public override void Update(GameTime gameTime)
        {
            if (Health <= 0)
            {
                this.IsRemoved = true;
            }
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer > 6)
            {
                Shoot();
                _timer = 0;
            }

        }

        private Vector2 Rotate(float angle, float distance, Vector2 centre, float theta)
        {
            return new Vector2((float)(distance * Math.Cos(angle + theta)), (float)(distance * Math.Sin(angle + theta))) + centre;
        }
        protected override void Shoot()
        {
            rotateCircle += 90f;
            for (int i = 0; i < numberBullets; i++)
            {
                theta = (float)(i * 2 * Math.PI / numberBullets);
                Bullet bullet = BulletType.Clone() as Bullet;
                bullet.Position  = Rotate(Rotation + rotateCircle, 30, this.Position, theta);
                bullet.Direction = new Vector2((float)Math.Cos(Rotation + rotateCircle + theta), (float)Math.Sin(Rotation + rotateCircle + theta));
                bullet.Speed = 3f;
                bullet.StartSpeed = new Vector2(this.VelocityX, this.VelocityY);
                bullet.LifeSpan = 6f;
                bullet.Parent = this;
                Bullets.Enqueue(bullet);
            }


        }

    }
}
