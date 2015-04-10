using Microsoft.Xna.Framework;
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


            Vector2 tpos = Camera.WorldPoint2Pixels(position);
            //float a = (float)mpos.Y - tpos.Y;
            //float l = (float)mpos.X - tpos.X;
            //float rot = (float)Math.Atan2(a, l);

            //turret.SetRotation(rot+(float)Math.PI/2f);

            KeyboardState state = Keyboard.GetState();

            //if (state.IsKeyDown(Keys.Up))
            //{
            //    this.position.Y += 0.1f;
            //}
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
