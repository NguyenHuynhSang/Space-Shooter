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
    public class Asteroid
    {
        public Texture2D texture;
        public Vector2 origin;
        public Vector2 position;
        public int speed,rankx,ranky;
        // Do xoay cua thien thach 
        public float RotationAngle;
        public bool isVisable;
        public Rectangle KhungHinh;
        Random rank = new Random();
        public Asteroid(Texture2D newTexture,Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            isVisable = true;
            speed = 2;
            //rankx = rank.Next(0, ThamSo.WindowWidth);
          //  ranky = rank.Next(0, ThamSo.WindownHeight);
        }


        public void LoadContent(ContentManager content)
        {
            //texture = content.Load<Texture2D>("asteroid");

            //origin.X = texture.Width/2;
            //origin.Y = texture.Height/2;
        }
            
        
        public void Draw(SpriteBatch spritebatch)
        {
            if (position.Y==-50)
            {
                position.X = rank.Next(5,ThamSo.WindowWidth-5);
            }

            if (isVisable)
            {
                spritebatch.Draw(texture, KhungHinh, Color.White);
                //spritebatch.Draw(texture, KhungHinh, null, Color.White, RotationAngle, origin, SpriteEffects.None, 0f);
            }



        }

        public void Update(GameTime gametime)
        {
            speed = ThamSo.TocDoLoadMap + 5;
            KhungHinh = new Rectangle((int)position.X, (int)position.Y, texture.Width/4, texture.Height/4);
            position.Y+=speed;
            if (position.Y==ThamSo.WindownHeight)
            {
                position.Y = -50;
            }
            

            //Xoay thien thach loi khi khung hinh cung xoay theo
            //=> fix co the load them content de tao hieu ung nhu dang xoay

            //float elapsed = (float)gametime.ElapsedGameTime.TotalSeconds;
            //RotationAngle += elapsed;
            //float cirle = MathHelper.Pi * 2;
            //RotationAngle %=cirle;


        }
    }
}
