﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class AnimatedSprite : Sprite
    {

        protected int ncols, nrows;
        protected Point currentFrame;
        private float animationInterval = 1f / 5f;
        private float animationTimer = 0f;

        public override void Update(GameTime gameTime)
        {
            animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (animationTimer > animationInterval)
            {
                animationTimer = 0f;
                nextframe();
            }



            base.Update(gameTime);
        }

        public bool Loop
        {
            get;
            set;

        }

        protected virtual void nextframe()
        {
            if (currentFrame.X < ncols - 1)
            {
                currentFrame.X++;
            }
            else if (currentFrame.Y < nrows - 1)
            {
                currentFrame.X = 0;
                currentFrame.Y++;
            }
            else if (Loop)
            {
                currentFrame = Point.Zero;
            }
        }


        public AnimatedSprite(ContentManager content, string filename, int nrows, int ncols) :
            base(content, filename)
        {
            this.ncols = ncols;
            this.nrows = nrows;
            this.pixelsize.X = this.pixelsize.X / ncols;
            this.pixelsize.Y = this.pixelsize.Y / nrows;
            this.size = new Vector2(1f, (float)pixelsize.Y / (float)pixelsize.X);
            this.currentFrame = Point.Zero;
            Loop = true;
        }


        public override void Draw(GameTime gametime)
        {
            source = new Rectangle((int)(currentFrame.X * pixelsize.X), (int)(currentFrame.Y * pixelsize.Y), (int)pixelsize.X, (int)pixelsize.Y);
            base.Draw(gametime);

        }

        public override void EnableCollisions()
        {

            this.HasCollisions = true;
            //this.radius = (float)Math.Sqrt(Math.Pow(size.X / 2, 2) + Math.Pow(size.Y / 2, 2));
            pixels = new Color[(int)(pixelsize.X * pixelsize.Y)];
            image.GetData<Color>(0, new Rectangle((int)(currentFrame.X * pixelsize.X), (int)(currentFrame.Y * pixelsize.Y), (int)pixelsize.X, (int)pixelsize.Y), pixels, 0, (int)(pixelsize.X * pixelsize.Y));


        }





    }
}
