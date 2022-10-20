using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ScrollingShooter
{
    class Explosion
    {
        Animation explosionAnimation;
        Vector2 position;
        public bool active;
        int timeToLive;

        public int Width
        {
            get { return explosionAnimation.frameWidth; }
        }

        public int Height
        {
            get { return explosionAnimation.frameHeight; }
        }
        public void Initialize(Animation animation, Vector2 position)
        {
            explosionAnimation = animation;
            this.position = position;
            active = true;
            timeToLive = 30;
        }
        public void Update(GameTime gameTime)
        {
            explosionAnimation.Update(gameTime);
            timeToLive -= 1;
            if(timeToLive <= 0)
            {
                this.active = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            explosionAnimation.Draw(spriteBatch);
        }
    }
}
