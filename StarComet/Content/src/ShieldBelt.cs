using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StarComet.Content.src
{
    internal class ShieldBelt : Interfaces.IDrawable
    {
        public Shield[] _shieldArr;
        private List<int> _shieldsToRecovery;
        private Shield _shieldPrefab;
        private int MaxShields = 5;
        private float theta;
        private float Rotation;

        public ShieldBelt(ContentManager Content)
        {
            _shieldPrefab = new Shield(Content.Load<Texture2D>("Sprites/Shield"), 0f, 1f, new Vector2(0, 0), new Vector2(0, 0));
            _shieldArr = new Shield[MaxShields];
            _shieldsToRecovery = new List<int>();
        }

        public void OrbitRotation(Player P)
        {
            Rotation += 0.05f;
            for (int i = 0; i < _shieldArr.Length; i++)
            {
                theta = (float)(i * 2 * Math.PI / MaxShields);
                if (!(_shieldArr[i] is null))
                {
                    _shieldArr[i].Position = Rotate(Rotation, 20, P.Position, theta);
                }
            }
        }

        private Vector2 Rotate(float angle, float distance, Vector2 centre, float theta)
        {
            return new Vector2((float)(distance * Math.Cos(angle + theta)), (float)(distance * Math.Sin(angle + theta))) + centre;
        }

        public void InitialiseShield()
        {
            RefreshMemory();
            if (_shieldsToRecovery.Count != 0)
            {
                Shield A = (Shield)_shieldPrefab.Clone();
                _shieldArr[_shieldsToRecovery.Min()] = A;
            }
        }

        public void Update(Player P)
        {
            OrbitRotation(P);
            DeleteShields();
        }

        public void RefreshMemory()
        {
            _shieldsToRecovery.Clear();
            for (int i = 0; i < _shieldArr.Length; i++)
            {
                if (_shieldArr[i] is null)
                {
                    _shieldsToRecovery.Add(i);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var Shield in _shieldArr)
            {
                if (!(Shield is null))
                {
                    Shield.Draw(gameTime, spriteBatch);
                }
            }
        }

        private void DeleteShields()
        {
            for (int i = 0; i < _shieldArr.Length; i++)
            {
                if (!(_shieldArr[i] is null))
                {
                    if (_shieldArr[i].IsRemoved)
                    {
                        _shieldArr[i] = null;
                    }
                }
            }
        }
    }
}