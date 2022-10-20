using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrollingShooter
{
    class Animation
    {
        #region Declarations
        Texture2D spriteStrip;
        float scale;
        float elapsedTime;
        int frameTime;
        int frameCount;
        int currentFrame;
        Color color;
        Rectangle destRect = new Rectangle();
        Rectangle sourceRect = new Rectangle();
        public int frameWidth;
        public int frameHeight;
        public bool active;
        public bool looping;
        public Vector2 position;

        private List<Rectangle> frames = new List<Rectangle>();
        #endregion

        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frameTime, Color color, float scale, bool looping)
        {
            this.color = color;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frameTime;
            this.scale = scale;
            this.looping = looping;
            this.position = position;
            spriteStrip = texture;

            elapsedTime = 0;
            currentFrame = 0;

            active = true;

            sourceRect = new Rectangle(0, (currentFrame * frameHeight), frameWidth, frameHeight);

            destRect = new Rectangle(
                (int)position.X - (int)(frameWidth * this.scale) / 2,
                (int)position.Y - (int)(frameHeight * this.scale) / 2,
                (int)(frameWidth * this.scale),
                (int)(frameHeight * this.scale));

            for(int x = 0; x < frameCount; x++)
            {
                frames.Add(new Rectangle((frameWidth * x), 0, frameWidth, frameHeight));
            }
        }

        public void Update(GameTime gameTime)
        {

            if (active == false) return;
            

            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTime > frameTime)
            {
                currentFrame++;
                if (currentFrame == frameCount)
                {
                    currentFrame = 0;
                    if(looping == false)
                        active = false;
                }

                elapsedTime = 0; //reset the elapsed time to zero
            }

            sourceRect = frames[currentFrame];
            destRect = new Rectangle(
                (int)position.X,
                (int)position.Y,
                frameWidth,
                frameHeight);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(spriteStrip, destRect, sourceRect, color);

        }
    }
}
