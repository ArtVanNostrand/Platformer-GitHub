using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Enemy : AnimatedSprite
    {

        public Enemy(ContentManager Content, string assetName): base(Content, assetName,1,1)
        {
            this.EnableCollisions();
            this.Scl(0.4f);
        }

        //public bool PlayerDetection(Sprite s, out Sprite collided)
        //{



        //}




    }
}
