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
    class Character : AnimatedSprite
    {

        int jumpflag = 0, slamflag = 0, dashflag = 0, contdash = 0, xd = 0, directionfaced = 1, estado = 0, flagSalto = 0;
        int health = 3, score = 0, magic = 0, totalmagic = 0, explosioncont = 0, flagPlatf = 0;
        int afterimageflag = 0;
        int[] bluestarstimerflag = new int[1000], explosiontimerflag = new int[1000], afterimagecreated = new int[7];
        float jumptime = 0f, dashcooldown = 5, bluestarcooldown=0, holdtime = 0f, auxmov = 0f, auxsalto=0f;
        float invincibilityflashtime = 3f, dashflagtimer = 1f, totaltime = 0f, stunlock = 0f, afterimagetimer = 9f;
        float[] bluestarstimer = new float[1000], explosiontimer = new float[1000];
        float[] distPlatforms = new float[4];
        float[] altPlatforms = new float[6];
        bool ZPressed = false;
        public bool canjump = false;

        int holddirection = 0;
        float accel = 60;

        Texture2D hearts;
        SoundEffect soundjump, soundslam, soundboom, soundwaterget, soundgethit, soundgameover, sound1up;
        SpriteFont fontquartz;
        SpriteBatch spriteBatch;
        Sprite[] bluestars = new Sprite[999], explosion = new Sprite[999], afterimage= new Sprite[7];


        public Character(ContentManager content, SpriteBatch spriteBatch) : base(content, "sonicstill", 1, 1)
        {
            this.spriteBatch = spriteBatch;
            this.EnableCollisions();
            this.Scl(0.3f);

            //Platforms width
            distPlatforms[1] = 0.3f;
            distPlatforms[2] = 0.55f;
            distPlatforms[3] = 0.35f;

            //Platforms height
            altPlatforms[1] = 0.44f;
            altPlatforms[2] = 0.34f;
            altPlatforms[3] = 0.33f;
            altPlatforms[4] = 0.42f;
            altPlatforms[5] = 0.56f;

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
            sound1up = content.Load<SoundEffect>("sound1up");

        }




        public override void SetScene(Scene s)
        {
            this.scene = s;
        }

        
        //public override void SetEnemy(Enemy e)
        //{
        //    this.enemy2 = e;
        //}



        void timers(GameTime gameTime)
        {
            totaltime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            jumptime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            dashcooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;
            bluestarcooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;
            invincibilityflashtime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            dashflagtimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            afterimagetimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            stunlock += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //holdtime -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int x = 0; x < 999; x++)
            {
                if (explosiontimerflag[x] == 1)
                {
                    explosiontimer[x]++;
                }
                if (explosiontimer[x] > 7)
                {
                    explosion[x].Destroy();
                }
            }

            for (int x = 0; x < 999; x++)
            {
                if (bluestarstimerflag[x] == 1)
                {
                    bluestarstimer[x]++;
                }
                if (bluestarstimer[x] > 7)
                {
                    bluestars[x].Destroy();
                }
            }


            if (afterimageflag == 1)
            {
                if (afterimagetimer > 0.2f)
                {
                    afterimageflag = 2;
                }

            }

            if (afterimageflag == 2)
            {
                for (int x = 1; x < 6; x++)
                {
                    if (afterimagecreated[x] == 1)
                    {
                        afterimage[x].Destroy();
                        afterimagecreated[x] = 0;
                    }
                }
                afterimageflag = 0;
            }
        }

        void gettinghit()
        {

            if (invincibilityflashtime > 3f)
            {
                holdtime = 0f;
                health -= 1;
                score -= 100;
                invincibilityflashtime = 0f;
                stunlock = 0f;
                soundgethit.Play();

                if (health < 1)
                {
                    soundgameover.Play();
                }
            }

            

        }

        void HUD(GameTime gameTime)
        {

            spriteBatch.DrawString(fontquartz, "Time: " + totaltime, new Vector2(8f, 5f), Color.Black);
            spriteBatch.DrawString(fontquartz, "Score: " + score, new Vector2(10f, 25f), Color.Black);
            spriteBatch.DrawString(fontquartz, "Magic: " + magic, new Vector2(10f, 45f), Color.Black);

            if (health > 0)
            {
                spriteBatch.Draw(hearts, new Vector2(50f,560f));
            }
            if (health > 1)
            {
                spriteBatch.Draw(hearts, new Vector2(65f, 560f));
            }
            if (health > 2)
            {
                spriteBatch.Draw(hearts, new Vector2(80f, 560f));
            }
            if (health > 3)
            {
                spriteBatch.Draw(hearts, new Vector2(95f, 560f));
            }
            if (health > 4)
            {
                spriteBatch.Draw(hearts, new Vector2(110f, 560f));
            }


        }

        public override void Update(GameTime gameTime){

            Vector2 tpos = Camera.WorldPoint2Pixels(position);

            if (flagSalto == 1) calcAnimInterval(3);
            else calcAnimInterval(holdtime);

            timers(gameTime);
            movimento(gameTime);
            //enemyai();
            colisao2();

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

        //void enemyai()
        //{
        //    Sprite enemy2;
        //    if (enemy.PlayerDetectionleft(this, out enemy2))
        //    {
        //        if (enemy2.name == "crab")
        //        {
        //            enemy2.position.X = enemy2.position.X + 0.05f;
        //        }

        //    }

            


        //}

        void movimento(GameTime gameTime)
        {
            holddirection = 0;
            if (stunlock > 0.2f)
            {
                KeyboardState state = Keyboard.GetState();

                if (position.Y == 0)
                {
                    canjump = true;
                }

                //apenas para testing
                if (state.IsKeyDown(Keys.I))
                {
                    this.position.Y += 0.3f;
                }
                if (state.IsKeyDown(Keys.K))
                {
                    this.position.Y -= 0.3f;
                }
                if (state.IsKeyDown(Keys.L))
                {
                    this.position.X += 0.3f;
                }
                if (state.IsKeyDown(Keys.J))
                {
                    this.position.X -= 0.3f;
                }
                //apenas para testing
                if (state.IsKeyDown(Keys.Y))
                {
                    magic = magic + 10;
                }
                //apenas para testing
                if (state.IsKeyDown(Keys.U))
                {
                    health++;
                }

                //movimento basico
                if (state.IsKeyDown(Keys.Right))
                {
                    holdtime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    holddirection = 1;

                    if (directionfaced == 2)
                    {
                        if (jumpflag == 0)
                        {
                            stunlock = 0.08f;
                        }
                        holdtime = 0f;
                    }
                    directionfaced = 1;
                    if (jumpflag == 0 || flagPlatf == 1)
                    {
                        if (estado != 3)
                        {
                            ReplaceImage("SonicCorrerDireita", 1, 4);
                            this.Scale(1.6f);
                        }
                        estado = 3;
                    }
                    else
                    {
                        if (estado != 1 && jumpflag == 0) ReplaceImage("sonicstill", 1, 1);
                        estado = 1;
                    }



                }
                else if (state.IsKeyDown(Keys.Left))
                {
                    holddirection = 2;
                    holdtime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (directionfaced == 1)
                    {
                        holdtime = 0f;
                        if (jumpflag == 0)
                        {
                            stunlock = 0.08f;
                        }
                    }
                    directionfaced = 2;
                    if (jumpflag == 0 || flagPlatf == 1)
                    {
                        if (estado != 4)
                        {
                            ReplaceImage("SonicCorrerEsquerda", 1, 4);
                            this.Scale(1.6f);
                        }
                        estado = 4;
                    }
                    else
                    {
                        if (estado != 2 && jumpflag == 0) ReplaceImage("sonicstillR", 1, 1);
                        estado = 2;
                    }





                }

                else
                {
                    if (estado == 3 && holdtime == 0)
                    {
                        ReplaceImage("sonicstill", 1, 1);
                        estado = 1;
                    }
                    else if (estado == 4 && holdtime == 0)
                    {
                        ReplaceImage("sonicstillR", 1, 1);
                        estado = 2;
                    }
                }

                aceleracao(gameTime);
                Sprite other;
                Vector2 colPosition;
                if (scene.Collides(this, out other, out colPosition))
                {
                    if (other.name != "imagewaterdrop2" && other.name != "crab" && other.name != "spriteenemy2")
                    {
                        if (directionfaced == 1)
                        {
                            this.position.X -= accel;
                        }
                        else
                        {
                            this.position.X += accel;
                        }
                    }

                }
            }

        }

        void colisao1()
        {

            Sprite other;
            Vector2 colPosition;
            if (scene.Collides(this, out other, out colPosition) && other.name != "imagewaterdrop2" && other.name != "lifes")
            {
                if (this.position.Y > other.position.Y)
                {
                    if (other.name == "platform1" && (this.position.X - other.position.X) <= distPlatforms[1] ||
                        other.name == "platform2" && (this.position.X - other.position.X) <= distPlatforms[2]||
                        (other.name == "imagerock1" || other.name == "imagerock2" || other.name == "imagerock3" || other.name == "3spikes") &&
                        (this.position.X - other.position.X) <= distPlatforms[3])
                    {
                        if (directionfaced == 1 && flagPlatf == 0)
                        {
                            ReplaceImage("sonicstill", 1, 1);
                        }
                        else if (flagPlatf == 0)
                        {
                            ReplaceImage("sonicstillR", 1, 1);
                        }
                        if (other.name == "platform1") this.position.Y = other.position.Y + altPlatforms[1];
                        else if (other.name == "platform2") this.position.Y = other.position.Y + altPlatforms[2];
                        else if (other.name == "imagerock1") this.position.Y = other.position.Y + altPlatforms[3];
                        else if (other.name == "imagerock2") this.position.Y = other.position.Y + altPlatforms[4];
                        else if (other.name == "imagerock3") this.position.Y = other.position.Y + altPlatforms[5];
                        else if (other.name == "3spikes") this.position.Y = other.position.Y + 0.32f;
                        canjump = true;
                        flagPlatf = 1;
                        flagSalto = 0;
                    }

                }

                if (other.name == "3spikes")
                {
                    gettinghit();
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
         
        }

        void colisao2()
        {
           Sprite other;
           Vector2 colPosition;

                if (scene.Collides(this, out other, out colPosition))
                {
                    if (other.name == "imagewaterdrop2")
                    {
                        score = score + 2;
                        magic = magic + 1;
                        totalmagic = totalmagic + 1;

                        other.Destroy();
                        soundwaterget.Play();

                        bluestars[totalmagic] = new Sprite(cmanager, "bluesparks");
                        scene.AddSprite(bluestars[totalmagic]);
                        bluestars[totalmagic].SetPosition(other.position);
                        bluestarstimerflag[totalmagic] = 1;
                        bluestars[totalmagic].Scale(0.12f);

                    }
                    else if (other.name == "lifes")
                    {
                        other.Destroy();
                        sound1up.Play();
                        health = health + 1;


                    }
                    else if (other.name == "crab" || other.name == "spriteenemy2")
                    {

                        if (jumpflag == 1 || dashflag == 1 || slamflag == 1)
                        {
                            other.Destroy();
                            explosioncont++;
                            explosiontimerflag[explosioncont] = 1;
                            explosion[explosioncont] = new Sprite(cmanager, "explosao30x30");
                            scene.AddSprite(explosion[explosioncont]);
                            explosion[explosioncont].SetPosition(other.position);
                            explosion[explosioncont].Scale(0.2f);

                            score = score + 10;
                            soundboom.Play();
                        }
                        else
                        {
                            if (invincibilityflashtime > 3f)
                            {
                                gettinghit();
                                if (directionfaced == 1)
                                {
                                    position.X -= 0.4f;
                                }
                                else
                                {
                                    position.X += 0.4f;
                                }
                            }
                        }
                    }


                }
        }





        void aceleracao(GameTime gameTime)
        {

            if (holddirection == 0)
            {
                holdtime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                holdtime -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (holdtime < 0f)
                {
                    holdtime = 0f;
                }
            }

            if (holddirection > 0)
            {
                holdtime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (holdtime > 3f)
            {
                holdtime = 3f;
            }
            if (holdtime < 1f)
            {
                accel = holdtime / 45;
            }
            if (holdtime >= 1f && holdtime < 2f)
            {
                accel = holdtime / 43;
            }
            if (holdtime >= 2f && holdtime < 3f)
            {
                accel = holdtime / 41;
            }
            if (holdtime >= 3f)
            {
                accel = holdtime / 40;
            }

            if (jumpflag == 1)
            {
                if (holddirection > 0)
                {


                    if (holdtime < 1.7f)
                    {
                        accel = 0.04f;
                    }


                }

            }

            if (holddirection == 0)
            {
                if (directionfaced == 1)
                {
                    position.X += accel;
                }
                else
                {
                    position.X -= accel;
                }
            }

            if (holddirection == 1)
            {
                position.X += accel;
            }
            if (holddirection == 2)
            {
                position.X -= accel;
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
                    flagSalto = 1;
                    flagPlatf = 0;
                    if (directionfaced == 1)
                    {
                        ReplaceImage("SaltarSonic", 1, 8);
                        this.Scale(1.5f);
                    }
                    else
                    {
                        ReplaceImage("SaltarSonicEsquerda", 1, 8);
                        this.Scale(1.5f);
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

            if (jumpflag == 1)
            {

                //ascender
                if (jumptime < 0.35f)
                {
                    this.position.Y += 0.04f;
                }
                if (jumptime > 0.35f && jumptime < 0.45f)
                {
                    this.position.Y += 0.03f;
                }
                if (jumptime > 0.45f && jumptime < 0.50f)
                {
                    this.position.Y += 0.02f;
                }
                //ascender

                //descender
                   if (jumptime >= 0.50f && jumptime < 0.60f)
                    {
                        this.position.Y -= 0.03f;
                        auxsalto = 0.03f;
                    }
                    if (jumptime >= 0.60f && jumptime < 0.75f){
                    
                        this.position.Y -= 0.04f;
                        auxsalto = 0.04f;
                    }
                    if (jumptime >= 0.75f && jumptime < 0.90f)
                    {
                        this.position.Y -= 0.053f;
                        auxsalto = 0.053f;
                    }
                    if (jumptime >= 0.90f && jumptime < 1.10f)
                    {
                        this.position.Y -= 0.065f;
                        auxsalto = 0.065f;
                    }
                    if (jumptime > 1.10f && jumptime <= 1.35f)
                    {
                        this.position.Y -= 0.080f;
                        auxsalto = 0.080f;
                    }
                    if (jumptime >= 1.35f)
                    {
                        this.position.Y -= 0.1f;
                        auxsalto = 0.1f;
                    }

                    colisao1();
                //descender

                //para
                if (this.position.Y < 0f)
                {
                        this.position.Y = 0f;
                        if (directionfaced == 1)
                        {
                            ReplaceImage("sonicstill", 1, 1);
                        }
                        else
                        {
                            ReplaceImage("sonicstillR", 1, 1);
                        }
                        jumptime = 0f;
                        canjump = true;
                        jumpflag = 0;
                        flagSalto = 0;
                    }
                //para
            }
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
                ReplaceImage("sonicball", 1, 1);
                this.position.Y -= 0.16f;
                Sprite other;
                Vector2 colPosition;
                if (scene.Collides(this, out other, out colPosition))
                {
                    this.position.Y += 0.16f;
                    slamflag = 0;
                }
                if (this.position.Y < 0f)
                {
                    this.position.Y = 0f;
                    ReplaceImage("sonicstill", 1, 1);
                    slamflag = 0;
                }
            }
        }


        void dash()
        {

            if (magic > 0)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.X) && dashflag == 0 && dashcooldown > 0.2f)
                {
                    magic -= 1;
                    dashflag = 1;
                    soundslam.Play();
                    holdtime = 2f;
                    xd = 0;
                }
                if (dashflag == 1)
                {
             
                    if (directionfaced == 1)
                    {

                        xd++;
                        if (jumpflag == 0)
                        {
                            afterimage[xd] = new Sprite(cmanager, "sonicafterimage");
                            afterimage[xd].Scl(0.4f);
                        }
                        if (jumpflag == 1)
                        {
                            afterimage[xd] = new Sprite(cmanager, "sonicballA");
                            afterimage[xd].Scl(0.27f);
                        }
                        
                        scene.AddSprite(afterimage[xd]);
                        afterimage[xd].SetPosition(this.position);
                        afterimagecreated[xd] = 1;
         

                        this.position.X += 0.25f;
                        Sprite other;
                        Vector2 colPosition;

                        //para nao deixar passar a frente de obstaculos/objetos
                        if (scene.Collides(this, out other, out colPosition))
                        {
                            if (other.name != "crab" && other.name != "spriteenemy2")
                            {
                                do
                                {
                                    this.position.X -= 0.01f;
                                } while (scene.Collides(this, out other, out colPosition));
                                contdash = 0;
                                dashflag = 0;
                                dashcooldown = 0f;
                                afterimagetimer = 0f;
                                afterimageflag = 1;
                            }
                            
                        }
                        //
                    }
                    if (directionfaced == 2)
                    {

                        xd++;
                        if (jumpflag == 0)
                        {
                            afterimage[xd] = new Sprite(cmanager, "sonicafterimageR");
                            afterimage[xd].Scl(0.4f);
                        }
                        if (jumpflag == 1)
                        {
                            afterimage[xd] = new Sprite(cmanager, "sonicballA");
                            afterimage[xd].Scl(0.27f);
                        }
                        
                        scene.AddSprite(afterimage[xd]);
                        afterimage[xd].SetPosition(this.position);
                        afterimagecreated[xd] = 1;
         
               

                        this.position.X -= 0.25f;
                        Sprite other;
                        Vector2 colPosition;

                        if (scene.Collides(this, out other, out colPosition))
                        {
                            if (other.name != "crab" && other.name != "spriteenemy2")
                            {
                                do
                                {
                                    this.position.X += 0.01f;
                                } while (scene.Collides(this, out other, out colPosition));
                                contdash = 0;
                                dashflag = 0;
                                dashcooldown = 0f;
                                afterimagetimer = 0f;
                                afterimageflag = 1;
                            }
                          
                        }
                    }
                    contdash++;
                    if (contdash == 5)
                    {
                        contdash = 0;
                        dashflag = 0;
                        dashcooldown = 0f;
                        afterimagetimer = 0f;
                        afterimageflag = 1;
                    }

                }

            }

        }
    }
}
