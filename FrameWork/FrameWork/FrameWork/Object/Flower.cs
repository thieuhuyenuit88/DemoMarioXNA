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
    class Flower : MyObject
    {
        int Y;
        public Flower(Game _Game, int _X, int _Y, int _SX, int _SY, int _TotalFrame)
            : base(_Game, _X, _Y, _SX, _SY, _TotalFrame)
        {
            POSITION = new Vector3(POSITION.X, POSITION.Y, 0.9f);
            ID = MyID.FLOWER;
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
                        CURRENTFRAME = (++CURRENTFRAME) % 4;
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
                    if (STATUS == MyStatus.ACTIVE)
                    {
                        STATUS = MyStatus.DEATH;
                    }
                    break;
            }
        }
        public override void UpdateMove(GameTime mGameTime)
        {
            switch (STATUS)
            {
                case MyStatus.DEATH:
                case MyStatus.START:
                    break;
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
                        VELOC = new Vector3(0, 0, 0);

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
