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
    class MountainSmall: MyObject
    {
        public MountainSmall(Game _Game, int _X, int _Y, int _SX, int _SY, int _TotalFrame,float depth)
            : base(_Game, _X, _Y, _SX, _SY, _TotalFrame)
        {
            POSITION = new Vector3(POSITION.X, POSITION.Y, 0.73f + depth);
            ID = MyID.MOUNTAIN_SMALL;
            SPRITE = RSManager.Instance(_Game).SPRITE(ID);
        }
    }
}
