using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SantellosEscape.Screens.GameScreens.Avoider
{
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

        public Player()
        {
            m_texImage = null;
            m_vecPosition = Vector2.Zero;
            m_bAlive = true;
        }

        public Player(Vector2 vecPosition, Texture2D texImage)
        {
            m_texImage = texImage;
            m_vecPosition = vecPosition;
            m_bAlive = true;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sprBatch)
        {
            base.Draw(sprBatch);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Left) && m_vecPosition.X > 2)
            {
                m_vecVelocity = new Vector2(-5, 0);
            }
            else if (keyState.IsKeyDown(Keys.Right) && m_vecPosition.X < (272-2-m_texImage.Width))
            {
                m_vecVelocity = new Vector2(5, 0);
            }
            else
            {
                m_vecVelocity = Vector2.Zero;
            }

            base.Update(gameTime);
        }

        public void Kill()
        {
            m_bAlive = false;
        }
    }
}
