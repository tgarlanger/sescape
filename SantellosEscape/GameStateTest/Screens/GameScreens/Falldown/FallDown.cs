using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

using SantellosEscape.Screens;

namespace SantellosEscape.Screens.GameScreens.FallDown
{
    public class FallDown : GameScreen
    {
        private Player player;
        private List<Row> rows;
        private List<GameObject> GameObjects;
        private Texture2D BackArrow;
        private int arrowFrame;

        private int score;
        private int gameBegin;
        private int bonus;
        public int HighScore;

        private string PlayState;
        private SpriteFont scoreFont;
        private Texture2D Menu;

        private float ScrollSpeed = -1;
        private float PowerSpawnRate = 15;
        private float Gravity = 5;

        private const float ScrollIncreaseRate = -0.0005f;
        private const float PowerIncreaseRate = -0.002f;

        private bool firstRun;
        GameObject[] Background;

        private Texture2D cursor;
        private List<SoundEffect> SoundEffects;

        int rad;

        public FallDown()
        {
            player = new Player();
            player.Active = true;
            player.Visible = true;
            player.Value = "Player";
            player.Position = Vector2.One;

            Background = new GameObject[2];
            Background[0] = new GameObject();
            Background[0].Position = Vector2.Zero;
            Background[1] = new GameObject();
            Background[1].Position = new Vector2(0, 485);

            GameObjects = new List<GameObject>();
            loadPowerups();
            GameObjects.Add(player);

            gameBegin = 0;
            HighScore = 0;
            bonus = 0;
            arrowFrame = 0;

            firstRun = true;
            PlayState = "Menu";

            rows = new List<Row>();

            Random r = new Random();
            for (int i = 0; i < 5; i++)
            {
                Row row = new Row();
                row.YPosition = i * 100 + 300;
                row.CreateBlocks(r);
                row.Randomize(r);
                rows.Add(row);
            }
            ScreenOrientation = ScreenOrientation.Portrait;

            SoundEffects = new List<SoundEffect>();
        }

        private void resetGame()
        {
            bonus = 0;
            ScrollSpeed = -1;
            PowerSpawnRate = 15;
            player.Position = Vector2.One;
            Random r = new Random();
            for (int i = 0; i < 5; i++)
            {
                rows[i].YPosition = i * 100 + 300;
                rows[i].Randomize(r);
            }
            PlayState = "Game";
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content, SpriteBatch sprBatch)
        {
            m_sprBatch = sprBatch;

            scoreFont = Content.Load<SpriteFont>("Falldown/Textures/ScoreFont");
            player.Texture = Content.Load<Texture2D>("Falldown/Textures/playerSheet");
            GameObjects[0].Texture = Content.Load<Texture2D>("Falldown/Textures/freeze2");
            GameObjects[1].Texture = Content.Load<Texture2D>("Falldown/Textures/speed");
            GameObjects[2].Texture = Content.Load<Texture2D>("Falldown/Textures/drill2");
            GameObjects[3].Texture = Content.Load<Texture2D>("Falldown/Textures/score2");
            Block.texture = Content.Load<Texture2D>("Falldown/Textures/block2");
            Menu = Content.Load<Texture2D>("Falldown/Textures/menu");
            BackArrow = Content.Load<Texture2D>("Falldown/Textures/arrow");
            cursor = Content.Load<Texture2D>("Falldown/Textures/cursor");
            Texture2D backGroundTexture = Content.Load<Texture2D>("Falldown/Textures/background3");
            Background[0].Texture = backGroundTexture;
            Background[1].Texture = backGroundTexture;

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = MediaPlayer.Volume / 20;
            MediaPlayer.Play(Content.Load<Song>("Falldown/Sounds/soundtrack"));

            SoundEffects.Add(Content.Load<SoundEffect>("Falldown/Sounds/freeze"));
            SoundEffects.Add(Content.Load<SoundEffect>("Falldown/Sounds/speed"));
            SoundEffects.Add(Content.Load<SoundEffect>("Falldown/Sounds/drill"));
            SoundEffects.Add(Content.Load<SoundEffect>("Falldown/Sounds/score"));
            firstRun = true;
            PlayState = "Menu";
            base.LoadContent(Content, sprBatch);
        }

        private void loadPowerups()
        {
            GameObject freezePowerup = new GameObject();
            freezePowerup.Visible = false;
            freezePowerup.Value = "freeze";
            freezePowerup.Duration = 3;
            GameObjects.Add(freezePowerup);

            GameObject speedPowerup = new GameObject();
            speedPowerup.Visible = false;
            speedPowerup.Value = "speed";
            speedPowerup.Duration = 3;
            GameObjects.Add(speedPowerup);

            GameObject drillPowerup = new GameObject();
            drillPowerup.Visible = false;
            drillPowerup.Value = "drill";
            drillPowerup.Duration = 2;
            GameObjects.Add(drillPowerup);

            GameObject scorePowerup = new GameObject();
            scorePowerup.Visible = false;
            scorePowerup.Value = "score";
            GameObjects.Add(scorePowerup);

        }

        public override void Update(GameTime gameTime)
        {
            if (PlayState == "Game")
            {
                HandleRows(gameTime);
                HandlePowerups(gameTime);

                if (!GameObjects[0].Active)
                {
                    Background[0].scroll(ScrollSpeed);
                    Background[1].scroll(ScrollSpeed);
                }
                //speed item dictates movement and gravity
                if (GameObjects[1].Active)
                {
                    Gravity = 10;
                    player.movementSpeed = 6;
                }
                else
                {
                    Gravity = 5;
                    player.movementSpeed = 3;
                }
                //player can't move while drill is active
                if (!GameObjects[2].Active)
                    player.Update(gameTime);

                ScrollSpeed += ScrollIncreaseRate;
                PowerSpawnRate += PowerIncreaseRate;

                rad = (int)(Math.Round(PowerSpawnRate));
                if (rad < 3)
                    rad = 3;

                score = gameTime.TotalRealTime.Seconds;
                score += gameTime.TotalRealTime.Minutes * 60;
                score -= gameBegin;
                score *= 10;
                score += bonus;

                if (gameTime.TotalGameTime.Seconds % rad == 0 && gameTime.TotalGameTime.Milliseconds == 0)
                {
                    addPowerup();
                }

            }
            else if (PlayState == "Menu")
            {
                if (score > HighScore)
                    HighScore = score;
                Rectangle mouseRec = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
                if (mouseRec.Intersects(new Rectangle(0, 480-50, 50, 50)))
                {
                    arrowFrame = 1;
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        MediaPlayer.Stop();
                        ScreenState = ScreenState.Hidden;
                    }
                }
                else
                    arrowFrame = 0;

                if (Mouse.GetState().LeftButton == ButtonState.Pressed && !firstRun)
                {
                    gameBegin = gameTime.TotalRealTime.Seconds + (gameTime.TotalRealTime.Minutes * 60);
                    resetGame();
                }
            }
            //Ensure game doesn't start until player sees the menu
            if (Mouse.GetState().LeftButton == ButtonState.Released)
                firstRun = false;

            base.Update(gameTime);
        }

        //adds a random power up at the bottom of the screen in a random position
        private void addPowerup()
        {
            Random rand = new Random();
            int powerUpIndex = rand.Next(0, 4);
            bool noPowerVisible = true;

            for (int i = 0; i < 4; i++)
            {
                if (GameObjects[i].Visible)
                    noPowerVisible = false;
            }
            if (noPowerVisible)
            {
                GameObjects[powerUpIndex].Visible = true;
                GameObjects[powerUpIndex].Position = new Vector2(rand.Next(0, 272 - GameObjects[powerUpIndex].Texture.Width), 480 - GameObjects[powerUpIndex].Texture.Width);
            }
        }


        //Scrolls rows, player and any game objects that are visible
        private void HandleRows(GameTime gameTime)
        {
            foreach (GameObject gObject in GameObjects)
            {
                gObject.isColliding = false;
                gObject.tempYpos = 0;
            }
            foreach (Row row in rows)
            {
                //rows will not update during freeze
                if (!GameObjects[0].Active)
                    row.Update(ScrollSpeed);

                foreach (Block block in row.blocks)
                {
                    foreach (GameObject gObject in GameObjects)
                    {
                        if (gObject.Visible && gObject.Value != "Player")
                        {
                            if (gObject.BoundingRectangle.Intersects(block.boundingRectangle) && !block.isEmpty && row.YPosition > gObject.Position.Y)
                            {
                                gObject.isColliding = true;
                                gObject.tempYpos = row.YPosition - gObject.Texture.Height + 1;
                            }
                        }
                    }
                    if (player.getBounds().Intersects(block.boundingRectangle) && !block.isEmpty && row.YPosition > player.Position.Y)
                    {
                        //Destroys block during drill
                        if (GameObjects[2].Active)
                        {
                            block.isEmpty = true;
                        }
                        else
                        {
                            player.isColliding = true;
                            player.tempYpos = row.YPosition - player.Texture.Height + 1;
                        }
                    }
                }
            }
            //sets the new position using appropriate collision and clamping
            foreach (GameObject gObject in GameObjects)
            {
                if (gObject.isColliding)
                    gObject.Position = new Vector2(gObject.Position.X, gObject.tempYpos);
                else
                    gObject.Position = new Vector2(gObject.Position.X, gObject.Position.Y + Gravity);

                if (gObject.Position.Y > 480 - gObject.Texture.Height)
                    gObject.Position = new Vector2(gObject.Position.X, 480 - gObject.Texture.Height);
                //Ends Current Game if player hits top
                if (gObject.Position.Y < 0 && gObject.Value == "Player")
                    PlayState = "Menu";
                if (gObject.Position.Y < 0 - gObject.Texture.Height)
                    gObject.Visible = false;
            }

        }
        //activates and deactivates powerups based on collisions and durations
        private void HandlePowerups(GameTime gameTime)
        {
            for (int i = 0; i < 4; i++)
            {
                if (player.BoundingRectangle.Intersects(GameObjects[i].BoundingRectangle) && GameObjects[i].Visible)
                {
                    //Score Item will never be active
                    if (i != 3)
                        GameObjects[i].Active = true;
                    else
                        bonus += 150;

                    SoundEffects[i].Play();
                    GameObjects[i].Visible = false;
                    GameObjects[i].StartTime = gameTime.TotalRealTime.Seconds + gameTime.TotalRealTime.Minutes * 60;
                }
                if (GameObjects[i].Active)
                {
                    //turn off power once its duration is over
                    if ((gameTime.TotalRealTime.Seconds + gameTime.TotalRealTime.Minutes * 60) - GameObjects[i].StartTime >= GameObjects[i].Duration)
                        GameObjects[i].Active = false;
                }

            }
        }

        public override void Draw(GameTime gameTime)
        {
            m_sprBatch.Begin();

            Background[0].Draw(m_sprBatch);
            Background[1].Draw(m_sprBatch);
            //m_sprBatch.Draw(Background[0].Texture, new Rectangle((int)Background[0].Position.X, (int)Background[0].Position.Y, 272, 600), Color.White);

            //replace player sprite if drill is active
            if (GameObjects[2].Active)
                player.Draw(m_sprBatch, GameObjects[2].Texture);
            else
                player.Draw(m_sprBatch);

            foreach (Row row in rows)
            {
                row.Draw(m_sprBatch);
            }

            foreach (GameObject powerup in GameObjects)
            {
                if (powerup.Visible && powerup.Value != "Player")
                    powerup.Draw(m_sprBatch);
            }

            m_sprBatch.DrawString(scoreFont, "Score: " + score.ToString(), new Vector2(160, 450), Color.DarkRed);

            if (PlayState == "Menu")
            {
                DrawMenu();
                m_sprBatch.Draw(cursor, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
            }
            m_sprBatch.End();

            base.Draw(gameTime);
        }

        private void DrawMenu()
        {
            m_sprBatch.DrawString(scoreFont, "High Score: " + HighScore.ToString(), new Vector2(70, 375), Color.DarkRed);
            m_sprBatch.Draw(Menu, Vector2.Zero, Color.White);
            m_sprBatch.Draw(BackArrow, new Rectangle(0, 480-50, 50, 50), new Rectangle(50 * arrowFrame, 0, 50, 50), Color.White);
        }
    }
}