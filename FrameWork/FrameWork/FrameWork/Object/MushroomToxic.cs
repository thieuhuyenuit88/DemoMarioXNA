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
    class MushroomToxic:MyObject
    {
        public MushroomToxic(Game _Game,int _X, int _Y, int _SX, int _SY, int _TotalFrame)
            : base(_Game, _X, _Y, _SX, _SY, _TotalFrame)
        {
            POSITION = new Vector3(POSITION.X, POSITION.Y, 0.92f);
            ID = MyID.MUSHROOM_TOXIC;
            ACCEL = new Vector3(0, 0.001f, 0);
            VELOC = new Vector3(-0.1f, 0, 0);
            STATUS = MyStatus.ACTIVE;
            SPRITE = RSManager.Instance(_Game).SPRITE(ID);
        }
        public override void UpdateAnimate(GameTime mGameTime)
        {
            switch (STATUS)
            {
                case MyStatus.BEFORE_DEATH2:
                    break;
                case MyStatus.BEFORE_DEATH1:
                    CURRENTFRAME = 2;
                    break;
                case MyStatus.ACTIVE:
                    if (TIME.StopWatch(100, mGameTime))
                    {
                        CURRENTFRAME = (CURRENTFRAME + 1) % 2;
                    }
                    break;
            }
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
                            case MyStatus.ACTIVE:
                                if (dir == DIR.TOP)
                                {
                                    STATUS = MyStatus.BEFORE_DEATH1;
                                }
                                break;
                        }
                    }
                    //if (Obj.STATUS == MyStatus.INVI)
                    //{
                    //    if (STATUS == MyStatus.ACTIVE)
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
                    if (STATUS == MyStatus.ACTIVE)
                    {
                        if (Obj.STATUS == MyStatus.ACTIVE || Obj.STATUS == MyStatus.RUN || Obj.STATUS == MyStatus.STATIC || Obj.STATUS == MyStatus.BEFORE_DEATH3)
                        {
                            if (dir == DIR.BOTTOM)
                            {
                                if (Obj.VELOC.Y != 0)
                                {
                                    VELOC = new Vector3(VELOC.X, -0.4f, 0);
                                    STATUS = MyStatus.BEFORE_DEATH2;
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
                    if (STATUS == MyStatus.ACTIVE)
                    {
                        if (Obj.STATUS == MyStatus.ACTIVE)
                        {
                            VELOC = new Vector3(VELOC.X, -0.3f, 0);
                            STATUS = MyStatus.BEFORE_DEATH2;
                        }
                    }
                    break;
                case MyID.TITLE:
                    if (STATUS == MyStatus.ACTIVE)
                    {
                        if (Obj.STATUS == MyStatus.RUN)
                        {
                            VELOC = new Vector3(VELOC.X, -0.3f, 0);
                            STATUS = MyStatus.BEFORE_DEATH2;
                        }

                        if (Obj.STATUS == MyStatus.STATIC)
                        {
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
            }
        }
        public override void UpdateMove(GameTime mGameTime)
        {
            switch (STATUS)
            {
                case MyStatus.BEFORE_DEATH2:
                case MyStatus.ACTIVE:
                    base.UpdateMove(mGameTime);
                    break;
            }            
        }
        public override void Update(GameTime mGameTime)
        {
            UpdateAnimate(mGameTime);
            UpdateMove(mGameTime);
            switch (STATUS)
            {
                case MyStatus.BEFORE_DEATH2:
                case MyStatus.BEFORE_DEATH1:
                    TimeUpdate += mGameTime.ElapsedGameTime.Milliseconds;
                    if (TimeUpdate > 1500)
                    {
                        TimeUpdate -= 1500;
                        STATUS = MyStatus.DEATH;
                    }
                    break;
            }
        }
        public override void Render(SpriteBatch SpriteBactch)
        {
            switch (STATUS)
            {
                case MyStatus.BEFORE_DEATH2:
                    SPRITE.Effect = SpriteEffects.FlipVertically;
                    base.Render(SpriteBactch);
                    break;
                case MyStatus.BEFORE_DEATH1:
                case MyStatus.ACTIVE:
                    SPRITE.Effect = SpriteEffects.None;
                    base.Render(SpriteBactch);
                    break;
            }
        }
    }
}
