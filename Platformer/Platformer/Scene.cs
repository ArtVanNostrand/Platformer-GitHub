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


        public Scene(SpriteBatch sb)
        {
            this.spriteBatch = sb;
            this.sprites = new List<Sprite>();

        }

        public void AddSprite(Sprite s)
        {
            this.sprites.Add(s);
            s.SetScene(this);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var sprite in sprites)
            {
                sprite.Update(gameTime);
            }

        }

        public void Draw(GameTime gameTime)
        {
            if (sprites.Count > 0)
            {
                this.spriteBatch.Begin();
                foreach (var sprite in sprites)
                {
                    sprite.Draw(gameTime);
                }
                this.spriteBatch.End();
            }

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
