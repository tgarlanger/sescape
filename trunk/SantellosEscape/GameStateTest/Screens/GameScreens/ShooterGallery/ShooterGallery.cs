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
        Vector2[] compPos = new Vector2[4];
        Vector2 Velocity;
        Texture2D[] comp1 = new Texture2D[4];
        Random randComp = new Random();
        int CompNumber;

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
            compPos[0] = new Vector2(175, 200);
            compPos[1] = new Vector2(25, 200);
            compPos[2] = new Vector2(345, 200);
            compPos[3] = new Vector2(450, 200);

            Velocity.X = 0;
            Velocity.Y = -1.0f;

            ScreenOrientation = ScreenOrientation.Landscape;
        }
        // set the height and width of the monitor

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        //public override void LoadContent()
        public override void  LoadContent(ContentManager Content, SpriteBatch sprBatch)
        {
            m_sprBatch = sprBatch;

            //m_sprBatch.GraphicsDevice.Viewport.Height = 272;
            //m_sprBatch.GraphicsDevice.Viewport.Width = 480;

            //rec1.LoadContent();
            // TODO: use this.Content to load your game content here

            m_ReticuleTex = Content.Load<Texture2D>("Items/reticule");
            TableTex = Content.Load<Texture2D>("Items/Tablebar");
            comp1[0] = Content.Load<Texture2D>("Items/computer");
            comp1[1] = Content.Load<Texture2D>("Items/computer");
            comp1[2] = Content.Load<Texture2D>("Items/computer");
            comp1[3] = Content.Load<Texture2D>("Items/computer");

            m_texBackground = Content.Load<Texture2D>("ShooterGallery/background");
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
            if (gameTime.TotalRealTime.Milliseconds % 100 == 0)
            {
                CompNumber = randComp.Next(3);

                compPos[CompNumber].Y = 150;
            }

            mstate = Mouse.GetState();


            if (mstate.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (MouseRec.Intersects(BoundingBoxGet(compPos[i])))
                        compPos[i].Y = 210;
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

            m_sprBatch.Draw(comp1[0], compPos[0], Color.White);
            m_sprBatch.Draw(comp1[1], compPos[1], Color.White);
            m_sprBatch.Draw(comp1[2], compPos[2], Color.White);
            m_sprBatch.Draw(comp1[3], compPos[3], Color.White);

            m_sprBatch.Draw(m_ReticuleTex, new Vector2(mstate.X, mstate.Y), Color.White);
            //rec1.Draw(m_sprBatch);


            // TODO: Add your drawing code here
            m_sprBatch.End();

            base.Draw(gameTime);
        }
    }
}