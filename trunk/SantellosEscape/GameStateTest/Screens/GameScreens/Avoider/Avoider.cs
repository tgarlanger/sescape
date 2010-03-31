using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace SantellosEscape.Screens.GameScreens.Avoider
{
    class Avoider : GameScreen
    {
        private Player m_player1;

        private List<Enemy> m_lstEnemies;

        private Texture2D m_texEnemy;

        private Texture2D m_texBackground;

        private List<Texture2D> m_lstProjectileTextures;

        private int m_iScore;

        private int m_iNextAddEnemy;

        private SpriteFont m_sprFont;

        private Random m_rndRand;

        private Texture2D m_texOverLay;

        private bool m_bGameActive;

        private SoundEffect m_sndThrow;

        // ***** CHANGE THE 3 TO THE NUMBER OF TEXTURES IN THE PROJECTILES FOLDER
        private const int NUM_PROJECTILE_TEXTURES = 3;

        /// <summary>
        /// Initializes a new instance of the <see cref="Avoider"/> class.
        /// </summary>
        public Avoider()
        {
            m_lstEnemies = new List<Enemy>();

            m_iScore = 0;
            m_iNextAddEnemy = 1500;

            m_rndRand = new Random(5280);

            ScreenOrientation = ScreenOrientation.Portrait;

            m_bGameActive = false;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="Content">The content.</param>
        /// <param name="sprBatch">The SPR batch.</param>
        public override void LoadContent(ContentManager Content, SpriteBatch sprBatch)
        {
            m_sprBatch = sprBatch;

            Texture2D texTemp = Content.Load<Texture2D>("Avoider/Graphics/spritesheet");

            m_texEnemy = Content.Load<Texture2D>("Avoider/Devil");

            m_texBackground = Content.Load<Texture2D>("Avoider/background");

            m_sprFont = Content.Load<SpriteFont>("Avoider/Fonts/SpriteFont1");

            m_player1 = new Player(new Vector2((272/2),480-30-texTemp.Height), texTemp);

            m_lstProjectileTextures = new List<Texture2D>();

            m_texOverLay = Content.Load<Texture2D>("Avoider/Graphics/menu");

            m_player1.ScreamSound = Content.Load<SoundEffect>("Avoider/Sounds/Scream");

            m_sndThrow = Content.Load<SoundEffect>("Avoider/Sounds/Throw");

            for (int index = 0; index < NUM_PROJECTILE_TEXTURES; index++)
            {
                m_lstProjectileTextures.Add(Content.Load<Texture2D>("Avoider/Projectiles/" + index.ToString())); 
                // lstProjectileTextures[index] = Content.Load<Texture2D>("Avoider/Projectiles/" + index.ToString());
            }

            m_lstEnemies.Add(new Enemy(m_texEnemy,new Vector2(m_rndRand.Next(10,272-m_texEnemy.Width-10),10),new Vector2(1,0),1000,m_lstProjectileTextures));

            m_lstEnemies[m_lstEnemies.Count - 1].ThrowSound = m_sndThrow;

            base.LoadContent(Content, sprBatch);
        }

        /// <summary>
        /// Updates the Avoider game in respect to specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            if (m_player1.Alive && m_bGameActive)
            {
                m_player1.Update(gameTime);

                if (gameTime.TotalGameTime.Milliseconds % 100 == 0)
                {
                    m_iScore += 10;

                    if (m_iScore >= m_iNextAddEnemy)
                    {
                        m_lstEnemies.Add(new Enemy(m_texEnemy, new Vector2(m_rndRand.Next(10, 272 - m_texEnemy.Width - 10), 10), new Vector2(1, 0), 1000, m_lstProjectileTextures));

                        m_lstEnemies[m_lstEnemies.Count - 1].ThrowSound = m_sndThrow;
                        m_iNextAddEnemy *= 2;
                    }
                }

                foreach (Enemy enemy in m_lstEnemies)
                {
                    enemy.Update(gameTime);
                    enemy.CheckCollisions(ref m_player1);
                }
            }
            else if (!m_bGameActive)
            {
                MouseState mState = Mouse.GetState();

                if (mState.LeftButton == ButtonState.Pressed && mState.Y > 400)
                {
                    m_bGameActive = true;
                }
            }

            if (!m_player1.Alive)
            {
                m_bGameActive = false;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the Avoider game in respect tospecified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(GameTime gameTime)
        {
            if (m_player1.Alive /*&& m_bGameActive*/)
            {
                m_sprBatch.Begin();

                m_sprBatch.Draw(m_texBackground, new Vector2(0, 0), Color.White);

                m_player1.Draw(m_sprBatch);

                foreach (Enemy enemy in m_lstEnemies)
                {
                    enemy.Draw(m_sprBatch);
                }

                m_sprBatch.DrawString(m_sprFont, "Score: " + m_iScore.ToString(), new Vector2(0, 480 - 20), Color.Yellow);

                m_sprBatch.End();

                base.Draw(gameTime);
            }
            if (!m_bGameActive)
            {
                m_sprBatch.Begin();

                m_sprBatch.Draw(m_texOverLay, Vector2.Zero, Color.White);

                m_sprBatch.End();
            }
        }
    }
}
