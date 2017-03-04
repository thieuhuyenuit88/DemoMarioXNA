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
    class Break:MyObject
    {
        public float rotation;
        public Break(Game _Game, int _X, int _Y, int _SX, int _SY, int _TotalFrame)
            : base(_Game, _X, _Y, _SX, _SY, _TotalFrame)
        {
            POSITION = new Vector3(POSITION.X, POSITION.Y, 0.96f);
            ID = MyID.BREAK;
            CURRENTFRAME = 1;
            ACCEL = new Vector3(0, 0.004f, 0);
            STATUS = MyStatus.ACTIVE;
            SPRITE = RSManager.Instance(_Game).SPRITE(ID);
        }

        public override void UpdateMove(GameTime mGameTime)
        {
            switch (STATUS)
            {
                case MyStatus.ACTIVE:
                    rotation += mGameTime.ElapsedGameTime.Milliseconds;
                    base.UpdateMove(mGameTime);
                    break;
            }

        }

        public override void Render(SpriteBatch SpriteBactch)
        {
            switch (STATUS)
            {
                case MyStatus.ACTIVE:
                    SPRITE.Rotation = 2 * (float)((rotation) * (Math.PI)) / 180.0f;
                    base.Render(SpriteBactch);
                    break;
            }
        }
    }
}
