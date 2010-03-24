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

namespace FallDown
{
    class Player : GameObject
    {
        public int movementSpeed = 3;

        public void Update()
        {
            Vector2 newPosition;
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                newPosition = new Vector2(Position.X + movementSpeed, Position.Y);
                if (newPosition.X < 272 - this.Texture.Width)
                    Position = newPosition;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                newPosition = new Vector2(Position.X - movementSpeed, Position.Y);
                if (newPosition.X > 0)
                    Position = newPosition;
            }

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D t)
        {
            spriteBatch.Draw(t, new Vector2(Position.X, Position.Y + 20), Color.White);
        }
        public Rectangle getBounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y + 30, Texture.Width, 10);
        }
    }
}