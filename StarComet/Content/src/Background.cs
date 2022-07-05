using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace StarComet.Content.src
{
    internal class Background : Interfaces.IDrawable
    {
        private Vector2 _currentPlate;
        private List<Tile> _background = new List<Tile>();
        private Texture2D _backgr;
        private int halfB;

        public Background(ContentManager Content, Vector2 OriginPosition)
        {
            this._backgr = Content.Load<Texture2D>("Sprites/Space");
            this._currentPlate = OriginPosition;
            this.halfB = _backgr.Width / 2;
        }

        public float Coordinate(int x) => x * (int)halfB;

        public void Fill(Vector2 PlayerPosition)
        {
            int x = (int)PlayerPosition.X / halfB;

            int y = (int)PlayerPosition.Y / halfB;

            if (new Vector2(x, y) != _currentPlate)
            {
                if (x % 2 != 0)
                {
                    if (x > 0)
                    {
                        x++;
                    }
                    else
                        x--;
                }
                if (y % 2 != 0)
                {
                    if (y > 0)
                    {
                        y++;
                    }
                    else
                        y--;
                }

                _background.Clear();
            }

            if (_background.Count == 0)
            {
                _currentPlate = new Vector2(x, y);

                _background.Add(new Tile(_backgr, Coordinate(x) - halfB, Coordinate(y) - halfB, new Vector2(x, y)));
                _background.Add(new Tile(_backgr, Coordinate(x + 2) - halfB, Coordinate(y) - halfB, new Vector2(x, y)));
                _background.Add(new Tile(_backgr, Coordinate(x) - halfB, Coordinate(y + 2) - halfB, new Vector2(x, y)));
                _background.Add(new Tile(_backgr, Coordinate(x - 2) - halfB, Coordinate(y) - halfB, new Vector2(x, y)));
                _background.Add(new Tile(_backgr, Coordinate(x) - halfB, Coordinate(y - 2) - halfB, new Vector2(x, y)));

                _background.Add(new Tile(_backgr, Coordinate(x + 4) - halfB, Coordinate(y) - halfB, new Vector2(x, y)));
                _background.Add(new Tile(_backgr, Coordinate(x - 4) - halfB, Coordinate(y) - halfB, new Vector2(x, y)));

                _background.Add(new Tile(_backgr, Coordinate(x - 2) - halfB, Coordinate(y - 2) - halfB, new Vector2(x, y)));
                _background.Add(new Tile(_backgr, Coordinate(x - 2) - halfB, Coordinate(y + 2) - halfB, new Vector2(x, y)));
                _background.Add(new Tile(_backgr, Coordinate(x + 2) - halfB, Coordinate(y + 2) - halfB, new Vector2(x, y)));
                _background.Add(new Tile(_backgr, Coordinate(x + 2) - halfB, Coordinate(y - 2) - halfB, new Vector2(x, y)));

                _background.Add(new Tile(_backgr, Coordinate(x - 4) - halfB, Coordinate(y - 2) - halfB, new Vector2(x, y)));
                _background.Add(new Tile(_backgr, Coordinate(x - 4) - halfB, Coordinate(y + 2) - halfB, new Vector2(x, y)));
                _background.Add(new Tile(_backgr, Coordinate(x + 4) - halfB, Coordinate(y + 2) - halfB, new Vector2(x, y)));
                _background.Add(new Tile(_backgr, Coordinate(x + 4) - halfB, Coordinate(y - 2) - halfB, new Vector2(x, y)));
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Tile sp in this._background)
            {
                spriteBatch.Draw(
                    sp._sprite,
                    new Rectangle((int)sp.x, (int)sp.y, 512, 512),
                    null,
                    Color.White);
            }
        }

    }
}