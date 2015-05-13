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

        bool enemygoleft = false, enemygoright = false;
        Sprite e;

        public Enemy(ContentManager Content, string assetName): base(Content, assetName,1,1)
        {
            this.EnableCollisions();
            this.Scl(0.4f);
        }

        //public bool PlayerDetectionleft(Sprite player, out Sprite enemy)
        //{
        //    enemygoleft = false;


        //    if (((enemy.position.X - player.position.X) < 1f) && (enemy.position.X > player.position.X))
        //    {
        //        enemygoleft = true;
        //    }
        //    if (((enemy.position.X - player.position.X) < 1f) && (enemy.position.X < player.position.X))
        //    {
        //        enemygoright = true;
        //    }

        //    return enemygoleft;
        //}


        //public bool PlayerDetectionright(Sprite player, out Sprite enemy)
        //{
        //    enemygoright = false;

        //    if (((enemy.position.X - player.position.X) < 1f) && (enemy.position.X < player.position.X))
        //    {
        //        enemygoright = true;
        //    }

        //    return enemygoright;
        //}
     




    }
}
