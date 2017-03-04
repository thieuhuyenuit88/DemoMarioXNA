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
using FrameWork.FrameWork.Object;
namespace FrameWork.FrameWork.Graphics
{
    class RSManager
    {
        private static RSManager instance;
        private Sprite MarioSmall;
        private Sprite MarioBig;
        private Sprite MarioSuper;
        private Sprite Boss;
        private Sprite Title;
        private Sprite Bullet, BulletBoss;
        private Sprite Mushroom_Toxic,Mushroom_1Up,Mushroom_Big;
        Sprite Base, Pipe, Brick, Brick_Break, Break,Break_Boss, Question, Coin, Flower, Blood;

        private Sprite fence,
            win_pole,
            pole,
            post,
            building, cloud, grass, mountain_small, mountain_big;

        protected RSManager(Game _Game)
        {
            MarioSmall = new Sprite(_Game, @"Image/MarioSmaller", new Point(50, 50), 6, Color.White);
            MarioBig = new Sprite(_Game, @"Image/MarioLager", new Point(50, 100), 5, Color.White);
            MarioSuper = new Sprite(_Game, @"Image/MarioSuper", new Point(50, 100), 5, Color.White);
            Boss = new Sprite(_Game, @"Image/Boss", new Point(100, 110), 4, Color.White);
            Break_Boss =  new Sprite(_Game, @"Image/Boss", new Point(25, 22), 4, Color.White);
            Brick_Break = new Sprite(_Game, @"Image/Brick_Break", new Point(50, 50), 2, Color.White);
            Break = new Sprite(_Game, @"Image/Brick_Break", new Point(25, 25), 2, Color.White);
            Base = new Sprite(_Game, @"Image/Base", new Point(50, 50),3, Color.White);
            Pipe = new Sprite(_Game, @"Image/pipe", new Point(50, 50), 2, Color.White);
            Question = new Sprite(_Game, @"Image/Question", new Point(50, 50), 3, Color.White);
            Coin = new Sprite(_Game, @"Image/Coin", new Point(50, 50), 6, Color.White);
            Mushroom_Toxic = new Sprite(_Game,@"Image/Fungi",new Point(50,50),3,Color.White);
            Mushroom_Big = new Sprite(_Game, @"Image/Lager", new Point(50, 50), 2, Color.White);
            Title = new Sprite(_Game, @"Image/tittle", new Point(50, 72), 5, Color.White);
            Brick = new Sprite(_Game, @"Image/brick", new Point(50, 50), 2, Color.White);
            Flower = new Sprite(_Game, @"Image/Flower", new Point(50, 50), 4, Color.White);
            Bullet = new Sprite(_Game, @"Image/Fire", new Point(34, 34),10, Color.White);
            BulletBoss = new Sprite(_Game, @"Image/FireBoss", new Point(89, 50), 10, Color.White);

            fence = new Sprite(_Game, @"Image/fence", new Point(50, 50), 6, Color.White);
            pole = new Sprite(_Game, @"Image/pole", new Point(50, 50), 6, Color.White);

            post = new Sprite(_Game, @"Image/post", new Point(50, 50), 3, Color.White);

            building = new Sprite(_Game, @"Image/Building", new Point(500, 550), 1, Color.White);
            cloud = new Sprite(_Game, @"Image/Cloud", new Point(100, 80), 3, Color.White);
            grass = new Sprite(_Game, @"Image/Grass", new Point(100, 50), 3, Color.White);
            mountain_small = new Sprite(_Game, @"Image/MoutainSmall", new Point(500, 200), 1, Color.White);
            mountain_big = new Sprite(_Game, @"Image/MoutanLager", new Point(828, 400), 1, Color.White);
            win_pole = new Sprite(_Game, @"Image/win_pole", new Point(50, 450), 6, Color.White);
            Blood = new Sprite(_Game, @"Image/Blood", new Point(10, 21), 1, Color.White);
        }
        public static RSManager Instance(Game _Game)
        {
            if (instance == null)
            {
                instance = new RSManager( _Game);
            }
            return instance;
        }
        public Sprite SPRITE(MyID _ID)
        {
            switch (_ID)
            {
                case MyID.MARIO_SMALL:
                    return MarioSmall;
                case MyID.MARIO_BIG:
                    return MarioBig;
                case MyID.MARIO_SUPER:
                    return MarioSuper;
                case MyID.BREAK:
                    return Break;
                case MyID.BRICK_BREAK:
                    return Brick_Break;
                case MyID.BASE:
                    return Base;
                case MyID.PIPE:
                    return Pipe;
                case MyID.QUESTION_MARK:
                    return Question;
                case MyID.COIN:
                    return Coin;
                case MyID.MUSHROOM_BIG:
                case MyID.MUSHROOM_1UP:
                    return Mushroom_Big;
                case MyID.MUSHROOM_TOXIC:
                    return Mushroom_Toxic;
                case MyID.TITLE:
                    return Title;
                case MyID.BRICK_HARD:
                    return Brick;
                case MyID.FLOWER:
                    return Flower;
                case MyID.BULLET:
                    return Bullet;
                case MyID.FENCE:
                    return fence;
                case MyID.POLE:
                    return pole;
                case MyID.POST:
                    return post;
                case MyID.BUILDING:
                    return building;
                case MyID.CLOUD:
                    return cloud;
                case MyID.GRASS:
                    return grass;
                case MyID.MOUNTAIN_SMALL:
                    return mountain_small;
                case MyID.MOUNTAIN_BIG:
                    return mountain_big;
                case MyID.WINPOLE:
                    return win_pole;
                case MyID.BOSS:
                    return Boss;
                case MyID.BULLETBOSS:
                    return BulletBoss;
                case MyID.BLOOD:
                    return Blood;
                case MyID.BOSS_BREAK:
                    return Break_Boss;
            }
            return null;
        }

    }
}
