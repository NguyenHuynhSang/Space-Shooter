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
    public class Bullet
    {
        public Rectangle KhungHinh;
        public Texture2D texture;
        public Vector2 origin;
        public Vector2 position;
        public bool isVisible;
        public float speed;

        //contructer

        public Bullet(Texture2D _texture)
        {
            speed = 10;
            texture = _texture;
            // co hieu khi bat su kien nguoi choi bam space dan se hien ra
            isVisible = false;
        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture,position,Color.White);
        }
    }
}
