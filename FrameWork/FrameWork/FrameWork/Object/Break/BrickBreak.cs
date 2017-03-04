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
    class BrickBreak:MyObject
    {
        int Y;
        Game game;
        List<MyObject> ListBreak;

        public BrickBreak(Game _Game, int _X, int _Y, int _SX, int _SY, int _TotalFrame)
            : base(_Game, _X, _Y, _SX, _SY, _TotalFrame)
        {
            game = _Game;
            POSITION = new Vector3(POSITION.X, POSITION.Y, 0.9f);
            ID = MyID.BRICK_BREAK;
            STATUS = MyStatus.ACTIVE;
            SPRITE = RSManager.Instance(_Game).SPRITE(ID);
            ListBreak = new List<MyObject>();
            Y=_Y; 
        }
        public override void UpdateMove(GameTime mGameTime) 
        {
            switch (STATUS)
            {
                case MyStatus.RUN:
                    base.UpdateMove(mGameTime);
                    break;
                case MyStatus.BEFORE_DEATH3:
                    for (int i = 0; i < ListBreak.Count; i++)
                    {
                        ListBreak[i].UpdateMove(mGameTime);
                    }
                    if (Y - POSITION.Y > 10)
                    {
                        POSITION = new Vector3(POSITION.X, 0, POSITION.Z);
                    }
                    else
                        base.UpdateMove(mGameTime);
                    break;
            }
            
        }
        public override void UpdateAnimate(GameTime mGameTime){}
        public override void Update(GameTime mGameTime)
        {
            UpdateAnimate(mGameTime);
            UpdateMove(mGameTime);
            switch (STATUS)
            {
                case MyStatus.RUN:
                    if (POSITION.Y > Y)
                    {
                        VELOC = new Vector3(0, 0, 0);
                        ACCEL = new Vector3(0, 0, 0);
                        POSITION = new Vector3(POSITION.X, Y, POSITION.Z);
                        STATUS = MyStatus.ACTIVE;
                    }
                    break;
                case MyStatus.BEFORE_DEATH3:
                    TimeUpdate += mGameTime.ElapsedGameTime.Milliseconds;
                    if (TimeUpdate > 800)
                    {
                        TimeUpdate -= 800;
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
        public override void ActionCollision(MyObject Obj)
        {
            DIR dir = DirectionCollision(Obj);
            switch (Obj.ID)
            {
                case MyID.MARIO_SMALL:
                    if (Obj.STATUS == MyStatus.ACTIVE || Obj.STATUS == MyStatus.DOWN || Obj.STATUS == MyStatus.UP || Obj.STATUS == MyStatus.INVI || Obj.STATUS == MyStatus.CHANGE || Obj.STATUS == MyStatus.SHOT)
                    {
                        if (STATUS == MyStatus.ACTIVE && dir == DIR.BOTTOM)
                        {
                            STATUS = MyStatus.RUN;
                            ACCEL = new Vector3(0, 0.001f, 0);
                            VELOC = new Vector3(0, -0.17f, 0);
                        }
                    }
                    break;
                case MyID.MARIO_BIG:
                case MyID.MARIO_SUPER:
                    if (Obj.STATUS == MyStatus.ACTIVE)
                    {
                        if (STATUS == MyStatus.ACTIVE && dir == DIR.BOTTOM)
                        {
                            STATUS = MyStatus.BEFORE_DEATH3;
                            for (int i = 0; i < 4; i++)
                            {
                                ListBreak.Add(new Break(game,(int)POSITION.X + 25 *(i%2),(int)POSITION.Y + 25*(i/2),25,25,4));
                            }
                            for (int i = 0; i < ListBreak.Count; i++)
                            {
                                if (i == 0)
                                    ListBreak[i].VELOC = new Vector3(-0.5f, -0.6f, 0);
                                else if(i==2)
                                    ListBreak[i].VELOC = new Vector3(-0.7f, -0.8f, 0);
                                else if(i==1)
                                    ListBreak[i].VELOC = new Vector3(0.5f, -0.6f, 0);
                                else
                                    ListBreak[i].VELOC = new Vector3(0.7f, -0.8f, 0);
                            }
                            //ACCEL = new Vector3(0, 0.001f, 0);
                            VELOC = new Vector3(0, -1.0f, 0);
                        }
                    }
                    break;
            }
        }
        public override void Render(SpriteBatch SpriteBactch)
        {
            SPRITE.CurFrame = 1;
            switch (STATUS)
            {
                case MyStatus.RUN:
                case MyStatus.ACTIVE:
                    SPRITE.Rotation = 0;
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
