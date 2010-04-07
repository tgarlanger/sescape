using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SantellosEscape.Screens.GameScreens.Avoider
{
    class AnimationControl
    {
        private int m_iFrame;

        public int Frame
        {
            get
            {
                return m_iFrame;
            }
        }

        private int m_iMaxFrames;

        public int MaxFrames
        {
            get
            {
                return m_iMaxFrames;
            }
            set
            {
                m_iMaxFrames = value;
            }
        }

        private int m_iFrameRate;

        private int m_iTicks;

        private int m_iFrameHeight;

        public int FrameHeight
        {
            get
            {
                return m_iFrameHeight;
            }
            set
            {
                m_iFrameHeight = value;
            }
        }

        public AnimationControl()
        {

            m_iFrame = 0;
            m_iTicks = 0;
            m_iFrameRate = 10;
            m_iMaxFrames = 1;
        }

        public void Update()
        {
            m_iTicks++;

            if (m_iTicks >= m_iFrameRate)
            {
                m_iFrame++;

                if (m_iFrame >= m_iMaxFrames)
                {
                    m_iFrame = 0;
                }

                m_iTicks = 0;
            }
        }
    }
}
