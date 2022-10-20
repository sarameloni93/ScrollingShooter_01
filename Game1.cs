using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
namespace ScrollingShooter
{
    public class Game1 : Game
    {
        #region Declarations
        GraphicsDevice details;
        Vector2 graphicsInfo;

        //GUI
        private SpriteFont guiFont, menuFont;
        Texture2D legend;
        Texture2D playerLife;
        GUI guiInfo = new GUI();

        //GAME STATES
        enum GameStates { TitleScreen, Start, Playing, Pause, End, GameOver};
        GameStates gameState = GameStates.TitleScreen;
        float gameOverTimer = 0.0f;
        float gameOverDelay = 6.0f;
        float waveCompleteTimer = 0.0f;
        float waveCompleteDelay = 3.0f;
        float endTimer = 0.0f;
        float endDelay = 2.0f;
        Texture2D titleScreen, gameOver;

        KeyboardState oldState;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        float scale = 1f;

        //PLAYER
        Player player;
        Texture2D playerTexture;


        //ENEMY
        Texture2D enemyTexture1;
        Texture2D enemyTexture2;
        EnemyManager enemyPlane = new EnemyManager();



        //SOUNDS
        private SoundEffect laserSound;
        private SoundEffect explosionSound;
        private Song gameMusic;
        Sounds SND = new Sounds();


        //LASER
        Texture2D laserTexture;
        LaserManager laserBeams = new LaserManager();

        //EXPLOSION
        Texture2D vfx;
        ExplosionManager VFX = new ExplosionManager();
        Texture2D PExplosion;
        ExplosionManager playerExplosion = new ExplosionManager();

        //background
        Texture2D mainBackground;
        Rectangle rectBackground;
        int screenWidth, screenHeight;
        Texture2D islandTexture1;
        Texture2D islandTexture2;
        Texture2D islandTexture3;
        IslandManager islands = new IslandManager();

        #endregion

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Globals.screenWidth;   
            _graphics.PreferredBackBufferHeight = Globals.screenHeight;
            _graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            oldState = Keyboard.GetState();
            player = new Player();

            //BACKGROUND
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;
            rectBackground = new Rectangle(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y, screenWidth, screenHeight);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            //GUI
            guiFont = Content.Load<SpriteFont>("GUIFont");
            menuFont = Content.Load<SpriteFont>("MenuFont");
            legend = Content.Load<Texture2D>("legend");
            guiInfo.Initialize(0, 100, 3);
            titleScreen = Content.Load<Texture2D>("titleScreen");
            gameOver = Content.Load <Texture2D>("gameOver");
            playerLife = Content.Load<Texture2D>("life");

            graphicsInfo.X = GraphicsDevice.Viewport.Width;
            graphicsInfo.Y = GraphicsDevice.Viewport.Height;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            details = GraphicsDevice;


            //PLAYER
            Vector2 playerPosition = new Vector2(
                GraphicsDevice.Viewport.TitleSafeArea.X + GraphicsDevice.Viewport.TitleSafeArea.Width / 2, 
                GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height - 150);

            Animation playerAnimation = new Animation();
            playerTexture = Content.Load<Texture2D>("playerAnimation");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 66, 65, 3, 30, Color.White, scale, true);

            player.Initialize(playerAnimation, playerPosition, graphicsInfo);
            PExplosion = Content.Load<Texture2D>("playerExplosion");
            playerExplosion.Initialize(vfx, PExplosion, details);


            //ENEMY
            enemyTexture1 = Content.Load<Texture2D>("enemy1");
            enemyTexture2 = Content.Load<Texture2D>("enemy2");
            enemyPlane.Initialize(enemyTexture1, enemyTexture2, details);
            


            //LASER
            laserTexture = Content.Load<Texture2D>("laser");
            laserBeams.Initialize(laserTexture, details);
            vfx = Content.Load<Texture2D>("explosion");
            VFX.Initialize(vfx, PExplosion, details);

            //BACKGROUND
            mainBackground = Content.Load<Texture2D>("sky");

            islandTexture1 = Content.Load<Texture2D>("island1");
            islandTexture2 = Content.Load<Texture2D>("island2");
            islandTexture3 = Content.Load<Texture2D>("island3");

            islands.Initialize(islandTexture1, islandTexture2, islandTexture3, details);

            //SOUNDS
            laserSound = Content.Load<SoundEffect>("playershotsound");
            explosionSound = Content.Load<SoundEffect>("explosionSound");
            gameMusic = Content.Load<Song>("themesong");
            SND.Initialize(laserSound, explosionSound);
            MediaPlayer.Play(gameMusic);  //play only when playing(game state), not on menu

            //

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {



            switch (gameState)
            {
                case GameStates.TitleScreen:

                    if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        gameState = GameStates.Start;
                    }
                    break;

                case GameStates.Start:
                    waveCompleteTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (waveCompleteTimer > waveCompleteDelay)
                    {
                        
                        gameState = GameStates.Playing;
                        waveCompleteTimer = 0.0f;
                    }
                    break;

                case GameStates.Playing:

                    islands.UpdateIslands(gameTime);

                    player.Update(gameTime);

                    enemyPlane.UpdateEnemies(gameTime, player, VFX, guiInfo, SND);

                    laserBeams.UpdateManagerLaser(gameTime, player, VFX, guiInfo, SND);
                    VFX.UpdateExplosions(gameTime);



                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        gameState = GameStates.Pause;
                    }
                        
                    break;


            
                case GameStates.GameOver:
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();
                    guiInfo.SCORE = 0;
                    guiInfo.PlayerHP = 100;
                    guiInfo.LIVES = 3;

                    gameOverTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (gameOverTimer > gameOverDelay)
                    {

                        gameState = GameStates.TitleScreen;
                        MediaPlayer.Play(gameMusic);
                        gameOverTimer = 0.0f;

                    }
                    break;

                case GameStates.Pause:
                    MediaPlayer.Pause();
                    if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        MediaPlayer.Resume();

                        gameState = GameStates.Playing;
                    }

                    if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Q))
                    {
                        gameState = GameStates.GameOver;
                    }

                    break;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(mainBackground, rectBackground, Color.White);
            islands.DrawIslands(_spriteBatch);

            if (gameState == GameStates.TitleScreen)
            {
                _spriteBatch.Draw(titleScreen, new Rectangle(0, 0, titleScreen.Width, titleScreen.Height), Color.White);
                _spriteBatch.DrawString(menuFont, "Press SPACE to start the game", new Vector2(300, 800), Color.Yellow);

            }


            if (gameState == GameStates.Pause)
            {
                _spriteBatch.DrawString(menuFont, "Pause ", new Vector2(300, 200), Color.Yellow);
                _spriteBatch.DrawString(menuFont, "Press Q to QUIT", new Vector2(300, 500), Color.Yellow);
                _spriteBatch.DrawString(menuFont, "Press P to continue playing", new Vector2(300, 700), Color.Yellow);
            }

            if (gameState == GameStates.Start)
            {
                _spriteBatch.DrawString(menuFont, "Starting...", new Vector2(300, 300), Color.Yellow);
            }

            if (gameState == GameStates.Playing)
            {
                player.Draw(_spriteBatch);
                enemyPlane.DrawEnemies(_spriteBatch);

                laserBeams.DrawLasers(_spriteBatch);
                VFX.DrawExplosions(_spriteBatch);
                CheckPlayerDeath(gameTime);

                _spriteBatch.Draw(legend, new Vector2(0, 0), Color.White);
                _spriteBatch.DrawString(guiFont, "Score: " + guiInfo.SCORE, new Vector2(5, 5), Color.Yellow);
                _spriteBatch.DrawString(guiFont, "HP: " + guiInfo.PlayerHP, new Vector2(400, 5), Color.Yellow);
            
                for(int i=1; i <= guiInfo.LIVES; i++)
                {
                    _spriteBatch.Draw(playerLife, new Vector2(700 + i*80, 10), Color.White);
                }
            }       


            if(gameState == GameStates.GameOver)
            {
                _spriteBatch.Draw(gameOver, new Rectangle(0, 0, titleScreen.Width, titleScreen.Height), Color.White);

            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CheckPlayerDeath(GameTime gameTime)
        {
            if(guiInfo.LIVES <= 0)
            {
                VFX.AddPlayerExplosion(player, SND);
                endTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (endTimer > endDelay)
                {
                    gameState = GameStates.GameOver;
                    waveCompleteTimer = 0.0f;
                }
            }
        }




    }
}
