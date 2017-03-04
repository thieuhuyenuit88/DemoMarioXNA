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
namespace FrameWork
{
    class Mario:MyObject
    {
        public MyObject Visible;
        private Vector2 scale;
        public int dri;
        public Game Game;
        public int bullets;
        public Mario(Game _Game, int _X, int _Y, int _SX, int _SY, int _TotalFrame, MyObject _visible)
            : base(_Game, _X, _Y, _SX, _SY, _TotalFrame)
        {
            Game = _Game;
            POSITION = new Vector3(POSITION.X, POSITION.Y, 1);
            ID = MyID.MARIO_SUPER;
            STATUS = MyStatus.ACTIVE;
            dri = 1;
            Visible = _visible;
            bullets = 20;
            scale = new Vector2(1.0f,1.0f);
            ACCEL = new Vector3(0, 0.004f, 0);
            SPRITE = RSManager.Instance(_Game).SPRITE(ID);
        }
        public override void ActionCollision(MyObject Obj)
        {
            DIR Dir = DirectionCollision(Obj);
            switch (Obj.ID)
            {
                case MyID.PIPE:
                case MyID.BASE:
                case MyID.BRICK_BREAK:
                case MyID.BRICK_HARD:
                case MyID.QUESTION_MARK:
                    if (STATUS == MyStatus.ACTIVE || STATUS == MyStatus.DOWN || STATUS == MyStatus.UP||STATUS==MyStatus.INVI||STATUS==MyStatus.CHANGE||STATUS==MyStatus.SHOT||STATUS==MyStatus.WIN)
                    {
                        if (Obj.STATUS == MyStatus.ACTIVE || Obj.STATUS == MyStatus.STATIC || Obj.STATUS == MyStatus.RUN || Obj.STATUS == MyStatus.BEFORE_DEATH3)
                        {
                            switch (Dir)
                            {
                                case DIR.BOTTOM:
                                    VELOC = new Vector3(VELOC.X, 0, 0);
                                    POSITION = new Vector3(POSITION.X, Obj.POSITION.Y - SIZE.Y + 1, POSITION.Z);
                                    break;
                                case DIR.TOP:
                                    VELOC = new Vector3(VELOC.X, -1 * VELOC.Y, 0);
                                    POSITION = new Vector3(POSITION.X, Obj.POSITION.Y + Obj.SIZE.Y, POSITION.Z);
                                    break;
                                case DIR.LEFT:
                                    if (VELOC.X < 0)
                                    {
                                        VELOC = new Vector3(0, VELOC.Y, 0);
                                    }
                                    POSITION = new Vector3(Obj.POSITION.X + Obj.SIZE.X, POSITION.Y, POSITION.Z);
                                    break;
                                case DIR.RIGHT:
                                    if (VELOC.X > 0)
                                    {
                                        VELOC = new Vector3(0, VELOC.Y, 0);
                                    }
                                    POSITION = new Vector3(Obj.POSITION.X - SIZE.X, POSITION.Y, POSITION.Z);
                                    break;
                            }
                        }
                    }
                            break;
                case MyID.MUSHROOM_BIG:
                                if (STATUS == MyStatus.ACTIVE||STATUS==MyStatus.INVI)
                                {
                                        switch (ID)
                                        {
                                            case MyID.MARIO_SMALL:
                                                if (Obj.STATUS == MyStatus.ACTIVE||Obj.STATUS==MyStatus.RUN)
                                                {
                                                    STATUS = MyStatus.UP;
                                                }
                                                break;
                                        }
                                }                           
                            break;
                case MyID.FLOWER:
                                if (STATUS == MyStatus.ACTIVE||STATUS==MyStatus.INVI)
                                {
                                        switch (ID)
                                        {
                                            case MyID.MARIO_SMALL:
                                                if (Obj.STATUS == MyStatus.ACTIVE||Obj.STATUS==MyStatus.RUN)
                                                {
                                                    STATUS = MyStatus.UP;
                                                }
                                                break;
                                            case MyID.MARIO_BIG:
                                                if (Obj.STATUS == MyStatus.ACTIVE||Obj.STATUS==MyStatus.RUN)
                                                {
                                                    STATUS = MyStatus.CHANGE;

                                                    ID = MyID.MARIO_SUPER;
                                                    SPRITE = RSManager.Instance(Game).SPRITE(ID);
                                                }
                                                break;
                                        }
                                }                           
                            break;
                case MyID.MUSHROOM_1UP:
                            break;
                case MyID.MUSHROOM_TOXIC:
                            if (STATUS == MyStatus.ACTIVE || STATUS == MyStatus.SHOT || STATUS == MyStatus.INVI)
                                {
                                    if (Obj.STATUS == MyStatus.ACTIVE)
                                    {
                                        switch (Dir)
                                        {
                                            case DIR.BOTTOM:
                                                VELOC = new Vector3(VELOC.X, -1 * VELOC.Y * 0.8f, 0);
                                                POSITION = new Vector3(POSITION.X, Obj.POSITION.Y - SIZE.Y + 1, POSITION.Z);
                                                break;
                                            case DIR.RIGHT:
                                            case DIR.LEFT:
                                                if (STATUS != MyStatus.INVI)
                                                {
                                                    if (ID == MyID.MARIO_SMALL)
                                                    {
                                                        VELOC = new Vector3(0, -0.9f, 0);
                                                        STATUS = MyStatus.BEFORE_DEATH1;
                                                    }
                                                    if (ID == MyID.MARIO_BIG)
                                                    {
                                                        STATUS = MyStatus.DOWN;

                                                        ID = MyID.MARIO_SMALL;
                                                        SPRITE = RSManager.Instance(Game).SPRITE(ID);
                                                        SIZE = new Vector3(SIZE.X, 50, 0);
                                                        POSITION = new Vector3(POSITION.X, POSITION.Y + 50, POSITION.Z);
                                                    }
                                                    if (ID == MyID.MARIO_SUPER)
                                                    {
                                                        STATUS = MyStatus.CHANGE;

                                                        ID = MyID.MARIO_BIG;
                                                        SPRITE = RSManager.Instance(Game).SPRITE(ID);
                                                    }
                                                }
                                                break;
                                        }  
                                    }
                                }                          
                            break;
                case MyID.TITLE:
                                if (Dir == DIR.BOTTOM)
                                {
                                    if (STATUS == MyStatus.ACTIVE||STATUS==MyStatus.SHOT||STATUS==MyStatus.INVI)
                                    {
                                        if (Obj.STATUS == MyStatus.STATIC || Obj.STATUS == MyStatus.ACTIVE || Obj.STATUS == MyStatus.RUN)
                                        {
                                            POSITION = new Vector3(POSITION.X, Obj.POSITION.Y - SIZE.Y - 5, POSITION.Z);
                                            VELOC = new Vector3(VELOC.X, -1.0f, 0);
                                        }
                                    }
                                }
                                else
                                {
                                    if (STATUS == MyStatus.ACTIVE || STATUS == MyStatus.SHOT)
                                    {
                                        if (Obj.STATUS == MyStatus.ACTIVE || Obj.STATUS == MyStatus.RUN)
                                        {
                                            if (ID == MyID.MARIO_SMALL)
                                            {
                                                VELOC = new Vector3(0, -0.9f, 0);
                                                STATUS = MyStatus.BEFORE_DEATH1;
                                            }
                                            if (ID == MyID.MARIO_BIG)
                                            {
                                                STATUS = MyStatus.DOWN;

                                                ID = MyID.MARIO_SMALL;
                                                SPRITE = RSManager.Instance(Game).SPRITE(ID);
                                                SIZE = new Vector3(SIZE.X, 50, 0);
                                                POSITION = new Vector3(POSITION.X, POSITION.Y + 50, POSITION.Z);
                                            }
                                            if (ID == MyID.MARIO_SUPER)
                                            {
                                                STATUS = MyStatus.CHANGE;

                                                ID = MyID.MARIO_BIG;
                                                SPRITE = RSManager.Instance(Game).SPRITE(ID);
                                            }
                                        }
                                    }
                                }
                            break;
                case MyID.BOSS:
                            if (STATUS == MyStatus.ACTIVE || STATUS == MyStatus.SHOT || STATUS == MyStatus.INVI)
                            {
                                if (Obj.STATUS == MyStatus.ACTIVE || Obj.STATUS == MyStatus.SHOT || Obj.STATUS == MyStatus.HURT)
                                {
                                    switch (Dir)
                                    {
                                        case DIR.BOTTOM:
                                            VELOC = new Vector3(VELOC.X, -1 * VELOC.Y * 0.9f, 0);
                                            POSITION = new Vector3(POSITION.X, Obj.POSITION.Y - SIZE.Y + 1, POSITION.Z);
                                            break;
                                        case DIR.RIGHT:
                                        case DIR.LEFT:
                                            if (STATUS != MyStatus.INVI)
                                            {
                                                if (ID == MyID.MARIO_SMALL)
                                                {
                                                    VELOC = new Vector3(0, -0.9f, 0);
                                                    STATUS = MyStatus.BEFORE_DEATH1;
                                                }
                                                if (ID == MyID.MARIO_BIG)
                                                {
                                                    STATUS = MyStatus.DOWN;

                                                    ID = MyID.MARIO_SMALL;
                                                    SPRITE = RSManager.Instance(Game).SPRITE(ID);
                                                    SIZE = new Vector3(SIZE.X, 50, 0);
                                                    POSITION = new Vector3(POSITION.X, POSITION.Y + 50, POSITION.Z);
                                                }
                                                if (ID == MyID.MARIO_SUPER)
                                                {
                                                    STATUS = MyStatus.CHANGE;

                                                    ID = MyID.MARIO_BIG;
                                                    SPRITE = RSManager.Instance(Game).SPRITE(ID);
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                case MyID.BULLETBOSS:
                            if (STATUS == MyStatus.ACTIVE || STATUS == MyStatus.SHOT)
                            {
                                if (Obj.STATUS == MyStatus.ACTIVE)
                                {
                                    if (ID == MyID.MARIO_SMALL)
                                    {
                                        VELOC = new Vector3(0, -0.9f, 0);
                                        STATUS = MyStatus.BEFORE_DEATH1;
                                    }
                                    if (ID == MyID.MARIO_BIG)
                                    {
                                        STATUS = MyStatus.DOWN;

                                        ID = MyID.MARIO_SMALL;
                                        SPRITE = RSManager.Instance(Game).SPRITE(ID);
                                        SIZE = new Vector3(SIZE.X, 50, 0);
                                        POSITION = new Vector3(POSITION.X, POSITION.Y + 50, POSITION.Z);
                                    }
                                    if (ID == MyID.MARIO_SUPER)
                                    {
                                        STATUS = MyStatus.CHANGE;

                                        ID = MyID.MARIO_BIG;
                                        SPRITE = RSManager.Instance(Game).SPRITE(ID);
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
                case MyStatus.WIN:
                    VELOC = new Vector3(0.2f, 0, 0);
                    base.UpdateMove(mGameTime);
                    break;
                case MyStatus.SHOT:
                case MyStatus.CHANGE:
                case MyStatus.INVI:
                case MyStatus.UP:
                case MyStatus.DOWN:
                case MyStatus.BEFORE_DEATH1:
                case MyStatus.ACTIVE:
                    if (ACCEL.X == 0)
                    {
                        if (VELOC.X != 0)
                        {
                            if (VELOC.X > 0)
                            {
                                VELOC = new Vector3(VELOC.X - 0.008f, VELOC.Y, 0);
                                if (VELOC.X < 0)
                                {
                                    VELOC = new Vector3(0, VELOC.Y, 0);
                                }
                            }
                            else
                            {

                                VELOC = new Vector3(VELOC.X + 0.008f, VELOC.Y, 0);
                                if (VELOC.X > 0)
                                {
                                    VELOC = new Vector3(0, VELOC.Y, 0);
                                }
                            }
                        }
                    }

                    if (VELOC.X < -0.50f) VELOC = new Vector3(-0.50f, VELOC.Y, 0);
                    if (VELOC.X > 0.50f) VELOC = new Vector3(0.50f, VELOC.Y, 0);

                    if (POSITION.X < 0)
                    {
                        POSITION = new Vector3(0, POSITION.Y, POSITION.Z);
                        VELOC = new Vector3(0, VELOC.Y, 0);
                    }
                    /*
                     neu' roi xuong vuc
                     */
                    if (STATUS != MyStatus.BEFORE_DEATH1)
                    {
                        if (POSITION.Y > Game.GraphicsDevice.Viewport.Height)
                        {
                            ID = MyID.MARIO_SMALL;
                            SPRITE = RSManager.Instance(Game).SPRITE(ID);
                            SIZE = new Vector3(SIZE.X, 50, 0);

                            VELOC = new Vector3(0, -1.8f, 0);
                            STATUS = MyStatus.BEFORE_DEATH1;
                        }
                    }
                    base.UpdateMove(mGameTime);
                    ACCEL = new Vector3(0, ACCEL.Y, 0);
                    break;
            }
            
        }
        public override void UpdateAnimate(GameTime mGameTime)
        {
            switch (STATUS)
            {
                //case MyStatus.UP:
                //case MyStatus.DOWN:
                case MyStatus.WIN:
                case MyStatus.CHANGE:
                case MyStatus.INVI:
                case MyStatus.ACTIVE:
                    if (VELOC.X != 0)
                    {
                        if (ACCEL.X * VELOC.X < 0)
                            CURRENTFRAME = 4;
                        else
                        {
                            if (TIME.StopWatch(100, mGameTime))
                            {
                                CURRENTFRAME = (CURRENTFRAME + 1) % 3;
                            }

                        }
                    }
                    else
                    {
                        CURRENTFRAME = 0;
                    }
                    if (VELOC.Y != 0)
                    {
                        CURRENTFRAME = 3;
                    }
                    if (dri * VELOC.X < 0)
                    {
                        dri *= -1;
                    }
                    if (STATUS == MyStatus.INVI||STATUS==MyStatus.CHANGE)
                    {
                        PERCENT = ((Game1)Game).rnd.Next(0, 255);
                    }
                    //if (STATUS == MyStatus.UP || STATUS == MyStatus.DOWN)
                    //{
                    //    scale = new Vector2((float)((Game1)Game).rnd.Next(3, 6) / 3, (float)((Game1)Game).rnd.Next(3, 6) / 3);
                    //    PERCENT = ((Game1)Game).rnd.Next(0, 255);
                    //}
                    break;
                case MyStatus.SHOT:
                    CURRENTFRAME = 2;
                    break;
                case MyStatus.BEFORE_DEATH1:
                    CURRENTFRAME = 5;
                    break;
                case MyStatus.UP:
                case MyStatus.DOWN:
                    CURRENTFRAME = 0;
                    scale = new Vector2((float)((Game1)Game).rnd.Next(3, 6) / 3, (float)((Game1)Game).rnd.Next(3, 6) / 3);
                    PERCENT = ((Game1)Game).rnd.Next(0, 255);
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
                    TimeUpdate += mGameTime.ElapsedGameTime.Milliseconds;
                    if (TimeUpdate > 100)
                    {
                        TimeUpdate -= 100;
                        STATUS = MyStatus.ACTIVE;
                    }
                    break;
                case MyStatus.BEFORE_DEATH1:
                    TimeUpdate += mGameTime.ElapsedGameTime.Milliseconds;
                    if (TimeUpdate > 4000)
                    {
                        TimeUpdate -= 4000;
                        STATUS = MyStatus.DEATH;
                    }
                    break;
                case MyStatus.UP:
                    TimeUpdate += mGameTime.ElapsedGameTime.Milliseconds;
                    if (TimeUpdate > 500)
                    {
                        TimeUpdate -= 500;

                        ID = MyID.MARIO_BIG;
                        SPRITE = RSManager.Instance(Game).SPRITE(ID);
                        SIZE = new Vector3(SIZE.X, 100, 0);
                        POSITION = new Vector3(POSITION.X, POSITION.Y - 50, POSITION.Z);

                        STATUS = MyStatus.ACTIVE;
                    }
                    break;
                case MyStatus.DOWN:
                    TimeUpdate += mGameTime.ElapsedGameTime.Milliseconds;
                    if (TimeUpdate > 500)
                    {
                        TimeUpdate -= 500;

                        STATUS = MyStatus.INVI;
                    }
                    break;
                case MyStatus.INVI:
                    TimeUpdate += mGameTime.ElapsedGameTime.Milliseconds;
                    if (TimeUpdate > 2000)
                    {
                        TimeUpdate -= 2000;

                        STATUS = MyStatus.ACTIVE;
                    }
                    break;
                case MyStatus.CHANGE:
                    TimeUpdate += mGameTime.ElapsedGameTime.Milliseconds;
                    if (TimeUpdate > 500)
                    {
                        TimeUpdate -= 500;

                        STATUS = MyStatus.ACTIVE;
                    }
                    break;
            }
        }
        public override void Render(SpriteBatch SpriteBactch)
        {
            switch (STATUS)
            {
                case MyStatus.DOWN:
                case MyStatus.UP:
                    base.COLOR = new Color(255, 255, 255, PERCENT);
                    SPRITE.Scale =new Vector2(scale.X,scale.Y);
                    SPRITE.Effect = (dri > 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                    base.Render(SpriteBactch);
                    base.COLOR = new Color(255, 255, 255, 255);
                    SPRITE.Scale = new Vector2(1.0f, 1.0f);
                    break;
                case MyStatus.CHANGE:
                case MyStatus.INVI:
                    base.COLOR = new Color(255, 255, 255, PERCENT);
                    SPRITE.Effect = (dri > 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                    base.Render(SpriteBactch);
                    base.COLOR = new Color(255, 255, 255, 255);
                    break;
                case MyStatus.WIN:
                case MyStatus.SHOT:
                case MyStatus.ACTIVE:
                    SPRITE.Effect = (dri > 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                    base.Render(SpriteBactch);
                    break;
                case MyStatus.BEFORE_DEATH1:
                    SPRITE.Effect = SpriteEffects.None;
                    base.Render(SpriteBactch);
                    break;
            }

        }
    }
}
