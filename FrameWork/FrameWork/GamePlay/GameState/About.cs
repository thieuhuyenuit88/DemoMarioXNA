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

using FrameWork.FrameWork.QuadTree;
using FrameWork.FrameWork.iPlay;
using FrameWork.FrameWork.Graphics;
using FrameWork.FrameWork.Object;
using FrameWork.FrameWork.Audio;
using FrameWork.FrameWork;

namespace FrameWork.GamePlay.GameState
{
    class About : iState
    {
        Sprite m_spBack, m_Circle1, m_Circle2;

        public About(iPlay _iPlay, Game game)
            : base(_iPlay, game)
        {
            
            ID = STATEGAME.ABOUT;
        }
        public override void Init()
        {
            Audio.Instance().StopAllBack();
            Audio.Instance().Play("b_about_state");

            m_Circle1 = RSMainMenu.Instance(Game).SPRITE(1);
            m_Circle2 = RSMainMenu.Instance(Game).SPRITE(2);
            m_spBack = RSMainMenu.Instance(Game).SPRITE(9);

            m_Circle1.Scale = new Vector2(1.48f, 1.48f);
            m_Circle1.Position = new Vector2(447, 319);
            m_Circle1.Color = new Color(255, 0, 0, 0);

            m_Circle2.Scale = new Vector2(1.5f, 1.5f);
            m_Circle2.Position = new Vector2(432, 304);
            m_Circle2.Color = new Color(0, 255, 0, 0);
        }
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) 
                || Keyboard.GetState().IsKeyDown(Keys.Enter) 
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
           {

               FrameWork.Audio.Audio.Instance().Play("e_pipe");
               Play.NextState = new MainMenu(Play, Game);
           }
        }
        public override void Render(GameTime gameTime, SpriteBatch _SpriteBatch)
        {
            Game.GraphicsDevice.Clear(Color.White);
            _SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
           
            m_spBack.Render(_SpriteBatch);

            m_Circle1.Rotation = -(float)(gameTime.TotalGameTime.TotalMilliseconds * Math.PI) / 1440.0f;
            m_Circle1.Render(_SpriteBatch);

            m_Circle2.Rotation = (float)(gameTime.TotalGameTime.TotalMilliseconds * Math.PI) / 1000.0f;
            m_Circle2.Render(_SpriteBatch);

            _SpriteBatch.End();
        }
    }
}
