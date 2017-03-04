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
using FrameWork.FrameWork.iPlay;
using FrameWork.GamePlay.GameState;

namespace FrameWork.GamePlay
{
    class GamePlay:iPlay
    {
        
        public GamePlay(Game _game)
            : base(_game)
        {

        }

        public override void Initialize()
        {
            CurrentState = new MainMenu(this, this.Game);
            NextState = new MainMenu(this, this.Game);
            CurrentState.Init();
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            if (CurrentState.ID== NextState.ID)         
            {
               CurrentState.Update(gameTime);
            }
            else
            {
                CurrentState.Destroy();
                CurrentState = NextState;
                CurrentState.Init();
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            //Spritebatch.Begin(SpriteSortMode.FrontToBack,BlendState.AlphaBlend);
            CurrentState.Render(gameTime,Spritebatch);
            //Spritebatch.End();
            base.Draw(gameTime);
        }

    }
}
