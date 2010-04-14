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
    public class Computer2
    {
        int GameTimer;
        int Duration = 2;
        int CDTimer;
        public Vector2 Position;
        public Vector2 Velocity;
        public Rectangle BoundingBox;
        bool isMoving = false;




        public Computer2()
        {

            GameTimer = 0;
            Duration = 2;
            CDTimer = 0;
            Position = new Vector2(0, 0);
            Velocity = new Vector2(0, 0);
            BoundingBox.Height = 0;
            BoundingBox.Width = 0;
        }

        public void Initialize()
        {
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, 30, 30);

        }
        public void Random(GameTime gameTime)
        {
#if WINDOWS
            if (Position.X < 0)
            {
                GameTimer = gameTime.TotalGameTime.Seconds + (gameTime.TotalGameTime.Minutes * 60);
                CDTimer = gameTime.TotalGameTime.Seconds + (gameTime.TotalGameTime.Minutes * 60);
                Velocity.X = 1;
                isMoving = true;
            }
            else if (Position.X > 480)
            {
                GameTimer = gameTime.TotalGameTime.Seconds + (gameTime.TotalGameTime.Minutes * 60);
                CDTimer = gameTime.TotalGameTime.Seconds + (gameTime.TotalGameTime.Minutes * 60);
                Velocity.X = -1;
                isMoving = true;
            }
            else if (Position.Y <0)
            {
                GameTimer = gameTime.TotalGameTime.Seconds + (gameTime.TotalGameTime.Minutes * 60);
                CDTimer = gameTime.TotalGameTime.Seconds + (gameTime.TotalGameTime.Minutes * 60);
                Velocity.Y = 1;
                isMoving = true;
            }
#endif
#if ZUNE
            if (Position.Y < 0)
            {
                GameTimer = gameTime.TotalGameTime.Seconds + (gameTime.TotalGameTime.Minutes * 60);
                CDTimer = gameTime.TotalGameTime.Seconds + (gameTime.TotalGameTime.Minutes * 60);
                Velocity.Y = 1;
                isMoving = true;
            }
            else if (Position.Y > 480)
            {
                GameTimer = gameTime.TotalGameTime.Seconds + (gameTime.TotalGameTime.Minutes * 60);
                CDTimer = gameTime.TotalGameTime.Seconds + (gameTime.TotalGameTime.Minutes * 60);
                Velocity.Y = -1;
                isMoving = true;
            }
            else if (Position.X < 0)
            {
                GameTimer = gameTime.TotalGameTime.Seconds + (gameTime.TotalGameTime.Minutes * 60);
                CDTimer = gameTime.TotalGameTime.Seconds + (gameTime.TotalGameTime.Minutes * 60);
                Velocity.X = 1;
                isMoving = true;
            }
#endif
        }
        public void Move(GameTime gameTime)
        {
#if WINDOWS

            if (isMoving)
            {
                if (Position.X < 80)
                {
                    if (Position.X == 0)
                    {
                        Velocity.X = 0;
                        isMoving = false;

                    }
                }
                else if (Position.X > 400)
                {


                    if (Position.X == 442)
                    {
                        Velocity.X = 0;
                        isMoving = false;

                    }
                }
                else if (Position.Y < 80 )
                {
                    if(Position.Y == 0)
                    Velocity.Y = 0;
                    isMoving = false;
                }
            }
            if (isMoving == false)
            {

                if ((gameTime.TotalGameTime.Seconds + (gameTime.TotalGameTime.Minutes * 60)) - GameTimer >= Duration)
                {
                    if (Position.X > 400)
                    {

                        Velocity.X = 1;
                    }
                    else if (Position.X < 80)
                        Velocity.X = -1;

                    else if (Position.Y < 50)
                        Velocity.Y = -1;
                }
            }

#endif
#if ZUNE
            if (isMoving)
            {
                if (Position.Y < 80)
                {
                    if (Position.Y == 0)
                    {
                        Velocity.Y = 0;
                        isMoving = false;

                    }
                }
                else if (Position.Y > 400)
                {


                    if (Position.Y == 442)
                    {
                        Velocity.Y = 0;
                        isMoving = false;

                    }
                }
                else if (Position.X < 80)
                {
                    if (Position.X == 0)
                        Velocity.X = 0;
                    isMoving = false;
                }
            }
            if (isMoving == false)
            {

                if ((gameTime.TotalGameTime.Seconds + (gameTime.TotalGameTime.Minutes * 60)) - GameTimer >= Duration)
                {
                    if (Position.Y > 400)
                    {

                        Velocity.Y = 1;
                    }
                    else if (Position.Y < 80)
                        Velocity.Y = -1;

                    else if (Position.X < 50)
                        Velocity.X = -1;
                }
            }
#endif


            Position += Velocity;
        }

        public void Checkbounds()
        {
#if WINDOWS
            if (Position.X >= 512)
            {
                Position.X = 510;
                Velocity.X = 0;
            }
            else if (Position.X < -42)
            {
                Position.X = -40;
                Velocity.X = 0;
            }
            else if (Position.Y < -42)
            {
                Position.Y = -40;
                Velocity.Y = 0;
            }
        
#endif
#if ZUNE
            if (Position.Y >= 512)
            {
                Position.Y = 510;
                Velocity.Y = 0;
            }
            else if (Position.Y < -42)
            {
                Position.Y = -40;
                Velocity.Y = 0;
            }
            else if (Position.X < -42)
            {
                Position.X = -40;
                Velocity.X = 0;
            }
#endif
        }



        //end of class

    }

}
