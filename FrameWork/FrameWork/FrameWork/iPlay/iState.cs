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
public enum STATEGAME
{
    MAINMENU,
    STARTGAME,
    LOADINGGAME,
    MENUINGAME,
    ABOUT,
    MAINGAME,
    CHOSEMAP,
    WINSTATE,
    OPTION
}
namespace FrameWork.FrameWork.iPlay
{
   
   public class iState
    {
        private STATEGAME m_ID;
        private iPlay m_iPlay;
        private Game m_Game;
        public STATEGAME ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        public iPlay Play
        {
            get { return m_iPlay; }
            set { m_iPlay = value; }
        }
        public Game Game
        {
            get { return m_Game; }
            set { m_Game = value; }
        }
        public iState(iPlay _iPlay, Game _game)
        {
            m_iPlay = _iPlay;
            Game = _game;
        }
        public virtual void Init() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Render(GameTime gameTime,SpriteBatch _SpriteBatch) { }
        public virtual void Destroy() { }
    }
}
