using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace ScrollingShooter
{
    class ParallaxingBackground
    {
        #region Declarations
        Texture2D texture;
        Vector2 positions;
        int speed;
        int bgHeight, bgWidth;
        Random random = new Random();
        TimeSpan islandSpawnTime = TimeSpan.FromSeconds(10f);
        TimeSpan previousSpawnTime = TimeSpan.Zero;
        #endregion

        public void Initialize(ContentManager content, String texturePath, int screenWidth, int screenHeight, int speed)
        {
            bgHeight = screenHeight;
            bgWidth = screenWidth;
            texture = content.Load<Texture2D>(texturePath);
            this.speed = speed;

            positions = new Vector2(random.Next(0, bgWidth - 100), -100);
        }

        public void Update(GameTime gameTime)
        {
            int randomPosition = random.Next(0, bgWidth - 100);

            while (gameTime.TotalGameTime - previousSpawnTime > islandSpawnTime)
            {
                previousSpawnTime = gameTime.TotalGameTime;

                positions = new Vector2(randomPosition, 0);

            }


            positions.Y -= speed;

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            Rectangle rectBg = new Rectangle((int)positions.X, (int)positions.Y, 100, 100);
            spriteBatch.Draw(texture, rectBg, Color.White);
        }
    }


}
