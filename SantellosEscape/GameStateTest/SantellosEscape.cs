using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using SantellosEscape.Screens;
using SantellosEscape.Screens.GameScreens.FallDown;
using SantellosEscape.Screens.GameScreens.Avoider;

namespace GameStateTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SantellosEscape : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private List<Screen> m_lstGameScreens;

        private SpriteFont m_sprFont;

        private int m_iActiveScreenIndex;

        public SantellosEscape()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 272;

            m_lstGameScreens = new List<Screen>(1);

            List<string> strMenuItems = new List<string>();
            strMenuItems.Add("Avoider");
            strMenuItems.Add("Fall Down");
            strMenuItems.Add("Shooting Gallery");
            strMenuItems.Add("Credits");
            strMenuItems.Add("Quit");

            m_iActiveScreenIndex = 0;

            m_lstGameScreens.Add(new MenuScreen(strMenuItems,"GameState/Graphics/Menu/MenuBackground","GameState/Fonts/SpriteFont1",new Vector2(0,140)));
            m_lstGameScreens[0].ScreenState = ScreenState.Active;
            m_lstGameScreens[0].Name = "Main Menu";

            m_lstGameScreens.Add(new FallDown());
            m_lstGameScreens[1].ScreenState = ScreenState.Hidden;
            m_lstGameScreens[1].ScreenType = ScreenType.Game;
            m_lstGameScreens[1].Name = "Fall Down";

            m_lstGameScreens.Add(new Avoider());
            m_lstGameScreens[2].ScreenState = ScreenState.Hidden;
            m_lstGameScreens[2].ScreenType = ScreenType.Game;
            m_lstGameScreens[2].Name = "Avoider";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            foreach (Screen gs in m_lstGameScreens)
            {
                if (gs.ScreenState == ScreenState.Active)
                {
                    gs.Initialize();
                }
            }

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            m_sprFont = Content.Load<SpriteFont>("GameState/Fonts/SpriteFont1");

            foreach (Screen gs in m_lstGameScreens)
            {
                if (gs.ScreenState == ScreenState.Active)
                {
                    if (spriteBatch == null)
                    {
                        throw new Exception("spriteBatch is null!");
                    }
                    gs.LoadContent(Content,spriteBatch);
                }
            }

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            foreach (Screen gs in m_lstGameScreens)
            {
                if (gs.ScreenState == ScreenState.Active)
                {
                    gs.UnloadContent();
                }
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            foreach (Screen gs in m_lstGameScreens)
            {
                if (gs.ScreenState == ScreenState.Active)
                {
                    if (gs.ScreenType == ScreenType.Menu &&
                         gs.Name.Equals("Main Menu"))
                    {
                        if (((MenuScreen)gs).SelectedIndex != -1)
                        {
                            string strGameName = ((MenuScreen)gs).MenuItems[((MenuScreen)gs).SelectedIndex];

                            bool bGamefound = false;

                            for ( int index = 0; index < m_lstGameScreens.Count; index++ )
                            {
                                if (m_lstGameScreens[index].Name.Equals(strGameName))
                                {
                                    m_lstGameScreens[index].ScreenState = ScreenState.Active;
                                    bGamefound = true;
                                    m_lstGameScreens[m_iActiveScreenIndex].ScreenState = ScreenState.Hidden;
                                    //m_lstGameScreens[index].LoadContent(Content, spriteBatch);
                                    LoadContent();
                                    m_iActiveScreenIndex = index;
                                    break;
                                }
                            }

                            if (!bGamefound)
                            {
                                spriteBatch.Begin();

                                spriteBatch.DrawString(m_sprFont,strGameName + " - NoT FouND!",new Vector2(10,400),Color.Black);

                                spriteBatch.End();
                            }
                        }
                    }

                    gs.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            foreach (Screen gs in m_lstGameScreens)
            {
                if (gs.ScreenState == ScreenState.Active)
                {
                    gs.Draw(gameTime);

                    if (gs.ScreenType == ScreenType.Menu)
                    {
                        if (((MenuScreen)gs).SelectedIndex != -1)
                        {
                            spriteBatch.Begin();

                            spriteBatch.DrawString(m_sprFont, ((MenuScreen)gs).MenuItems[((MenuScreen)gs).SelectedIndex], new Vector2(10, 300), Color.Blue);
                            //spriteBatch.DrawString(m_sprFont, ((MenuScreen)gs).SelectedIndex.ToString(), new Vector2(10, 300), Color.Blue);
                            spriteBatch.End();
                        }
                    }
                }
            }

            base.Draw(gameTime);
        }
    }
}
