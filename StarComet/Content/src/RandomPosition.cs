using Microsoft.Xna.Framework;
using System;


namespace StarComet.Content.src
{
    class RandomPosition
    {
        private Random rnd;
        public RandomPosition() { rnd = new Random();}

        public float GetRandomNumForPosition(int num1, int num2)
        {
            int i1 = rnd.Next(-num2, -num1);
            int i2 = rnd.Next(num1, num2);
            if ((i1 + i2) > 0)
            {
                return i1;
            }
            return i2;
        }

        public float GetPositiveNum(int Min, int Max) => rnd.Next(Min, Max);
        public Vector2 GetPositionAroundObject(GameItem Object, float Radius, int i) 
        {
            float theta = 2 * (float)Math.PI * i;
            float x = (float)Math.Sin(theta) * Radius;
            float y = (float)Math.Cos(theta) * Radius;

            return new Vector2(Object.Position.X + x, Object.Position.Y + y);
        }
    }
}
