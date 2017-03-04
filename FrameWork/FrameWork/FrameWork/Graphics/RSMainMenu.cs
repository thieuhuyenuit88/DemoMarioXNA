using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FrameWork.FrameWork.Graphics
{
    class RSMainMenu
    {
        private static RSMainMenu instance;
        private Sprite Circle;
        Sprite Circle1;
        Sprite Circle2;
        Sprite Back, BackAbout;
        Sprite DialogWin, DialogLost, Button, SoundIcon, Cursor;
        private RSMainMenu(Game _Game)
        {

            Circle = new Sprite(_Game, @"Image/Circle", new Point(100, 100), 1, Color.White);
            Circle1 = new Sprite(_Game, @"Image/Circle1", new Point(130, 130), 1, Color.White);
            Circle2 = new Sprite(_Game, @"Image/Circle2", new Point(160, 160), 1, Color.White);
            Back = new Sprite(_Game, @"Image/Back", new Point(1024, 768), 1, Color.White);
            BackAbout = new Sprite(_Game, @"Image/BackAbout", new Point(1024, 768), 1, Color.White);
            DialogWin = new Sprite(_Game, @"Image/DialogWin", new Point(600, 500), 1, Color.White);
            DialogLost = new Sprite(_Game, @"Image/DialogLost", new Point(600, 500), 1, Color.White);
            Button = new Sprite(_Game, @"Image/Button", new Point(161, 40), 2, Color.White);
            SoundIcon = new Sprite(_Game, @"Image/Sound_Icon", new Point(90, 90), 2, Color.White);
            Cursor = new Sprite(_Game, @"Image/Cursor", new Point(50, 50), 1, Color.White);
        }

        public static RSMainMenu Instance(Game _Game)
        {
            if (instance == null)
            {
                instance = new RSMainMenu(_Game);
            }
            return instance;
        }
        public Sprite SPRITE(int _ID)
        {
            switch (_ID)
            {
                case 0:
                    return Circle;
                case 1:
                    return Circle1;
                case 2:
                    return Circle2;
                case 3:
                    return Back;
                case 4:
                    return DialogWin;
                case 5:
                    return DialogLost;
                case 6:
                    return Button;
                case 7:
                    return SoundIcon;
                case 8:
                    return Cursor;
                case 9:
                    return BackAbout;
            }
            return null;
        }
    }

}
