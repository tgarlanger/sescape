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
using SantellosEscape.Screens.GameScreens.ShooterGallery;
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

        //private MenuScreen m_menuScreen;

        private SpriteFont m_sprFont;

       //s private int m_iActiveScreenIndex;

        public SantellosEscape()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 272;

            m_lstGameScreens = new List<Screen>(1);

            //m_menuScreen = new MenuScreen();

          //  m_iActiveScreenIndex = 0;
      
            m_lstGameScreens.Add(new MenuScreen());
            m_lstGameScreens[0].ScreenState = ScreenState.Active;
            m_lstGameScreens[0].ScreenType = ScreenType.Menu;
            m_lstGameScreens[0].Name = "Main Menu";

            m_lstGameScreens.Add(new Avoider());
            m_lstGameScreens[1].ScreenState = ScreenState.Hidden;
            m_lstGameScreens[1].ScreenType = ScreenType.Game;
            m_lstGameScreens[1].Name = "Avoider";

            m_lstGameScreens.Add(new FallDown());
            m_lstGameScreens[2].ScreenState = ScreenState.Hidden;
            m_lstGameScreens[2].ScreenType = ScreenType.Game;
            m_lstGameScreens[2].Name = "Fall Down";

            m_lstGameScreens.Add(new ShooterGallery());
            m_lstGameScreens[3].ScreenState = ScreenState.Hidden;
            m_lstGameScreens[3].ScreenType = ScreenType.Game;
            m_lstGameScreens[3].Name = "Shooting Gallery";

            m_lstGameScreens.Add(new CreditsScreen());
            m_lstGameScreens[4].ScreenState = ScreenState.Hidden;
            m_lstGameScreens[4].ScreenType = ScreenType.Credits;
            m_lstGameScreens[4].Name = "Credits";
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
                    gs.LoadContent(Content, spriteBatch);
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
            {
                if (MediaPlayer.State == MediaState.Playing)
                {
                    MediaPlayer.Stop();
                }

                this.Exit();
            }

            bool anyGameActive = false;
            foreach (Screen gs in m_lstGameScreens)
            {
                if (gs.ScreenState == ScreenState.Active)
                {
                    anyGameActive = true;
                    if (gs.ScreenType == ScreenType.Menu && gs.Name.Equals("Main Menu"))
                    {
                        if (((MenuScreen)gs).SelectedItem != -1)
                        {
                            if (((MenuScreen)gs).SelectedItem == 4)
                            {
                                if (MediaPlayer.State == MediaState.Playing)
                                {
                                    MediaPlayer.Stop();
                                }

                                this.Exit();
                                break;
                            }

                            m_lstGameScreens[((MenuScreen)gs).SelectedItem + 1].ScreenState = ScreenState.Active;
                            m_lstGameScreens[((MenuScreen)gs).SelectedItem + 1].LoadContent(Content, spriteBatch);
                            SetOrientation(m_lstGameScreens[((MenuScreen)gs).SelectedItem + 1].ScreenOrientation);
                            
                            gs.ScreenState = ScreenState.Hidden;
                            ((MenuScreen)gs).SelectedItem = -1;
                            break;
                        }
                    }
                    gs.Update(gameTime);
                }
            }
            if (!anyGameActive)
                m_lstGameScreens[0].ScreenState = ScreenState.Active;

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
                }
            }

            base.Draw(gameTime);
        }

        private void SetOrientation(ScreenOrientation orientation)
        {
            switch (orientation)
            {
                case ScreenOrientation.Landscape:
                    graphics.PreferredBackBufferHeight = 272;
                    graphics.PreferredBackBufferWidth = 480;
#if ZUNE
                   // RenderTarget2D 
#endif
                    graphics.ApplyChanges();
                    break;
                case ScreenOrientation.Portrait:
                    graphics.PreferredBackBufferHeight = 480;
                    graphics.PreferredBackBufferWidth = 272;
                    graphics.ApplyChanges();
                    break;
            }
        }
    }
}