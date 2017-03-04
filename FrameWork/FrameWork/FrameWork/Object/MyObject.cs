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
using FrameWork.Environment.Timer;
using FrameWork.FrameWork.Graphics;

namespace FrameWork.FrameWork.Object
{
    enum MyID
    {
        MARIO_SMALL,MARIO_BIG,MARIO_SUPER, //mario
        BOSS, BOSS_BREAK, //boss
        TITLE, //rua`
        BULLET,//dan.
        BULLETBOSS, //dan. boss
        MUSHROOM_TOXIC, MUSHROOM_BIG, MUSHROOM_1UP,//nam'
        BASE,//gach. nen`
        PIPE,//ong'
        QUESTION_MARK,//gach. hoi?
        BRICK_BREAK,BREAK , BRICK_HARD,//gach. be?, gach. cung'
        COIN,// tien`
        FLOWER,//hoa
        FENCE,
        WINPOLE,
        POLE,
        POST,
        BUILDING,
        CLOUD,
        GRASS,
        MOUNTAIN_SMALL,
        MOUNTAIN_BIG,
        BLOOD
    }
    enum MyStatus
    {
        START, ACTIVE, RUN, UP, SHOT, HURT, DOWN, INVI,CHANGE, STATIC, BEFORE_DEATH1, BEFORE_DEATH2, BEFORE_DEATH3 ,DEATH, WIN
    }
    enum DIR
    {
        NONE,LEFT,RIGHT,TOP,BOTTOM
    }
    class MyObject
    {
        MyID Id;
        MyStatus Status, oldStatus;
        Vector3 Position;
        Vector3 Veloc;
        Vector3 Accel;
        Vector3 Size;
        Color Color;
        int CurrentFrame, TotalFrame;
        Timer Time;
        int percent;
        Sprite Sprite;

        public Double TimeUpdate;
        public MyObject(MyObject _Obj)
        {
            VELOC = new Vector3(_Obj.VELOC.X, _Obj.VELOC.Y, _Obj.VELOC.Z);
            POSITION = new Vector3(_Obj.POSITION.X, _Obj.POSITION.Y, _Obj.POSITION.Z);
            SIZE = new Vector3(_Obj.Size.X, _Obj.Size.Y, _Obj.Size.Z);
            ID = _Obj.ID;
            STATUS = _Obj.STATUS;
        }
        public MyObject(Game _Game, int x, int y, int sx, int sy, int TotalFrame)
        {
            POSITION = new Vector3(x, y, 0);
            Size = new Vector3(sx, sy, 0);
            VELOC = Vector3.Zero;
            ACCEL = Vector3.Zero;
            COLOR = Microsoft.Xna.Framework.Color.White;
            CurrentFrame = 0;
            Time = new Timer();
            TOTALFRAME = TotalFrame;
        }
        public MyID ID { 
            get { return Id; }
            set { Id = value; }
        }
        public Rectangle RECT
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y); }
        }
        public MyStatus STATUS {
            get { return Status; }
            set { Status = value; }
        }
        public MyStatus OLDSTATUS
        {
            get { return oldStatus; }
            set { oldStatus = value; }
        }
        public Vector3 POSITION {
            get { return Position; }
            set { Position = value; }
        }
        public Timer TIME
        {
            get { return Time; }
            set { Time = value; }
        }
        public Vector3 VELOC
        {
            get { return Veloc; }
            set { Veloc = value; }
        }
        public Vector3 ACCEL
        {
            get { return Accel; }
            set { Accel = value; }
        }
        public Vector3 SIZE
        {
            get { return Size; }
            set { Size = value; }
        }
        public Color COLOR
        {
            get { return Color; }
            set { Color = value; }
        }

        public int CURRENTFRAME
        {
            get { return CurrentFrame; }
            set { CurrentFrame = value; }
        }
   
        public int TOTALFRAME
        {
            get { return TotalFrame; }
            set { TotalFrame = value; }
        }
        
        public int PERCENT
        {
            get { return percent; }
            set { percent = value; }
        }
        public Sprite SPRITE
        {
            get { return Sprite; }
            set { Sprite = value; }
        }
        public DIR DirectionCollision(MyObject _Obj)
        {
            if (RECT.Intersects(_Obj.RECT))
            {
                float top = Math.Abs(RECT.Top - _Obj.RECT.Bottom);
                float botom = Math.Abs(RECT.Bottom - _Obj.RECT.Top);
                float left = Math.Abs(RECT.Left - _Obj.RECT.Right);
                float right = Math.Abs(RECT.Right - _Obj.RECT.Left);
                float rs = Math.Min(Math.Min(right, left), Math.Min(top, botom));
                if (rs == top)
                {
                    return DIR.TOP;
                }
                if (rs == botom)
                {
                    return DIR.BOTTOM;
                }
                if (rs == left)
                {
                    return DIR.LEFT;
                }
                if (rs == right)
                {
                    return DIR.RIGHT;
                }
             }
            return DIR.NONE;
            
        }
        virtual public void Init() { }
        virtual public void UpdateMove(GameTime mGameTime)
        {
           
            Position.X += Veloc.X * mGameTime.ElapsedGameTime.Milliseconds;
            Position.Y += Veloc.Y * mGameTime.ElapsedGameTime.Milliseconds;
            Veloc.X += Accel.X * mGameTime.ElapsedGameTime.Milliseconds;
            Veloc.Y += Accel.Y * mGameTime.ElapsedGameTime.Milliseconds;
        }
        virtual public void UpdateAnimate(GameTime mGameTime) { }
        virtual public void Update(GameTime mGameTime)
        {
            UpdateAnimate(mGameTime);
            UpdateMove(mGameTime);
        }
        virtual public void ActionCollision(MyObject Obj) 
        {
        }
        virtual public void Render(SpriteBatch SpriteBactch)
        {
            Sprite.Position = new Vector2(POSITION.X, POSITION.Y);
            Sprite.Depth = POSITION.Z;
            Sprite.Color = Color;
            Sprite.CurFrame = CurrentFrame;
            Sprite.Render(SpriteBactch);
        }
        virtual public void Destroy() { }
    }
}
    