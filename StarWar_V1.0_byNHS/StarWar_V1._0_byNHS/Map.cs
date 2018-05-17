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
    public class Map
    {
        public Texture2D _texture;
        public Vector2 BG_vitri1,BG_vitri2;
        //toc do hieu ung map thay doi khi spaceship chay
        public int TocDo;
        public Rectangle KhungHinh;


        // contructer

        public Map()
        {
            _texture = null;
            BG_vitri1 =new Vector2(0,0);
            BG_vitri2 = new Vector2(0,-1024);
            TocDo = 5;
        }
        public void LoadContent(ContentManager _content)
        {
            _texture = _content.Load<Texture2D>("space");  
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_texture,BG_vitri1,Color.White);
            spritebatch.Draw(_texture,BG_vitri2, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            BG_vitri1.Y = BG_vitri1.Y + TocDo;
            BG_vitri2.Y = BG_vitri2.Y + TocDo;
            if (BG_vitri1.Y>=_texture.Height)
            {
                BG_vitri1.Y = 0;
                BG_vitri2.Y = -_texture.Height;
            }
            TocDo = ThamSo.TocDoLoadMap;
        }
    }
}
