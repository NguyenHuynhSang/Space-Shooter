using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace StarWar_V1._0_byNHS
{
    public class SoundManager
    {
        public SoundEffect bulletSound;
        public Song bgMusic;


        //contructer

        public SoundManager()
        {
            bulletSound = null;
            bgMusic = null;
        }

        
        public void LoadContent(ContentManager _content)
        {
            bulletSound = _content.Load<SoundEffect>("bulletsound");
            bgMusic = _content.Load<Song>("lactroi");
        }

    }
}
