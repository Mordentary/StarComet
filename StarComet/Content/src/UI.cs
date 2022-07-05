using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace StarComet.Content.src
{
    internal class UI
    {
        public FrameCounter Fps { get; private set; }
        private SpriteFont _font;
        public UI(ContentManager Content)
        {
            _font = Content.Load<SpriteFont>("src/Fonts/Font");
            Fps = new FrameCounter();
        }

        public void DrawUI(SpriteBatch spriteBatch, Player P, Camera C)
        {
            if (P.Limit)
            {
                spriteBatch.DrawString(_font, "Now, return from whence thou cam'st", new Vector2(-160, -100) + C.Position, Color.Yellow);
            }
            if (P.Health <= 0)
            {
                spriteBatch.DrawString(_font, "YOU ARE DEAD", new Vector2(-100, -100) + C.Position, Color.Red);
                P.IsDead = true;
  
            }
            spriteBatch.DrawString(_font, "Score:" + P.Score.ToString(), new Vector2(-600, -200) + C.Position, Color.Yellow);

            spriteBatch.DrawString(_font, "Speed:" + Math.Round(P._acceleration, 1).ToString(), new Vector2(290, -200) + C.Position, Color.Yellow);

            spriteBatch.DrawString(_font, "Bullet:" + P.CountOfBullet.ToString(), new Vector2(500, -200) + C.Position, Color.Yellow);

            spriteBatch.DrawString(_font, "Health:" + P.Health.ToString(), new Vector2(400, -200) + C.Position, Color.Yellow);

           // Fps.DrawFps(spriteBatch, _font, new Vector2(300, -300) + C.Position, Color.MonoGameOrange);
        }
    }
}