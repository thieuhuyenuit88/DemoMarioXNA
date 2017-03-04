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
    class BulletBoss: MyObject
    {
        int dri;
        public BulletBoss(Game _Game,int _X, int _Y, int _SX, int _SY, int _TotalFrame, int _dri)
            : base(_Game, _X, _Y, _SX, _SY, _TotalFrame)
        {
            POSITION = new Vector3(POSITION.X, POSITION.Y, 0.9f);
            ID = MyID.BULLETBOSS;
            STATUS = MyStatus.START;
            dri = _dri;
            SPRITE = RSManager.Instance(_Game).SPRITE(ID);
        }

        public override void UpdateMove(GameTime mGameTime)
        {
            switch (STATUS)
            {
                case MyStatus.ACTIVE:
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
                //case MyStatus.BEFORE_DEATH1:
                //    CURRENTFRAME = 2;
                //    break;
            }
        }
        public override void ActionCollision(MyObject Obj)
        {
            DIR dir = DirectionCollision(Obj);
            switch (Obj.ID)
            {
                case MyID.BRICK_BREAK:
                case MyID.BASE:
                case MyID.QUESTION_MARK:
                case MyID.BRICK_HARD:
                case MyID.PIPE:
                case MyID.MARIO_BIG:
                case MyID.MARIO_SMALL:
                case MyID.MARIO_SUPER:
                    STATUS = MyStatus.DEATH;//.BEFORE_DEATH1;
                    break;
            }
        }

        public override void Update(GameTime mGameTime)
        {
            UpdateAnimate(mGameTime);
            UpdateMove(mGameTime);
            //switch (STATUS)
            //{
            //    case MyStatus.BEFORE_DEATH1:
            //        TimeUpdate += mGameTime.ElapsedGameTime.Milliseconds;
            //        if (TimeUpdate > 200)
            //        {
            //            TimeUpdate -= 200;
            //            STATUS = MyStatus.DEATH;
            //        }
            //        break;
            //}
        }
        public override void Render(SpriteBatch SpriteBactch)
        {
            switch (STATUS)
            {
                //case MyStatus.BEFORE_DEATH1:
                case MyStatus.ACTIVE:
                    SPRITE.Effect = (dri > 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                    base.Render(SpriteBactch);
                    break;
            }
        }
    }
}
