﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SantellosEscape.Screens
{
    class MenuScreen : Screen
    {
        public List<Texture2D> MenuItems { get; set; }
        public Texture2D Cursor { get; set; }
        public Texture2D Background { get; set; }
        public Vector2 ListOrigin { get; set; }
        public float ListSpacing { get; set; }
        public int SelectedItem { get; set; }
        
        private int[] frame;
        private Vector2 frameSize;

        Song m_songBackgroundMusic;

        public MenuScreen()
        {
            SelectedItem = -1;
            ScreenType = ScreenType.Menu;
            ScreenOrientation = ScreenOrientation.Landscape;

            MenuItems = new List<Texture2D>();
            ListOrigin = new Vector2(40, 175);
            ListSpacing = 60;

            frame = new int[5];
            frameSize = new Vector2(200, 50);
        }

        public override void LoadContent(ContentManager Content, SpriteBatch sprBatch)
        {
            m_sprBatch = sprBatch;

            MenuItems.Add(Content.Load<Texture2D>("GameState/Graphics/Menu/dualAssault"));
            MenuItems.Add(Content.Load<Texture2D>("GameState/Graphics/Menu/dualExodus"));
            MenuItems.Add(Content.Load<Texture2D>("GameState/Graphics/Menu/payback"));
            MenuItems.Add(Content.Load<Texture2D>("GameState/Graphics/Menu/credits"));
            MenuItems.Add(Content.Load<Texture2D>("GameState/Graphics/Menu/exit"));
            Cursor = Content.Load<Texture2D>("FallDown/Textures/cursor");
            Background = Content.Load<Texture2D>("GameState/Graphics/Menu/MenuBackground");

            MediaPlayer.IsRepeating = true;
            m_songBackgroundMusic = Content.Load<Song>("GameState/Sounds/SantellosEscapeMenu");
            MediaPlayer.Play(m_songBackgroundMusic);

            base.LoadContent(Content, m_sprBatch);
        }

        public override void Update(GameTime gameTime)
        {
            ScreenOrientation = ScreenOrientation.Portrait;
  
            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(m_songBackgroundMusic);
            }

            bool anySelected = false;
#if ZUNE
            TouchCollection collection = TouchPanel.GetState();

            if (collection.Count == 1)
            {
                for (int i = 0; i < MenuItems.Count; i++)
                {
                    Rectangle itemRect = new Rectangle((int)ListOrigin.X, (int)(ListOrigin.Y + (ListSpacing * i)), (int)frameSize.X, (int)frameSize.Y);
                    if (itemRect.Intersects(new Rectangle((int)collection[0].Position.X, (int)collection[0].Position.Y, 1, 1)))
                    {
                        frame[i] = 0;
                        if (collection[0].State == TouchLocationState.Pressed || collection[0].State == TouchLocationState.Moved)
                            frame[i] = 1;
                        else if (collection[0].State == TouchLocationState.Released)
                        {
                            MediaPlayer.Stop();

                            SelectedItem = i;
                            anySelected = true;
                            break;
                        }
                    }
                    else
                        frame[i] = 0;
                }
            }
#else
            Rectangle mouseRec = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);

            for (int i = 0; i < MenuItems.Count; i++)
            {
                Rectangle itemRect = new Rectangle((int)ListOrigin.X, (int)(ListOrigin.Y + (ListSpacing * i)), (int)frameSize.X, (int)frameSize.Y);
                if (itemRect.Intersects(mouseRec))
                {
                    frame[i] = 1;
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        MediaPlayer.Stop();

                        SelectedItem = i;
                        anySelected = true;
                        break;
                    }
                }
                else
                    frame[i] = 0;
            }
#endif
            if (!anySelected)
            {
                SelectedItem = -1;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            m_sprBatch.Begin();

            m_sprBatch.Draw(Background, Vector2.Zero, Color.White);

            for (int i = 0; i < MenuItems.Count; i++)
            {
                m_sprBatch.Draw(MenuItems[i], new Rectangle((int)ListOrigin.X, (int)(ListOrigin.Y + (ListSpacing * i)), (int)frameSize.X, (int)frameSize.Y), new Rectangle(200 * frame[i], 0, 200, 78), Color.White);
            }

#if WINDOWS
            m_sprBatch.Draw(Cursor, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
#endif
            m_sprBatch.End();

            base.Draw(gameTime);
        }
    }
}
