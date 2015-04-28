using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Level : AnimatedSprite
    {

        //GraphicsDeviceManager graphics;
        //SpriteBatch spriteBatch;
        //Scene scene;
        //private List<Sprite> sprites;
    
        public Level(ContentManager Content): base(Content, "block 30x30 v2",1,1)
        {
            this.EnableCollisions();
            this.Scl(0.4f);

        }






    }
}
