using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace SantellosEscape.Screens
{
    class CreditsScreen : Screen
    {
        private Texture2D Background;
        private Texture2D Cursor;
        private Texture2D Arrow;
       
        private Vector2 ArrowPosition;
        private int arrowFrame;

        private List<string> ListNames;
        private SpriteFont ListFont;
        private Vector2 ListOrigin;
        private int ListOffset;

        public CreditsScreen()
        {
            ScreenType = ScreenType.Credits;
            ScreenOrientation = ScreenOrientation.Portrait;

            arrowFrame = 0;

            ListOrigin = new Vector2(50, 200);
            ListOffset = 30;

            ListNames = new List<string>();
            ListNames.Add("Anthony Garlanger");
            ListNames.Add("Pat Ostler");
            ListNames.Add("Jacob Pelka");
            ListNames.Add("David Roszkowski");
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content, Microsoft.Xna.Framework.Graphics.SpriteBatch sprBatch)
        {
            m_sprBatch = sprBatch;

            Background = Content.Load<Texture2D>("GameState/Graphics/Credits/creditScreen");
            Cursor = Content.Load<Texture2D>("FallDown/Textures/cursor");
            Arrow = Content.Load<Texture2D>("FallDown/Textures/arrow");
            ListFont = Content.Load<SpriteFont>("FallDown/Textures/ScoreFont");

            ArrowPosition = new Vector2(0, 480 - 50);

            base.LoadContent(Content, sprBatch);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            m_sprBatch.Begin();

            m_sprBatch.Draw(Background, Vector2.Zero, Color.White);
            for (int i = 0; i < ListNames.Count; i++)
            {
                m_sprBatch.DrawString(ListFont, ListNames[i], new Vector2(ListOrigin.X, ListOrigin.Y + (ListOffset * i)), Color.DarkRed);
            }
            m_sprBatch.DrawString(ListFont, "Additional Textures \nby Professor Santello", new Vector2(ListOrigin.X, ListOrigin.Y + (ListOffset * (ListNames.Count +1))), Color.DarkRed);
            m_sprBatch.Draw(Arrow, new Rectangle((int)ArrowPosition.X, (int)ArrowPosition.Y, 50, 50), new Rectangle((50 * arrowFrame), 0, 50, 50), Color.White);
            m_sprBatch.Draw(Cursor, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);

            m_sprBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
#if ZUNE
            TouchCollection collection = TouchPanel.GetState();
            if (collection.Count == 1)
            {
                if (new Rectangle((int)collection[0].Position.X, (int)collection[0].Position.Y, 1, 1).Intersects(new Rectangle(0, 480 - 50, 50, 50)))
                {
                    arrowFrame = 0;
                    if (collection[0].State == TouchLocationState.Moved || collection[0].State == TouchLocationState.Pressed)
                        arrowFrame = 1;
                    else if (collection[0].State == TouchLocationState.Released)
                    {
                        ScreenState = ScreenState.Hidden;
                    }
                }
                else
                    arrowFrame = 0;
            }
#else
            Rectangle mouseRec = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
            if (mouseRec.Intersects(new Rectangle((int)ArrowPosition.X, (int)ArrowPosition.Y, 50, 50)))
            {   
                arrowFrame = 1;
                if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                    ScreenState = ScreenState.Hidden;
            }
            else
                arrowFrame = 0;
#endif
            base.Update(gameTime);
        }

       
    }
}
