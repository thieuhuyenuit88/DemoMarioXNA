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
    class Title : MyObject
    {
        public Title(Game _Game, int _X, int _Y, int _SX, int _SY, int _TotalFrame, int _DirVeloc)
            : base(_Game, _X, _Y, _SX, _SY, _TotalFrame)
        {
            POSITION = new Vector3(POSITION.X, POSITION.Y, 0.94f);
            ACCEL = new Vector3(0, 0.001f, 0);
            VELOC = (_DirVeloc > 0) ? new Vector3(0.1f, 0.0f, 0.0f) : new Vector3(-0.1f, 0.0f, 0.0f);
            ID = MyID.TITLE;
            CURRENTFRAME = 1;
            STATUS = MyStatus.ACTIVE;
            SPRITE = RSManager.Instance(_Game).SPRITE(ID);
        }
        public override void UpdateAnimate(GameTime mGameTime)
        {
            switch (STATUS)
            {
                case MyStatus.ACTIVE:
                    if (TIME.StopWatch(100, mGameTime))
                    {
                        CURRENTFRAME = (CURRENTFRAME + 1) % 2;
                    }
                    break;
                case MyStatus.BEFORE_DEATH1:
                case MyStatus.STATIC:
                    CURRENTFRAME = 3;
                    break;
                case MyStatus.RUN:
                    if (TIME.StopWatch(100, mGameTime))
                    {
                        CURRENTFRAME = Math.Abs((CURRENTFRAME - 1) % 3) + 2;
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
                    if (Obj.STATUS == MyStatus.ACTIVE || Obj.STATUS == MyStatus.DOWN || Obj.STATUS == MyStatus.UP
                        || Obj.STATUS == MyStatus.CHANGE||Obj.STATUS==MyStatus.SHOT||Obj.STATUS==MyStatus.INVI)
                    {
                        if ((STATUS == MyStatus.ACTIVE || STATUS == MyStatus.RUN) && dir == DIR.TOP)
                        {
                            STATUS = MyStatus.STATIC;
                            VELOC = Vector3.Zero;
                            SIZE = new Vector3(50, 50, 0);
                        }
                        if (STATUS == MyStatus.STATIC && dir == DIR.LEFT)
                        {
                            STATUS = MyStatus.RUN;
                            POSITION = new Vector3(Obj.POSITION.X + 50, POSITION.Y, POSITION.Z);
                            VELOC = new Vector3(0.6f, 0.0f, 0.0f);
                        }
                        if (STATUS == MyStatus.STATIC && dir == DIR.RIGHT)
                        {
                            STATUS = MyStatus.RUN;
                            POSITION = new Vector3(Obj.POSITION.X -SIZE.X, POSITION.Y, POSITION.Z);
                            VELOC = new Vector3(-0.6f, 0.0f, 0.0f);
                        }
                    }
                    //if (Obj.STATUS == MyStatus.INVI)
                    //{
                    //    if(STATUS==MyStatus.ACTIVE||STATUS==MyStatus.RUN)
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
                case MyID.PIPE:
                case MyID.BASE:
                case MyID.BRICK_BREAK:
                case MyID.BRICK_HARD:
                case MyID.QUESTION_MARK:
                    if (STATUS == MyStatus.ACTIVE || STATUS == MyStatus.RUN ||
                        STATUS == MyStatus.STATIC)
                    {
                        if (Obj.STATUS == MyStatus.ACTIVE || Obj.STATUS == MyStatus.RUN || Obj.STATUS == MyStatus.STATIC || Obj.STATUS == MyStatus.BEFORE_DEATH3)
                        {
                            switch (dir)
                            {
                                case DIR.BOTTOM:
                                    if (Obj.VELOC.Y != 0)
                                    {
                                        VELOC = new Vector3(VELOC.X, -0.4f, 0);
                                        STATUS = MyStatus.BEFORE_DEATH1;
                                    }
                                    VELOC = new Vector3(VELOC.X, 0, 0);
                                    POSITION = new Vector3(POSITION.X, Obj.POSITION.Y - SIZE.Y + 1, POSITION.Z);
                                    break;
                                case DIR.LEFT:
                                    if (VELOC.X < 0)
                                    {
                                        VELOC = new Vector3(Math.Abs(VELOC.X), VELOC.Y, 0);
                                    }
                                    POSITION = new Vector3(Obj.POSITION.X + Obj.SIZE.X, POSITION.Y, POSITION.Z);
                                    break;
                                case DIR.RIGHT:
                                    if (VELOC.X > 0)
                                    {
                                        VELOC = new Vector3(-1.0f * Math.Abs(VELOC.X), VELOC.Y, 0);
                                    }
                                    POSITION = new Vector3(Obj.POSITION.X - SIZE.X, POSITION.Y, POSITION.Z);
                                    break;
                            }
                        }
                    }
                    break;
                case MyID.BULLET:
                    if (STATUS == MyStatus.ACTIVE||STATUS == MyStatus.RUN||STATUS==MyStatus.STATIC)
                    {
                        if (Obj.STATUS == MyStatus.ACTIVE)
                        {
                            VELOC = new Vector3(VELOC.X, -0.4f, 0);
                            STATUS = MyStatus.BEFORE_DEATH1;
                        }
                    }
                    break;
                case MyID.TITLE:
                    if (STATUS == MyStatus.ACTIVE || STATUS == MyStatus.RUN || STATUS == MyStatus.STATIC)
                    {
                        if ((STATUS == MyStatus.ACTIVE || STATUS == MyStatus.STATIC) && (Obj.STATUS == MyStatus.RUN))
                        {
                            VELOC = new Vector3(VELOC.X, -0.3f, 0);
                            STATUS = MyStatus.BEFORE_DEATH1;
                        }
                        else
                        {
                            if (STATUS != MyStatus.RUN)
                            {
                                if (dir == DIR.LEFT)
                                {
                                    VELOC = new Vector3(Math.Abs(VELOC.X), VELOC.Y, 0);
                                }
                                if (dir == DIR.RIGHT)
                                {
                                    VELOC = new Vector3(-Math.Abs(VELOC.X), VELOC.Y, 0);
                                }
                            }
                        }
                    }
                    break;
            }
        }
        public override void UpdateMove(GameTime mGameTime)
        {
            base.UpdateMove(mGameTime);
        }
        public override void Update(GameTime mGameTime)
        {
            UpdateAnimate(mGameTime);
            UpdateMove(mGameTime);
            switch (STATUS)
            {
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
                case MyStatus.ACTIVE:
                    SPRITE.Effect = (VELOC.X < 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                    base.Render(SpriteBactch);
                    break;
                case MyStatus.STATIC:
                    SPRITE.Effect = SpriteEffects.None;
                    base.Render(SpriteBactch);
                    break;
                case MyStatus.RUN:
                    SPRITE.Effect = SpriteEffects.None;
                    base.Render(SpriteBactch);
                    break;
                case MyStatus.BEFORE_DEATH1:
                    SPRITE.Effect = SpriteEffects.FlipVertically;
                    base.Render(SpriteBactch);
                    break;
            }
        }
    }
}
