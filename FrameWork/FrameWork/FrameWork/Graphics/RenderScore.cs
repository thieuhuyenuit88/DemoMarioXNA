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

namespace FrameWork.FrameWork.Graphics
{
    class RenderScore
    {
        SpriteFont Font;
        int posX, posY, _Y;
        int score;
        public int status;
        double timeUpdate;

        public RenderScore(Game _game, SpriteFont _font, int x, int y, int _score)
        {
            Font = _font;
            posX = x;
            posY = _Y = y;
            score = _score;
            status = 1;
        }

        public void Update(GameTime _gametime)
        {
            if (status == 1)
            {
                posY -= 4;
            }

            timeUpdate += _gametime.ElapsedGameTime.Milliseconds;
            if (timeUpdate > 500)
            {
                timeUpdate -= 500;
                status = 2;
            }
        }

        public void Render(SpriteBatch _SpriteBatch)
        {
            if (status == 1)
                _SpriteBatch.DrawString(Font, score.ToString(),
                    new Vector2(posX, posY), Color.Gold, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 1.0f);
        }
    }
}
