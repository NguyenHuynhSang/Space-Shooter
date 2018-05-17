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

        /// //Khoi tao spaceship,enemy,map...
        SpaceShip myShip = new SpaceShip();
        Map gameMap = new Map();

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
           
            spriteBatch.End();
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
