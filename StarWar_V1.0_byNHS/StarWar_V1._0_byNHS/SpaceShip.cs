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
        public Texture2D _texture,bulletTexture,lifeTexture;
        public int life;
       
        public float bulletDelay;
        public List<Bullet> bulletList;

        SoundManager sound = new SoundManager();


        // set vi tri cua spaceship tren truc toa do oxy
        public Vector2 vitri;
        public int TocDo;
        public Point lastMouse;
        public int texturesOriginalWidth=100;
        public int texturesOriginalHeight=100;
        // co hieu khi xet va cham va khung hinh khi ve len GUI xet va cham khi cac khung hinh chong cheo len nhau
        public bool isVaCham;
        public Rectangle KhungHinh,lifeKhungHinh;
        //DelayShip
        private bool DelayShip;
        public bool isVisiable;
        // điều chỉnh thời gian xuất hiện của  animation
        public float timer;
        public float interval;

        // Khoi tao mac dinh 1 spaceship
        public SpaceShip()
        {
            bulletList = new List<Bullet>();
            _texture = null;
            bulletDelay = 1;
            // set vi tri mat dinh cua spaceship
            vitri = new Vector2(300,600);
            TocDo = 10;
            isVaCham = false;
            life = ThamSo.playerLife;
            isVisiable = true;
            DelayShip = false;
            timer = 0f;
            interval = 50f;
        }

        //load content

        public void LoadContent(ContentManager _content)
        {
            // tai len phi thuyen  tu content chi duy nhat 1 image; 
           
            _texture = _content.Load<Texture2D>("spaceship");
            bulletTexture = _content.Load<Texture2D>("bullet");
            lifeTexture = _content.Load<Texture2D>("heart");
            sound.LoadContent(_content);

        }

        // Draw
        public void Draw(SpriteBatch spriteBatch)
       
        {
            //ve phi truyen ra GUI
            if (DelayShip == false)
            {
                spriteBatch.Draw(_texture, KhungHinh, Color.White);
            }
          
            //ve trai tim(player life) ra GUI
            for (int i = 0; i < life; i++)
            {
                lifeKhungHinh = new Rectangle(50 + (i * 50), 5, 40, 40);
                spriteBatch.Draw(lifeTexture, lifeKhungHinh, Color.White);
            }
            
          
            foreach (Bullet item in bulletList)
            {
                item.Draw(spriteBatch);
            }

        }
       // sua loi ship bi delay khi nguoi dung nhan phim, hoac phim va chuot cung luc
        bool Moflag = false;
        private int templife=3;
        // Update
        float second = 0;
        public void Update(GameTime gameTime)
        {
            DelayShip = false;
            timer += (float)gameTime.ElapsedGameTime.Milliseconds;
            
            if (timer > interval && life<templife)
            {
                DelayShip = true;
                timer = 0f;
                second += (float)gameTime.ElapsedGameTime.TotalSeconds;
                templife = life;


            }
            //if (second > 2f)
            //{
            //    DelayShip = false;
            //    second = 0f;
            //    templife = -10;
            //    //timer = 0f;
            //}

            //so frame trong content explosion animation

            KhungHinh = new Rectangle((int)vitri.X, (int)vitri.Y, _texture.Width/5+5, _texture.Height/5+5);
            Moflag = false;
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
                Moflag = true;
            }

            
       
         
            if (keystate.IsKeyDown(Keys.S))
            {
                vitri.Y += TocDo;
                Mouse.SetPosition((int)vitri.X, (int)vitri.Y);
                //de cai nay` dung uc che qua
                //ThamSo.TocDoLoadMap = 3;
                Moflag = true;

            }
         
            if (keystate.IsKeyDown(Keys.A))
            {
             
                vitri.X -= TocDo;
                Mouse.SetPosition((int)vitri.X, (int)vitri.Y);
                Moflag = true;
            }
            if (keystate.IsKeyDown(Keys.D))
            {
               
                vitri.X += TocDo;
                Mouse.SetPosition((int)vitri.X, (int)vitri.Y);
                      Moflag = true;
            }
            if (keystate.IsKeyDown(Keys.LeftShift)||ms.RightButton==ButtonState.Pressed)
            {
               
                ThamSo.TocDoLoadMap ++;
                if (ThamSo.TocDoLoadMap > 8)
                {
                    ThamSo.TocDoLoadMap = 8;
                }
            }
            else
            {
              
                ThamSo.TocDoLoadMap = 5;
            }

            //set vi tri spaceship khi re chuot
            if (Moflag==false)
            {
                vitri.X = ms.X;
                vitri.Y = ms.Y;

            }

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
                newBullet.position = new Vector2(vitri.X+newBullet.texture.Width*2-2,vitri.Y);
               
                newBullet.isVisible = true;
                if (bulletList.Count()<15)
                {
                    bulletList.Add(newBullet);
                }
            }
            if (bulletDelay == 0)
            {
                bulletDelay = 15;
            }

        }
        //remove bullet ra khoi list neu bullet vuot ra man hinh, hoac va cham voi enermy
        public void updateBullet()
        {
            foreach (Bullet item in bulletList)
            {
                //bao khung hinh bullet de xu ly va cham
                item.KhungHinh = new Rectangle((int)item.position.X,(int)item.position.Y,item.texture.Width,item.texture.Height);
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
