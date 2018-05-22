using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace StarWar_V1._0_byNHS
{
    public class Explosion
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;

        // điều chỉnh thời gian xuất hiện của  animation
        public float timer;
        public float interval;


        public int currentframe, spritewidth, spriteheight;
        public Rectangle sourceRec;
        public bool isVisable;


        public Explosion(Texture2D newtex,Vector2 newpo)
        {
            position = newpo;
            texture = newtex;

            // chieu chinh animation explosion slow or fast
            timer = 0f;
            interval = 50f;
            currentframe = 1;
            spritewidth = 135;
            spriteheight = 105;
            isVisable = true;
        }



        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("");
        }
        public void update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {
                currentframe++;
                timer = 0f;
            }

            //so frame trong content explosion animation
            if (currentframe==11)
            {
                isVisable = false;
                currentframe = 0;
            }

            sourceRec = new Rectangle(spritewidth*currentframe,0,spritewidth,spritewidth);
            origin = new Vector2(sourceRec.Width/2,sourceRec.Height/2);




        }


        public void Draw(SpriteBatch spritebatch)
        {
            if (isVisable==true)
            {
                spritebatch.Draw(texture,position,sourceRec,Color.White,0f,origin,1.0f,SpriteEffects.None,0); 
            }
           
        }

    }
}
                                                                                                                   