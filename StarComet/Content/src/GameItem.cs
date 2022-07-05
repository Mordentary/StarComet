using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace StarComet.Content.src
{
    public class GameItem : Interfaces.IDrawable, Interfaces.ICollidable, ICloneable
    {
        public Texture2D _sprite;
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; protected set; }
        public Vector2 Direction { get; set; }

        public Vector2 Origin;

        public GameItem Parent;

        internal bool IsRemoved;
        public float _acceleration { get;  set; }
        protected float VelocityX { get; set; }
        protected float VelocityY { get; set; }
        public float Rotation { get; set; }
        protected float Scale { get; set; }
        public float Speed { get;  set; }

        public readonly Color[] TextureData;
        public List<GameItem> Children { get; protected set; }

        public GameItem(Texture2D Sprite, float Rotation, float Scale, Vector2 Position, Vector2 Velocity) 
        {
          
            this._sprite = Sprite;
            this.Rotation = Rotation;
            this.Scale = Scale;
            this.Position = Position;
            this.Velocity = Velocity;
            Origin = new Vector2(_sprite.Width / 2, _sprite.Height / 2);
            TextureData = new Color[_sprite.Width * _sprite.Height];
            _sprite.GetData(TextureData);
        }

        public GameItem(Texture2D Sprite)
        {
            this._sprite = Sprite;
            Origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
            TextureData = new Color[_sprite.Width * _sprite.Height];
            _sprite.GetData(TextureData);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this._sprite, 
                this.Position, 
                null, 
                Color.White, 
                this.Rotation, 
                new Vector2(this._sprite.Width / 2, this._sprite.Height / 2), 
                this.Scale, 
                SpriteEffects.None, 
                0);
            if (!(this.Children is null) && this.Children.Count > 0)
            {
                DrawChildren(gameTime, spriteBatch);
            }
          
        }

        public void DrawChildren(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            foreach (Bullet Bullet in this.Children)
            {
                Bullet.Draw(gameTime, spriteBatch);
            }
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public virtual void OnCollide(GameItem sprite)
        {
        }

        #region Collision

        public Matrix Transform
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-Origin, 0)) *
                  Matrix.CreateRotationZ(this.Rotation) *
                  Matrix.CreateTranslation(new Vector3(Position, 0));
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, _sprite.Width, _sprite.Height);
            }
        }

        public bool Intersects(GameItem sprite)
        {
            // Вычисляем матрицу, которая преобразуется из локального пространства A в мировое пространство, а затем в локальное пространство B
            var transformAToB = this.Transform * Matrix.Invert(sprite.Transform);

            // Когда точка перемещается в локальном пространстве A, она перемещается в локальном пространстве B с
            // фиксированное направление и расстояние пропорционально движению в A.
            // Этот алгоритм проходит через A по одному пикселю за раз по осям X и Y A
            // Рассчитываем аналогичные шаги в B:
            var stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
            var stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

            // Вычислить верхний левый угол A в локальном пространстве B.
            // Эта переменная будет повторно использоваться для отслеживания начала каждой строки
            var yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

            for (int yA = 0; yA < this.Rectangle.Height; yA++)
            {

                // Начинаем с начала строки
                var posInB = yPosInB;

                for (int xA = 0; xA < this.Rectangle.Width; xA++)
                {

                    // Округляем до ближайшего пикселя
                    var xB = (int)Math.Round(posInB.X);
                    var yB = (int)Math.Round(posInB.Y);

                    if (0 <= xB && xB < sprite.Rectangle.Width &&
                        0 <= yB && yB < sprite.Rectangle.Height)
                    {

                        // Получаем цвета перекрывающихся пикселей
                        var colourA = this.TextureData[xA + yA * this.Rectangle.Width];
                        var colourB = sprite.TextureData[xB + yB * sprite.Rectangle.Width];


                        // Если оба пикселя не полностью прозрачны
                        if (colourA.A != 0 && colourB.A != 0)
                        {
                            return true;
                        }
                    }


                    // Переход к следующему пикселю в строке
                    posInB += stepX;
                }


                // Переход к следующей строке
                yPosInB += stepY;
            }

            // Пересечений не найдено
            return false;
        }

        #endregion Collision
    }
}