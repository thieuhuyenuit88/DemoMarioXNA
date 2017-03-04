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
    class Bullet:MyObject
    {
        Game game;
        int dri;
        public Bullet(Game _Game,int _X, int _Y, int _SX, int _SY, int _TotalFrame, int _dri)
            : base(_Game, _X, _Y, _SX, _SY, _TotalFrame)
        {
            game = _Game;
            POSITION = new Vector3(POSITION.X, POSITION.Y, 0.9f);
            ID = MyID.BULLET;
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
                    SIZE = new Vector3(26, 26, 0);
                    if (TIME.StopWatch(100, mGameTime))
                    {
                        CURRENTFRAME = (CURRENTFRAME + 1) % 4;
                    }
                    break;
                case MyStatus.BEFORE_DEATH1:
                    SIZE = new Vector3(34, 34, 0);
                    CURRENTFRAME = (CURRENTFRAME - 3) % 6 + 4;
                    break;
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
                    if (STATUS == MyStatus.ACTIVE)
                    {
                        if (Obj.STATUS == MyStatus.ACTIVE || Obj.STATUS == MyStatus.RUN )
                        {
                            if (dir == DIR.BOTTOM)
                            {
                                POSITION = new Vector3(POSITION.X, Obj.POSITION.Y - SIZE.Y ,POSITION.Z);
                                VELOC = new Vector3(VELOC.X, -0.25f, 0);
                            }
                            else
                                STATUS = MyStatus.BEFORE_DEATH1;
                        }
                    }
                    break;
                case MyID.MUSHROOM_TOXIC:
                case MyID.TITLE:
                case MyID.BOSS:
                    if (STATUS == MyStatus.ACTIVE)
                    {
                        if (Obj.STATUS == MyStatus.ACTIVE || Obj.STATUS == MyStatus.RUN || Obj.STATUS == MyStatus.STATIC || Obj.STATUS == MyStatus.SHOT)
                        {
                            STATUS = MyStatus.BEFORE_DEATH1;
                        }
                    }
                    break;
            }
        }

        public override void Update(GameTime mGameTime)
        {
            UpdateAnimate(mGameTime);
            UpdateMove(mGameTime);
            switch (STATUS)
            {
                case MyStatus.BEFORE_DEATH1:
                    TimeUpdate += mGameTime.ElapsedGameTime.Milliseconds;
                    if (TimeUpdate > 200)
                    {
                        TimeUpdate -= 200;
                        STATUS = MyStatus.DEATH;
                    }
                    break;
            }
        }

        public override void Render(SpriteBatch SpriteBactch)
        {
            switch (STATUS)
            {
                case MyStatus.BEFORE_DEATH1:
                case MyStatus.ACTIVE:
                    SPRITE.Effect = (dri > 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                    base.Render(SpriteBactch);
                    break;
            }
        }
    }
}
