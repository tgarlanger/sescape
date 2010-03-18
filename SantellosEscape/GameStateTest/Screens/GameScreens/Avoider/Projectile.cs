using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SantellosEscape.Screens.GameScreens.Avoider
{
    class Projectile : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Projectile"/> class.
        /// </summary>
        public Projectile()
        {
            m_texImage = null;
            m_vecPosition = Vector2.Zero;
            m_vecVelocity = Vector2.Zero;
        }

        public Projectile(Texture2D texImage, Vector2 vecPosition, Vector2 vecVelocity)
        {
            m_texImage = texImage;
            m_vecPosition = vecPosition;
            m_vecVelocity = vecVelocity;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch sprBatch)
        {
            base.Draw(sprBatch);
        }

        public void CheckCollisions(ref Player player1)
        {
            if (Bounds.Intersects(player1.Bounds))
            {
                player1.Kill();
            }
        }
    }
}
