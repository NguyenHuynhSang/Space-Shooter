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
    public class SpaceShip
    {
        //Dinh nghia player can gi
        public Texture2D _texture,bulletTexture;
       
        public float bulletDelay;
        public List<Bullet> bulletList;

        SoundManager sound = new SoundManager();


        // set vi tri cua spaceship tren truc toa do oxy
        public Vector2 vitri;
        public int TocDo;
        public Point lastMouse;
        public int texturesOriginalWidth=100;
        public int texturesOriginalHeight=100;
        private bool usingKeyBoard=false;
        // co hieu khi xet va cham va khung hinh khi ve len GUI xet va cham khi cac khung hinh chong cheo len nhau
        public bool isVaCham;
        public Rectangle KhungHinh;

        // Khoi tao mac dinh 1 spaceship
        public SpaceShip()
        {
            bulletList = new List<Bullet>();
            _texture = null;
            bulletDelay = 10;
            // set vi tri mat dinh cua spaceship
            vitri = new Vector2(300,600);
            TocDo = 10;
            isVaCham = false;
           
        }

        //load content

        public void LoadContent(ContentManager _content)
        {
            // tai len phi thuyen  tu content chi duy nhat 1 image; 

            _texture = _content.Load<Texture2D>("spaceship");
            bulletTexture = _content.Load<Texture2D>("bullet");
            sound.LoadContent(_content);

        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            //ve phi truyen ra GUI
            KhungHinh = new Rectangle((int)vitri.X, (int)vitri.Y, texturesOriginalHeight, texturesOriginalWidth);

            spriteBatch.Draw(_texture,KhungHinh, Color.White);
            foreach (Bullet item in bulletList)
            {
                item.Draw(spriteBatch);
            }

        }
        // Update
        
        public void Update(GameTime gameTime)
        {
           
            MouseState ms = Mouse.GetState();
            
            //getting keyboard state every frame
            KeyboardState keystate = Keyboard.GetState();
            //shoot bullet
            if (keystate.IsKeyDown(Keys.Space)||ms.LeftButton==ButtonState.Pressed)
            {
                Shoot();
            }
        
            updateBullet();

           
           

            // ship controls
            if (keystate.IsKeyDown(Keys.W))
            {
                vitri.Y -= TocDo;
                Mouse.SetPosition((int)vitri.X, (int)vitri.Y);
                usingKeyBoard = true;
            }

            
       
         
            if (keystate.IsKeyDown(Keys.S))
            {
                vitri.Y += TocDo;
                Mouse.SetPosition((int)vitri.X, (int)vitri.Y);
                //de cai nay` dung uc che qua
                //ThamSo.TocDoLoadMap = 3;

            }
         
            if (keystate.IsKeyDown(Keys.A))
            {
             
                vitri.X -= TocDo;
                Mouse.SetPosition((int)vitri.X, (int)vitri.Y);
            }
            if (keystate.IsKeyDown(Keys.D))
            {
               
                vitri.X += TocDo;
                Mouse.SetPosition((int)vitri.X, (int)vitri.Y);
            }
            if (keystate.IsKeyDown(Keys.LeftShift)||ms.RightButton==ButtonState.Pressed)
            {
               
                ThamSo.TocDoLoadMap ++;
                if (ThamSo.TocDoLoadMap>8)
                {
                    ThamSo.TocDoLoadMap = 8;
                }
            }
            else
            {
              
                ThamSo.TocDoLoadMap = 5;
            }

         //set vi tri spaceship khi re chuot
                vitri.X = ms.X;
                vitri.Y = ms.Y;

            KeepSpaceShip();

        }

        // xu ly khong cho spaceship chay ra khoi khung hinh
        private void KeepSpaceShip()
        {
            if (vitri.X <= 0) vitri.X = 0;
            if (vitri.X >= ThamSo.WindowWidth-KhungHinh.Width+5) vitri.X = ThamSo.WindowWidth-KhungHinh.Width+5;
            if (vitri.Y <= 0) vitri.Y = 0;
            if (vitri.Y >= ThamSo.WindownHeight-KhungHinh.Height) vitri.Y = ThamSo.WindownHeight-KhungHinh.Height;
        }

        //Set vi tri mac dinh cua bullet khi spaceship ban
        public void Shoot()
        {

          
            // chi ban khi delay=0, va dat vi tri tai vi tri spaceship, visiable =true hien bullet ra GUI
            if (bulletDelay>=0)
            {
                bulletDelay--;
            }
            if (bulletDelay<=0)
            {
                sound.bulletSound.Play();
                Bullet newBullet = new Bullet(bulletTexture);
                newBullet.position = new Vector2(vitri.X+newBullet.texture.Width*2+2,vitri.Y);
               
                newBullet.isVisible = true;
                if (bulletList.Count()<20)
                {
                    bulletList.Add(newBullet);
                }
            }
            if (bulletDelay==0)
            {
                bulletDelay = 10;
            }

        }
        //remove bullet ra khoi list neu bullet vuot ra man hinh, hoac va cham voi enermy
        public void updateBullet()
        {
            foreach (Bullet item in bulletList)
            {
                //set moverment for bullet
                item.position.Y -= item.speed;
                if (item.position.Y<=0)
                {
                    item.isVisible = false;
                }
            }
            // remove
            for (int i = 0; i < bulletList.Count(); i++)
            {
                if (!bulletList[i].isVisible)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }   
            }
        }
    }
}
