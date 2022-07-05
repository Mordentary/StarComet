using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarComet.Content.src
{
    internal class Tile
    {
        public Texture2D _sprite { get; private set; }

        public float x { get; set; }

        public float y { get; set; }

        public Vector2 Code { get; set; }

        public Tile(Texture2D Sprite, float x, float y, Vector2 Code)
        {
            this.Code = Code;
            this._sprite = Sprite;
            this.x = x;
            this.y = y;
        }
    }
}