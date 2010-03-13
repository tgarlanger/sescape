using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameStateTest.Screens
{
    class MenuScreen : Screen
    {
        private List<string> m_strMenuItems;

        public List<string> MenuItems
        {
            get
            {
                return m_strMenuItems;
            }
            set
            {
                m_strMenuItems = value;
            }
        }

        private Texture2D m_texImage;

        private Vector2 m_vecTextPosition;

        private string m_strFilePath;

        private string m_strFontPath;

        private SpriteFont m_sprFont;

        private Texture2D m_texPointer;

        private Rectangle m_recMouse;

        private List<Rectangle> m_lstMenuButtons;

        private int m_iSelectedIndex;

        public int SelectedIndex;

        private Texture2D m_texSelection;

        public MenuScreen(List<string> strMenuItems, string strFilePath, string strFontPath, Vector2 vecPosition )
        {
            m_strMenuItems = strMenuItems;
            m_strFilePath = strFilePath;
            m_vecTextPosition = vecPosition;
            m_strFontPath = strFontPath;

            m_iSelectedIndex = -1;

            m_lstMenuButtons = new List<Rectangle>();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            m_sprBatch.Begin();

            m_sprBatch.Draw(m_texImage, new Vector2(0, 0), Color.White);

            if (m_iSelectedIndex > -1)
            {
                m_sprBatch.Draw(m_texSelection, m_lstMenuButtons[m_iSelectedIndex], Color.White);
            }

            for ( int index = 0; index < m_strMenuItems.Count; index++ )
            {
                m_sprBatch.DrawString(m_sprFont, m_strMenuItems[index], m_vecTextPosition+new Vector2(5,m_sprFont.LineSpacing*index), Color.Black);
            }

            m_sprBatch.Draw(m_texPointer, new Vector2(m_recMouse.X,m_recMouse.Y), Color.White);

            m_sprBatch.End();

            base.Draw(gameTime);
        }

        public override void LoadContent(ContentManager Content, SpriteBatch sprBatch)
        {
            m_sprBatch = sprBatch;

            m_texImage = Content.Load<Texture2D>(m_strFilePath);
            m_sprFont = Content.Load<SpriteFont>(m_strFontPath);

            m_texPointer = Content.Load<Texture2D>("GameState/Graphics/Menu/MousePointer");
            m_texSelection = Content.Load<Texture2D>("GameState/Graphics/Menu/MenuSelection");

            for (int index = 0; index < m_strMenuItems.Count; index++)
            {
                m_lstMenuButtons.Add(new Rectangle(0, 
                    Convert.ToInt16(m_vecTextPosition.Y + (m_sprFont.LineSpacing * index)),
                    Convert.ToInt16(m_sprFont.MeasureString(m_strMenuItems[index]).X),
                    Convert.ToInt16(m_sprFont.MeasureString(m_strMenuItems[index]).Y)));
            }

 	        base.LoadContent(Content,m_sprBatch);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mstate = Mouse.GetState();

            m_recMouse = new Rectangle(mstate.X, mstate.Y, m_texPointer.Width, m_texPointer.Height);

            if (mstate.LeftButton == ButtonState.Pressed)
            {
                foreach (Rectangle rec in m_lstMenuButtons)
                {
                    if (rec.Intersects(m_recMouse))
                    {
                        m_iSelectedIndex = Convert.ToInt16((m_recMouse.Y - m_vecTextPosition.Y) / m_sprFont.LineSpacing);
                    }
                }
            }
            else
            {
                m_iSelectedIndex = -1;
            }

            base.Update(gameTime);
        }
    }
}
