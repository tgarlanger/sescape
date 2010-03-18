﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SantellosEscape.Screens.GameScreens.Avoider
{
    class Avoider : GameScreen
    {
        private Player m_player1;

        private List<Enemy> m_lstEnemies;

        private Texture2D m_texEnemy;

        private Texture2D m_texBackground;

        // ***** CHANGE THE 3 TO THE NUMBER OF TEXTURES IN THE PROJECTILES FOLDER
        private const int NUM_PROJECTILE_TEXTURES = 3;

        /// <summary>
        /// Initializes a new instance of the <see cref="Avoider"/> class.
        /// </summary>
        public Avoider()
        {
            m_lstEnemies = new List<Enemy>();
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="Content">The content.</param>
        /// <param name="sprBatch">The SPR batch.</param>
        public override void LoadContent(ContentManager Content, SpriteBatch sprBatch)
        {
            Texture2D texTemp = Content.Load<Texture2D>("Avoider/Smiley");

            m_texEnemy = Content.Load<Texture2D>("Avoider/Devil");

            m_texBackground = Content.Load<Texture2D>("Avoider/background");

            m_player1 = new Player(new Vector2(m_sprBatch.GraphicsDevice.Viewport.Height - 60), texTemp);

            List<Texture2D> lstProjectileTextures = new List<Texture2D>(NUM_PROJECTILE_TEXTURES);

            for (int index = 0; index < NUM_PROJECTILE_TEXTURES; index++)
            {
                lstProjectileTextures[index] = Content.Load<Texture2D>("Avoider/Projectiles/" + index.ToString());
            }

            m_lstEnemies.Add(new Enemy(m_texEnemy,new Vector2(10,10),new Vector2(10,0),3,lstProjectileTextures));

            base.LoadContent(Content, sprBatch);
        }

        /// <summary>
        /// Updates the Avoider game in respect to specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            if (m_player1.Alive)
            {
                m_player1.Update(gameTime);

                foreach (Enemy enemy in m_lstEnemies)
                {
                    enemy.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the Avoider game in respect tospecified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(GameTime gameTime)
        {
            m_sprBatch.Draw(m_texBackground, new Vector2(0, 0), Color.White);

            m_player1.Draw(m_sprBatch);

            foreach (Enemy enemy in m_lstEnemies)
            {
                enemy.Draw(m_sprBatch);
            }

            base.Draw(gameTime);
        }
    }
}
