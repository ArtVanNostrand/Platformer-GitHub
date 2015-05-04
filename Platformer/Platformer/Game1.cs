#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
#endregion

namespace Platformer
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Texture2D life, icon;
        SpriteBatch spriteBatch;
        Scene scene;
        Character sonic;
        Song musiclevel1;
        bool ZPressed = false;
        
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            //1200x600
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            Camera.SetGraphicsDeviceManager(graphics);
            Camera.SetTarget(Vector2.Zero);
            Camera.SetWorldWidth(5);

            base.Initialize();

            MediaPlayer.Play(musiclevel1);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            scene = new Scene(spriteBatch);

            //Images:
            //sonic.vi
            life = Content.Load<Texture2D>("lifes");
            icon = Content.Load<Texture2D>("sonicIcon");

            SlidingBackground fundo = new SlidingBackground(Content, "oceano");
            scene.AddBackground(fundo);

            scene.AddSprite(new Level(Content, "platform1").At(new Vector2(4, 0)));
            scene.AddSprite(new Level(Content, "platform1").At(new Vector2(5, 1)));

            scene.AddSprite(new Level(Content, "platform1").At(new Vector2(-4, -0.1f)));
            scene.AddSprite(new Level(Content, "platform1").At(new Vector2(-4, 0.3f)));
            scene.AddSprite(new Level(Content, "platform1").At(new Vector2(-4, 0.7f)));
            scene.AddSprite(new Level(Content, "platform1").At(new Vector2(-4, 1.1f)));
            scene.AddSprite(new Level(Content, "platform1").At(new Vector2(-4, 1.5f)));

            scene.AddSprite(new Level(Content, "crab").At(new Vector2(6, -0.1f)));

            for (float i = -100; i < 100;  i+= 16)
            {
                scene.AddSprite(new Sprite(Content, "sand").Scl((float)Camera.worldWidth * 3.2f)
            .At(new Vector2(i, -0.740f)));   // de 16 em 16(x)
            }

            for (float i = -100; i < 100; i += 16)
            {
                scene.AddSprite(new Sprite(Content, "nuvens").Scl((float)Camera.worldWidth * 3.2f)
            .At(new Vector2(i, 3.2f)));   // de 16 em 16(x)
            }

            scene.AddSprite(new Pickups(Content).At(new Vector2(1, 1)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(4, 1)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(5, 2)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(10, 2)));

            //Sound Effects:
         


            //Music:
            musiclevel1 = Content.Load<Song>("[Music] Sonic Generations - Angel Island Zone -Jukebox-");

           

            scene.AddSprite(new Character(Content));

            // TODO: use this.Content to load your game content here
        }


        protected override void UnloadContent()
        {
          musiclevel1.Dispose();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyUp(Keys.Z)) ZPressed = false;
            if (ZPressed == false && Keyboard.GetState().IsKeyDown(Keys.Z))
             {
                 ZPressed = true;

             }

            scene.Update(gameTime);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            scene.Draw(gameTime);
            spriteBatch.Begin();
            spriteBatch.Draw(icon, new Vector2(30, 30));
            spriteBatch.Draw(life, new Vector2(60, 30));
            spriteBatch.Draw(life, new Vector2(90, 30));
            spriteBatch.Draw(life, new Vector2(120, 30));
            spriteBatch.End();
            base.Draw(gameTime);
        }





    }
}
