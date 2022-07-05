using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace StarComet.Content.src
{
    internal class EnemyController : Interfaces.IDrawable, Interfaces.IUpdatable
    {
        public List<Bullet> AllBullets;
        public List<DefaultEnemy> Enemies;
        private Bullet _bulletPrefab;
        private readonly DefaultEnemy _defaultEnemyPrefab;
        private readonly RamEnemy _ramEnemyPrefab;
        private readonly RingEnemy _ringEnemyPrefab;
        private readonly RandomPosition rndP;
        private float _timer, _secondTimer, thirdTimer;

        public EnemyController(ContentManager Content)
        {
            _defaultEnemyPrefab = new DefaultEnemy(Content.Load<Texture2D>("Sprites/DefaultEnemy"), Vector2.Zero, new Vector2(0, 0), 0, 1);
            _bulletPrefab = new Bullet(Content.Load<Texture2D>("Sprites/Bullet"));
            _ramEnemyPrefab = new RamEnemy(Content.Load<Texture2D>("Sprites/RamEnemy"), Vector2.Zero, new Vector2(0, 0), 0, 1);
            _ringEnemyPrefab = new RingEnemy(Content.Load<Texture2D>("Sprites/RingEnemy"), Vector2.Zero, new Vector2(0, 0), 0, 1);
            rndP = new RandomPosition();
            AllBullets = new List<Bullet>();
            Enemies = new List<DefaultEnemy>();
        }
        public void SpawnEnemies(Player P, GameTime gameTime)
        {
            SpawnRingEnemy(P, gameTime);
            SpawnRamEnemy(P, gameTime);
            SpawnDefaultEnemy(P, gameTime);
            RemoveDistantEnemies(P, 3000);
            Debug.WriteLine(AllBullets.Count);
        }

        private void RemoveDistantEnemies(Player P, float Distance)
        {
            foreach (var Enemy in Enemies)
            {
                if (Vector2.Distance(P.Position, Enemy.Position) > Distance)
                {
                    Enemy.IsRemoved = true;
                }
            }
        }


        public void SpawnRingEnemy(Player P, GameTime gameTime)
        {
            thirdTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (thirdTimer > 20)
            {
                RingEnemy _ringEnemy = new RingEnemy(_ringEnemyPrefab._sprite, Vector2.Zero, new Vector2(0, 0), 0, 1);
                _ringEnemy.BulletType = _bulletPrefab;
                _ringEnemy.FollowDistance = rndP.GetPositiveNum(200, 500);
                _ringEnemy.Position = P.Position + new Vector2(rndP.GetRandomNumForPosition(300, 700), rndP.GetRandomNumForPosition(300, 700));
                Enemies.Add(_ringEnemy);
                thirdTimer = 0;
            }

        }
        private void SpawnRamEnemy(Player P, GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds > 10)
            {
              _secondTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (_secondTimer > 15)
            {
                RamEnemy _ramEnemy = new RamEnemy(_ramEnemyPrefab._sprite, Vector2.Zero, new Vector2(0, 0), 0, 1);
                _ramEnemy.FollowTarget = P;
                _ramEnemy.FollowDistance = 5f;
                _ramEnemy.Speed = 6f;
                _ramEnemy.Position = P.Position + new Vector2(rndP.GetRandomNumForPosition(400, 800), rndP.GetRandomNumForPosition(400, 800));
                Enemies.Add(_ramEnemy);
                _secondTimer = 0;
            }
        }
        public void SpawnDefaultEnemy(Player P, GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > 3)
            {
                DefaultEnemy _defaultEnemy = new DefaultEnemy(_defaultEnemyPrefab._sprite, Vector2.Zero, new Vector2(0, 0), 0, 1);
                _defaultEnemy.FollowTarget = P;
                _defaultEnemy.BulletType = _bulletPrefab;
                _defaultEnemy.FollowDistance = rndP.GetPositiveNum(200, 500);
                _defaultEnemy.Position = P.Position + new Vector2(rndP.GetRandomNumForPosition(300, 700), rndP.GetRandomNumForPosition(300, 700));
                Enemies.Add(_defaultEnemy);
                _timer = 0;
            }
          
        }


        #region UpdateMethods

        public void DeleteEnemies()
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
               
                if (this.Enemies[i].IsRemoved)
                {
                    this.Enemies.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            UpdateAllEnemies(gameTime);
            DeleteEnemies();
            RefreshAllEnemyBullets();
            UpdateAllEnemyBullets(gameTime);
        }

        private void DeleteBullets()
        {
            for (int i = 0; i < AllBullets.Count; i++)
            {
                if ((AllBullets[i] as Bullet).IsRemoved)
                {
                    AllBullets.RemoveAt(i);
                    i--;
                }
            }
        }

        private void RefreshAllEnemyBullets()
        {
            foreach (var Enemy in this.Enemies)
            {
                while (!(Enemy.Bullets.Count == 0))
                {
                    AllBullets.Add(Enemy.Bullets.Dequeue());
                }
            }
        }

        private void UpdateAllEnemies(GameTime gameTime)
        {
            foreach (var _enemy in this.Enemies)
            {
              
                if (_enemy.GetType().Name == "RamEnemy") 
                {
                    _enemy.Follow();
                }
                if (_enemy.GetType().Name == "DefaultEnemy")
                {
                    _enemy.Follow();
                    _enemy.Update(gameTime); 
                }
                if (_enemy.GetType().Name == "RingEnemy")
                {
                    _enemy.Update(gameTime);
                }
            }
        }

        private void UpdateAllEnemyBullets(GameTime gameTime)
        {
            foreach (var Bullet in AllBullets)
            {
                Bullet.Update(gameTime);
            }
            DeleteBullets();
        }
        #endregion UpdateMethods

        #region DrawMethods

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var Enemy in Enemies)
            {
                Enemy.Draw(gameTime, spriteBatch);
            }
            DrawAllBullets(gameTime, spriteBatch);
        }

        private void DrawAllBullets(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Bullet Bullet in AllBullets)
            {
                Bullet.Draw(gameTime, spriteBatch);
            }
        }
        #endregion DrawMethods
    }
}