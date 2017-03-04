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
using FrameWork;

namespace FrameWork.GamePlay.GameState
{
    class LoadingGame : iState
    {
        string _MapName;
        private Mario mario;
        List<MyObject> CameraContent;
        List<MyObject> Flowers;
        SpriteFont Font;
        public LoadingGame(iPlay _iPlay, string MapName,Game game):base(_iPlay,game)
        {
           ID = STATEGAME.LOADINGGAME;
           _MapName = MapName;
        }
        public override void Init()
        {
            Audio.Instance().StopAllBack();
            Audio.Instance().Play("b_loading_state");

            Font = Game.Content.Load<SpriteFont>("SpriteFont1");
            CameraContent = new List<MyObject>();
            Flowers = new List<MyObject>();
            mario = new Mario(Game, 0, 250, 50, 100, 2, null);
            mario.SPRITE = RSManager.Instance(Game).SPRITE(MyID.MARIO_SUPER);
            
            CameraContent.Add(new Base_LeftOn(Game, 0, 350, 50, 50, 2));
            for (int i = 0; i < 19;i++ )
            {
                CameraContent.Add(new Base_On(Game, 50+i*50, 350, 50, 50, 2));
            }           
            CameraContent.Add(new Base_RightOn(Game, 1000, 350, 50, 50, 2));

            CameraContent.Add(new Base_LeftUnder(Game, 0, 400, 50, 50, 2));
            for (int i = 0; i < 19; i++)
            {
                CameraContent.Add(new Base_Under(Game, 50 + i * 50, 400, 50, 50, 2));
            }            
            CameraContent.Add(new Base_RightUnder(Game, 1000, 400, 50, 50, 2));

            for (int i = 0; i < 22; i++)
            {
                Flowers.Add(new Flower(Game, i*50, 350, 50, 50, 2));
            }   
           
        }
        public override void Update(GameTime gameTime)
        {
            mario.ACCEL = new Vector3(0.00004f, 0, 0);
            mario.Update(gameTime);
            foreach (MyObject i in Flowers)
            {
                i.Update(gameTime);
                if (i.POSITION.X<mario.POSITION.X && i.STATUS == MyStatus.START)
                {
                    i.STATUS = MyStatus.RUN;
                    i.VELOC = new Vector3(0, -0.08f, 0);
                }             

            }
            if (mario.POSITION.X > 974)
            {
                mario.POSITION = new Vector3(974, mario.POSITION.Y, 0);
                if (Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Play.NextState = new MainGame(Play,_MapName, Game);
                }                
            }
        }
        public override void Render(GameTime gameTime, SpriteBatch _SpriteBatch)
        {
            _SpriteBatch.Begin();
            RSMainMenu.Instance(Game).SPRITE(3).Render(_SpriteBatch);
            foreach (MyObject i in Flowers)
            {
                i.Render(_SpriteBatch);
            }
            foreach (MyObject i in CameraContent)
            {
                i.Render(_SpriteBatch);
            }
           
            mario.Render(_SpriteBatch);
            if (mario.POSITION.X >= 974)
            {
                if ((gameTime.TotalGameTime.TotalMilliseconds%1000)>400)
                {
                    _SpriteBatch.DrawString(Font,"Press SPACE or ENTER to continue", new Vector2(300, 400), 
                        Color.Black , 0, Vector2.Zero,1.5f, SpriteEffects.None, 1.0f);
                }
            }
            _SpriteBatch.End();
        }
    }
}
