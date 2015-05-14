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
        Texture2D life, icon, box;
        SpriteBatch spriteBatch;
        SpriteFont fontquartz;
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
            fontquartz = Content.Load<SpriteFont>("fontquartz");
            box = Content.Load<Texture2D>("bloco");

            SlidingBackground fundo = new SlidingBackground(Content, "oceano");
            scene.AddBackground(fundo);

            //platforms:
            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.3f).At(new Vector2(4, 0)));
            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.3f).At(new Vector2(5, 1)));

            scene.AddSprite(new Level(Content, "platform2").Scl((float)2.5f).At(new Vector2(2, 1.5f)));
            scene.AddSprite(new Level(Content, "platform2").Scl((float)2.5f).At(new Vector2(1f, 1.5f)));
            scene.AddSprite(new Level(Content, "platform2").Scl((float)2.5f).At(new Vector2(0, 1.5f)));
            scene.AddSprite(new Level(Content, "platform2").Scl((float)2.5f).At(new Vector2(-1, 1.5f)));

            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, -0.1f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, 0.3f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, 0.7f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, 1.1f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, 1.5f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, 1.9f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, 2.3f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, 2.7f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, 3.1f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, 3.6f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, 4.1f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, 4.6f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, 5.1f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, 5.6f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, 6.1f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, 6.6f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, 7.1f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, 7.6f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(-4, 8.1f)));


            scene.AddSprite(new Level(Content, "imagerock1").At(new Vector2(30f, -0.1f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(31.35f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(31.57f, -0.10f)));
            scene.AddSprite(new Level(Content, "imagerock2").Scl((float)1.15f).At(new Vector2(30.8f, 0f)));
            scene.AddSprite(new Level(Content, "imagerock3").Scl((float)1.3f).At(new Vector2(32f, 0.1f)));

            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.3f).At(new Vector2(40f, 0.1f)));
            scene.AddSprite(new Level(Content, "platform1").Scl((float)1.3f).At(new Vector2(41f, 1f)));

            scene.AddSprite(new Level(Content, "platform3").Scl((float)1.3f).At(new Vector2(42.24f, 1.6f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(42.24f, 1.2f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(42.24f, 0.8f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(42.24f, 0.4f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(42.24f, 0f)));

            scene.AddSprite(new Level(Content, "platform3").Scl((float)1.3f).At(new Vector2(45.25f, 1.6f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(45.25f, 1.2f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(45.25f, 0.8f)));
            scene.AddSprite(new Level(Content, "platform4").Scl((float)1.3f).At(new Vector2(45.25f, 0.5f)));


            scene.AddSprite(new Level(Content, "platform2").Scl((float)2.5f).At(new Vector2(49f, 2.3f)));
            scene.AddSprite(new Level(Content, "lifes").Scl((float)0.5f).At(new Vector2(49f, 2.5f)));



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
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(48, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(48.22f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(48.44f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(48.66f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(48.88f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(49.10f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(49.32f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(49.54f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(49.76f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(49.98f, -0.10f)));
            //

            scene.AddSprite(new Enemy(Content, "spriteenemy2").At(new Vector2(51f, -0.06f)));
            scene.AddSprite(new Enemy(Content, "spriteenemy2").At(new Vector2(52f, -0.06f)));

            //
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(53f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(53.22f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(53.44f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(53.66f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(53.88f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(54.10f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(54.32f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(54.54f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(54.76f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(54.98f, -0.10f)));
            //

            scene.AddSprite(new Pickups(Content).At(new Vector2(55.5f, 0.1f)));

            //
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(56f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(56.22f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(56.44f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(56.66f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(56.88f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(57.10f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(57.32f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(57.54f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(57.76f, -0.10f)));
            scene.AddSprite(new Level(Content, "3spikes").Scl((float)0.55f).At(new Vector2(57.98f, -0.10f)));
            //


            //extra lives:
            scene.AddSprite(new Level(Content, "lifes").Scl((float)0.5f).At(new Vector2(-1f, 2.3f)));

            //enemies:

            scene.AddSprite(new Enemy(Content, "crab").At(new Vector2(6f, -0.06f)));

            scene.AddSprite(new Level(Content, "crab").Scl((float)1f).At(new Vector2(8, -0.06f)));
            scene.AddSprite(new Level(Content, "crab").Scl((float)1f).At(new Vector2(10, -0.06f)));

            scene.AddSprite(new Level(Content, "crab").Scl((float)1f).At(new Vector2(42.75f, -0.06f)));
            scene.AddSprite(new Level(Content, "crab").Scl((float)1f).At(new Vector2(43.25f, -0.06f)));
            scene.AddSprite(new Level(Content, "crab").Scl((float)1f).At(new Vector2(43.75f, -0.06f)));
            scene.AddSprite(new Level(Content, "crab").Scl((float)1f).At(new Vector2(44.25f, -0.06f)));
            scene.AddSprite(new Level(Content, "crab").Scl((float)1f).At(new Vector2(44.75f, -0.06f)));
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
            scene.AddSprite(new Pickups(Content).At(new Vector2(15.20f, 0.4f)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(30f, 0.4f)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(30.8f, 0.5f)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(32f, 0.6f)));

            //
            scene.AddSprite(new Pickups(Content).At(new Vector2(43.75f, 1.8f)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(43.75f, 1.4f)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(43.75f, 1)));
            scene.AddSprite(new Pickups(Content).At(new Vector2(43.75f, 0.6f)));
            //

            //Sound Effects:

            //Music:
            musiclevel1 = Content.Load<Song>("[Music] Sonic Generations - Angel Island Zone -Jukebox-");

            sonic = new Character(Content, spriteBatch, graphics);
            scene.AddSprite(sonic);
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
            if (sonic.gameover == 0) scene.Update(gameTime);
            else MediaPlayer.Stop();
            base.Update(gameTime);
        }

        private void DrawRectangle(Rectangle r, Color c)
        {
            spriteBatch.Draw(box, new Rectangle(r.X, r.Y, r.Width, 1), c);
            spriteBatch.Draw(box, new Rectangle(r.X, r.Y, 1, r.Height), c);
            spriteBatch.Draw(box, new Rectangle(r.X, r.Y + r.Height - 1, r.Width, 1), c);
            spriteBatch.Draw(box, new Rectangle(r.X + r.Width - 1, r.Y, 1, r.Height), c);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (sonic.gameover == 0) scene.Draw(gameTime);
          
            spriteBatch.Begin();

            spriteBatch.DrawString(fontquartz, "Time: " + sonic.totaltime, new Vector2(8f, 5f), Color.Black);
            spriteBatch.DrawString(fontquartz, "Score: " + sonic.score, new Vector2(10f, 25f), Color.Black);
            spriteBatch.DrawString(fontquartz, "Magic: " + sonic.magic, new Vector2(10f, 45f), Color.Black);
            if (sonic.gameover == 0) spriteBatch.Draw(icon, new Rectangle(4, 550, 50, 50), Color.White);

            if (sonic.gameover == 1)
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(icon, new Vector2(420, 18));
                spriteBatch.Draw(icon, new Vector2(450, 18));
                spriteBatch.Draw(icon, new Vector2(480, 18));
                spriteBatch.Draw(icon, new Vector2(510, 18));
                spriteBatch.Draw(icon, new Vector2(540, 18));
                spriteBatch.Draw(icon, new Vector2(570, 18));
                spriteBatch.Draw(icon, new Vector2(600, 18));
                spriteBatch.Draw(icon, new Vector2(630, 18));
                spriteBatch.Draw(icon, new Vector2(660, 18));
                spriteBatch.Draw(icon, new Vector2(690, 18));
                spriteBatch.Draw(icon, new Vector2(720, 18));
                spriteBatch.Draw(icon, new Vector2(418, 50));
                spriteBatch.Draw(icon, new Vector2(418, 80));
                spriteBatch.Draw(icon, new Vector2(418, 110));
                spriteBatch.Draw(icon, new Vector2(418, 140));
                spriteBatch.Draw(icon, new Vector2(418, 170));
                spriteBatch.Draw(icon, new Vector2(418, 200));
                spriteBatch.Draw(icon, new Vector2(418, 230));
                spriteBatch.Draw(icon, new Vector2(418, 260));
                spriteBatch.Draw(icon, new Vector2(720, 50));
                spriteBatch.Draw(icon, new Vector2(720, 80));
                spriteBatch.Draw(icon, new Vector2(720, 110));
                spriteBatch.Draw(icon, new Vector2(720, 140));
                spriteBatch.Draw(icon, new Vector2(720, 170));
                spriteBatch.Draw(icon, new Vector2(720, 200));
                spriteBatch.Draw(icon, new Vector2(720, 230));
                spriteBatch.Draw(icon, new Vector2(720, 260));
                spriteBatch.Draw(icon, new Vector2(418, 290));
                spriteBatch.Draw(icon, new Vector2(450, 290));
                spriteBatch.Draw(icon, new Vector2(480, 290));
                spriteBatch.Draw(icon, new Vector2(510, 290));
                spriteBatch.Draw(icon, new Vector2(540, 290));
                spriteBatch.Draw(icon, new Vector2(570, 290));
                spriteBatch.Draw(icon, new Vector2(600, 290));
                spriteBatch.Draw(icon, new Vector2(630, 290));
                spriteBatch.Draw(icon, new Vector2(660, 290));
                spriteBatch.Draw(icon, new Vector2(690, 290));
                spriteBatch.Draw(icon, new Vector2(720, 290));
                DrawRectangle(new Rectangle(450, 50, 269, 239), Color.Red);
                spriteBatch.DrawString(fontquartz, "GAME OVER", new Vector2(530, 70), Color.Blue);
                spriteBatch.DrawString(fontquartz, "Time: " + sonic.totaltime, new Vector2(475, 150), Color.Blue);
                spriteBatch.DrawString(fontquartz, "Magic: " + sonic.magic, new Vector2(475, 180), Color.Blue);
                spriteBatch.DrawString(fontquartz, "Score:" + sonic.score, new Vector2(475, 210), Color.Blue);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
