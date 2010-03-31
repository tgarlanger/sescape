using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SantellosEscape.Screens.GameScreens.Avoider
{
    public enum PlayerDirection
    {
        Right, Left
    }

    class Player : GameObject
    {
        private bool m_bAlive;

        public bool Alive
        {
            get
            {
                return m_bAlive;
            }
        }

        private PlayerDirection m_plyrDirection;

        public PlayerDirection PlayerDirection
        {
            get
            {
                return m_plyrDirection;
            }
            set
            {
                m_plyrDirection = value;
            }
        }

        private int m_iFrame;

        private int m_iMaxFrames;

        private int m_iFrameRate;

        private int m_iTicks;

        public Player()
        {
            m_texImage = null;
            m_vecPosition = Vector2.Zero;
            m_bAlive = true;
            m_iFrame = 0;
            m_iMaxFrames = 2;
            m_plyrDirection = PlayerDirection.Right;
            m_iFrameRate = 10;
            m_iTicks = 0;
        }

        public Player(Vector2 vecPosition, Texture2D texImage)
        {
            m_texImage = texImage;
            m_vecPosition = vecPosition;
            m_bAlive = true;
            m_iFrame = 0;
            m_iMaxFrames = 2;
            m_plyrDirection = PlayerDirection.Right;
            m_iFrameRate = 10;
            m_iTicks = 0;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sprBatch)
        {
            SpriteEffects sprEffect = SpriteEffects.None;
            if (m_plyrDirection == PlayerDirection.Left)
            {
                sprEffect = SpriteEffects.FlipHorizontally;

            }
            sprBatch.Draw(m_texImage, m_vecPosition,
                new Rectangle(20 * m_iFrame, 0, m_texImage.Width / m_iMaxFrames, m_texImage.Height),
                Color.White, 0, Vector2.Zero, 1, sprEffect, 1);
        }

        public override void Update(GameTime gameTime)
        {
            m_iTicks++;

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Left) && m_vecPosition.X > 2)
            {
                m_vecVelocity = new Vector2(-5, 0);
                m_plyrDirection = PlayerDirection.Left;
            }
            else if (keyState.IsKeyDown(Keys.Right) && m_vecPosition.X < (272 - 2 - (m_texImage.Width / m_iMaxFrames)))
            {
                m_vecVelocity = new Vector2(5, 0);
                m_plyrDirection = PlayerDirection.Right;
            }
            else
            {
                m_vecVelocity = Vector2.Zero;
            }

            if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.Left))
            {
                if (m_iTicks > m_iFrameRate)
                {
                    m_iFrame++;
                    if (m_iFrame >= m_iMaxFrames)
                    {
                        m_iFrame = 0;
                    }

                    m_iTicks = 0;
                }
            }

            base.Update(gameTime);
        }

        public void Kill()
        {
            m_bAlive = false;
        }
    }
}