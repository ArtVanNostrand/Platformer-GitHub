﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Character : Sprite
    {
        int jumpflag = 0, slamflag = 0, dashflag = 0, contdash = 0, directionfaced = 1, health=3, score=0;
        float jumptime = 0f, dashcooldown = 5, bluestarcooldown=0, movtimer = 0, auxmov = 0f, auxsalto=0f;
        bool ZPressed = false;
        public bool canjump = false;
        //public int hearts;

        Texture2D hearts;
        SoundEffect soundjump, soundslam, soundboom, soundwaterget;
        SpriteFont fontquartz;
        SpriteBatch spriteBatch;

        public Character(ContentManager content, SpriteBatch spriteBatch) : base(content,"sonicstill")
        {
            this.spriteBatch = spriteBatch;
            this.EnableCollisions();
            this.Scl(0.4f);
            //AnimatedSprite animated = new AnimatedSprite(content, "SonicCorrerInicio", 1, 4);
            //animated.Scl(0.4f);
            //animated.EnableCollisions();


            //Sound Effects:
            soundjump = content.Load<SoundEffect>("soundeffectjump");
            soundslam = content.Load<SoundEffect>("soundslam");
            soundboom = content.Load<SoundEffect>("soundboom");
            soundwaterget = content.Load<SoundEffect>("soundgetdrop");


            //Assets:
            hearts = content.Load<Texture2D>("lifes");


            //Fonts:
            fontquartz = content.Load<SpriteFont>("fontquartz");

        }




        public override void SetScene(Scene s)
        {
            this.scene = s;
         
        }

        public override void Update(GameTime gameTime){

           //timers
           jumptime += (float)gameTime.ElapsedGameTime.TotalSeconds;
           dashcooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;
           bluestarcooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 tpos = Camera.WorldPoint2Pixels(position);


            //float a = (float)mpos.Y - tpos.Y;
            //float l = (float)mpos.X - tpos.X;
            //float rot = (float)Math.Atan2(a, l);
            //turret.SetRotation(rot+(float)Math.PI/2f);

            movimento(gameTime);

            //Draw(gameTime);

            //habilidades
            jump();
            slam();
            dash();


            Camera.SetTarget(this.position);

            base.Update(gameTime);

        }


        public override void Draw(GameTime gameTime)
        {
           

            spriteBatch.DrawString(fontquartz, "Score:" + score, new Vector2(10f, 10f), Color.Black);

            if (health > 0)
            {
                spriteBatch.Draw(hearts, new Vector2(30f, 30f));
            }
            if (health > 1)
            {
                spriteBatch.Draw(hearts, new Vector2(40f, 30f));
            }
            if (health > 2)
            {
                spriteBatch.Draw(hearts, new Vector2(50f, 30f));
            }


            base.Draw(gameTime);
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
                    ReplaceImage("sonicstill");
                }
                else
                {
                    ReplaceImage("sonicstill");
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
                    ReplaceImage("sonicstillR");
                }
                else
                {
                    ReplaceImage("sonicstillR");
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
                        ReplaceImage("sonicstill");
                    }
                    else
                    {
                        ReplaceImage("sonicstillR");
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
                    if ((other.name == "platform1" || other.name == "platform2") && this.position.Y > other.position.Y + 0.38f &&
                        (this.position.X - other.position.X) <= 0.3f)
                    {
                        this.position.Y += auxsalto;
                        canjump = true;
                    }
                    if (other.name == "crab")
                    {
                        if (jumpflag == 0)
                        {
                            health = health - 1;
                        }
                        if (jumpflag == 1)
                        {
                            other.Destroy();
                            Sprite explosion;
                            explosion = new Sprite(cmanager, "explosao30x30");
                            scene.AddSprite(explosion);
                            explosion.SetPosition(other.position);
                            explosion.Scale(0.2f);

                            score = score + 10;
                            soundboom.Play();
                        }
                    }
                    if (other.name == "3spikes")
                    {
                        health = health - 1;
                        //soundgethit().Play();
                    }
                    if (other.name == "imagewaterdrop2")
                    {
                        other.Destroy();
                        soundwaterget.Play();
                        Sprite bluestars;
                        bluestars = new Sprite(cmanager, "bluesparks");
                        scene.AddSprite(bluestars);
                        bluestars.SetPosition(other.position);
                        bluestars.Scale(0.2f);
                    }
                    else
                    {
                        if (other.position.Y > this.position.Y)
                        {
                            this.position.Y -= auxsalto;
                            jumptime = 0.51f;
                        }
                    }
                }
                //colidir

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
                        ReplaceImage("sonicstill");
                    }
                    else
                    {
                        ReplaceImage("sonicstillR");
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
