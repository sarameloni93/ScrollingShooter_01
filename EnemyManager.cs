using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;


namespace ScrollingShooter
{
    class EnemyManager
    {
        #region Declarations
        Texture2D enemyTexture;
        Texture2D enemyTexture2;
        static public List<Enemy> enemiesType1 = new List<Enemy>();
        static public List<Enemy> enemiesType2 = new List<Enemy>();

        //GraphicsDeviceManager graphics;

        TimeSpan enemy1SpawnTime = TimeSpan.FromSeconds(1.0f);
        TimeSpan enemy2SpawnTime = TimeSpan.FromSeconds(3.0f);

        TimeSpan previousSpawnTime1 = TimeSpan.Zero;
        TimeSpan previousSpawnTime2 = TimeSpan.Zero;


        Random random = new Random();

        Vector2 graphicsInfo;

        #endregion

        public void Initialize(Texture2D texture, Texture2D texture2, GraphicsDevice graphics)
        {
            
            graphicsInfo.X = graphics.Viewport.Width;
            graphicsInfo.Y = graphics.Viewport.Height;
            enemyTexture = texture;
            enemyTexture2 = texture2;

        }

        private void AddEnemy1()
        {
            Animation enemyAnimation = new Animation();
            Enemy enemy1 = new Enemy();

            enemyAnimation.Initialize(enemyTexture, Vector2.Zero, 33, 33, 3, 30, Color.White, 1f, true);

            int newX = (int)graphicsInfo.X;
            Vector2 position = new Vector2(random.Next(0, newX ), 0);


            enemy1.Initialize(enemyAnimation, position, 10, 10, 2.0f);


            enemiesType1.Add(enemy1);


        }
        private void AddEnemy2()
        {
            Animation enemyAnimation2 = new Animation();

            enemyAnimation2.Initialize(enemyTexture2, Vector2.Zero, 33, 33, 3, 30, Color.White, 1f, true);

            int newX = (int)graphicsInfo.X;
            Vector2 position = new Vector2(random.Next(0, newX - 50), 0);

            Enemy enemy2 = new Enemy();


            enemy2.Initialize(enemyAnimation2, position, 20, 20, 5.0f);

            enemiesType2.Add(enemy2);


        }
        public void UpdateCollision(Player player, ExplosionManager VFX, GUI guiInfo, Sounds snd)
        {
            Rectangle rect1, rect2;

            rect1 = new Rectangle(
                (int)player.position.X,
                (int)player.position.Y,
               player.Width, player.Height);

            for (int i = 0; i < enemiesType1.Count; i++)
            {
                rect2 = new Rectangle(
                    (int)enemiesType1[i].position.X,
                    (int)enemiesType1[i].position.Y,
                    enemiesType1[i].Width,
                    enemiesType1[i].Height);

                if (rect1.Intersects(rect2))
                {
                    player.health -= enemiesType1[i].damage;
                    //System.Diagnostics.Debug.WriteLine(" hit ");
                    enemiesType1[i].health = 0;
                    
                    VFX.AddExplosion(enemiesType1[i].LocationEnemy, snd);
                    enemiesType1.RemoveAt(i);
                    guiInfo.SCORE += 20;   //////?? when intersects with enemy, losing HP and incr score ??
                    guiInfo.PlayerHP -= 25;

                    if(guiInfo.PlayerHP == 0 && guiInfo.LIVES > 0)
                    {
                        player.active = false;
                        guiInfo.LIVES--;
                        guiInfo.PlayerHP = 100;
                    }
                }
            }

            for (int i = 0; i < enemiesType2.Count; i++)
            {
                rect2 = new Rectangle(
                    (int)enemiesType2[i].position.X,
                    (int)enemiesType2[i].position.Y,
                    enemiesType2[i].Width,
                    enemiesType2[i].Height);

                if (rect1.Intersects(rect2))
                {
                    player.health -= enemiesType2[i].damage;
                    guiInfo.PlayerHP -= 25;

                    enemiesType2[i].health -= 10;

                    VFX.AddExplosion(enemiesType2[i].LocationEnemy, snd);
                    enemiesType2.RemoveAt(i);
                    guiInfo.SCORE += 20; 

                    if (guiInfo.PlayerHP == 0 && guiInfo.LIVES > 0)
                    {
                        player.active = false;
                        guiInfo.LIVES--;
                        guiInfo.PlayerHP = 100;
                    }
                }
            }
        }

        public void UpdateEnemies(GameTime gameTime, Player player, ExplosionManager VFX, GUI guiInfo, Sounds snd)
        {

            //spawn new enemy every 1.5sec
            if (gameTime.TotalGameTime - previousSpawnTime1 > enemy1SpawnTime)
            {
                previousSpawnTime1 = gameTime.TotalGameTime;
                AddEnemy1();
            }

            if (gameTime.TotalGameTime - previousSpawnTime2 > enemy2SpawnTime)
            {
                previousSpawnTime2 = gameTime.TotalGameTime;
                AddEnemy2();
            }

            UpdateCollision(player, VFX, guiInfo, snd);

            //update enemies
            for (int i = (enemiesType1.Count - 1); i >= 0; i--)
            {
                enemiesType1[i].Update(gameTime);
                if (enemiesType1[i].active == false)
                {
                    enemiesType1.RemoveAt(i);
                }
            }

            for (int i = (enemiesType2.Count - 1); i >= 0; i--)
            {
                enemiesType2[i].Update(gameTime);
                if (enemiesType2[i].active == false)
                {
                    enemiesType2.RemoveAt(i);
                }
            }
        }

        public void DrawEnemies(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < enemiesType1.Count; i++)
            {
                enemiesType1[i].Draw(spriteBatch);
            }

            for (int i = 0; i < enemiesType2.Count; i++)
            {
                enemiesType2[i].Draw(spriteBatch);
            }
        }

       
    }
}
