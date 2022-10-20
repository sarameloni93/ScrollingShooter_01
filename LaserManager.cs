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
    class LaserManager
    {
        #region Declarations
        static Texture2D laserTexture;
        static Rectangle laserRectangle;
        static public List<Laser> laserBeams;
        const float SECONDS_IN_MINUTE = 60f;
        const float RATE_OF_FIRE = 200f;

        //how fast
        static TimeSpan laserSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
        static TimeSpan previousLaserSpawnTime;

        GraphicsDeviceManager graphics;
        static Vector2 graphicsInfo;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        GamePadState currentGamePadState;
        GamePadState previousGamePadState;
        #endregion

        public void Initialize(Texture2D texture, GraphicsDevice Graphics)
        {
            laserBeams = new List<Laser>();
            previousLaserSpawnTime = TimeSpan.Zero;
            laserTexture = texture;
            graphicsInfo.X = Graphics.Viewport.Width;
            graphicsInfo.Y = Graphics.Viewport.Height;
        }

        private static void FireLaser(GameTime gameTime, Player p, Sounds SND)
        {
            if(gameTime.TotalGameTime - previousLaserSpawnTime > laserSpawnTime)
            {
                previousLaserSpawnTime = gameTime.TotalGameTime;
                AddLaser(p);
                SND.LAZER.Play();
            }
        }

        private static void AddLaser(Player p)
        {
            Animation laserAnimation = new Animation();
            laserAnimation.Initialize(laserTexture, p.position,
                46, 16, 1, 30, Color.White, 1f, true);
            Laser laser = new Laser();
            var laserPosition = p.position;
            //adjust the laser position to match the end of the cannon
            laserPosition.Y += 10;
            laserPosition.X += 20;

            laser.Initialize(laserAnimation, laserPosition, 30f, 10);
            laserBeams.Add(laser);
        }


        public void UpdateManagerLaser(GameTime gameTime, Player p, ExplosionManager VFX, GUI guiInfo, Sounds snd)
        {
            previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;

            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            currentKeyboardState = Keyboard.GetState();

            if(Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
            {
                FireLaser(gameTime, p, snd);
            }

            for(var i = 0; i < laserBeams.Count; i++)
            {
                laserBeams[i].Update(gameTime);
                if(!laserBeams[i].active || laserBeams[i].position.Y > graphicsInfo.Y)
                {
                    laserBeams.Remove(laserBeams[i]);
                }
            }

            foreach (Enemy e in EnemyManager.enemiesType1)
            {
                Rectangle enemyRectangle = new Rectangle(
                    (int)e.position.X,
                    (int)e.position.Y,
                    e.Width,
                    e.Height);


                foreach (Laser L in LaserManager.laserBeams)
                {
                    laserRectangle = new Rectangle(
                        (int)L.position.X,
                        (int)L.position.Y,
                        L.Width,
                        L.Height);

                    if (laserRectangle.Intersects(enemyRectangle))
                    {
                        //play sound explosion

                        //show explosion
                        VFX.AddExplosion(e.position, snd);

                        //kill enemy
                        e.health = 0;
                        e.active = false;
                        guiInfo.SCORE += 20; //enemy1
                        

                        //record kill

                        //kill laser
                        L.active = false;
                    }
                }


            }
            foreach (Enemy e in EnemyManager.enemiesType2)
            {
                Rectangle enemyRectangle = new Rectangle(
                    (int)e.position.X,
                    (int)e.position.Y,
                    e.Width,
                    e.Height);


                foreach (Laser L in LaserManager.laserBeams)
                {
                    laserRectangle = new Rectangle(
                        (int)L.position.X,
                        (int)L.position.Y,
                        L.Width,
                        L.Height);

                    if (laserRectangle.Intersects(enemyRectangle))
                    {
                        //play sound explosion

                        //show explosion
                        VFX.AddExplosion(e.position, snd);

                        //kill enemy
                        e.health = 0;
                        e.active = false;
                        guiInfo.SCORE += 35; //enemy1


                        //record kill

                        //kill laser
                        L.active = false;
                    }
                }


            }
        }

        public void DrawLasers(SpriteBatch spriteBatch)
        {
            foreach(var l in laserBeams)
            {
                l.Draw(spriteBatch);
            }
        }

    }
}
