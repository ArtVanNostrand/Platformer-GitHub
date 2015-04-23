using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Level
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Scene scene;

        public Level(ContentManager Content)
        {

            //this.EnableCollisions();

            scene.AddSprite(new Sprite(Content, "block 30x30 v2").Scl((float)Camera.worldWidth * 0.07f)
 .At(new Vector2(4f, 0f)));

            scene.AddSprite(new Sprite(Content, "block 30x30 v2").Scl((float)Camera.worldWidth * 0.07f)
.At(new Vector2(5f, 1f)));


        }






    }
}
