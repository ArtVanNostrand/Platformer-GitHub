﻿using Microsoft.Xna.Framework;
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
        Sprite player;
    
        public Enemy(ContentManager Content, string assetName, Sprite player): base(Content, assetName,1,1)
        {
            this.EnableCollisions();
            this.Scl(0.4f);
            this.player = player;
        }

        public bool PlayerDetectionleft()
        {
            enemygoleft = false;


            if (((this.position.X - player.position.X) < 2.22f) && (this.position.X > player.position.X))
            {
                enemygoleft = true;
               
            }
       

            return enemygoleft;
        }


        public bool PlayerDetectionright()
        {
            enemygoright = false;

            if (((player.position.X - this.position.X) < 2.72f) && (this.position.X < player.position.X))
            {
                enemygoright = true;
            }

            return enemygoright;
        }





        public override void Update(GameTime gameTime)
        {

    
            
            if (PlayerDetectionleft())
            {
                if (this.name == "crab")
                {
                    this.position.X = this.position.X + 0.02f;
                    enemycollision();
                 
                }
                else if(this.name == "spriteenemy2"){
                    this.position.X = this.position.X - 0.02f;
                    enemycollision();
               
                }
            }

            if (PlayerDetectionright())
            {
                if (this.name == "crab")
                {
                    this.position.X = this.position.X - 0.02f;
                    enemycollision();
                }
                else if (this.name == "spriteenemy2")
                {
                    this.position.X = this.position.X + 0.02f;
                    enemycollision();
                }
            }

      

        }

        void enemycollision()
        {
            Sprite other;
            Vector2 colPosition;

          

                if (scene.Collides(this, out other, out colPosition))
                {
                    if (other.name != "sonic")
                    {
                        if (PlayerDetectionright())
                        {
                            if (this.name == "crab")
                            {
                                this.position.X += 0.02f;
                            }
                            else
                            {
                                this.position.X -= 0.02f;
                            }

                        }
                        if (PlayerDetectionleft())
                        {
                            if (this.name == "crab")
                            {
                                this.position.X -= 0.02f;
                            }
                            else
                            {
                                this.position.X += 0.02f;
                            }
                        }
                    }
                }
          
        }





    }
}
