using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace SantellosEscape.Screens.GameScreens.Avoider
{
    class Enemy : GameObject
    {
        private int m_iShotFrequency;

        private List<Projectile> m_lstProjectiles;

        private List<Texture2D> m_lstProjectileTextures;

        private Random m_rndRand;

        private SoundEffect m_sndThrow;

        public SoundEffect ThrowSound
        {
            get
            {
                return m_sndThrow;
            }
            set
            {
                m_sndThrow = value;
            }
        }

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

            if (m_vecPosition.X > (272 - 5 - m_texImage.Width) ||
                m_vecPosition.X < 5)
            {
                m_vecVelocity.X *= -1;
            }

            /*
            foreach (Projectile pro in m_lstProjectiles)
            {
                if (pro.Bounds.Y > 480)
                {
                    m_lstProjectiles.Remove(pro);
                    continue;
                }
                pro.Update(gameTime);
            }
             * */
            for (int index = 0; index < m_lstProjectiles.Count; index++)
            {
                if (m_lstProjectiles[index].Bounds.Y > 480)
                {
                    m_lstProjectiles.RemoveAt(index);
                    continue;
                }

                m_lstProjectiles[index].Update(gameTime);
            }
            
            if (gameTime.TotalGameTime.Milliseconds % m_iShotFrequency == 0)
            {
                m_lstProjectiles.Add(new Projectile(
                    m_lstProjectileTextures[m_rndRand.Next(0,m_lstProjectileTextures.Count)],
                    m_vecPosition+new Vector2(0,m_texImage.Height+5),
                    new Vector2(0,m_rndRand.Next(5,15))));

                m_sndThrow.Play();
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

        public void CheckCollisions(ref Player player1)
        {
            foreach (Projectile pro in m_lstProjectiles)
            {
                pro.CheckCollisions(ref player1);
            }
        }
    }
}
