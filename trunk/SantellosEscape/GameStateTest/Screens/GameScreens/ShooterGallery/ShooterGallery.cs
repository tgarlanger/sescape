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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ShooterGallery : GameScreen
    {

        private Texture2D m_ReticuleTex;
        MouseState mstate;
        Rectangle_addition rec1 = new Rectangle_addition();
        private Texture2D TableTex;
        List<Computer2> compPos;
        List<Computer2> ZcompPos;
        float Score = 0.0f;
        float timer;
        private string PlayState;
        private int arrowFrame;
        private int gameBegin;
        private bool firstRun;
        private Texture2D BackArrow;
        Texture2D[] comp1 = new Texture2D[6];
        Texture2D[] compz = new Texture2D[6];
        Texture2D monitor;
        Texture2D Windows;
        Texture2D Gun;
        Texture2D DoubleScore1;
        Texture2D Menu;
        Texture2D ZMenu;
        Random randComp = new Random();
        Random randX = new Random();
        Random randY = new Random();
        Vector2[] DefaultPlace = new Vector2[6];
        int CompNumber;
        SpriteFont FinalScore;
        bool UPDATE;
        bool DoubleScore = true;
        bool Fail;
        // bool Called;
        int iFrame;
        SoundEffect soundEffect;



        Texture2D m_texBackground;

        public Rectangle BoundingBoxGet(Vector2 Position)
        {
            return new Rectangle((int)Position.X, (int)Position.Y, 30, 30);
        }


        public Rectangle MouseRec
        {
            get { return new Rectangle(mstate.X, mstate.Y, 30, 30); }
        }




        public ShooterGallery()
        {
#if WINDOWS
            compPos = new List<Computer2>();
            for (int i = 0; i < 6; i++)
            {
                compPos.Add(new Computer2());
            }
            PlayState = "Menu";
           


            //compPos[0].Position = new Vector2(-30, 200);
            // compPos[1].Position = new Vector2(-30, 300);
            // compPos[2].Position = new Vector2(510, 200);
            // compPos[3].Position = new Vector2(510, 300);


            compPos[0].Position = new Vector2(-40, 35);
            compPos[1].Position = new Vector2(-40, 105);
            compPos[2].Position = new Vector2(510, 40);
            compPos[3].Position = new Vector2(510, 115);
            compPos[4].Position = new Vector2(250, -40);
            compPos[5].Position = new Vector2(150, -40);

           



            DefaultPlace[0] = new Vector2(-40, 35);
            DefaultPlace[1] = new Vector2(-40, 105);
            DefaultPlace[2] = new Vector2(510, 40);
            DefaultPlace[3] = new Vector2(510, 115);
            DefaultPlace[4] = new Vector2(250, -40);
            DefaultPlace[5] = new Vector2(150, -40);






            ScreenOrientation = ScreenOrientation.Landscape;
#endif
#if ZUNE
            compPos = new List<Computer2>();
            for (int i = 0; i < 4; i++)
            {
                compPos.Add(new Computer2());
            }



            //compPos[0].Position = new Vector2(200, -30);
            // compPos[1].Position = new Vector2(300, -30);
            // compPos[2].Position = new Vector2(200, 510);
            // compPos[3].Position = new Vector2(300, 510);


            compPos[0].Position = new Vector2(115, -40);
            compPos[1].Position = new Vector2(210, -40);
            compPos[2].Position = new Vector2(115, 510);
            compPos[3].Position = new Vector2(210, 510);





            DefaultPlace[0] = new Vector2(115, -40);
            DefaultPlace[1] = new Vector2(210, -40);
            DefaultPlace[2] = new Vector2(115, 510);
            DefaultPlace[3] = new Vector2(210, 510);





            ScreenOrientation = ScreenOrientation.Portrait;
#endif
        }
        // set the height and width of the monitor

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        //public override void LoadContent()
        public override void LoadContent(ContentManager Content, SpriteBatch sprBatch)
        {

            m_sprBatch = sprBatch;
            FinalScore = Content.Load<SpriteFont>("ShooterGallery/Font/SpriteFont1");
            BackArrow = Content.Load<Texture2D>("Falldown/Textures/arrow");
            firstRun = true;
            PlayState = "Menu";
            //m_sprBatch.GraphicsDevice.Viewport.Height = 272;
            //m_sprBatch.GraphicsDevice.Viewport.Width = 480;

            //rec1.LoadContent();
            // TODO: use this.Content to load your game content here
#if WINDOWS
            m_ReticuleTex = Content.Load<Texture2D>("ShooterGallery/Items/reticule");
            TableTex = Content.Load<Texture2D>("ShooterGallery/Items/Tablebar");
            comp1[0] = Content.Load<Texture2D>("ShooterGallery/Items/computer1");
            comp1[1] = Content.Load<Texture2D>("ShooterGallery/Items/computer1");
            comp1[2] = Content.Load<Texture2D>("ShooterGallery/Items/computer");
            comp1[3] = Content.Load<Texture2D>("ShooterGallery/Items/computer");
            comp1[4] = Content.Load<Texture2D>("ShooterGallery/Items/Zcomputer1");
            comp1[5] = Content.Load<Texture2D>("ShooterGallery/Items/Zcomputer1");
            monitor = Content.Load<Texture2D>("ShooterGallery/Items/Monitor");
            Windows = Content.Load<Texture2D>("ShooterGallery/Items/Windows");
            Gun = Content.Load<Texture2D>("ShooterGallery/Items/gun");
            Menu = Content.Load<Texture2D>("ShooterGallery/Items/Menu");
            soundEffect = Content.Load<SoundEffect>("ShooterGallery/Items/Gun1");
            m_texBackground = Content.Load<Texture2D>("ShooterGallery/background");
#endif
#if ZUNE
            m_ReticuleTex = Content.Load<Texture2D>("ShooterGallery/Items/reticule");
            //TableTex = Content.Load<Texture2D>("ShooterGallery/Items/Tablebar");
            comp1[0] = Content.Load<Texture2D>("ShooterGallery/Items/Zcomputer1");
            comp1[1] = Content.Load<Texture2D>("ShooterGallery/Items/Zcomputer1");
            comp1[2] = Content.Load<Texture2D>("ShooterGallery/Items/Zcomputer");
            comp1[3] = Content.Load<Texture2D>("ShooterGallery/Items/Zcomputer");
            monitor = Content.Load<Texture2D>("ShooterGallery/Items/ZMonitor");
            Windows = Content.Load<Texture2D>("ShooterGallery/Items/ZWindows");
            Gun = Content.Load<Texture2D>("ShooterGallery/Items/Zgun");
            ZMenu = Content.Load<Texture2D>("ShooterGallery/Items/ZMenu");
            soundEffect = Content.Load<SoundEffect>("ShooterGallery/Items/Gun1");
            m_texBackground = Content.Load<Texture2D>("ShooterGallery/background");
#endif

            base.LoadContent(Content, m_sprBatch);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            timer = 59;
            if (gameTime.TotalRealTime.Seconds - timer == 0)
            {
                PlayState = "Menu";
                firstRun = true;
            }
            if (PlayState == "Game")
            {
                iFrame = 0;
                if ((gameTime.ElapsedRealTime.TotalSeconds - timer) > 60) Fail = true;

                if (UPDATE == false)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        compPos[i].Move(gameTime);
                        compPos[i].Checkbounds();

                    }

                    if (gameTime.TotalGameTime.Milliseconds % 1000 == 0)
                    {
                        CompNumber = randComp.Next(4);
                        if (randComp.Equals(5))
                        {

                        }

                        compPos[CompNumber].Random(gameTime);
                    }


                    mstate = Mouse.GetState();
                }


#if WINDOWS
                    if (mstate.LeftButton == ButtonState.Pressed)
                    {
                        {
                            soundEffect.Play();
                            iFrame++;
                            for (int i = 0; i <= 3; i++)
                            {
                                if (MouseRec.Intersects(BoundingBoxGet(compPos[i].Position)))
                                {

                                    compPos[i].Position = DefaultPlace[i];
                                    compPos[i].Velocity.X = 0;
                                    Score += 50;

                                    if (DoubleScore)
                                    {
                                        Score += 50;
                                    }
                                    if (Score == 2500)
                                    {

                                        PlayState = "Menu";
                                        firstRun = false;
                                    }

                                }
                            }

                        }
                    }    
#endif
#if ZUNE
                TouchCollection collection = TouchPanel.GetState();

                if (collection.Count == 1)
                {
                    Rectangle touchRec = new Rectangle((int)collection[0].Position.X, (int)collection[0].Position.Y, 10, 10);
                    
                    if (collection[0].State == TouchLocationState.Pressed)
                    {

                        soundEffect.Play();
                        iFrame++;
                        for (int i = 0; i <= 3; i++)
                        {
                            if (touchRec.Intersects(BoundingBoxGet(compPos[i].Position)))
                            {

                                compPos[i].Position = DefaultPlace[i];
                                compPos[i].Velocity.X = 0;
                                Score += 50;

                                if (DoubleScore)
                                {
                                    Score += 50;
                                }
                                if (Score == 2500)
                                {
                                    UPDATE = true;
                                    PlayState = "Menu";
                                    firstRun = true;
                                }

                            }
                        }
                    }
                }
#endif
            }


#if WINDOWS
            else if (PlayState == "Menu")
            {
                Rectangle mouseRec = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
                if (mouseRec.Intersects(new Rectangle(0, 272 - 50, 50, 50)))
                {
                    arrowFrame = 1;
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        MediaPlayer.Stop();
                        ScreenState = ScreenState.Hidden;
                    }
                }
                else
                    arrowFrame = 0;

                if (Mouse.GetState().LeftButton == ButtonState.Pressed && !firstRun)
                {
                    gameBegin = gameTime.TotalRealTime.Seconds + (gameTime.TotalRealTime.Minutes * 60);
                    resetGame();

                }
                //Ensure game doesn't start until player sees the menu
                if (Mouse.GetState().LeftButton == ButtonState.Released)
                    firstRun = false;

            }
#else
            else if (PlayState == "Menu")
            {
                TouchCollection touchCollection = TouchPanel.GetState();
                if (touchCollection.Count > 0)
                {
                    Rectangle touchRec = new Rectangle((int)touchCollection[0].Position.X, (int)touchCollection[0].Position.Y, 1, 1);

                    if (touchRec.Intersects(new Rectangle(0, 272 - 50, 50, 50)))
                    {
                        arrowFrame = 1;
                        if (touchCollection[0].State == TouchLocationState.Pressed)
                        {
                            MediaPlayer.Stop();
                            ScreenState = ScreenState.Hidden;
                        }
                    }
                }
                else
                    arrowFrame = 0;
                if (touchCollection.Count > 0)
                {
                    if (touchCollection[0].State == TouchLocationState.Pressed && firstRun)
                    {
                        gameBegin = gameTime.TotalRealTime.Seconds + (gameTime.TotalRealTime.Minutes * 60);
                        resetGame();

                    }
                    //Ensure game doesn't start until player sees the menu
                    if (touchCollection[0].State == TouchLocationState.Released)
                        firstRun = false;
                }
            }
#endif



            //if (Mouse.GetState().LeftButton == ButtonState.Released)
            // firstRun = false;

            // TODO: Add your update logic here

            base.Update(gameTime);

        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {


            m_sprBatch.Begin();
#if WINDOWS
            if (PlayState == "Game")
            {
                m_sprBatch.Draw(m_texBackground, new Vector2(0, 0), Color.White);

                m_sprBatch.Draw(TableTex, new Vector2(0, 200), Color.White);
                m_sprBatch.Draw(TableTex, new Vector2(50, 200), Color.White);
                m_sprBatch.Draw(TableTex, new Vector2(0, 220), Color.White);
                m_sprBatch.Draw(TableTex, new Vector2(50, 220), Color.White);
                m_sprBatch.Draw(Windows, new Vector2(0, 0), Color.White);

                m_sprBatch.Draw(comp1[0], compPos[0].Position, Color.White);
                m_sprBatch.Draw(comp1[1], compPos[1].Position, Color.White);
                m_sprBatch.Draw(comp1[2], compPos[2].Position, Color.White);
                m_sprBatch.Draw(comp1[3], compPos[3].Position, Color.White);
                m_sprBatch.Draw(comp1[4], compPos[4].Position, Color.White);
                m_sprBatch.Draw(comp1[5], compPos[5].Position, Color.White);

                m_sprBatch.Draw(monitor, new Vector2(0, 0), Color.White);

                m_sprBatch.Draw(Gun, new Vector2(200, 170), new Rectangle(75 * iFrame, 0, Gun.Width / 2, Gun.Height),
                    Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);

                m_sprBatch.DrawString(FinalScore, Score.ToString(), new Vector2(445, 253), Color.Gold);




                m_sprBatch.Draw(m_ReticuleTex, new Vector2(mstate.X, mstate.Y), Color.White);
                //rec1.Draw(m_sprBatch);
            }
#endif
#if ZUNE
            if(PlayState == "Game")
            {
            m_sprBatch.Draw(m_texBackground, new Vector2(0, 0), Color.White);

           
            m_sprBatch.Draw(Windows, new Vector2(0, 0), Color.White);

            m_sprBatch.Draw(comp1[0], compPos[0].Position, Color.White);
            m_sprBatch.Draw(comp1[1], compPos[1].Position, Color.White);
            m_sprBatch.Draw(comp1[2], compPos[2].Position, Color.White);
            m_sprBatch.Draw(comp1[3], compPos[3].Position, Color.White);

            m_sprBatch.Draw(monitor, new Vector2(0, 0), Color.White);

            m_sprBatch.Draw(Gun, new Vector2(0, 200), new Rectangle(0, 75 * iFrame, Gun.Width, Gun.Height / 2),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);

            m_sprBatch.DrawString(FinalScore, Score.ToString(), new Vector2(50,50), Color.Gold,1.57f, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);

            if (UPDATE)
            {
                m_sprBatch.DrawString(FinalScore, "You Win", new Vector2(240, 141), Color.Gold);
            }
        }

#endif
            if (PlayState == "Menu")
            {
                DrawMenu();
#if WINDOWS
                m_sprBatch.Draw(m_ReticuleTex, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
#endif

            }
            // TODO: Add your drawing code here
            m_sprBatch.End();


            base.Draw(gameTime);
        }
        private void DrawMenu()
        {
            // m_sprBatch.DrawString(FinalScore, "High Score: " + HighScore.ToString(), new Vector2(70, 375), Color.DarkRed);
#if WINDOWS
            m_sprBatch.Draw(Menu, Vector2.Zero, Color.White);
#else
            m_sprBatch.Draw(ZMenu, Vector2.Zero, Color.White);
#endif
            m_sprBatch.Draw(BackArrow, new Rectangle(50, 50, 50, 50), new Rectangle(50 * arrowFrame, 0, 50, 50), Color.White,1.57f, Vector2.Zero, SpriteEffects.None, 1);
            //m_sprBatch.Draw(, new Vector2(0, 0), Color.White);
        }
        private void resetGame()
        {
            timer = 0;
            Score = 0;
            PlayState = "Game";
            firstRun = true;
        }
    }
}