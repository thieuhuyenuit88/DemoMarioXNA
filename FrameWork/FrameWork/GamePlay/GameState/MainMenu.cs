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
    enum IDChose
    {
        ABOUT, HIGHTSCORE, NEWGAME, OPTION, EXIT
    }
    class Chose
    {
        #region Fields
        IDChose mID;
        int mIndex;
        int mStatus;
        int x, y;
        double angle;
        float sx, sy;
        Sprite mSprite, mSprite1, mSprite2;
        #endregion

        #region Propertie
        public int STATUS
        {
            get { return mStatus; }
            set { mStatus = value; }
        }
        public int INDEX
        {
            get { return mIndex; }
            set { mIndex = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public IDChose ID
        {
            get { return mID; }
            set { mID = value; }
        }
        #endregion

        #region Constructor
        public Chose(Game _Game, IDChose _ID, int _Index)
        {
            mID = _ID; STATUS = INDEX = _Index;
            x = INDEX * 150 + 162;
            y = 0;
            sx = sy = 0;
            mSprite = RSMainMenu.Instance(_Game).SPRITE(0);
            mSprite1 = RSMainMenu.Instance(_Game).SPRITE(1);
            mSprite2 = RSMainMenu.Instance(_Game).SPRITE(2);
            mSprite1.Color = new Color(255, 0, 0, 0);
            mSprite2.Color = new Color(0, 255, 0, 0);
        }
        #endregion

        public void Update(GameTime gameTime)
        {
            if (y < 300)
            {
                y += 4;
                if (y > 300)
                {
                    y = 300;
                }
            }
            angle = gameTime.TotalGameTime.TotalMilliseconds;
            if (mStatus > mIndex)
            {
                x -= 5;
                if (x < mIndex * 150 + 162)
                {
                    mStatus = mIndex;
                    if (mIndex == -1)
                    {
                        mStatus = mIndex = 4;
                        x = mIndex * 150 + 162;
                    }
                }

            }
            if (mStatus < mIndex)
            {
                x += 5;
                if (x > mIndex * 150 + 162)
                {
                    mStatus = mIndex;
                    if (mIndex == 5)
                    {
                        mStatus = mIndex = 0;
                        x = mIndex * 150 + 162;
                    }
                }
            }
            sy = sx = (1000 - Math.Abs(x - 462)) / (float)1000;
        }
        public void Render(SpriteBatch SpriteBactch, SpriteFont _Font)
        {

            mSprite.Position = new Vector2(x, y);
            mSprite.Scale = new Vector2(sx, sy);
            mSprite.Depth = 0.9f;
            mSprite.Render(SpriteBactch);
            if (mStatus == mIndex && mIndex == 2 && Y == 300)
            {
                mSprite1.Depth = 0.9f;
                mSprite1.Rotation = (float)(angle * Math.PI) / 1440.0f;
                mSprite1.Position = new Vector2(x - 15, y - 15);
                mSprite1.Scale = new Vector2(sx, sy);
                mSprite1.Render(SpriteBactch);

                mSprite2.Depth = 0.9f;
                mSprite2.Rotation = -(float)(angle * Math.PI) / 1000.0f;
                mSprite2.Position = new Vector2(x - 30, y - 30);
                mSprite2.Scale = new Vector2(sx, sy);
                mSprite2.Render(SpriteBactch);
            }

            switch (mID)
            {
                case IDChose.NEWGAME:
                    SpriteBactch.DrawString(_Font, "Play \nGame", new Vector2(x + 38, y + 32),
                 Color.Yellow, 0, new Vector2(7, 7), sx, SpriteEffects.None, 1.0f);
                    break;
                case IDChose.ABOUT:
                    SpriteBactch.DrawString(_Font, "About", new Vector2(x + 30, y + 40),
                 Color.Yellow, 0, new Vector2(7, 7), sx, SpriteEffects.None, 1.0f);
                    break;
                case IDChose.OPTION:
                    SpriteBactch.DrawString(_Font, "Option", new Vector2(x + 28, y + 40),
                 Color.Yellow, 0, new Vector2(7, 7), sx, SpriteEffects.None, 1.0f);
                    break;
                case IDChose.EXIT:
                    SpriteBactch.DrawString(_Font, "Exit", new Vector2(x + 40, y + 40),
                 Color.Yellow, 0, new Vector2(7, 7), sx, SpriteEffects.None, 1.0f);
                    break;
                case IDChose.HIGHTSCORE:
                    SpriteBactch.DrawString(_Font, "Hight \nScore", new Vector2(x + 30, y + 30),
                 Color.Yellow, 0, new Vector2(7, 7), sx, SpriteEffects.None, 1.0f);
                    break;
            }

        }
    }
    class MainMenu : iState
    {

        List<Chose> choses;
        SpriteFont Font;
        bool isReady;
        iState NextState;
        Sprite mBack;
        public MainMenu(iPlay _iPlay, Game game)
            : base(_iPlay, game)
        {
            ID = STATEGAME.MAINMENU;
        }
        public override void Init()
        {
            Audio.Instance().StopAllBack();
            Audio.Instance().Play("b_menu");

            choses = new List<Chose>();
            NextState = null;
            Font = Game.Content.Load<SpriteFont>("SpriteFont1");
            mBack = RSMainMenu.Instance(Game).SPRITE(3);
            mBack.Depth = 0.0f;
            for (int i = 0; i < 5; i++)
            {
                choses.Add(new Chose(Game, (IDChose)i, i));
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (NextState == null)
            {
                for (int i = 0; i < 5; i++)
                {
                    choses[i].Update(gameTime);
                }

                #region Check Ready
                isReady = true;
                for (int i = 0; i < 5; i++)
                {
                    if (choses[i].INDEX != choses[i].STATUS || choses[i].Y != 300)
                    {
                        isReady = false;
                        break;
                    }
                }
                #endregion

                #region Check Press Key
                if (isReady)
                {
                    #region Press Left
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        Audio.Instance().Play("e_chose");
                        for (int i = 0; i < 5; i++)
                        {
                            choses[i].INDEX = choses[i].INDEX - 1;

                        }
                    }
                    #endregion

                    #region Press Right
                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        Audio.Instance().Play("e_chose");
                        for (int i = 0; i < 5; i++)
                        {
                            choses[i].INDEX = choses[i].INDEX + 1;
                        }
                    }
                    #endregion

                    #region Press Space or enter
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        Audio.Instance().Play("e_pipe");
                        for (int i = 0; i < 5; i++)
                        {
                            if (choses[i].INDEX == 2)
                            {
                                switch (choses[i].ID)
                                {
                                    case IDChose.NEWGAME:
                                        NextState = new ChoseMap(Play, Game);
                                        break;
                                    case IDChose.OPTION:
                                        NextState = new Option(Play, Game);
                                        break;
                                    case IDChose.EXIT:
                                        Game.Exit();
                                        break;
                                    case IDChose.ABOUT:
                                        NextState = new About(Play, Game);
                                        break;
                                }
                            }

                        }
                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    if (choses[i].Y < 700)
                    {
                        choses[i].Y = choses[i].Y + 6;
                    }
                    else
                        Play.NextState = NextState;


                }
            }

        }
        public override void Render(GameTime gameTime, SpriteBatch _SpriteBatch)
        {
            Game.GraphicsDevice.Clear(Color.Blue);
            _SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            mBack.Render(_SpriteBatch);

            for (int i = 0; i < 5; i++)
            {
                choses[i].Render(_SpriteBatch, Font);
            }

            _SpriteBatch.End();
        }

    }
}
