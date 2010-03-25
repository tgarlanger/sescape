using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;

namespace SantellosEscape.Screens.GameScreens.ShooterGallery
{
    class Computer
    {
        public Vector2 Position;
         public Rectangle BoundingBox;
        
        public void computer(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }
            

        
        public void Initialize()
        {
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, 30, 30);
        }
        public void update()
        { 
            

        }



        
    }
    
}
