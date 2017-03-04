using System;
using System.IO;
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
    class ChoseMapItem
    {
        double angle;
        float sx, sy;
        Sprite mSprite, mSprite1, mSprite2;
        public int STATUS
        {
            get;
            set;
        }
        public int INDEX
        {
            get;
            set;
        }
        public int Y
        {
            get;
            set;
        }
        public int X
        {
            get;
            set;
        }
        public string MapName
        {
            get;
            set;
        }
        public ChoseMapItem(Game _Game, string _MapName, int _Index)
        {
            MapName = _MapName;
            STATUS = INDEX = _Index;
            X = INDEX * 150 + 162;
            Y = 0;
            sx = sy = 0;
            mSprite = RSMainMenu.Instance(_Game).SPRITE(0);
            mSprite1 = RSMainMenu.Instance(_Game).SPRITE(1);
            mSprite2 = RSMainMenu.Instance(_Game).SPRITE(2);
            mSprite1.Color = new Color(255, 0, 0, 0);
            mSprite2.Color = new Color(0, 255, 0, 0);
        }
        public void Update(GameTime gameTime)
        {
            if (Y < 300)
            {
                Y += 4;
                if (Y > 300)
                {
                    Y = 300;
                }
            }
            angle = gameTime.TotalGameTime.TotalMilliseconds;
            if (STATUS > INDEX)
            {
                X -= 5;
                if (X < INDEX * 150 + 162)
                {
                    STATUS = INDEX;
                    if (INDEX == -1)
                    {
                        STATUS = INDEX = 4;
                        X = INDEX * 150 + 162;
                    }
                }

            }
            if (STATUS < INDEX)
            {
                X += 5;
                if (X > INDEX * 150 + 162)
                {
                    STATUS = INDEX;
                    if (INDEX == 5)
                    {
                        STATUS = INDEX = 0;
                        X = INDEX * 150 + 162;
                    }
                }
            }
            sy = sx = (1000 - Math.Abs(X - 462)) / (float)1000;
        }
        public void Render(SpriteBatch SpriteBactch, SpriteFont _Font)
        {

            mSprite.Position = new Vector2(X, Y);
            mSprite.Scale = new Vector2(sx, sy);
            mSprite.Depth = 0.9f;
            mSprite.Render(SpriteBactch);
            if (STATUS == INDEX && INDEX == 2 && Y == 300)
            {
                mSprite1.Depth = 0.9f;
                mSprite1.Rotation = (float)(angle * Math.PI) / 1440.0f;
                mSprite1.Position = new Vector2(X - 15, Y - 15);
                mSprite1.Scale = new Vector2(sx, sy);
                mSprite1.Render(SpriteBactch);

                mSprite2.Depth = 0.9f;
                mSprite2.Rotation = -(float)(angle * Math.PI) / 1000.0f;
                mSprite2.Position = new Vector2(X - 30, Y - 30);
                mSprite2.Scale = new Vector2(sx, sy);
                mSprite2.Render(SpriteBactch);
            }
            SpriteBactch.DrawString(_Font, MapName, new Vector2(X + 24, Y + 40),
                 Color.Yellow, 0, new Vector2(7, 7), sx, SpriteEffects.None, 1.0f);
        }
    }
    class ChoseMap : iState
    {
        DirectoryInfo _dir;
        List<string> _NameMaps;
        SpriteFont Font;
        Sprite mBack;
        bool isReady;
        iState NextState;
        List<ChoseMapItem> MapItems;
        public ChoseMap(iPlay _iPlay, Game game)
            : base(_iPlay, game)
        {
            ID = STATEGAME.CHOSEMAP;
        }
        public override void Init()
        {
            NextState = null;
            Font = Game.Content.Load<SpriteFont>("SpriteFont1");
            MapItems = new List<ChoseMapItem>();
            mBack = RSMainMenu.Instance(Game).SPRITE(3);
            mBack.Depth = 0.0f;

            _NameMaps = new List<string>();
            _dir = new DirectoryInfo("Content/Map/");

            foreach (FileInfo flInfo in _dir.GetFiles())
            {
                string[] s = flInfo.Name.Split('.');
                _NameMaps.Add(s[0]);
            }

            MapItems = new List<ChoseMapItem>();
            for (int i = 0; i < 5; i++)
            {
                MapItems.Add(new ChoseMapItem(Game, _NameMaps[0], i));
                _NameMaps.Remove(_NameMaps[0]);
            }

        }
        public override void Update(GameTime gameTime)
        {
            if (NextState == null)
            {
                for (int i = 0; i < 5; i++)
                {
                    MapItems[i].Update(gameTime);
                }

                #region Check Ready
                isReady = true;
                for (int i = 0; i < 5; i++)
                {
                    if ((MapItems[i].INDEX != MapItems[i].STATUS) || (MapItems[i].Y != 300))
                    {
                        isReady = false;
                        break;
                    }
                }
                #endregion

                if (isReady)
                {
                    #region Press Key left & Right
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        Audio.Instance().Play("e_chose");

                        for (int i = 0; i < 5; i++)
                        {
                            MapItems[i].INDEX = MapItems[i].INDEX - 1;
                            if (MapItems[i].STATUS == 0)
                            {
                                _NameMaps.Add(MapItems[i].MapName);
                                MapItems[i].MapName = _NameMaps[0];
                                _NameMaps.Remove(_NameMaps[0]);
                            }
                        }
                    }
                    else
                        if (Keyboard.GetState().IsKeyDown(Keys.Right))
                        {
                            Audio.Instance().Play("e_chose");
                            for (int i = 0; i < 5; i++)
                            {
                                MapItems[i].INDEX = MapItems[i].INDEX + 1;
                                if (MapItems[i].STATUS == 4)
                                {
                                    _NameMaps.Insert(0, MapItems[i].MapName);
                                    MapItems[i].MapName = _NameMaps.Last();
                                    _NameMaps.Remove(_NameMaps.Last());
                                }

                            }
                        }
                    #endregion

                    #region Press Space
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        Audio.Instance().Play("e_pipe");
                        for (int i = 0; i < 5; i++)
                        {
                            if (MapItems[i].INDEX == 2)
                            {
                                NextState = new LoadingGame(Play, MapItems[i].MapName, Game);
                            }

                        }
                    }
                    #endregion Set next state

                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    if (MapItems[i].Y < 700)
                    {
                        MapItems[i].Y = MapItems[i].Y + 6;
                    }
                    else Play.NextState = NextState;

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
                MapItems[i].Render(_SpriteBatch, Font);
            }

            _SpriteBatch.End();
        }
    }
}
