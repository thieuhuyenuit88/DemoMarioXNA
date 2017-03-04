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
using FrameWork.FrameWork.Graphics;
namespace FrameWork.FrameWork.Object
{
    class Boss: MyObject
    {
        public int Blood;
        public int Direct;
        private Vector2 scale;
        double TimeUpdate1;
        Random Rdn;
        Game game;
        List<MyObject> ListBreak;
        public Boss(Game _Game, int _X, int _Y, int _SX, int _SY, int _TotalFrame)
            : base(_Game, _X, _Y, _SX, _SY, _TotalFrame)
        {
            game = _Game;
            POSITION = new Vector3(POSITION.X, POSITION.Y, 1);
            ID = MyID.BOSS;
            STATUS = MyStatus.ACTIVE;
            ACCEL = new Vector3(0, 0.001f, 0);
            VELOC = new Vector3(-0.15f, 0, 0);
            Direct = -1;
            scale = new Vector2(1.0f, 1.0f);
            Rdn = new Random();
            ListBreak = new List<MyObject>();
            Blood = 240;
            SPRITE = RSManager.Instance(_Game).SPRITE(ID);
        }

        public override void ActionCollision(MyObject Obj)
        {
            DIR dir = DirectionCollision(Obj);
            switch (Obj.ID)
            {
                case MyID.MARIO_SMALL:
                case MyID.MARIO_BIG:
                case MyID.MARIO_SUPER:
                    if (Obj.STATUS == MyStatus.ACTIVE || Obj.STATUS == MyStatus.DOWN || Obj.STATUS == MyStatus.UP || Obj.STATUS == MyStatus.INVI || Obj.STATUS == MyStatus.CHANGE || Obj.STATUS == MyStatus.SHOT)
                    {
                        switch (STATUS)
                        {
                            case MyStatus.SHOT:
                            case MyStatus.ACTIVE:
                                if (dir == DIR.TOP)
                                {
                                    STATUS = MyStatus.HURT;
                                }
                                break;
                        }
                    }
                    //if (Obj.STATUS == MyStatus.INVI)
                    //{
                    //    if (STATUS == MyStatus.ACTIVE||STATUS == MyStatus.SHOT||STATUS == MyStatus.HURT)
                    //    {
                    //        if (dir == DIR.LEFT)
                    //        {
                    //            if (VELOC.X < 0)
                    //            {
                    //                VELOC = new Vector3(Math.Abs(VELOC.X), VELOC.Y, 0);
                    //            }
                    //            POSITION = new Vector3(Obj.POSITION.X + Obj.SIZE.X, POSITION.Y, POSITION.Z);
                    //        }
                    //        if (dir == DIR.RIGHT)
                    //        {
                    //            if (VELOC.X > 0)
                    //            {
                    //                VELOC = new Vector3(-1.0f * Math.Abs(VELOC.X), VELOC.Y, 0);
                    //            }
                    //            POSITION = new Vector3(Obj.POSITION.X - SIZE.X, POSITION.Y, POSITION.Z);
                    //        }
                    //    }
                    //}
                    break;
                case MyID.BRICK_BREAK:
                case MyID.BASE:
                case MyID.QUESTION_MARK:
                case MyID.BRICK_HARD:
                case MyID.PIPE:
                    if (STATUS == MyStatus.ACTIVE||STATUS == MyStatus.SHOT||STATUS == MyStatus.HURT)
                    {
                        if (Obj.STATUS == MyStatus.ACTIVE || Obj.STATUS == MyStatus.RUN || Obj.STATUS == MyStatus.STATIC || Obj.STATUS == MyStatus.BEFORE_DEATH3)
                        {
                            if (dir == DIR.BOTTOM)
                            {
                                if (Obj.VELOC.Y != 0)
                                {
                                    if(STATUS != MyStatus.HURT)
                                        STATUS = MyStatus.HURT;
                                }
                                else
                                    VELOC = new Vector3(VELOC.X, 0, 0);
                                POSITION = new Vector3(POSITION.X, Obj.POSITION.Y - SIZE.Y + 1, POSITION.Z);
                            }
                            if (dir == DIR.LEFT)
                            {
                                if (VELOC.X < 0)
                                {
                                    VELOC = new Vector3(-1 * VELOC.X, VELOC.Y, 0);
                                }
                                POSITION = new Vector3(Obj.POSITION.X + Obj.SIZE.X, POSITION.Y, POSITION.Z);
                            }
                            if (dir == DIR.RIGHT)
                            {
                                if (VELOC.X > 0)
                                {
                                    VELOC = new Vector3(-1 * VELOC.X, VELOC.Y, 0);
                                }
                                POSITION = new Vector3(Obj.POSITION.X - SIZE.X, POSITION.Y, POSITION.Z);
                            }
                        }
                    }
                    break;
                case MyID.BULLET:
                    switch (STATUS)
                    {
                        case MyStatus.SHOT:
                        case MyStatus.ACTIVE:
                            STATUS = MyStatus.HURT;
                            break;
                    }
                    break;
            }
        }
        public override void UpdateMove(GameTime mGameTime)
        {
            switch (STATUS)
            {
                case MyStatus.BEFORE_DEATH3:
                    for (int i = 0; i < ListBreak.Count; i++)
                    {
                        ListBreak[i].UpdateMove(mGameTime);
                    }
                    POSITION = new Vector3(POSITION.X, 0, POSITION.Z);
                    base.UpdateMove(mGameTime);
                    break;
                case MyStatus.HURT:
                //case MyStatus.SHOT:
                case MyStatus.ACTIVE:
                    if (Direct * VELOC.X < 0)
                    {
                        Direct *= -1;
                    }
                    TimeUpdate += mGameTime.ElapsedGameTime.Milliseconds;
                    if(TimeUpdate > 1500)
                    {
                        TimeUpdate -= 1500;
                        switch (Rdn.Next(2))
                        {
                            case 0:
                                VELOC = new Vector3(VELOC.X, VELOC.Y, 0);
                                break;
                            case 1:
                                VELOC = new Vector3(-1 * VELOC.X, VELOC.Y, 0);
                                break;
                        }
                    }
                    base.UpdateMove(mGameTime);
                    break;
            }

        }
        public override void UpdateAnimate(GameTime mGameTime)
        {
            switch (STATUS)
            {
                case MyStatus.ACTIVE:
                    if (TIME.StopWatch(100, mGameTime))
                    {
                        CURRENTFRAME = (CURRENTFRAME + 1) % 3;
                    }
                    break;
                case MyStatus.SHOT:
                    CURRENTFRAME = 0;
                    break;
                case MyStatus.HURT:
                    CURRENTFRAME = 2;
                    PERCENT = Rdn.Next(0, 255);
                    break;
                case MyStatus.BEFORE_DEATH1:
                    CURRENTFRAME = 2;
                    PERCENT = Rdn.Next(0, 255);
                    scale = new Vector2((float)Rdn.Next(3, 6) / 3, (float)Rdn.Next(3, 6) / 3);
                    break;  
            }
        }
        public override void Update(GameTime mGameTime)
        {
            UpdateAnimate(mGameTime);
            UpdateMove(mGameTime); 
            switch (STATUS)
            {
                case MyStatus.SHOT:
                    TimeUpdate1 += mGameTime.ElapsedGameTime.Milliseconds;
                    if (TimeUpdate1 > 1000)
                    {
                        TimeUpdate1 -= 1000;
                        STATUS = MyStatus.ACTIVE;
                    }
                    break;
                case MyStatus.ACTIVE:
                    TimeUpdate1 += mGameTime.ElapsedGameTime.Milliseconds;
                    if (TimeUpdate1 > 2000)
                    {
                        TimeUpdate1 -= 2000;
                        if (Rdn.Next(3) == 0 || Rdn.Next(3) == 1)
                        {
                            STATUS = MyStatus.SHOT;
                        }
                    }
                    break;
                case MyStatus.HURT:
                    TimeUpdate1 += mGameTime.ElapsedGameTime.Milliseconds;
                    if (TimeUpdate1 > 1000)
                    {
                        TimeUpdate1 -= 1000;
                        if (Rdn.Next(3) == 0||Rdn.Next(3) == 1)
                        {
                            STATUS = MyStatus.SHOT;
                        }
                        else
                            STATUS = MyStatus.ACTIVE;
                    }
                    break;
                case MyStatus.BEFORE_DEATH1:
                    TimeUpdate1 += mGameTime.ElapsedGameTime.Milliseconds;
                    if (TimeUpdate1 > 2000)
                    {
                        TimeUpdate1 -= 2000;
                        STATUS = MyStatus.BEFORE_DEATH3;
                        for (int i = 0; i < 20; i++)
                        {
                            ListBreak.Add(new BossBreak(game, (int)POSITION.X + 25 * (i % 4), (int)POSITION.Y + 22 * (i / 5), 25, 22,20));
                        }
                        for (int i = 0; i < ListBreak.Count; i++)
                        {
                            if (i ==1 || i==2)
                                ListBreak[i].VELOC = new Vector3(0, -0.6f, 0);
                            else if (i == 17 || i == 18)
                                ListBreak[i].VELOC = new Vector3(0,  0.6f, 0);
                            else if (i == 0||i==4)
                                ListBreak[i].VELOC = new Vector3(-0.5f, -0.6f, 0);
                            else if (i == 3||i==7)
                                ListBreak[i].VELOC = new Vector3(0.5f, -0.6f, 0);
                            else if (i == 16||i==12)
                                ListBreak[i].VELOC = new Vector3(-0.5f, 0.6f, 0);
                            else if (i == 19||i==15)
                                ListBreak[i].VELOC = new Vector3(0.5f, 0.6f, 0);
                            else if (i == 8 || i==9)
                                ListBreak[i].VELOC = new Vector3(-0.5f, 0, 0);
                            else if (i == 10 || i == 11)
                                ListBreak[i].VELOC = new Vector3(0.5f, 0, 0);
                        }
                    }
                    break;
                case MyStatus.BEFORE_DEATH3:
                    TimeUpdate1 += mGameTime.ElapsedGameTime.Milliseconds;
                    if (TimeUpdate1 > 800)
                    {
                        TimeUpdate1 -= 800;
                        STATUS = MyStatus.DEATH;
                        for (int i = 0; i < ListBreak.Count; i++)
                        {
                            ListBreak[i].STATUS = MyStatus.DEATH;
                        }
                        ListBreak.Clear();
                    }
                    break; 
            }
        }
        public override void Render(SpriteBatch SpriteBactch)
        {
            switch (STATUS)
            {
                case MyStatus.BEFORE_DEATH1:
                    base.COLOR = new Color(255, 255, 255, PERCENT);
                    SPRITE.Scale = new Vector2(scale.X,scale.Y);
                    SPRITE.Effect = (Direct > 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                    base.Render(SpriteBactch);
                    base.COLOR = new Color(255, 255, 255, 255);
                    SPRITE.Scale = new Vector2(1.0f,1.0f);
                    break;
                case MyStatus.HURT:
                    base.COLOR = new Color(255, 255, 255, PERCENT);
                    SPRITE.Effect = (Direct > 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                    base.Render(SpriteBactch);
                    base.COLOR = new Color(255, 255, 255, 255);
                    break;
                case MyStatus.SHOT:
                case MyStatus.ACTIVE:
                    SPRITE.Effect = (Direct > 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                    base.Render(SpriteBactch);
                    break;
                case MyStatus.BEFORE_DEATH3:
                    for (int i = 0; i < ListBreak.Count; i++)
                    {
                        ListBreak[i].Render(SpriteBactch);
                    }
                    break;
            }
        }
    }
}
