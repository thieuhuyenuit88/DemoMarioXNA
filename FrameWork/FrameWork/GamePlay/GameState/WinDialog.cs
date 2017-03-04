#region Using
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
#endregion
namespace FrameWork.GamePlay.GameState
{
    class WinDialog:iState
    {
        SpriteFont m_Font;
        Rectangle m_RectButton, m_RectCursor, m_RectNext;
        bool m_isTouchButton, m_isTouchNext;
        Sprite m_Dialog, m_Button, m_Cursor, m_Next;

        MouseState old_mouseState, mouseState;

        MainGame m_MainGame;

        public WinDialog(iPlay _iPlay, Game game, MainGame _MainGame)
            : base(_iPlay, game)
        {
            ID = STATEGAME.WINSTATE;
            m_MainGame = _MainGame;
        }

        public override void Init()
        {
            m_Dialog = RSMainMenu.Instance(Game).SPRITE(4);
            m_Dialog.Color = new Color(1,1, 1, 0.8f);
            m_Button = RSMainMenu.Instance(Game).SPRITE(6);
            m_Cursor = RSMainMenu.Instance(Game).SPRITE(8);
            m_Next = RSMainMenu.Instance(Game).SPRITE(6);

            m_RectButton = new Rectangle(580, 520, 161, 40);
            m_RectNext = new Rectangle(320, 520, 161, 40);
            m_RectCursor = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 50, 50);

            m_isTouchButton = false;
            m_isTouchNext = false;

            m_Dialog.Position = new Vector2(200, 100);
            m_Button.Position = new Vector2(m_RectButton.X, m_RectButton.Y);
            m_Cursor.Position = new Vector2(m_RectCursor.X, m_RectCursor.Y);
            m_Next.Position = new Vector2(m_RectNext.X, m_RectNext.Y);

            m_Font = Game.Content.Load<SpriteFont>("SpriteFont2");

        }
        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            m_RectCursor = new Rectangle(mouseState.X, mouseState.Y, 50, 50);

            #region Update Button
            if (m_RectButton.Intersects(m_RectCursor))
            {
                if (!m_isTouchButton)
                {
                    Audio.Instance().Play("e_chose");
                }
                if (mouseState.LeftButton == ButtonState.Pressed
                    && old_mouseState.LeftButton == ButtonState.Released)
                {
                    Audio.Instance().Play("e_click");
                    this.Play.NextState = new MainMenu(Play, Game);
                }
                m_isTouchButton = true;
            }
            else m_isTouchButton = false;
            #endregion

            #region Update Button Next
            
            if (m_RectNext.Intersects(m_RectCursor))
            {
                if (!m_isTouchNext)
                {
                    Audio.Instance().Play("e_chose");
                }
                if (mouseState.LeftButton == ButtonState.Pressed
                    && old_mouseState.LeftButton == ButtonState.Released)
                {
                    Audio.Instance().Play("e_click");
                    this.Play.NextState = new LoadingGame(Play, "Level" + (m_MainGame.Level + 1).ToString(), Game);
                }
                m_isTouchNext = true;
            }
            else m_isTouchNext = false;
            #endregion

            old_mouseState = mouseState;
        }
        public override void Render(GameTime gameTime, SpriteBatch _SpriteBatch)
        {

           _SpriteBatch.Begin(SpriteSortMode.Deferred,
               BlendState.AlphaBlend);


            #region Dialog
            m_Dialog.Render(_SpriteBatch);
            #endregion

            #region Render Button
            if (m_isTouchButton)
            {
                m_Button.CurFrame = 1;
            }
            else
            {
                m_Button.CurFrame = 0;
            }
            m_Button.Position = new Vector2(m_RectButton.X, m_RectButton.Y);           
            m_Button.Render(_SpriteBatch);
            #endregion

            #region Render Button
            if (m_isTouchNext)
            {
                m_Next.CurFrame = 1;
            }
            else
            {
                m_Next.CurFrame = 0;
            }
            m_Next.Position = new Vector2(m_RectNext.X, m_RectNext.Y);
            m_Next.Render(_SpriteBatch);
            #endregion

            _SpriteBatch.DrawString(m_Font, "YOU WIN", new Vector2(480, 290), Color.ForestGreen, 0, Vector2.Zero, 3.0f, SpriteEffects.None, 1.0f);
            _SpriteBatch.DrawString(m_Font, "Your score: \n" + m_MainGame.SCORE, new Vector2(480,370), Color.Red, 0, Vector2.Zero, 2.0f, SpriteEffects.None, 1.0f);
            _SpriteBatch.DrawString(m_Font, "Main Menu", new Vector2(600, 525), Color.Yellow);
            _SpriteBatch.DrawString(m_Font, "Next Level", new Vector2(340, 525), Color.Yellow);

            m_Cursor.Position = new Vector2(m_RectCursor.X, m_RectCursor.Y);
            m_Cursor.Render(_SpriteBatch);

            _SpriteBatch.End();
        }
    }
}
