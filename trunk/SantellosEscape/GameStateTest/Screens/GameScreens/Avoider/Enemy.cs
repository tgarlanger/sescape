using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SantellosEscape.Screens.GameScreens.Avoider
{
    class Enemy : GameObject
    {
        private int m_iShotFrequency;

        private List<Projectile> m_lstProjectiles;

        private List<Texture2D> m_lstProjectileTextures;

        private Random m_rndRand;

        public Enemy()
        {
            m_iShotFrequency = 0;
            m_texImage = null;
            m_vecPosition = Vector2.Zero;
            m_vecVelocity = Vector2.Zero;
            m_lstProjectiles = new List<Projectile>();
            m_lstProjectileTextures = new List<Texture2D>();
            m_rndRand = new Random(5280);
        }

        public Enemy(Texture2D texImage, Vector2 vecPosition, 
            Vector2 vecVelocity, int iShotFrequency, 
            List<Texture2D> lstProjectileTextures)
        {
            m_iShotFrequency = iShotFrequency;
            m_texImage = texImage;
            m_vecPosition = vecPosition;
            m_vecVelocity = vecVelocity;
            m_lstProjectiles = new List<Projectile>();
            m_lstProjectileTextures = lstProjectileTextures;
            m_rndRand = new Random(5280);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (m_vecPosition.Y > (272 - 5 - m_texImage.Width) ||
                m_vecPosition.Y < 5)
            {
                m_vecVelocity.Y *= -1;
            }

            foreach (Projectile pro in m_lstProjectiles)
            {
                pro.Update(gameTime);
            }

            if (gameTime.ElapsedGameTime.Seconds % m_iShotFrequency == 0)
            {
                m_lstProjectiles.Add(new Projectile());
            }
        }

        public override void Draw(SpriteBatch sprBatch)
        {
            base.Draw(sprBatch);

            foreach (Projectile pro in m_lstProjectiles)
            {
                pro.Draw(sprBatch);
            }
        }
    }
}
