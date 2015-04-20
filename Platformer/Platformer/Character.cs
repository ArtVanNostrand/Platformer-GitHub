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

       



        float jumptime = 0f;
        int jumpflag = 0;
        bool ZPressed = false;


        public Character(ContentManager content) : base(content,"imageplaceholderchar")
        {




        }

        public override void Draw(GameTime gameTime){

            base.Draw(gameTime);



        }

        public override void SetScene(Scene s)
        {
            this.scene = s;
         
        }

        public override void Update(GameTime gameTime){

           jumptime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 tpos = Camera.WorldPoint2Pixels(position);
            //float a = (float)mpos.Y - tpos.Y;
            //float l = (float)mpos.X - tpos.X;
            //float rot = (float)Math.Atan2(a, l);

            //turret.SetRotation(rot+(float)Math.PI/2f);

            KeyboardState state = Keyboard.GetState();



            //clicar saltar
            if (jumpflag == 0)
            {
                
            if (Keyboard.GetState().IsKeyUp(Keys.Z)) ZPressed = false;
            if (ZPressed == false && Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                ZPressed = true;
                    jumptime = 0f;
                    this.position.Y += 0.03f;
                    jumpflag = 1;
                }
            }
            //clicar saltar

            //ascender
            if (jumpflag > 0 && jumptime < 0.15f)
            {
                this.position.Y += 0.04f;
            }
            //ascender

            //descender
            if (jumpflag >0)
            {
                if (jumptime > 0.15f)
                {
                    this.position.Y -= 0.04f;
                }
            }
            //descender

            //para
            if (jumpflag > 0)
            {
                if (this.position.Y <= 0f)
                {
                    jumptime = 0f;
                    jumpflag = 0;
                }
            }
           //para


            //if (state.IsKeyDown(Keys.Down))
            //{
            //    this.position.Y -= 0.1f;
            //}

            if (state.IsKeyDown(Keys.Right))
            {
                this.position.X += 0.1f;
            }
            if (state.IsKeyDown(Keys.Left))
            {
                this.position.X -= 0.1f;
            }

            Camera.SetTarget(this.position);

            base.Update(gameTime);



        }





    }
}
