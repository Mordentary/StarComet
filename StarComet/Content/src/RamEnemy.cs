using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarComet.Content.src
{
    internal class RamEnemy : DefaultEnemy
    {
        public RamEnemy(Texture2D Sprite, Vector2 Position, Vector2 Velocity, float Rotation, float Scale) : base(Sprite, Position, Velocity, Rotation, Scale)
        {

        }
        public override void Follow()
        {
            if (FollowTarget == null)
                return;

            var distance = FollowTarget.Position - this.Position;
            Rotation = (float)Math.Atan2(distance.Y, distance.X);

            Direction = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation));

            var currentDistance = Vector2.Distance(this.Position, FollowTarget.Position);

            if (currentDistance > FollowDistance)
            {
                var t = MathHelper.Min((float)Math.Abs(currentDistance - FollowDistance), Speed);
                var velocity = Direction * t;

                Position += velocity;
            }
        }
        public override void OnCollide(GameItem sprite)
        {
            if (sprite.GetType().Name == "DefaultEnemy")
            {
                sprite.IsRemoved = true;
            }
        }
    }
}
