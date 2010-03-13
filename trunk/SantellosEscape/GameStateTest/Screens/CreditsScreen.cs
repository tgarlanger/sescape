using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateTest.Screens
{
    class CreditsScreen : Screen
    {
        private List<string> m_strCreditItems;

        public CreditsScreen(List<string> strCreditItems, string strFontPath)
        {
            m_strCreditItems = strCreditItems;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content, Microsoft.Xna.Framework.Graphics.SpriteBatch sprBatch)
        {
            base.LoadContent(Content, sprBatch);
        }
    }
}
