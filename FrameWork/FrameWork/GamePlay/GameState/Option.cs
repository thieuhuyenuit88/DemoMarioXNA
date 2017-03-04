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
    class Option : iState
    {
        SpriteFont m_Font;
        Rectangle m_RectEff, m_RectBack, m_RectButton, m_RectCursor;
        bool m_isTouchEff, m_isTouchBack, m_isTouchButton, m_isEff, m_isBack;
        Sprite m_Back, m_Dialog, m_SoundIcon, m_Button, m_Cursor;
        MouseState old_mouseState, mouseState;
        public Option(iPlay _iPlay, Game game)
            : base(_iPlay, game)
        {
            ID = STATEGAME.OPTION;
        }
        public override void Init()
        {
            Audio.Instance().StopAllBack();
            Audio.Instance().Play("b_option_state");

            m_Back = RSMainMenu.Instance(Game).SPRITE(3);
            m_Dialog = RSMainMenu.Instance(Game).SPRITE(4);
            m_Dialog.Color = new Color(1, 1, 1, 1.0f);
            m_Button = RSMainMenu.Instance(Game).SPRITE(6);
            m_Cursor = RSMainMenu.Instance(Game).SPRITE(8);
            m_SoundIcon = RSMainMenu.Instance(Game).SPRITE(7);

            m_RectEff = new Rectangle(700, 250, 90, 90);
            m_RectBack = new Rectangle(700, 400, 90, 90);
            m_RectButton = new Rectangle(450, 520, 161, 40);
            m_RectCursor = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 50, 50);

            m_isTouchBack = false;
            m_isTouchButton = false;
            m_isTouchEff = false;

            m_isEff = Audio.Instance().Effect;
            m_isBack = Audio.Instance().BackGround;

            m_Dialog.Position = new Vector2(200, 100);
            m_Button.Position = new Vector2(m_RectButton.X, m_RectButton.Y);
            m_Cursor.Position = new Vector2(m_RectCursor.X, m_RectCursor.Y);

            m_Font = Game.Content.Load<SpriteFont>("SpriteFont2");

        }
        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            m_RectCursor = new Rectangle(mouseState.X, mouseState.Y, 50, 50);

            #region Effect Update
            if (m_RectEff.Intersects(m_RectCursor))
            {
                if (!m_isTouchEff)
                {
                    Audio.Instance().Play("e_chose");
                }
                if (mouseState.LeftButton == ButtonState.Pressed
                    && old_mouseState.LeftButton == ButtonState.Released)
                {
                    Audio.Instance().Play("e_click");
                    if (m_isEff)
                    {
                        m_isEff = false;
                    }
                    else
                    {
                        m_isEff = true;
                    }

                }
                m_isTouchEff = true;
            }
            else m_isTouchEff = false;
            #endregion

            #region Back Update
            if (m_RectBack.Intersects(m_RectCursor))
            {
                if (!m_isTouchBack)
                {
                    Audio.Instance().Play("e_chose");
                }
                if (mouseState.LeftButton == ButtonState.Pressed
                    && old_mouseState.LeftButton == ButtonState.Released)
                {
                    Audio.Instance().Play("e_click");
                    if (m_isBack)
                    {
                        m_isBack = false;
                    }
                    else
                    {
                        m_isBack = true;
                    }
                }
                m_isTouchBack = true;
            }
            else m_isTouchBack = false;
            #endregion

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
                    Audio.Instance().BackGround = m_isBack;
                    Audio.Instance().Effect = m_isEff;
                    this.Play.NextState = new MainMenu(Play, Game);
                }
                m_isTouchButton = true;
            }
            else m_isTouchButton = false;
            #endregion

            old_mouseState = mouseState;
        }
        public override void Render(GameTime gameTime, SpriteBatch _SpriteBatch)
        {
            Game.GraphicsDevice.Clear(Color.Blue);
            _SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            #region Back ground
            m_Back.Render(_SpriteBatch);
            #endregion

            #region Dialog
            m_Dialog.Render(_SpriteBatch);
            #endregion

            #region Sound Effect
            if (m_isEff)
            {
                m_SoundIcon.CurFrame = 0;
            }
            else m_SoundIcon.CurFrame = 1;
            if (m_isTouchEff)
            {
                m_SoundIcon.Scale = new Vector2(1.2f, 1.2f);
            }
            else
            {
                m_SoundIcon.Scale = new Vector2(1.0f, 1.0f);
            }
            m_SoundIcon.Position = new Vector2(m_RectEff.X, m_RectEff.Y);
            m_SoundIcon.Render(_SpriteBatch);
            #endregion

            #region Sound Back Ground
            if (m_isBack)
            {
                m_SoundIcon.CurFrame = 0;
            }
            else m_SoundIcon.CurFrame = 1;
            if (m_isTouchBack)
            {
                m_SoundIcon.Scale = new Vector2(1.2f, 1.2f);
            }
            else
            {
                m_SoundIcon.Scale = new Vector2(1.0f, 1.0f);
            }
            m_SoundIcon.Position = new Vector2(m_RectBack.X, m_RectBack.Y);
            m_SoundIcon.Render(_SpriteBatch);
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
            m_Button.Render(_SpriteBatch);
            #endregion

            _SpriteBatch.DrawString(m_Font, "    Effect Sound", new Vector2(480, 290), Color.Black);
            _SpriteBatch.DrawString(m_Font, "BackGround Sound", new Vector2(480, 430), Color.Black);
            _SpriteBatch.DrawString(m_Font, "Main Menu", new Vector2(470, 525), Color.Yellow);

            m_Cursor.Position = new Vector2(m_RectCursor.X, m_RectCursor.Y);
            m_Cursor.Render(_SpriteBatch);

            _SpriteBatch.End();
        }

    }
}
