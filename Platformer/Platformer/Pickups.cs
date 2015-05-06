using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Pickups : AnimatedSprite
    {

        public Pickups(ContentManager Content)
            : base(Content, "imagewaterdrop2", 1, 1)
        {
            this.EnableCollisions();
            this.Scl(0.2f);

        }







    }
}
