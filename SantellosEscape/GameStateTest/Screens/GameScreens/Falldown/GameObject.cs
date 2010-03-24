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
    class GameObject
    {
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public bool Visible { get; set; }
        public Rectangle BoundingRectangle { get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); } }
        public string Value { get; set; }
        public int Duration { get; set; }
        public int StartTime { get; set; }
        public bool Active { get; set; }
        public bool isColliding { get; set; }
        public float tempYpos { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public void scroll(float scrollSpeed)
        {
            Position = new Vector2(Position.X, Position.Y + scrollSpeed);
            if (Position.Y < -Texture.Height)
                Position = new Vector2(Position.X, 477);
        }
    }
}