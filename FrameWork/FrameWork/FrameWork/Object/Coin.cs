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
    class Coin:MyObject
    {
        float rota;
        public Coin(Game _Game, int _X, int _Y, int _SX, int _SY, int _TotalFrame, int _Status)
            : base(_Game, _X, _Y, _SX, _SY, _TotalFrame)
        {
            POSITION = new Vector3(POSITION.X, POSITION.Y, 0.9f);
            ID = MyID.COIN;
            if (_Status == 1)
            {
                STATUS = MyStatus.ACTIVE;
            }
            else
                STATUS = MyStatus.START;
            SPRITE = RSManager.Instance(_Game).SPRITE(ID);
        }

        public override void UpdateAnimate(GameTime mGameTime)
        {
            switch (STATUS)
            {
                case MyStatus.ACTIVE:
                    if (mGameTime.TotalGameTime.TotalMilliseconds%600 <100)
                    {
                        CURRENTFRAME = 0;
                    }
                    else
                        if (mGameTime.TotalGameTime.TotalMilliseconds % 600 < 200)
                        {
                            CURRENTFRAME = 1;
                        }
                        else
                            if (mGameTime.TotalGameTime.TotalMilliseconds % 600 < 300)
                            {
                                CURRENTFRAME = 2;
                            }
                            else
                                if (mGameTime.TotalGameTime.TotalMilliseconds % 600 < 400)
                                {
                                    CURRENTFRAME =3;
                                }
                                else
                                    if (mGameTime.TotalGameTime.TotalMilliseconds % 600 < 500)
                                    {
                                        CURRENTFRAME = 4;
                                    }
                                    else
                                        CURRENTFRAME = 5;
                    break;
                case MyStatus.START:
                    break;
                case MyStatus.BEFORE_DEATH1:
                    if (TIME.StopWatch(100, mGameTime))
                    {
                        //CURRENTFRAME = (CURRENTFRAME + 1) % 6;
                        CURRENTFRAME = (CURRENTFRAME - 2) % 4 + 3;
                    }
                    rota += mGameTime.ElapsedGameTime.Milliseconds;
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
                        if (STATUS == MyStatus.ACTIVE)
                        {
                            STATUS = MyStatus.BEFORE_DEATH1;
                            //ACCEL = new Vector3(0, 0.001f, 0);
                            //VELOC = new Vector3(0, -0.5f, 0);                       
                        }
                    }
                    break;
                case MyID.BRICK_BREAK:
                    if (STATUS == MyStatus.ACTIVE)
                    {
                        if (Obj.STATUS == MyStatus.RUN || Obj.STATUS == MyStatus.ACTIVE)
                        {
                            if (dir == DIR.BOTTOM)
                            {
                                if (Obj.VELOC.Y != 0)
                                {
                                    VELOC = new Vector3(VELOC.X, Obj.VELOC.Y - 0.02f, 0);
                                    STATUS = MyStatus.BEFORE_DEATH1;
                                }
                            }
                        }
                    }
                    break;
            }
        }
        public override void UpdateMove(GameTime mGameTime)
        {
            if(STATUS==MyStatus.BEFORE_DEATH1)
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
                    if (TimeUpdate > 500)
                    {
                        TimeUpdate -= 500;
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
                    SPRITE.Rotation=0;
                    base.Render(SpriteBactch);
                    break;
                case MyStatus.START:
                    break;
                case MyStatus.BEFORE_DEATH1:
                    SPRITE.Rotation = 2*(float)((rota) * (Math.PI)) / 180.0f;
                    base.Render(SpriteBactch);
                    break;
            }            
        }
    }
}
