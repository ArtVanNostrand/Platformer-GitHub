using Microsoft.Xna.Framework;
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
        float invincibilityflashtime = 3f, totaltime=0f;
        bool ZPressed = false;
        public bool canjump = false;
        float[] distPlatforms = new float[3];

        Texture2D hearts;
        SoundEffect soundjump, soundslam, soundboom, soundwaterget, soundgethit, soundgameover;
        SpriteFont fontquartz;
        SpriteBatch spriteBatch;


        public Character(ContentManager content, SpriteBatch spriteBatch) : base(content,"sonicstill")
        {
            this.spriteBatch = spriteBatch;
            this.EnableCollisions();
            this.Scl(0.3f);
            //AnimatedSprite animated = new AnimatedSprite(content, "SonicCorrerInicio", 1, 4);
            //animated.Scl(0.4f);
            //animated.EnableCollisions();


            //Images:
            hearts = content.Load<Texture2D>("lifes");


            //Fonts:
            fontquartz = content.Load<SpriteFont>("fontquartz");


            //Sound Effects:
            soundjump = content.Load<SoundEffect>("soundeffectjump");
            soundslam = content.Load<SoundEffect>("soundslam");
            soundboom = content.Load<SoundEffect>("soundboom");
            soundwaterget = content.Load<SoundEffect>("soundgetdrop");
            soundgethit = content.Load<SoundEffect>("soundgethit");
            soundgameover = content.Load<SoundEffect>("soundgameover");

        }




        public override void SetScene(Scene s)
        {
            this.scene = s;
         
        }

        void timers(GameTime gameTime)
        {
            totaltime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            jumptime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            dashcooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;
            bluestarcooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;
            invincibilityflashtime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        }

        void gettinghit()
        {

            if (invincibilityflashtime > 3f)
            {
                health = health - 1;
                score = score - 100;
                invincibilityflashtime = 0f;
                soundgethit.Play();

                if (health < 1)
                {
                    //gameover
                    soundgameover.Play();
                }
            }

            

        }

        void HUD(GameTime gameTime)
        {

            spriteBatch.DrawString(fontquartz, "Time:" + totaltime, new Vector2(21f, 0f), Color.Black);
            spriteBatch.DrawString(fontquartz, "Score:" + score, new Vector2(25f, 15f), Color.Black);

            if (health > 0)
            {
                spriteBatch.Draw(hearts, new Vector2(15f, 90f));
            }
            if (health > 1)
            {
                spriteBatch.Draw(hearts, new Vector2(45f, 90f));
            }
            if (health > 2)
            {
                spriteBatch.Draw(hearts, new Vector2(75f, 90f));
            }


        }

        public override void Update(GameTime gameTime){

            Vector2 tpos = Camera.WorldPoint2Pixels(position);

            //float a = (float)mpos.Y - tpos.Y;
            //float l = (float)mpos.X - tpos.X;
            //float rot = (float)Math.Atan2(a, l);
            //turret.SetRotation(rot+(float)Math.PI/2f);

            timers(gameTime);
            movimento(gameTime);

            //habilidades
            jump();
            slam();
            dash();


            Camera.SetTarget(this.position);

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            HUD(gameTime);
         
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

            distPlatforms[1] = 0.3f;
            distPlatforms[2] = 0.55f;

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
                    if (this.position.Y > other.position.Y)
                    {
                        if (other.name == "platform1" && (this.position.X - other.position.X) <= distPlatforms[1] ||
                            other.name == "platform2" && (this.position.X - other.position.X) <= distPlatforms[2])
                        {
                            this.position.Y += auxsalto;
                            canjump = true;
                        }
                    }
                    if (other.name == "crab")
                    {
                        if (jumpflag == 0 && dashflag==0 && slamflag==0)
                        {
                            gettinghit();
                        }
                        if (jumpflag == 1 || dashflag==1 || slamflag==1)
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
                        gettinghit();
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

                        score = score + 1;
                    }
                    else
                    {
                        if (other.position.Y > this.position.Y)
                        {
                            if (other.name != "platform2") this.position.Y -= auxsalto;
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

            if (Keyboard.GetState().IsKeyDown(Keys.X) && dashflag==0 && dashcooldown>2f)
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
                    Sprite other;
                    Vector2 colPosition;
                    
                    //para nao deixar passar a frente de obstaculos/objetos
                    if (scene.Collides(this, out other, out colPosition))
                    {
                        do{
                        this.position.X -= 0.01f;
                        } while (scene.Collides(this, out other, out colPosition));

                        dashflag = 0;
                    }
                    //
                }
                if (directionfaced == 2)
                {
                
                    this.position.X -= 0.25f;
                    Sprite other;
                    Vector2 colPosition;

                    if (scene.Collides(this, out other, out colPosition))
                    {
                        do
                        {
                            this.position.X += 0.01f;
                        } while (scene.Collides(this, out other, out colPosition));
                        dashflag = 0;
                    }
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
