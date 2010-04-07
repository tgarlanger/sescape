using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;


namespace SantellosEscape.Screens.GameScreens.ShooterGallery
{

    class Rectangle_addition
    {

        GraphicsDeviceManager graphics;
        GraphicsDevice GDevice;
        SpriteBatch spriteBatch;
        private Texture2D TableTex;
        ContentManager content;


        public void LoadContent(SpriteBatch spriteBatch)
        {

            TableTex = content.Load<Texture2D>("Items/Tablebar");



        }

        private void Update()
        {
        }
        public void Draw(SpriteBatch spriteBatch)
        {


            spriteBatch.Draw(TableTex, new Vector2(0, 200), Color.White);


        }

    }
}
