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
    class Question : MyObject
    {
        MyObject Visible;
        public Question(Game _Game, int _X, int _Y, int _SX, int _SY, int _TotalFrame, MyObject _Visible)
                : base(_Game, _X, _Y, _SX, _SY, _TotalFrame)
        {
            POSITION = new Vector3(POSITION.X, POSITION.Y, 0.92f);
            Visible = _Visible;
            ID = MyID.QUESTION_MARK;
            CURRENTFRAME = 1;
            STATUS = MyStatus.ACTIVE;
            SPRITE = RSManager.Instance(_Game).SPRITE(ID);

        }
     public override void UpdateAnimate(GameTime mGameTime)
     {
         switch (STATUS)
         {
             case MyStatus.ACTIVE:
                 if (mGameTime.TotalGameTime.TotalMilliseconds % 400 < 200)
                 {
                     CURRENTFRAME = 1;
                 }
                 else CURRENTFRAME = 2;
                 break;
             case MyStatus.RUN:
                 CURRENTFRAME = 0;
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
                 if (Obj.STATUS == MyStatus.ACTIVE || Obj.STATUS == MyStatus.DOWN || Obj.STATUS == MyStatus.UP || Obj.STATUS == MyStatus.INVI||Obj.STATUS==MyStatus.CHANGE||Obj.STATUS==MyStatus.SHOT)
                 {
                     if (STATUS == MyStatus.ACTIVE && dir == DIR.BOTTOM)
                     {
                         STATUS = MyStatus.RUN;
                         switch (Visible.ID)
                         {
                             case MyID.FLOWER:                                 
                             case MyID.MUSHROOM_1UP:
                             case MyID.MUSHROOM_BIG:
                                 Visible.STATUS = MyStatus.RUN;
                                 Visible.VELOC = new Vector3(0, -0.04f, 0);
                                 //Visible.STATUS = MyStatus.ACTIVE;
                                 //Visible.POSITION = new Vector3(POSITION.X, POSITION.Y - 50, Visible.POSITION.Z);
                                 //Visible.ACCEL = new Vector3(0, 0.001f, 0);
                                 //Visible.VELOC = new Vector3(0.2f, 0, 0);
                                 break;
                             case MyID.COIN:
                                 Visible.STATUS = MyStatus.BEFORE_DEATH1;
                                 Visible.ACCEL = new Vector3(0, 0.001f, 0);
                                 Visible.VELOC = new Vector3(0, -0.5f, 0);
                                 break;
                         }
                     }
                 }
             break;

         }
     }
    }
}
