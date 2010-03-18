using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using SantellosEscape.Screens;

namespace SantellosEscape.Screens.GameScreens.FallDown
{
    class FallDown : GameScreen
    {
        Player player;
        List<Row> rows;
        List<GameObject> powerUps;

        int score;
        int gameBegin;
        int HighScore;

        string PlayState;
        SpriteFont scoreFont;

        float ScrollSpeed = -1;
        float ScrollIncreaseRate = -0.0005f;
        float Gravity = 5;

        Texture2D m_texBackground;

        public FallDown()
        {
            player = new Player();
            player.Position = Vector2.One;

            powerUps = new List<GameObject>();
            loadPowerups();

            score = 0;
            gameBegin = 0;
            HighScore = 0;

            PlayState = "Game";

            rows = new List<Row>();

            for (int i = 0; i < 5; i++)
            {
                Row row = new Row();
                row.YPosition = i * 100 + 300;
                row.CreateBlocks();
                rows.Add(row);
            }
        }

        private void resetGame()
        {
            score = 0;
            ScrollSpeed = -1;
            player.Position = Vector2.One;
            for (int i = 0; i < 5; i++)
            {
                rows[i].YPosition = i * 100 + 300;
                rows[i].Randomize();
            }
            PlayState = "Game";

        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content, SpriteBatch sprBatch)
        {
            m_sprBatch = sprBatch;

            scoreFont = Content.Load<SpriteFont>("Falldown/ScoreFont");
            player.Texture = Content.Load<Texture2D>("Falldown/player");
            powerUps[0].Texture = Content.Load<Texture2D>("Falldown/freeze");
            powerUps[1].Texture = Content.Load<Texture2D>("Falldown/player");
            powerUps[2].Texture = Content.Load<Texture2D>("Falldown/drill");
            powerUps[3].Texture = Content.Load<Texture2D>("Falldown/score");
            Texture2D blockTexture = Content.Load<Texture2D>("Falldown/block");

            m_texBackground = Content.Load<Texture2D>("Falldown/background");

            foreach (Row row in rows)
            {
                foreach (Block block in row.blocks)
                {
                    block.texture = blockTexture;
                }
                row.Randomize();
            }

            base.LoadContent(Content, sprBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (PlayState == "Game")
            {
                HandleCollisions(gameTime);
                HandlePowerups(gameTime);

                player.Update();

                ScrollSpeed += ScrollIncreaseRate;

                score = gameTime.TotalRealTime.Seconds;
                score += gameTime.TotalRealTime.Minutes * 60;
                score -= gameBegin;
                score *= 10;
            }
            else if (PlayState == "Menu")
            {
                if (score > HighScore)
                    HighScore = score;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    gameBegin = gameTime.TotalRealTime.Seconds + (gameTime.TotalRealTime.Minutes * 60);
                    resetGame();
                }
            }
            if (PlayState == "Game" && score % 50 == 0 && score != 0)
                addPowerup();

            base.Update(gameTime);
        }

        private void loadPowerups()
        {
            GameObject freezePowerup = new GameObject();
            freezePowerup.Visible = false;
            freezePowerup.Value = "freeze";
            freezePowerup.Duration = 2;
            powerUps.Add(freezePowerup);

            GameObject speedPowerup = new GameObject();
            speedPowerup.Visible = false;
            speedPowerup.Value = "speed";
            speedPowerup.Duration = 2;
            powerUps.Add(speedPowerup);

            GameObject drillPowerup = new GameObject();
            drillPowerup.Visible = false;
            drillPowerup.Value = "drill";
            drillPowerup.Duration = 2;
            powerUps.Add(drillPowerup);

            GameObject scorePowerup = new GameObject();
            scorePowerup.Visible = false;
            scorePowerup.Value = "score";
            powerUps.Add(scorePowerup);

        }

        private void addPowerup()
        {
            Random rand = new Random();
            int powerUpIndex = rand.Next(0, 4);

            if (!powerUps[powerUpIndex].Visible)
            {
                powerUps[powerUpIndex].Visible = true;
                powerUps[powerUpIndex].Position = new Vector2(rand.Next(0, 272 - powerUps[powerUpIndex].Texture.Width), 460);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            m_sprBatch.Begin();

            m_sprBatch.Draw(m_texBackground, Vector2.Zero, Color.White);

            player.Draw(m_sprBatch);

            foreach (Row row in rows)
            {
                row.Draw(m_sprBatch);
            }

            foreach (GameObject powerup in powerUps)
            {
                if (powerup.Visible)
                    powerup.Draw(m_sprBatch);
            }

            m_sprBatch.DrawString(scoreFont, "Score: " + score.ToString(), new Vector2(160, 430), Color.Yellow);

            if (PlayState == "Menu")
            {
                DrawMenu();
            }
            m_sprBatch.End();

            base.Draw(gameTime);
        }

        private void HandleCollisions(GameTime gameTime)
        {
            Vector2 newPosition;

            bool colliding = false;
            float yPos = 0;

            foreach (Row row in rows)
            {
                if (!powerUps[0].Active)
                    row.Update(ScrollSpeed);

                foreach (Block block in row.blocks)
                {
                    if (player.BoundingRectangle.Intersects(block.boundingRectangle) && !block.isEmpty && row.YPosition > player.Position.Y)
                    {
                        colliding = true;
                        yPos = row.YPosition - player.Texture.Height + 1;
                    }
                    foreach (GameObject powerup in powerUps)
                    {
                        if (powerup.Visible)
                        {
                            if (powerup.BoundingRectangle.Intersects(block.boundingRectangle) && !block.isEmpty)
                            {
                                powerup.Position = new Vector2(powerup.Position.X, row.YPosition - powerup.Texture.Height + 1);
                                if (powerup.Position.Y < 0)
                                    powerup.Visible = false;
                            }
                            else
                                powerup.Position = new Vector2(powerup.Position.X, powerup.Position.Y + Gravity);

                            if (powerup.Position.Y >= 480 - powerup.Texture.Height)
                                powerup.Position = new Vector2(powerup.Position.X, 480 - powerup.Texture.Height);
                        }
                    }
                }
            }

            if (colliding)
                newPosition = new Vector2(player.Position.X, yPos);
            else
                newPosition = new Vector2(player.Position.X, player.Position.Y + Gravity);

            if (newPosition.Y < 480 - player.Texture.Height)
                player.Position = newPosition;

            foreach (GameObject power in powerUps)
            {
                if (power.Visible)
                {
                    if (player.BoundingRectangle.Intersects(power.BoundingRectangle))
                    {
                        if (power.Value == "score")
                            score += 50;
                        else
                        {
                            power.Active = true;
                            power.StartTime = gameTime.ElapsedRealTime.Seconds;
                        }
                    }
                }
            }

            if (newPosition.Y < 0)
                PlayState = "Menu";
        }

        private void HandlePowerups(GameTime gameTime)
        {
            foreach (GameObject power in powerUps)
            {
                if (power.Active)
                    if (gameTime.ElapsedRealTime.Seconds - power.StartTime == power.Duration)
                        power.Active = false;

            }
        }

        private void DrawMenu()
        {
            m_sprBatch.DrawString(scoreFont, "High Score: " + HighScore.ToString(), new Vector2(50, 100), Color.Yellow);
            m_sprBatch.DrawString(scoreFont, "Tap to continue...", new Vector2(25, 200), Color.Red);
        }
    }
}
