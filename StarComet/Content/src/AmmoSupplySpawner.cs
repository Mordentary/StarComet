using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StarComet.Content.src
{
    internal class AmmoSupplySpawner : Interfaces.IDrawable
    {
        public readonly List<AmmoSupply> AllAmmo;
        private float _timer;
        private readonly AmmoSupply _ammoSupplyPrefab;
        private readonly RandomPosition rndP;

        public AmmoSupplySpawner(ContentManager Content)
        {
            rndP = new RandomPosition();
            this._ammoSupplyPrefab = new AmmoSupply(Content.Load<Texture2D>("Sprites/Ammo"));
            AllAmmo = new List<AmmoSupply>();
        }

        public void SpawnAmmo(Player P, GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > 10)
            {
                AmmoSupply AS = new AmmoSupply(_ammoSupplyPrefab._sprite);
                AS.Position =  P.Position + new Vector2(rndP.GetRandomNumForPosition(200, 300), rndP.GetRandomNumForPosition(200, 300));
                AllAmmo.Add(AS);
                _timer = 0;
            }
            DeleteAmmo();
        }
        private void DeleteAmmo()
        {
            for (int i = 0; i < AllAmmo.Count; i++)
            {
                if (AllAmmo[i].IsRemoved)
                {
                    AllAmmo.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var Supply in AllAmmo)
            {
                Supply.Draw(gameTime,spriteBatch);
            }
        }
    }
}