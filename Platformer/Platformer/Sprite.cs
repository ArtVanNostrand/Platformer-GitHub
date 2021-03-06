﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Sprite
    {
        public bool HasCollisions { protected set; get; }
        
        public string name;
        protected float radius;
        protected Texture2D image;
        public Vector2 position;
        protected Vector2 size;
        protected float rotation;
        protected Scene scene;
        protected Vector2 pixelsize;
        protected Rectangle? source = null;
        protected Color[] pixels;
        protected ContentManager cmanager;

        public Sprite(ContentManager contents, String assetName)
        {
            this.name = assetName;
            this.HasCollisions = false;
            this.rotation = 0f;
            this.position = Vector2.Zero;
            this.image = contents.Load<Texture2D>(assetName);
            this.pixelsize = new Vector2(image.Width, image.Height);
            this.size = new Vector2(1f, (float)image.Height / (float)image.Width);
            cmanager = contents;          
            this.radius = size.Length();

        }

        public virtual void Scale(float scale)
        {
            this.size *= scale;
        }

        public bool CollidesWith(Sprite other, out Vector2 collisionPoint)
        {
            collisionPoint = position; // calar o compilador

            if (!this.HasCollisions) return false;
            if (!other.HasCollisions) return false;

            float distance = (this.position - other.position).Length();
            if (distance <= this.radius + other.radius) return this.PixelTouches(other, out collisionPoint);
            else return (false);
        }

        public Sprite Scl(float scale)
        {
            this.Scale(scale);
            return this;
        }

        public virtual void SetScene(Scene s)
        {
            this.scene = s;
        }

        public virtual void Draw(GameTime gametime)
        {
            Rectangle pos = Camera.WorldSize2PixelRectangle(this.position, this.size);
            scene.spriteBatch.Draw(this.image, pos, source, Color.White, this.rotation, new Vector2(pixelsize.X / 2, pixelsize.Y / 2), SpriteEffects.None, 0);
        }

        public virtual void SetRotation(float r)
        {
            this.rotation = r;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Dispose()
        {
            this.image.Dispose();
        }

        internal void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        public Sprite At(Vector2 p)
        {
            this.SetPosition(p);
            return this;
        }

        public virtual void EnableCollisions()
        {
            this.HasCollisions = true;
            pixels = new Color[(int)(pixelsize.X * pixelsize.Y)];
            image.GetData<Color>(pixels);
        }

        public virtual void Destroy()
        {
            this.scene.RemoveSprite(this);
        }

        public Color GetColorAt(int x, int y)
        {
            // Se não houver collider, da erro!
            return pixels[x + y * (int)pixelsize.X];
        }

        private Vector2 VirtualWorldPointToImagePixel(Vector2 p)
        {
            Vector2 delta = p - position;
            float i = delta.X * pixelsize.X / size.X;
            float j = delta.Y * pixelsize.Y / size.Y;

            i += pixelsize.X * 0.5f;
            j = pixelsize.Y * 0.5f - j;

            return new Vector2(i, j);
        }

        public bool PixelTouches(Sprite other, out Vector2 collisionPoint)
        {
            collisionPoint = position;

            bool touches = false;

            int i = 0;

            while (touches == false && i < pixelsize.X)
            {
                int j = 0;
                while (touches == false && j < pixelsize.Y)
                {
                    if (GetColorAt(i, j).A > 0)
                    {
                        Vector2 CollidePoint = ImagePixelToVirtualWorld(i, j);
                        Vector2 otherPixel = other.VirtualWorldPointToImagePixel(CollidePoint);
                        if (otherPixel.X >= 0 && otherPixel.Y >= 0 && otherPixel.X < other.pixelsize.X &&
                            otherPixel.Y < other.pixelsize.Y)
                        {
                            if (other.GetColorAt((int)otherPixel.X, (int)otherPixel.Y).A > 0)
                            {
                                touches = true;
                                collisionPoint = CollidePoint;
                            }
                        }
                    }
                    j++;
                }
                i++;
            }
            return touches;
        }

        private Vector2 ImagePixelToVirtualWorld(int i, int j)
        {
            float x = i * size.X / (float)pixelsize.X;
            float y = j * size.Y / (float)pixelsize.Y;
            return new Vector2(position.X + x - (size.X * 0.5f), position.Y - y + (size.Y * 0.5f));

        }
        
    }
}
