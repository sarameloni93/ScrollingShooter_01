using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScrollingShooter
{
    class Laser
    {
        #region Declarations
        public Animation laserAnimation;
        float laserMoveSpeed;
        public Vector2 position;
        int damage;
        public bool active;
        //int range;

        public int Width
        {
            get { return laserAnimation.frameWidth; }
        }

        public int Height
        {
            get { return laserAnimation.frameHeight; }
        }
        #endregion


        public void Initialize(Animation animation, Vector2 position, float laserMoveSpeed, int damage) 
        {
            laserAnimation = animation;
            this.position = position;
            active = true;
            this.laserMoveSpeed = laserMoveSpeed;
            this.damage = damage;
        }

        public void Update(GameTime gameTime)
        {
            position.Y -= laserMoveSpeed;
            laserAnimation.position = position;
            laserAnimation.Update(gameTime);

        }

        public void UpdateEnemyLaser(GameTime gameTime)
        {
            position.Y += laserMoveSpeed;
            laserAnimation.position = position;
            laserAnimation.Update(gameTime);

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            laserAnimation.Draw(spriteBatch);

        }
    }
}
