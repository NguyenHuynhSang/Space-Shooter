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
    public class Enermy
    {
        public Rectangle KhungHinh;
        //enermy bullet
        public Texture2D texture,bulletTexture;
        public Vector2 position;
        public int speed, bulletdelay;
        public bool isVisable;
        public List<Bullet> dsBullet;
     


        //contructer

        public Enermy(Texture2D newtex,Vector2 newpo,Texture2D newbulletteture)
        {
            dsBullet = new List<Bullet>();
            texture = newtex;
            bulletTexture = newbulletteture;
            position = newpo;
            bulletdelay = 80;
            speed = 2;
            isVisable = true;
        }

        //update
        Random rank = new Random();

        public void Update(GameTime gameTime)
        {
            KhungHinh = new Rectangle((int)position.X,(int)position.Y,texture.Width-30,texture.Height-30);

            //cap nhat chuyen dong cua phi thuyen dich

            int ranky = rank.Next(-1, 1);
            position.Y += speed; 
            // position.X += ranky;
            //if (position.X==0 )
            //{
            //    position.X = 0;
            //}
            //if (position.X==ThamSo.WindowWidth)
            //{
            //    position.X = ThamSo.WindowWidth;
            //}

            if (position.Y>=ThamSo.WindownHeight+10)
            {
                position.Y = -20;
            }


            EnermyShoot();
            UpdateEnermyBullet();

        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture,KhungHinh,Color.White);

            foreach (Bullet b in dsBullet)
            {
                b.Draw(spritebatch);
            }

        }

      
        public void UpdateEnermyBullet()
        {
            foreach (Bullet item in dsBullet)
            {
                //bao khung hinh bullet de xu ly va cham
                item.KhungHinh = new Rectangle((int)item.position.X, (int)item.position.Y, item.texture.Width, item.texture.Height);
                //set moverment for bullet
                //now it down :D
                item.position.Y += item.speed;
                if (item.position.Y >=ThamSo.WindownHeight)
                {
                    item.isVisible = false;
                }
                 
            }
            // remove
            for (int i = 0; i < dsBullet.Count(); i++)
            {
                if (!dsBullet[i].isVisible)
                {
                    dsBullet.RemoveAt(i);
                    i--;
                }
            } 
        }

        public void EnermyShoot()
        {
            if (bulletdelay >= 0)
            {
                bulletdelay--;
            }

            if (bulletdelay <= 0)
            {
               
                Bullet newBullet = new Bullet(bulletTexture);
                newBullet.position = new Vector2(position.X+75,position.Y+30);

                newBullet.isVisible = true;
                if (dsBullet.Count() < 15)
                {
                    dsBullet.Add(newBullet);
                }
            }
            if (bulletdelay == 0)
            {
                bulletdelay = 80;
            }
        }



    }
}
