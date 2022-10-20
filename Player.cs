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
    class Player
    {
        #region Declarations
        public Texture2D playerTexture;
        public Vector2 position;
        public bool active;
        public int health;
        public Rectangle sourceRect;

        //Viewport
        Vector2 graphicsInfo;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        MouseState currentMouseState;
        MouseState previousMouseState;

        // movement speed for the player
        float playerMoveSpeed;
        public Animation playerAnimation;

        public int Width
        {
            get { return playerAnimation.frameWidth; } 
        }

        public int Height
        {
            get { return playerAnimation.frameHeight; }
        }

        #endregion

        public void Initialize(Animation animation, Vector2 position,Vector2 grInfo)
        {
            //PlayerTexture = texture;
            playerAnimation = animation;
            sourceRect = new Rectangle(0, 0, 100, 100);
            this.position = position;

            active = true;

            health = 100;

            graphicsInfo = grInfo;//Set the viewport
            playerMoveSpeed = 8.0f;
        }

        public void Update(GameTime gameTime)
        {
            //Save the previous state of the keyboard and game pad so we can determine single key/button presses
            previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;

            //Read the current state of the keyboard and gamepad and store it
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            currentKeyboardState = Keyboard.GetState();

            // Read the current state of the Mouse  and store it
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            //Get Mouse State then Capture the Button type and Respond Button Press
            Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

            //Get Thumbsticks Controls
            position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;
            position.Y += currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;

            //Use the Keyboard/DPad
            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                position.X -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                position.X += playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentGamePadState.DPad.Up == ButtonState.Pressed)
            {
                position.Y -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                position.Y += playerMoveSpeed;
            }


            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 posDelta = mousePosition - position;
                posDelta.Normalize();
                posDelta = posDelta * playerMoveSpeed;
                position = position + posDelta;
            }

            //Restrict the POsition to remain with the screen bandwidth
            position.X = MathHelper.Clamp(position.X, 0, graphicsInfo.X - Width);
            position.Y = MathHelper.Clamp(position.Y, 80, graphicsInfo.Y - Height);

            //Handle the animation for the player 
            playerAnimation.position = position;
            playerAnimation.Update(gameTime);
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            playerAnimation.Draw(spriteBatch);
        }
    }
}
