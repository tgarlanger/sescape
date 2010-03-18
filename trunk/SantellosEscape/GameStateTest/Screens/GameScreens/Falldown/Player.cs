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
        public bool isAlive {get; set;}
        public bool isColliding { get; set; }
        private int movementSpeed = 3;

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
    }
}
