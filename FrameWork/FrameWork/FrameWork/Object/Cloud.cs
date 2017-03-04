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
    class Cloud: MyObject
    {
        public Cloud(Game _Game, int _X, int _Y, int _SX, int _SY, int _TotalFrame, float depth)
            : base(_Game, _X, _Y, _SX, _SY, _TotalFrame)
        {
            POSITION = new Vector3(POSITION.X, POSITION.Y, 0.75f + depth);
            ID = MyID.CLOUD;
            CURRENTFRAME = 0;
            SPRITE = RSManager.Instance(_Game).SPRITE(ID);
        }

        public override void UpdateAnimate(GameTime mGameTime)
        {
            if (TIME.StopWatch(300, mGameTime))
            {
                CURRENTFRAME = (++CURRENTFRAME) % 3;
            }      
        }
    }
}
