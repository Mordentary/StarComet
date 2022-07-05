using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;


namespace StarComet.Content.src
{
    class Shield :  GameItem
    {
        public int Health;
        public Shield(Texture2D Sprite, float Rotation, float Scale, Vector2 Position, Vector2 Velocity) : base(Sprite, Rotation,  Scale,  Position,  Velocity) { Speed = 5f; Health = 5; }

        public override void OnCollide(GameItem sprite)
        {
         
            if (sprite is DefaultEnemy)
            {
                (sprite as DefaultEnemy).IsRemoved = true;
                this.IsRemoved = true;
            }
            if(sprite is Bullet)
            {
                (sprite as Bullet).IsRemoved = true;
                Health--;
                if (Health <= 0)
                {
                    this.IsRemoved = true;
                }
            }
        }



    }
}
