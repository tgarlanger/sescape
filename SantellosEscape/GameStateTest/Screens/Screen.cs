using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameStateTest.Screens
{
    public enum ScreenState
    {
        Hidden = 0, Active
    }

    public enum ScreenType
    {
        Menu, Credits, Game
    }

    public abstract class Screen
    {
        private ScreenState m_screenState;

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

        private ScreenType m_screenType;

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

        private string m_strName;

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

        protected SpriteBatch m_sprBatch;

        public Screen()
        {
        }

        public virtual void Initialize()
        {
        }

        public virtual void LoadContent(ContentManager Content, SpriteBatch sprBatch)
        {
        }

        public virtual void  UnloadContent()
        {
        }

        public virtual void  Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }

        public virtual void  Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }
    }
}
