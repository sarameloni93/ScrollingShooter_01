using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScrollingShooter
{
    class Island
    {

        public Animation islandAnim;
        public float islandMoveSpeed;
        public Vector2 position;
        public bool active;

        public int Width
        {
            get { return islandAnim.frameWidth; }
        }

        public int Height
        {
            get { return islandAnim.frameHeight; }

        }

        public void Initialize(Animation animation, Vector2 position, float islandMoveSpeed)
        {
            active = true;
            islandAnim = animation;
            this.position = position;
            this.islandMoveSpeed = islandMoveSpeed;
        }

        public void Update(GameTime gameTime)
        {
            position.Y += islandMoveSpeed;
            islandAnim.position = position;
            islandAnim.Update(gameTime);

            if (position.Y > Globals.screenHeight)
            {
               // System.Diagnostics.Debug.WriteLine(" island not active ");
                active = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            islandAnim.Draw(spriteBatch);

        }
    }
}
