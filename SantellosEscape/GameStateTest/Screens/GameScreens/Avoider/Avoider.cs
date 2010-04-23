using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace SantellosEscape.Screens.GameScreens.Avoider
{
    class Avoider : GameScreen
    {
        private Player m_player1;

        private List<Enemy> m_lstEnemies;

        private Texture2D m_texEnemy;

        private Texture2D m_texBackground;

        private Texture2D m_texCursor;

        private List<Texture2D> m_lstProjectileTextures;

        private int m_iScore;

        private int m_iNextAddEnemy;

        private SpriteFont m_sprFont;

        private Random m_rndRand;

        private Texture2D m_texOverLay;

        private bool m_bGameActive;

        private SoundEffect m_sndThrow;

        private Texture2D m_texBackArrow;

        private Rectangle m_recBackArrow;

        private int m_iHighScore;

        private bool m_bFirstLaunch;

        // ***** CHANGE THE VALUE TO THE NUMBER OF TEXTURES IN THE PROJECTILES FOLDER
        private const int NUM_PROJECTILE_TEXTURES = 4;

        /// <summary>
        /// Initializes a new instance of the <see cref="Avoider"/> class.
        /// </summary>
        public Avoider()
        {
            m_lstEnemies = new List<Enemy>();

            m_iScore = 0;
            m_iNextAddEnemy = 1500;

            m_rndRand = new Random(DateTime.Now.Second*DateTime.Now.Millisecond);

            ScreenOrientation = ScreenOrientation.Portrait;

            m_bGameActive = false;

            m_bFirstLaunch = true;

            m_iHighScore = 0;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="Content">The content.</param>
        /// <param name="sprBatch">The SPR batch.</param>
        public override void LoadContent(ContentManager Content, SpriteBatch sprBatch)
        {
            m_sprBatch = sprBatch;
            m_bFirstLaunch = true;

            Texture2D texTemp = Content.Load<Texture2D>("Avoider/Graphics/spritesheet");

            m_texEnemy = Content.Load<Texture2D>("Avoider/Graphics/Devil");

            m_texBackground = Content.Load<Texture2D>("Avoider/Graphics/background2");

            m_sprFont = Content.Load<SpriteFont>("Avoider/Fonts/SpriteFont1");

            m_player1 = new Player(new Vector2((272/2),480-30-texTemp.Height), texTemp);

            m_lstProjectileTextures = new List<Texture2D>();

            m_texOverLay = Content.Load<Texture2D>("Avoider/Graphics/menu");

            m_player1.ScreamSound = Content.Load<SoundEffect>("Avoider/Sounds/Scream");

            m_sndThrow = Content.Load<SoundEffect>("Avoider/Sounds/Throw");

            m_texCursor = Content.Load<Texture2D>("Falldown/Textures/cursor");

            m_texBackArrow = Content.Load<Texture2D>("Falldown/Textures/arrow");

            m_recBackArrow = new Rectangle(0, 480 - 50, 50, 50);

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Content.Load<Song>("Avoider/Sounds/SantellosEscapeAvoider"));

            for (int index = 0; index < NUM_PROJECTILE_TEXTURES; index++)
            {
                m_lstProjectileTextures.Add(Content.Load<Texture2D>("Avoider/Projectiles/" + index.ToString())); 
                // lstProjectileTextures[index] = Content.Load<Texture2D>("Avoider/Projectiles/" + index.ToString());
            }

            AddEnemy();

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
                        int iSeed = m_rndRand.Next(500) * m_rndRand.Next(300);
                        m_rndRand = new Random(iSeed);

                        AddEnemy();

                        m_iNextAddEnemy *= 2;
                    }
                }

                foreach (Enemy enemy in m_lstEnemies)
                {
                    enemy.Update(gameTime);
                    enemy.CheckCollisions(ref m_player1);
                }
            }

            if (!m_player1.Alive)
            {
                m_bGameActive = false;
                m_lstEnemies.Clear();

                m_iHighScore = m_iScore;
            }
            
            if (!m_bGameActive || m_bFirstLaunch)
            {
#if !ZUNE
                MouseState mState = Mouse.GetState();

                if (m_bFirstLaunch)
                {
                    if (mState.LeftButton == ButtonState.Released)
                    {
                        m_bFirstLaunch = false;
                    }
                }
                else if (mState.LeftButton == ButtonState.Pressed)
                {
                    if (m_recBackArrow.Contains(mState.X, mState.Y))
                    {
                        ScreenState = ScreenState.Hidden;
                    }
                    else
                    {
                        m_bGameActive = true;

                        Reset();
                    }
                }
#else
                TouchCollection touchCollection = TouchPanel.GetState();

                if (m_bFirstLaunch)
                {
                    if (touchCollection.Count == 0)
                    {
                        m_bFirstLaunch = false;
                    }
                }
                if (touchCollection.Count > 0)
                {
                    if (m_recBackArrow.Contains((int)touchCollection[0].Position.X,(int)touchCollection[0].Position.Y))
                    {
                        MediaPlayer.Stop();
                        ScreenState = ScreenState.Hidden;
                    }
                    else
                    {
                        m_bGameActive = true;

                        Reset();
                    }
                }
#endif
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the Avoider game in respect tospecified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(GameTime gameTime)
        {
            m_sprBatch.Begin();

            if (m_player1.Alive /*&& m_bGameActive*/)
            {
                //m_sprBatch.Begin();

                m_sprBatch.Draw(m_texBackground, new Vector2(0, 0), Color.White);

                m_player1.Draw(m_sprBatch);

                foreach (Enemy enemy in m_lstEnemies)
                {
                    enemy.Draw(m_sprBatch);
                }

                m_sprBatch.DrawString(m_sprFont, "Score: " + m_iScore.ToString(), new Vector2(0, 480 - 20), Color.Yellow);

                //m_sprBatch.End();

                base.Draw(gameTime);
            }
            if (!m_bGameActive)
            {
                Vector2 mousepos = new Vector2(Mouse.GetState().X,Mouse.GetState().Y);

                //m_sprBatch.Begin();

                m_sprBatch.Draw(m_texBackground, Vector2.Zero, Color.White);

                m_sprBatch.Draw(m_texOverLay, Vector2.Zero, Color.White);

                m_sprBatch.Draw(m_texBackArrow, /*new Rectangle(0, 480 - 50, 50, 50)*/ m_recBackArrow, new Rectangle(50 , 0, 50, 50), Color.White);
#if !ZUNE
                m_sprBatch.Draw(m_texCursor, mousepos, Color.White);
#endif
                m_sprBatch.DrawString(m_sprFont, "High Score: " + m_iHighScore.ToString(), new Vector2(100, 480 - 30), Color.Yellow);

                //m_sprBatch.End();
            }
#if ZUNE
            //m_sprBatch.DrawString(m_sprFont, "COUNT: " + TouchPanel.GetState().Count.ToString(), Vector2.Zero, Color.White);
#endif

            m_sprBatch.End();
        }

        private void Reset()
        {
            m_player1.Alive = true;
            m_lstEnemies.Clear();
            m_iScore = 0;
            m_iNextAddEnemy = 1500;
            m_bFirstLaunch = true;

            AddEnemy();
        }

        private void AddEnemy()
        {
            int ifireRate = m_rndRand.Next(50, 150) * m_rndRand.Next(4, 7) - m_rndRand.Next(300, 500);

            
            if (ifireRate < 0)
            {
                ifireRate += 500;
            }

            m_lstEnemies.Add(new Enemy(m_texEnemy,
                            new Vector2(m_rndRand.Next(25, 272 - m_texEnemy.Width - 25), 10),
                            new Vector2(m_rndRand.Next(1,3), 0),
                            ifireRate,
                            m_lstProjectileTextures));

            m_lstEnemies[m_lstEnemies.Count - 1].ThrowSound = m_sndThrow;
        }
    }
}
