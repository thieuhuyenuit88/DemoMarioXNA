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
    class Mushroom1Up:MyObject
    {
        int Y;
        public Mushroom1Up(Game _Game, int _X, int _Y, int _SX, int _SY, int _TotalFrame)
            : base(_Game, _X, _Y, _SX, _SY, _TotalFrame)
        {
            POSITION = new Vector3(POSITION.X, POSITION.Y, 0.9f);
            ID = MyID.MUSHROOM_1UP;
            STATUS = MyStatus.START;
            SPRITE = RSManager.Instance(_Game).SPRITE(ID);
            Y = _Y;
        }

        public override void UpdateAnimate(GameTime mGameTime)
        {
            switch (STATUS)
            {
                case MyStatus.RUN:
                case MyStatus.ACTIVE:
                    if (TIME.StopWatch(100, mGameTime))
                    {
                        CURRENTFRAME = 1;
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
                    if (STATUS == MyStatus.ACTIVE||STATUS==MyStatus.RUN)
                    {
                        if (Obj.STATUS != MyStatus.DEATH || Obj.STATUS != MyStatus.BEFORE_DEATH1)
                            STATUS = MyStatus.DEATH;
                    }
                    break;
                case MyID.BASE:
                case MyID.QUESTION_MARK:
                case MyID.BRICK_BREAK:
                case MyID.BRICK_HARD:
                case MyID.PIPE:
                    if (STATUS == MyStatus.ACTIVE)
                    {
                        if (Obj.STATUS == MyStatus.ACTIVE || Obj.STATUS == MyStatus.RUN || Obj.STATUS == MyStatus.STATIC || Obj.STATUS == MyStatus.BEFORE_DEATH3)
                        {
                            if (dir == DIR.BOTTOM)
                            {
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
            }
        }
        public override void UpdateMove(GameTime mGameTime)
        {
            switch (STATUS)
            {
                case MyStatus.RUN:
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
                case MyStatus.RUN:
                    if (POSITION.Y < Y - 50)
                    {
                        STATUS = MyStatus.ACTIVE;
                        POSITION = new Vector3(POSITION.X, Y - 50, POSITION.Z);
                        ACCEL = new Vector3(0, 0.001f, 0);
                        VELOC = new Vector3(0.2f, 0, 0);
                    }
                    break;
            }
        }
        public override void Render(SpriteBatch SpriteBactch)
        {
            switch (STATUS)
            {
                case MyStatus.START:
                    break;
                case MyStatus.RUN:
                case MyStatus.ACTIVE:
                    base.Render(SpriteBactch);
                    break;
            }
        }
    }
}