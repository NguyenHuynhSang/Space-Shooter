using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace StarWar_V1._0_byNHS
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
   
 
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        /// //Khoi tao cac doi tuong trong game such as ship, map, astoroid...ect
        SpaceShip myShip = new SpaceShip();
        Map gameMap = new Map();
        List<Asteroid> listAstoroid = new List<Asteroid>();
        List<Enermy> listEnermy = new List<Enermy>();
        SoundManager sm = new SoundManager();

        //constructor mac dinh


        public Game1()
        {
            

            graphics = new GraphicsDeviceManager(this);
            // set kich thuoc Screen game window khi bat dau game
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight =ThamSo.WindownHeight;
            graphics.PreferredBackBufferWidth = ThamSo.WindowWidth;
            this.Window.Title = "Star War v1.0";
            Content.RootDirectory = "Content";

            //set vi tri mac dinh cua cursors khi vao game
            Mouse.SetPosition(600, 600);
           
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //tai len content cua spaceship neu khong textual se la null=> khong draw duoc  
            myShip.LoadContent(Content);
         
            gameMap.LoadContent(Content);
            sm.LoadContent(Content);
            MediaPlayer.Play(sm.bgMusic);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Cap nhat moi thien thach trong danh sach
            foreach (Asteroid a in listAstoroid)
            {
                a.Update(gameTime);
            }
            LoadAsteroid();
            //xy ly va cham ship,bullet va asteroid
            foreach (Asteroid item in listAstoroid)
            {
                if (item.KhungHinh.Intersects(myShip.KhungHinh))
                {
                    item.isVisable = false;
                    sm.asteroidexplosion.Play();
                    myShip.life--;
                }
                for (int i = 0; i < myShip.bulletList.Count(); i++)
                {
                    if (item.KhungHinh.Intersects(myShip.bulletList[i].KhungHinh))
                    {
                        item.isVisable = false;
                        myShip.bulletList[i].isVisible = false;
                        sm.asteroidexplosion.Play(); 
                    }
                }
            }

            LoadEnermy();
            //xu ly va cham giua enermy va spaceship
            foreach (Enermy e in listEnermy)
            {
                if (e.KhungHinh.Intersects(myShip.KhungHinh))
                {
                    e.isVisable = false;
                    myShip.life--;
                    sm.asteroidexplosion.Play();
                }
                // xu ly va cham giua enermy bullet va spaceship 
                for (int i = 0; i <e.dsBullet.Count ; i++)
                {
                    if (myShip.KhungHinh.Intersects(e.dsBullet[i].KhungHinh))
                    {
                        myShip.life--;
                        e.dsBullet[i].isVisible = false;
                        sm.getshooted.Play();
                    }
                }
                //xy ly va cham giua spaceship va enermy
                for (int i = 0; i < myShip.bulletList.Count(); i++)
                {
                    if (myShip.bulletList[i].KhungHinh.Intersects(e.KhungHinh))
                    {
                        myShip.bulletList[i].isVisible = false;
                        e.isVisable = false;
                        sm.getshooted.Play();
                    }
                }
                e.Update(gameTime);
            }
           

            // TODO: Add your update logic here
            myShip.Update(gameTime);
           
            gameMap.Update(gameTime);

         
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            gameMap.Draw(spriteBatch);
            myShip.Draw(spriteBatch);
            foreach (Asteroid a in listAstoroid)
            {
                a.Draw(spriteBatch);
            }

            foreach (Enermy e in listEnermy)
            {
                e.Draw(spriteBatch);
            }
            spriteBatch.End();
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }   
        /// <summary>
        /// Load moi asteroid vi moi thien thach co vi tri khac nhau theo rank, nen can phai load lai va cho vao list
        /// </summary>
        Random rank = new Random();
        public void LoadAsteroid()
        {
            //remove astoroid neu no chay het man hinh GUI, neu k remove astoroid se chay mai khong add them duoc
            if (listAstoroid!=null)
            {
                for (int i = 0; i < listAstoroid.Count(); i++)
                {
                    if (listAstoroid[i].position.Y>ThamSo.WindownHeight+10)
                    {
                        listAstoroid.RemoveAt(i);
                        //luc nay so luong i se giam di i;
                        i--;
                    }
                }
            }
            int rankx = rank.Next(0, ThamSo.WindowWidth);
            int ranky = rank.Next(-ThamSo.WindownHeight / 2,-50);
            Asteroid temp = new Asteroid(Content.Load<Texture2D>("asteroid"),new Vector2(rankx,ranky));
            
            // so luong asto
            if (listAstoroid.Count()<ThamSo.SoluongAsteroid)
            {
                listAstoroid.Add(temp);
            }

            // remove astoroid khi va cham , trung dan ...
          
            for (int i = 0; i < listAstoroid.Count(); i++)
            {
                if (!listAstoroid[i].isVisable)
                {
                    listAstoroid.RemoveAt(i);
                    //luc nay so luong i se giam di i;
                    i--;
                }

            }
        }

        //load enermies
        public void LoadEnermy()
        {
            //remove astoroid neu no chay het man hinh GUI, neu k remove astoroid se chay mai khong add them duoc
            //if (listAstoroid != null)
            //{
            //    for (int i = 0; i < listAstoroid.Count(); i++)
            //    {
            //        if (listAstoroid[i].position.Y > ThamSo.WindownHeight + 10)
            //        {
            //            listAstoroid.RemoveAt(i);
            //            //luc nay so luong i se giam di i;
            //            i--;
            //        }
            //    }
            //}
            int rankx = rank.Next(0, ThamSo.WindowWidth);
            int ranky = rank.Next(-ThamSo.WindownHeight / 2, -50);


            Enermy temp = new Enermy(Content.Load<Texture2D>("enermyship"),new Vector2(rankx,ranky),Content.Load<Texture2D>("ebullet"));

            // so luong asto
            if (listEnermy.Count() < ThamSo.SoluongEnermy)
            {
                listEnermy.Add(temp);
            }

            // remove astoroid khi va cham , trung dan ...

            for (int i = 0; i < listEnermy.Count(); i++)
            {
                if (!listEnermy[i].isVisable)
                {
                    listEnermy.RemoveAt(i);
                    //luc nay so luong i se giam di i;
                    i--;
                }

            }
        }
    }
}
