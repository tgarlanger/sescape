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
    /// <summary>
    /// A MenuScreen Class derived from the base Screen class
    /// </summary>
    /// <seealso cref="Screen"/>
    class MenuScreen : Screen
    {
        /// <summary>
        /// A list of the individual items in the menu
        /// </summary>
        private List<string> m_strMenuItems;

        /// <summary>
        /// Gets or sets the menu items.
        /// </summary>
        /// <value>The menu items.</value>
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

        /// <summary>
        /// The Image to be shown in the background of the menu.
        /// </summary>
        private Texture2D m_texImage;

        /// <summary>
        /// The position to start drawing the menu items.
        /// </summary>
        private Vector2 m_vecTextPosition;

        /// <summary>
        /// Path to the image.
        /// </summary>
        private string m_strFilePath;

        /// <summary>
        /// Path to the SpriteFont.
        /// </summary>
        private string m_strFontPath;

        /// <summary>
        /// SpriteFont to use for the menu items.
        /// </summary>
        private SpriteFont m_sprFont;

        /// <summary>
        /// Texture to draw as the mouse pointer.
        /// </summary>
        private Texture2D m_texPointer;

        /// <summary>
        /// The rectangle of the mouse pointer.
        /// 
        /// Used for buttons
        /// </summary>
        private Rectangle m_recMouse;

        /// <summary>
        /// A list of all the button Rectangles.
        /// </summary>
        private List<Rectangle> m_lstMenuButtons;

        /// <summary>
        /// The index of the selected menu item.
        /// </summary>
        private int m_iSelectedIndex;

        /// <summary>
        /// Gets the index of the selected menu item.
        /// </summary>
        /// <value>The index of the selected menu item.</value>
        public int SelectedIndex
        {
            get
            {
                return m_iSelectedIndex;
            }
        }

        /// <summary>
        /// A texture to draw behind the selected menu item.
        /// </summary>
        private Texture2D m_texSelection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuScreen"/> class.
        /// </summary>
        /// <param name="strMenuItems">The list of menu items.</param>
        /// <param name="strFilePath">The file path of the menu image.</param>
        /// <param name="strFontPath">The file path of the SpriteFont.</param>
        /// <param name="vecPosition">The position to start drawing the menu items.</param>
        public MenuScreen(List<string> strMenuItems, string strFilePath, string strFontPath, Vector2 vecPosition )
        {
            ScreenType = ScreenType.Menu;
            m_strMenuItems = strMenuItems;
            m_strFilePath = strFilePath;
            m_vecTextPosition = vecPosition;
            m_strFontPath = strFontPath;

            m_iSelectedIndex = -1;

            m_lstMenuButtons = new List<Rectangle>();
        }

        /// <summary>
        /// Draws the instance in respect to the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            m_sprBatch.Begin();

            m_sprBatch.Draw(m_texImage, new Vector2(0, 0), Color.White);

            if (m_iSelectedIndex != -1)
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

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="Content">The content to load from.</param>
        /// <param name="sprBatch">The SpriteBatch to draw to.</param>
        public override void LoadContent(ContentManager Content, SpriteBatch sprBatch)
        {
            m_sprBatch = sprBatch;

            m_texImage = Content.Load<Texture2D>(m_strFilePath);
            m_sprFont = Content.Load<SpriteFont>(m_strFontPath);

            m_texPointer = Content.Load<Texture2D>("GameState/Graphics/Menu/MousePointer");
            m_texSelection = Content.Load<Texture2D>("GameState/Graphics/Menu/MenuSelection");

            for (int index = 0; index < m_strMenuItems.Count; index++)
            {
                m_lstMenuButtons.Add(new Rectangle(5, 
                    Convert.ToInt16(m_vecTextPosition.Y + (m_sprFont.LineSpacing * index)),
                    Convert.ToInt16(m_sprFont.MeasureString(m_strMenuItems[index]).X),
                    Convert.ToInt16(m_sprFont.MeasureString(m_strMenuItems[index]).Y)));
            }

 	        base.LoadContent(Content,m_sprBatch);
        }

        /// <summary>
        /// Updates the instance in respect to the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
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
