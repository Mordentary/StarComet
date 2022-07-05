using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarComet.Content.src
{
    public class Bullet : GameItem
    {
        public float LifeSpan { get; set; }
        private float _timer;
        public Vector2 StartSpeed;

        public Bullet(Texture2D Sprite) : base(Sprite)
        {
            this.Scale = 1; this.Speed = 3f;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan)
            {
                IsRemoved = true;
                _timer = 0;
            }
            Position += (Direction * Speed) + StartSpeed;
        }

        public override void OnCollide(GameItem sprite)
        {
            if (sprite == this.Parent)
                return;

            if (sprite is Bullet)
                return;

            if (sprite is Player)
            {
                (sprite as Player).Health--;
                this.IsRemoved = true;
            }
            if (sprite.GetType().Name == "RamEnemy")
            {
                this.IsRemoved = true;
                 
            }
            if (sprite.GetType().Name == "DefaultEnemy")
            {
                sprite.IsRemoved = true;
                this.IsRemoved = true;
            }
            if (sprite.GetType().Name == "RingEnemy")
            {
                (sprite as RingEnemy).Health--;
                this.IsRemoved = true;
            }
        }
    }
}