using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace SantellosEscape.Screens
{
    /// <summary>
    /// The possible states of a Screen
    /// </summary>
    /// <seealso cref="Screen"/>
    public enum ScreenState
    {
        Hidden = 0, Active
    }

    /// <summary>
    /// The possible types of a Screen
    /// </summary>
    /// <seealso cref="Screen"/>
    public enum ScreenType
    {
        Menu, Credits, Game
    }

    
    /// <summary>
    /// Possible Screen Orientations
    /// </summary>
    /// <seealso cref="Screen"/>
    public enum ScreenOrientation
    {
        Landscape, Portrait
    }

    /// <summary>
    /// A base Screen class
    /// 
    /// All other Screen types are derived from this class
    /// </summary>
    public abstract class Screen
    {
        /// <summary>
        /// The State of the Screen.
        /// </summary>
        private ScreenState m_screenState;

        /// <summary>
        /// Gets or sets the state of the Screen.
        /// </summary>
        /// <value>The state of the Screen.</value>
        public ScreenState ScreenState
        {
            get
            {
                return m_screenState;
            }
            set
            {
                m_screenState = value;
            }
        }

        /// <summary>
        /// The Type of the Screen.
        /// </summary>
        private ScreenType m_screenType;

        /// <summary>
        /// Gets or sets the type of the Screen
        /// </summary>
        /// <value>The type of the Screen.</value>
        public ScreenType ScreenType
        {
            get
            {
                return m_screenType;
            }
            set
            {
                m_screenType = value;
            }
        }

        /// <summary>
        /// The Orientation of the screen
        /// </summary>
        private ScreenOrientation m_screenOrientation;

        /// <summary>
        /// Gets or sets the screen orientation.
        /// </summary>
        /// <value>The screen orientation.</value>
        public ScreenOrientation ScreenOrientation
        {
            get
            {
                return m_screenOrientation;
            }
            set
            {
                m_screenOrientation = value;
            }
        }

        /// <summary>
        /// The name of the Screen
        /// </summary>
        private string m_strName;

        /// <summary>
        /// Gets or sets the name of the Screen.
        /// </summary>
        /// <value>The name of the Screen.</value>
        public string Name
        {
            get
            {
                return m_strName;
            }
            set
            {
                m_strName = value;
            }
        }

        /// <summary>
        /// The SpriteBatch to draw to
        /// </summary>
        protected SpriteBatch m_sprBatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="Screen"/> class.
        /// </summary>
        public Screen()
        {
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="Content">The content to load from.</param>
        /// <param name="sprBatch">The SpriteBatch to draw to.</param>
        public virtual void LoadContent(ContentManager Content, SpriteBatch sprBatch)
        {
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public virtual void  UnloadContent()
        {
        }

        /// <summary>
        /// Updates the instance in respect to the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void  Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }

        /// <summary>
        /// Draws the instance in respect to the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void  Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }
    }
}
