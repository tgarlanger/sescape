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
        float Score = 0.0f;
        Texture2D[] comp1 = new Texture2D[4];
        Texture2D monitor;
        Texture2D Windows;
        Texture2D Gun;
        Random randComp = new Random();
        Random randX = new Random();
        Random randY = new Random();
        Vector2[] DefaultPlace = new Vector2[4];
        int CompNumber;
        SpriteFont FinalScore;
        bool UPDATE;
        bool DoubleScore = true;
        bool Fail;
        bool Called;
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
            compPos = new List<Computer2>();
            for (int i = 0; i < 4; i++)
            {
                compPos.Add(new Computer2());
            }


            //compPos[0].Position = new Vector2(-30, 200);
            // compPos[1].Position = new Vector2(-30, 300);
            // compPos[2].Position = new Vector2(510, 200);
            // compPos[3].Position = new Vector2(510, 300);


            compPos[0].Position = new Vector2(-40, 35);
            compPos[1].Position = new Vector2(-40, 105);
            compPos[2].Position = new Vector2(510, 40);
            compPos[3].Position = new Vector2(510, 115);



            DefaultPlace[0] = new Vector2(-40, 35);
            DefaultPlace[1] = new Vector2(-40, 105);
            DefaultPlace[2] = new Vector2(510, 40);
            DefaultPlace[3] = new Vector2(510, 115);





            ScreenOrientation = ScreenOrientation.Landscape;
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
            //m_sprBatch.GraphicsDevice.Viewport.Height = 272;
            //m_sprBatch.GraphicsDevice.Viewport.Width = 480;

            //rec1.LoadContent();
            // TODO: use this.Content to load your game content here

            m_ReticuleTex = Content.Load<Texture2D>("ShooterGallery/Items/reticule");
            TableTex = Content.Load<Texture2D>("ShooterGallery/Items/Tablebar");
            comp1[0] = Content.Load<Texture2D>("ShooterGallery/Items/computer1");
            comp1[1] = Content.Load<Texture2D>("ShooterGallery/Items/computer1");
            comp1[2] = Content.Load<Texture2D>("ShooterGallery/Items/computer");
            comp1[3] = Content.Load<Texture2D>("ShooterGallery/Items/computer");
            monitor = Content.Load<Texture2D>("ShooterGallery/Items/Monitor");
            Windows = Content.Load<Texture2D>("ShooterGallery/Items/Windows");
            Gun = Content.Load<Texture2D>("ShooterGallery/Items/gun");
            soundEffect = Content.Load<SoundEffect>("ShooterGallery/Items/Gun1");
            m_texBackground = Content.Load<Texture2D>("ShooterGallery/background");

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
            iFrame = 0;
            if (gameTime.ElapsedRealTime.TotalSeconds == 60) Fail = true;
            if (Fail == false)
            {
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

                        compPos[CompNumber].Random(gameTime);
                    }


                    mstate = Mouse.GetState();
                }


                if (mstate.LeftButton == ButtonState.Pressed)
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
                            if (Score == 5000)
                                UPDATE = true;

                        }
                    }

                }
            }
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

            m_sprBatch.Draw(monitor, new Vector2(0, 0), Color.White);

            m_sprBatch.Draw(Gun, new Vector2(200, 170), new Rectangle(75 * iFrame, 0, Gun.Width / 2, Gun.Height),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);

            m_sprBatch.DrawString(FinalScore, Score.ToString(), new Vector2(445, 253), Color.Gold);

            if (UPDATE)
            {
                m_sprBatch.DrawString(FinalScore, "You Win", new Vector2(240, 141), Color.Gold);
            }


            m_sprBatch.Draw(m_ReticuleTex, new Vector2(mstate.X, mstate.Y), Color.White);
            //rec1.Draw(m_sprBatch);


            // TODO: Add your drawing code here
            m_sprBatch.End();

            base.Draw(gameTime);
        }
    }
}