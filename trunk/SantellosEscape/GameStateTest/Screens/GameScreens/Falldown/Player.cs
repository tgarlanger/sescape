using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace SantellosEscape.Screens.GameScreens.FallDown
{
    class Player : GameObject
    {
        public int movementSpeed = 3;
        private bool isFacingRight;
        private Rectangle[] frames = { new Rectangle(0, 0, 20, 40), new Rectangle(20, 0, 20, 40) };
        private int frame;

        public void Update(GameTime gameTime)
        {
            Vector2 newPosition;
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                newPosition = new Vector2(Position.X + movementSpeed, Position.Y);
                if (newPosition.X < 272 - this.Texture.Width / 2)
                    Position = newPosition;
                isFacingRight = true;
                if (gameTime.TotalRealTime.Milliseconds % 7 == 0)
                    frame = (frame == 0) ? 1 : 0;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                newPosition = new Vector2(Position.X - movementSpeed, Position.Y);
                if (newPosition.X > 0)
                    Position = newPosition;
                isFacingRight = false;
                if (gameTime.TotalRealTime.Milliseconds % 7 == 0)
                    frame = (frame == 0) ? 1 : 0;
            }

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D t)
        {
            //SpriteEffects flip = (!isFacingRight) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(t, new Vector2(Position.X, Position.Y + 20), Color.White);
            //
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects flip = (!isFacingRight) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(this.Texture,
                new Rectangle((int)Position.X, (int)Position.Y, 20, 40),
                frames[frame], Color.White, 0, Vector2.Zero, flip, 0);

        }
        public Rectangle getBounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y + 37, 20, 3);
        }
    }
}