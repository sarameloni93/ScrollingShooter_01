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
    class Enemy
    {
        #region Declarations
        public Animation enemyAnimation;
        public Vector2 position;
        public bool active;
        public int health;
        public int damage;
        public int value;
        float enemyMoveSpeed;
        #endregion


        public int Width
        {
            get { return enemyAnimation.frameWidth; }
        }

        public int Height
        {
            get { return enemyAnimation.frameHeight; }
        }

        public Vector2 LocationEnemy
        {
            get { return position; }
        }

        public void Initialize(Animation animation, Vector2 position, int health, int damage, float enemyMoveSpeed)
        {
            enemyAnimation = animation;
            this.position = position;
            active = true;
            this.health = health;   //10
            this.damage = damage;    //10
            this.enemyMoveSpeed = enemyMoveSpeed;    //6f
            value = 100;
        
        }

        public void Update(GameTime gameTime)
        {
            position.Y += enemyMoveSpeed;
            enemyAnimation.position = position;
            enemyAnimation.Update(gameTime);

            if (position.Y > Globals.screenHeight || health < 0)
            {
                active = false;
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            enemyAnimation.Draw(spriteBatch);

        }
    }
}
