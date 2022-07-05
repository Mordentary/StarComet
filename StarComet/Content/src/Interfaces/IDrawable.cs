using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarComet.Content.src.Interfaces
{
    interface IDrawable
    {
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
