using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Character : Sprite
    {

        int jumpflag = 0, slamflag = 0, dashflag = 0, contdash = 0, directionfaced=1;
        float jumptime = 0f, dashcooldown=5, movtimer=0;
        bool ZPressed = false;

        SoundEffect soundjump, soundslam;


        public Character(ContentManager content) : base(content,"imageplaceholderchar")
        {

            this.Scl(0.8f);

            //Sound Effects:
            soundjump = content.Load<SoundEffect>("soundeffectjump");
            soundslam = content.Load<SoundEffect>("soundslam");
        }


        public override void Draw(GameTime gameTime){

            base.Draw(gameTime);

        }

        public override void SetScene(Scene s)
        {
            this.scene = s;
         
        }

        public override void Update(GameTime gameTime){

           //timers
           jumptime += (float)gameTime.ElapsedGameTime.TotalSeconds;
           dashcooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 tpos = Camera.WorldPoint2Pixels(position);

            //float a = (float)mpos.Y - tpos.Y;
            //float l = (float)mpos.X - tpos.X;
            //float rot = (float)Math.Atan2(a, l);
            //turret.SetRotation(rot+(float)Math.PI/2f);

            movimento(gameTime);

            //habilidades
            jump();
            slam();
            dash();


            Camera.SetTarget(this.position);

            base.Update(gameTime);

        }

        void movimento(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            //apenas para testing
            if (state.IsKeyDown(Keys.T))
            {
                this.position.Y += 0.08f;
            }

            //movimento basico
            if (state.IsKeyDown(Keys.Right))
            {
                if (directionfaced == 2)
                {
                    movtimer = 0f;
                }
                directionfaced = 1;
                movtimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (movtimer > 2f)
                {
                    movtimer = 2f;
                }
                if (movtimer > 0f && movtimer < 0.5f)
                {
                    this.position.X += 0.05f;
                }
                if (movtimer > 0.5f && movtimer < 1f)
                {
                    this.position.X += 0.062f;
                }
                if (movtimer > 1f && movtimer < 1.5f)
                {
                    this.position.X += 0.074f;
                }
                if (movtimer > 1.5f && movtimer < 2f)
                {
                    this.position.X += 0.082f;
                }
                if (movtimer == 2)
                {
                    this.position.X += 0.09f;
                }

            }
            if (state.IsKeyDown(Keys.Left))
            {
                if (directionfaced == 1)
                {
                    movtimer = 0f;
                }
                directionfaced = 2;
                movtimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (movtimer > 2f)
                {
                    movtimer = 2f;
                }
                if (movtimer > 0f && movtimer < 0.5f)
                {
                    this.position.X -= 0.05f;
                }
                if (movtimer > 0.5f && movtimer < 1f)
                {
                    this.position.X -= 0.062f;
                }
                if (movtimer > 1f && movtimer < 1.5f)
                {
                    this.position.X -= 0.074f;
                }
                if (movtimer > 1.5f && movtimer < 2f)
                {
                    this.position.X -= 0.082f;
                }
                if (movtimer == 2)
                {
                    this.position.X -= 0.09f;
                }
            }



        }

        void jump()
        {
            //clicar saltar
            if (jumpflag == 0)
            {
                if (Keyboard.GetState().IsKeyUp(Keys.Z)) ZPressed = false;
                if (ZPressed == false && Keyboard.GetState().IsKeyDown(Keys.Z))
                {
                    ReplaceImage("placeholderjumping");
                    soundjump.Play();
                    ZPressed = true;
                    jumptime = 0f;
                    this.position.Y += 0.03f;
                    jumpflag = 1;
                }
            }
            //clicar saltar

            //ascender
            if (jumpflag > 0 && jumptime <0.35f)
            {
                this.position.Y += 0.04f;
            }
            if (jumpflag > 0 && jumptime >0.35f && jumptime < 0.45f)
            {
                this.position.Y += 0.03f;
            }
            if (jumpflag > 0 && jumptime > 0.45f && jumptime < 0.50f)
            {
                this.position.Y += 0.02f;
            }
            //ascender

            //descender
            if (jumpflag > 0)
            {
                if (jumptime > 0.50f && jumptime <0.62f)
                {
                    this.position.Y -= 0.03f;
                }
                if (jumptime > 0.62f && jumptime < 0.75f)
                {
                    this.position.Y -= 0.04f;
                }
                if (jumptime > 0.75f)
                {
                    this.position.Y -= 0.055f;
                }
            }
            //descender

            //para
            if (jumpflag > 0)
            {
                if (this.position.Y <= 0f)
                {
                    if (this.position.Y < 0f)
                    {
                        this.position.Y = 0f;
                    }
                    ReplaceImage("imageplaceholderchar");
                    jumptime = 0f;
                    jumpflag = 0;
                }
            }
            //para
        }


        void slam()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && this.position.Y > 0f)
            {
                if (slamflag == 0)
                {
                    soundslam.Play();
                    slamflag = 1;
                }
                ReplaceImage("placeholderjumping");
                this.position.Y -= 0.14f;
                if (this.position.Y < 0f)
                {
                    this.position.Y = 0f;
                    ReplaceImage("imageplaceholderchar");
                    slamflag = 0;
                }
            }
        }


        void dash()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.X) && dashflag==0 && dashcooldown>5f)
            {
                dashflag = 1;
                soundslam.Play();
                movtimer = 2f;
            }
            if (dashflag == 1)
            {
                if (directionfaced == 1)
                {
                    this.position.X += 0.25f;
                }
                if (directionfaced == 2)
                {
                    this.position.X -= 0.25f;
                }
                contdash++;
                if (contdash == 5)
                {
                    contdash = 0;
                    dashflag = 0;
                    dashcooldown = 0f;
                }
            }

        }


    }
}
