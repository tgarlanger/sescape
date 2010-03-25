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

namespace SantellosEscape.Screens.GameScreens.FallDown
{
    class Row
    {
        public List<Block> blocks { get; set; }
        public float YPosition { get; set; }

        public Row()
        {
            blocks = new List<Block>();
        }

        public void CreateBlocks(Random r)
        {
            blocks = new List<Block>();
            int numGaps = r.Next(1, 3);

            for (int i = 0; i < 8; i++)
            {
                Block block = new Block();
                block.position = new Vector2(i * 34, YPosition);
                block.isEmpty = false;
                blocks.Add(block);
            }
            for (int j = 0; j < numGaps; j++)
                blocks[r.Next(0, 7)].isEmpty = true;
        }

        public void Randomize(Random r)
        {
            Random random = new Random();
            int numGaps = r.Next(1, 3);

            foreach (Block block in blocks)
                block.isEmpty = false;

            for (int i = 0; i < numGaps; i++)
            {
                blocks[r.Next(0, 7)].isEmpty = true;
            }

        }

        public void Update(float scrollSpeed)
        {
            YPosition = YPosition + scrollSpeed;
            Random r = new Random();
            if (YPosition < -17)
            {
                YPosition = 480;
                Randomize(r);
            }

            foreach (Block block in blocks)
            {
                block.position = new Vector2(block.position.X, YPosition);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Block block in blocks)
            {
                if (!block.isEmpty)
                    block.Draw(spriteBatch);
            }
        }
    }
}