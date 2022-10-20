using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ScrollingShooter
{
    class IslandManager
    {
        #region Declarations
        Texture2D islandTexture1;
        Texture2D islandTexture2;
        Texture2D islandTexture3;

        static public List<Island> islandType1 = new List<Island>();
        static public List<Island> islandType2 = new List<Island>();
        static public List<Island> islandType3 = new List<Island>();

        TimeSpan islandSpawnTime1 = TimeSpan.FromSeconds(5.0f);
        TimeSpan islandSpawnTime2 = TimeSpan.FromSeconds(3.0f);
        TimeSpan islandSpawnTime3 = TimeSpan.FromSeconds(4.0f);

        TimeSpan previousSpawnTime1 = TimeSpan.Zero;
        TimeSpan previousSpawnTime2 = TimeSpan.Zero;
        TimeSpan previousSpawnTime3 = TimeSpan.Zero;

        Vector2 graphicsInfo;

        Random random = new Random();
        #endregion

        public void Initialize(Texture2D texture1, Texture2D texture2, Texture2D texture3, GraphicsDevice graphics)
        {
            graphicsInfo.X = graphics.Viewport.Width;
            graphicsInfo.Y = graphics.Viewport.Height;
            islandTexture1 = texture1;
            islandTexture2 = texture2;
            islandTexture3 = texture3;
        }

        public void AddIsland1()
        {
            Animation islandAnim = new Animation();
            Island island1 = new Island();

            islandAnim.Initialize(islandTexture1, Vector2.Zero, 62, 63, 1, 1, Color.White, 1, true);

            int newX = (int)graphicsInfo.X;
            Vector2 position = new Vector2(random.Next(0, newX - 60), 0);

            island1.Initialize(islandAnim, position, 4);
            islandType1.Add(island1);

        }

        public void AddIsland2()
        {
            Animation islandAnim = new Animation();
            Island island2 = new Island();

            islandAnim.Initialize(islandTexture2, Vector2.Zero, 62, 63, 1, 1, Color.White, 1, true);

            int newX = (int)graphicsInfo.X;
            Vector2 position = new Vector2(random.Next(0, newX - 60), 0);

            island2.Initialize(islandAnim, position, 4);
            islandType2.Add(island2);

        }

        public void AddIsland3()
        {
            Animation islandAnim = new Animation();
            Island island3 = new Island();

            islandAnim.Initialize(islandTexture1, Vector2.Zero, 62, 63, 1, 1, Color.White, 1, true);

            int newX = (int)graphicsInfo.X;
            Vector2 position = new Vector2(random.Next(0, newX - 60), 0);

            island3.Initialize(islandAnim, position, 4);
            islandType3.Add(island3);



        }

        public void UpdateIslands(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - previousSpawnTime1 > islandSpawnTime1)
            {
                previousSpawnTime1 = gameTime.TotalGameTime;
                
                AddIsland1();
            }

            if (gameTime.TotalGameTime - previousSpawnTime2 > islandSpawnTime2)
            {
                previousSpawnTime2 = gameTime.TotalGameTime;
                AddIsland2();
            }

            if (gameTime.TotalGameTime - previousSpawnTime3 > islandSpawnTime3)
            {
                previousSpawnTime3 = gameTime.TotalGameTime;
                AddIsland3();
            }

            for (int i = (islandType1.Count - 1); i >= 0; i--)
            {
                islandType1[i].Update(gameTime);
                if (islandType1[i].active == false)
                {
                    islandType1.RemoveAt(i);
                    System.Diagnostics.Debug.WriteLine(" remove island");

                }
            }

            for (int i = (islandType2.Count - 1); i >= 0; i--)
            {
                islandType2[i].Update(gameTime);
                if (islandType2[i].active == false)
                {
                    islandType2.RemoveAt(i);
                }
            }

            for (int i = (islandType3.Count - 1); i >= 0; i--)
            {
                islandType3[i].Update(gameTime);
                if (islandType3[i].active == false)
                {
                    islandType3.RemoveAt(i);
                }
            }
        }

        public void DrawIslands(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < islandType1.Count; i++)
            {
                islandType1[i].Draw(spriteBatch);
            }

            for (int i = 0; i < islandType2.Count; i++)
            {
                islandType2[i].Draw(spriteBatch);
            }

            for (int i = 0; i < islandType3.Count; i++)
            {
                islandType3[i].Draw(spriteBatch);
            }
        }

    }
}
