using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SantellosEscape.Screens.GameScreens.Avoider
{
    /// <summary>
    /// A visible Object in the Avoider Game
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// 2-Dimensional Position of the GameObject.
        /// </summary>
        protected Vector2 m_vecPosition;

        /// <summary>
        /// Visible Texture of the GameObject.
        /// </summary>
        protected Texture2D m_texImage;

        /// <summary>
        /// The Velocity of the GameObject.
        /// </summary>
        protected Vector2 m_vecVelocity;

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        /// <value>The bounds.</value>
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(Convert.ToInt16(m_vecPosition.X), 
                    Convert.ToInt16(m_vecPosition.Y), 
                    m_texImage.Width, 
                    m_texImage.Height);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        public GameObject()
        {
            m_texImage = null;
            m_vecPosition = Vector2.Zero;
            m_vecVelocity = Vector2.Zero;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        /// <param name="texImage">The Texture to use as the image.</param>
        /// <param name="vecPosition">The 2-Dimentional position of the GameObject.</param>
        public GameObject(Texture2D texImage, Vector2 vecPosition, Vector2 vecVelocity)
        {
            m_texImage = texImage;
            m_vecPosition = vecPosition;
            m_vecVelocity = vecVelocity;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
            m_vecPosition += m_vecVelocity;
        }

        /// <summary>
        /// Draws the GameObject to the specified SpriteBatch.
        /// </summary>
        /// <param name="sprBatch">The Sprite Batch to draw to.</param>
        public virtual void Draw(SpriteBatch sprBatch)
        {
            sprBatch.Draw(m_texImage, m_vecPosition, Color.White);
        }
    }
}
