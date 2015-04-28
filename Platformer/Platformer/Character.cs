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
        float jumptime = 0f, dashcooldown = 5, movtimer = 0, auxmov = 0f, auxsalto=0f;
        bool ZPressed = false;
        public bool canjump = false;

        SoundEffect soundjump, soundslam;


        public Character(ContentManager content) : base(content,"char90v2")
        {
            this.EnableCollisions();
            this.Scl(0.6f);

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

            if (position.Y == 0)
            {
                canjump=true;
            }

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
                if (jumpflag == 0)
                {
                    ReplaceImage("char90v2");
                }
                else
                {
                    ReplaceImage("char90v2saltar");
                }
                movtimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (movtimer > 2f)
                {
                    movtimer = 2f;
                }
                if (movtimer > 0f && movtimer < 0.5f)
                {
                    this.position.X += 0.05f;
                    auxmov = 0.05f;
                }
                if (movtimer > 0.5f && movtimer < 1f)
                {
                    this.position.X += 0.062f;
                    auxmov = 0.062f;
                }
                if (movtimer > 1f && movtimer < 1.5f)
                {
                    this.position.X += 0.074f;
                    auxmov = 0.074f;
                }
                if (movtimer > 1.5f && movtimer < 2f)
                {
                    this.position.X += 0.082f;
                    auxmov = 0.082f;
                }
                if (movtimer == 2)
                {
                    this.position.X += 0.09f;
                    auxmov = 0.09f;
                }
                Sprite other;
                Vector2 colPosition;
                if (scene.Collides(this, out other, out colPosition))
                {
                    this.position.X -= auxmov;
                }

            }
            if (state.IsKeyDown(Keys.Left))
            {
                if (directionfaced == 1)
                {
                    movtimer = 0f;
                }
                directionfaced = 2;
                if (jumpflag == 0)
                {
                    ReplaceImage("char90v2inv");
                }
                else
                {
                    ReplaceImage("char90v2saltarinv");
                }
                movtimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (movtimer > 2f)
                {
                    movtimer = 2f;
                }
                if (movtimer > 0f && movtimer < 0.5f)
                {
                    this.position.X -= 0.05f;
                    auxmov = 0.05f;
                }
                if (movtimer > 0.5f && movtimer < 1f)
                {
                    this.position.X -= 0.062f;
                    auxmov = 0.062f;
                }
                if (movtimer > 1f && movtimer < 1.5f)
                {
                    this.position.X -= 0.074f;
                    auxmov = 0.074f;
                }
                if (movtimer > 1.5f && movtimer < 2f)
                {
                    this.position.X -= 0.082f;
                    auxmov = 0.082f;
                }
                if (movtimer == 2)
                {
                    this.position.X -= 0.09f;
                    auxmov = 0.09f;
                }
                Sprite other;
                Vector2 colPosition;
                if (scene.Collides(this, out other, out colPosition))
                {
                    this.position.X += auxmov;
                }
             


            }



        }

        void jump()
        {
            //clicar saltar
            if (canjump==true)
            {
                if (Keyboard.GetState().IsKeyUp(Keys.Z) && canjump==true) ZPressed = false;
                if (ZPressed == false && Keyboard.GetState().IsKeyDown(Keys.Z))
                {

                    if (directionfaced == 1)
                    {
                        ReplaceImage("char90v2saltar");
                    }
                    else
                    {
                        ReplaceImage("char90v2saltarinv");
                    }
                    canjump = false;
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
                    auxsalto = 0.03f;
                }
                if (jumptime > 0.62f && jumptime < 0.75f)
                {
                    this.position.Y -= 0.04f;
                    auxsalto = 0.04f;
                }
                if (jumptime > 0.75f)
                {
                    this.position.Y -= 0.055f;
                    auxsalto = 0.055f;
                }
                Sprite other;
                Vector2 colPosition;
                //collidir
                if (scene.Collides(this, out other, out colPosition))
                {
                    if (other.name == "imagewaterdrop2")
                    {
                       other.Destroy();
                    }
                    this.position.Y += auxsalto;
                    if (directionfaced == 1)
                    {
                        ReplaceImage("char90v2");
                    }
                    else
                    {
                        ReplaceImage("char90v2inv");
                    }
                    canjump = true;
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
                    if (directionfaced == 1)
                    {
                        ReplaceImage("char90v2");
                    }
                    else
                    {
                        ReplaceImage("char90v2inv");
                    }
                    jumptime = 0f;
                    canjump = true;
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
                //ReplaceImage("charv1j2");
                this.position.Y -= 0.14f;
                if (this.position.Y < 0f)
                {
                    this.position.Y = 0f;
                    //ReplaceImage("charv1");
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
