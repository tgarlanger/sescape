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

namespace SantellosEscape.Screens.GameScreens.ShooterGallery
{
    class freeze
    {
        Vector2 Position;
        int GameTimer;
        int Duration = 2;
        

        public bool DoubleScore(GameTime gameTime)
        {
            //if (called == true)
            {
                GameTimer = gameTime.TotalGameTime.Seconds + (gameTime.TotalGameTime.Minutes * 60);
                if ((gameTime.TotalGameTime.Seconds + (gameTime.TotalGameTime.Minutes * 60)) - GameTimer >= Duration)
                    return false;
                else return true;
            }
        }


        
        
    }
}
