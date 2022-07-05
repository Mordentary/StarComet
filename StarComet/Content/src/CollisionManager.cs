using System.Collections.Generic;

namespace StarComet.Content.src
{
    internal class CollisionManager
    {
        public void UpdateCollision(Player Player, List<DefaultEnemy> Enemies, List<Bullet> AllBullet, Shield[] Shields, List<AmmoSupply> AmmoSupplies)
        {
            Shields_Enemies(Enemies, Shields, Player);
            Shields_Bullets(AllBullet, Shields);
            EnemyB_Player(Player, AllBullet);
            EnemyB_Enemy(Enemies, AllBullet);
            PlayerB_Enemy(Player, Enemies);
            PlayerCo_Enemy(Player, Enemies);
            Player_AmmoSupplies(Player, AmmoSupplies);
            RamEnemy_DefaultEnemy(Enemies);
        }

        private static void Shields_Bullets(List<Bullet> AllBullet, Shield[] Shields)
        {
            foreach (var Shield in Shields)
            {
                foreach (var Bullet in AllBullet)
                {
                    if (!(Shield is null))
                    {
                        if (Shield.Intersects(Bullet))
                        {
                            Shield.OnCollide(Bullet);
                        }
                    }
                }
            }
        }

        private static void Shields_Enemies(List<DefaultEnemy> Enemies, Shield[] Shields, Player Player)
        {
            foreach (var Shield in Shields)
            {
                foreach (var Enemy in Enemies)
                {
                    if (!(Shield is null))
                    {
                        if (Shield.Intersects(Enemy))
                        {
                            Player.Score++;
                            Shield.OnCollide(Enemy);
                        }
                    }
                }
            }
        }

        private static void RamEnemy_DefaultEnemy(List<DefaultEnemy> Enemies)
        {
            foreach (var R_Enemy in Enemies)
            {
                foreach (var D_Enemy in Enemies)
                {
                    if (R_Enemy.GetType().Name == "RamEnemy" && D_Enemy.GetType().Name == "DefaultEnemy" && R_Enemy.Intersects(D_Enemy))
                    {
                        R_Enemy.OnCollide(D_Enemy);
                    }
                }
            }
        }

        private static void Player_AmmoSupplies(Player Player, List<AmmoSupply> AmmoSupplies)
        {
            foreach (var Ammo in AmmoSupplies)
            {
                if (Player.Intersects(Ammo))
                {
                    Player.OnCollide(Ammo);
                }
            }
        }

        private static void PlayerCo_Enemy(Player Player, List<DefaultEnemy> Enemies)
        {
            foreach (DefaultEnemy Enemy in Enemies)
            {
                if (Player.Intersects(Enemy))
                {
                    Player.OnCollide(Enemy);
                }
            }
        }

        private static void PlayerB_Enemy(Player Player, List<DefaultEnemy> Enemies)
        {
            foreach (Bullet PlayerBullet in Player.Children)
            {
                foreach (var Enemy in Enemies)
                {
                    if (PlayerBullet.Intersects(Enemy))
                    {
                        Player.Score++;
                        PlayerBullet.OnCollide(Enemy);
                    }
                }
            }
        }

        private static void EnemyB_Enemy(List<DefaultEnemy> Enemies, List<Bullet> AllBullet)
        {
            foreach (Bullet EnemyBullet in AllBullet)
            {
                foreach (var Enemy in Enemies)
                {
                    if (EnemyBullet.Intersects(Enemy))
                    {
                        
                        EnemyBullet.OnCollide(Enemy);
                    }
                }
            }
        }

        private static void EnemyB_Player(Player Player, List<Bullet> AllBullet)
        {
            foreach (Bullet EnemyBullet in AllBullet)
            {
                if (EnemyBullet.Intersects(Player))
                {
                    EnemyBullet.OnCollide(Player);
                }
            }
        }
    }
}