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
    class Block
    {
        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        public Rectangle boundingRectangle { get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); } }
        public bool isEmpty { get; set; }

        public Block()
        {
            position = Vector2.Zero;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}