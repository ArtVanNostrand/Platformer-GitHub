using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Scene
    {
        public SpriteBatch spriteBatch { get; private set; }
        private List<Sprite> sprites;
        private List<SlidingBackground> backgrounds;

        public Scene(SpriteBatch sb)
        {
            this.spriteBatch = sb;
            this.sprites = new List<Sprite>();
            this.backgrounds = new List<SlidingBackground>();
        }

        public void AddSprite(Sprite s)
        {
            this.sprites.Add(s);
            s.SetScene(this);
        }

        public void AddBackground(SlidingBackground b)
        {
            this.backgrounds.Add(b);
            b.SetScene(this);
        }

        public bool Collides(Sprite s, out Sprite collided, out Vector2 collisionPoint)
        {
            bool collisionExists = false;
            collided = s;
            collisionPoint = Vector2.Zero;
            foreach (var sprite in sprites)
            {
                if (s == sprite) continue;
                if (s.CollidesWith(sprite, out collisionPoint))
                {
                    collisionExists = true;
                    collided = sprite;
                    break;
                }
            }
            return collisionExists;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var sprite in sprites.ToList())
            {
                sprite.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (sprites.Count > 0 || backgrounds.Count > 0)
            {
                this.spriteBatch.Begin();

                foreach (var background in backgrounds) background.Draw(gameTime);

                foreach (var sprite in sprites)
                {
                    sprite.Draw(gameTime);
                }
                this.spriteBatch.End();
            }

        }

        public void RemoveSprite(Sprite s)
        {
            this.sprites.Remove(s);
        }

        public void Dispose()
        {
            foreach (var sprite in sprites)
            {
                sprite.Dispose();
            }
        }
     
    }
}
