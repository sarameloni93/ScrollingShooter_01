using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ScrollingShooter
{
    class ExplosionManager
    {
        List<Explosion> explosions;
        Texture2D explosionTexture;
        Texture2D explosionPTexture;
        Vector2 graphicsInfo;

        public void Initialize(Texture2D texture, Texture2D textureP, GraphicsDevice Graphics)
        {
            graphicsInfo.X = Graphics.Viewport.Width;
            graphicsInfo.Y = Graphics.Viewport.Height;

            explosions = new List<Explosion>();
            explosionTexture = texture;
            explosionPTexture = textureP;
        }  

        public void AddExplosion(Vector2 enemyPosition, Sounds snd)
        {
            Animation explosionAnimation = new Animation();

            explosionAnimation.Initialize(explosionTexture,
                enemyPosition,
                33,
                33,
                6,
                30,
                Color.White,
                1.0f,
                true);

            Explosion explosion = new Explosion();
            explosion.Initialize(explosionAnimation, enemyPosition);

            explosions.Add(explosion);

            snd.EXPLOSION.Play();
        }
        public void AddPlayerExplosion(Player p, Sounds snd)
        {
            Animation explosionAnimationP = new Animation();
            explosionAnimationP.Initialize(
                explosionPTexture,
                (p.position),
                85,
                85,
                7,
                30,
                Color.White,
                1.0f,
                true);

            Explosion explosionPlayer = new Explosion();
            explosionPlayer.Initialize(explosionAnimationP, p.position);

            explosions.Add(explosionPlayer);

            snd.EXPLOSION.Play();
        }

        public void UpdateExplosions(GameTime gameTime)
        {
            for(var e = 0; e < explosions.Count; e++)
            {
                explosions[e].Update(gameTime);

                if (!explosions[e].active)   ////////////////if not active, remove
                    explosions.Remove(explosions[e]);
            }
        }

        public void DrawExplosions(SpriteBatch spriteBatch)
        {
            foreach(var e in explosions)
            {
                e.Draw(spriteBatch);
            }
        }
    }
}
