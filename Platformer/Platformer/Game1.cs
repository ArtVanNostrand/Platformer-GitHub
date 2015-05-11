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
            icon = Content.Load<Texture2D>("lifecounter");

            SlidingBackground fundo = new SlidingBackground(Content, "oceano");
            scene.AddBackground(fundo);

            //platforms:
            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.2f).At(new Vector2(4, 0)));
            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.2f).At(new Vector2(5, 1)));

            scene.AddSprite(new Level(Content, "platform2").Scl((float)2.5f).At(new Vector2(2, 1.5f)));
            scene.AddSprite(new Level(Content, "platform2").Scl((float)2.5f).At(new Vector2(1f, 1.5f)));
            scene.AddSprite(new Level(Content, "platform2").Scl((float)2.5f).At(new Vector2(0, 1.5f)));
            scene.AddSprite(new Level(Content, "platform2").Scl((float)2.5f).At(new Vector2(-1, 1.5f)));

            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.2f).At(new Vector2(-4, -0.1f)));
            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.2f).At(new Vector2(-4, 0.3f)));
            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.2f).At(new Vector2(-4, 0.7f)));
            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.2f).At(new Vector2(-4, 1.1f)));
            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.2f).At(new Vector2(-4, 1.5f)));

            scene.AddSprite(new Level(Content, "imagerock1").At(new Vector2(30f, -0.1f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(31.35f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(31.57f, -0.10f)));
            scene.AddSprite(new Level(Content, "imagerock2").Scl((float)1.15f).At(new Vector2(30.8f, 0f)));
            scene.AddSprite(new Level(Content, "imagerock3").Scl((float)1.3f).At(new Vector2(32f, 0.1f)));

            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.3f).At(new Vector2(40f, 0.1f)));
            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.3f).At(new Vector2(41f, 1f)));

            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.3f).At(new Vector2(42.24f, 1.6f)));
            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.3f).At(new Vector2(42.24f, 1.2f)));
            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.3f).At(new Vector2(42.24f, 0.8f)));
            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.3f).At(new Vector2(42.24f, 0.4f)));
            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.3f).At(new Vector2(42.24f, 0f)));

            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.3f).At(new Vector2(45.25f, 1.6f)));
            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.3f).At(new Vector2(45.25f, 1.2f)));
            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.3f).At(new Vector2(45.25f, 0.8f)));
            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.3f).At(new Vector2(45.25f, 0.5f)));

            scene.AddSprite(new Level(Content, "platform2").Scl((float)2.5f).At(new Vector2(43f, 1.8f)));
            scene.AddSprite(new Level(Content, "platform2").Scl((float)2.5f).At(new Vector2(44.5f, 1.8f)));

            //obstacles:
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(13.00f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(13.22f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(13.44f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(13.66f, -0.10f)));

            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.2f).At(new Vector2(14f, 0.6f)));

            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(13.88f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(14.10f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(14.32f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(14.54f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(14.76f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(14.98f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(15.20f, -0.10f)));

            //
            //scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(48, -0.10f)));
            //scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(48.22f, -0.10f)));
            //scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(48.44f, -0.10f)));
            //scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(48.66f, -0.10f)));
            //scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(48.88f, -0.10f)));
            //scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(49.10f, -0.10f)));
            //scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(49.32f, -0.10f)));
            //scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(49.54f, -0.10f)));
            //scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(49.76f, -0.10f)));
            //scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(49.98f, -0.10f)));
            //

            //extra lives:
            scene.AddSprite(new Level(Content, "lifes").Scl((float)0.5f).At(new Vector2(-1f, 2.3f)));

            //enemies:
            scene.AddSprite(new Level(Content, "crab").Scl((float)1f).At(new Vector2(6, -0.06f)));
            scene.AddSprite(new Level(Content, "crab").Scl((float)1f).At(new Vector2(8, -0.06f)));
            scene.AddSprite(new Level(Content, "crab").Scl((float)1f).At(new Vector2(10, -0.06f)));

            //
            //scene.AddSprite(new Level(Content, "crab").Scl((float)1f).At(new Vector2(1.5f, 1.8f)));
            //scene.AddSprite(new Level(Content, "crab").Scl((float)1f).At(new Vector2(42.75f, -0.06f)));
            //scene.AddSprite(new Level(Content, "crab").Scl((float)1f).At(new Vector2(43.25f, -0.06f)));
            //scene.AddSprite(new Level(Content, "crab").Scl((float)1f).At(new Vector2(43.75f, -0.06f)));
            //scene.AddSprite(new Level(Content, "crab").Scl((float)1f).At(new Vector2(44.25f, -0.06f)));
            //scene.AddSprite(new Level(Content, "crab").Scl((float)1f).At(new Vector2(44.75f, -0.06f)));
            //

            for (float i = -100; i < 100;  i+= 16)
            {
                scene.AddSprite(new Sprite(Content, "sand").Scl((float)Camera.worldWidth * 3.9f)
            .At(new Vector2(i, -0.790f))); //-0.740f  // de 16 em 16(x)
            }

            for (float i = -100; i < 100; i += 16)
            {
                scene.AddSprite(new Sprite(Content, "nuvens").Scl((float)Camera.worldWidth * 3.2f)
            .At(new Vector2(i, 4.2f)));   // de 16 em 16(x)
            }

            scene.AddSprite(new Pickups(Content).At(new Vector2(1, 1)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(4, 1)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(5, 2)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(10, 2)));

            scene.AddSprite(new Pickups(Content).At(new Vector2(20, 0)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(20.2f, 0)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(20.4f, 0)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(20.6f, 0)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(20.8f, 0)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(21.0f, 0)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(21.2f, 0)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(21.4f, 0)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(21.6f, 0)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(-4f, 1.9f)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(15.20f, 0.4f)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(30f, 0.4f)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(30.8f, 0.5f)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(32f, 0.6f)));

            //
            //scene.AddSprite(new Pickups(Content).At(new Vector2(43.75f, 1.8f)));
            //scene.AddSprite(new Pickups(Content).At(new Vector2(43.75f, 1.4f)));
            //scene.AddSprite(new Pickups(Content).At(new Vector2(43.75f, 1)));
            //scene.AddSprite(new Pickups(Content).At(new Vector2(43.75f, 0.6f)));
            //

            //Sound Effects:

            //Music:
            musiclevel1 = Content.Load<Song>("[Music] Sonic Generations - Angel Island Zone -Jukebox-");

            scene.AddSprite(new Character(Content, spriteBatch));
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
            spriteBatch.Draw(icon, new Rectangle(4, 550, 50, 50), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
