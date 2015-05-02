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


    
        public Level(ContentManager Content, string assetName): base(Content, assetName,1,1)
        {
            this.EnableCollisions();
            this.Scl(0.4f);

        }

    }
}
