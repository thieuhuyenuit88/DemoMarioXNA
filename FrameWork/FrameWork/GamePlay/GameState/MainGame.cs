using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
using FrameWork.Environment.Timer;
using FrameWork;
namespace FrameWork.GamePlay.GameState
{
    class MainGame :  iState 
    {
        private Mario mario;
        private Boss Boss;
        Camera2D Cam2D;
        List<MyObject> CameraContent;
        QuadTree Objs;
        MyObject bulletBoss, bullet, temp, win_pole, building;
        protected KeyboardState oldKeyboardState;
        public string _MapName;
        string FileName;
        public int[] Digital = new int[6];
        int[,] MatrixMap;
        //RSMap Map = new RSMap();
        private Sprite icon;
        private Sprite[] _Blood;
        Timer TimeGame;
        public int Level;

        static SpriteFont Font;
        double timeUpdate, timeUpdate1, timeUpdate2;
        int time;
        static int life = 3;
        Coin _coin;
        static int numberCoin = 0;
        static double score = 0;
        List<RenderScore> listScore;

        // Running or Pause (Menu in game )
        Boolean m_iRunning, m_iRunning1, m_iRunning2;
        MenuInGame m_Menu;
        WinDialog m_Win;
        LoseDialog m_Lose;

        public Boolean iRunning
        {
            get { return m_iRunning; }
            set { m_iRunning = value; }
        }
        
        public double SCORE
        {
            get { return score; }
            set { score = value; }
        }

        public int LIFE
        {
            get { return life; }
            set { life = value; }
        }

        private void ReadFile()
        {
            FileName = "Content/Map/" + _MapName + ".txt";
            string StrBuffer = string.Empty;
            int Row = 0;
            string[] StrTemp;

            StreamReader FileMap = File.OpenText(FileName);

            if ((StrBuffer = FileMap.ReadLine()) != null)
            {
                StrTemp = StrBuffer.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < 6; i++) 
                    Digital[i] = Convert.ToInt32(StrTemp[i]);
            }

            MatrixMap = new int[Digital[3],Digital[2]];

            while ((StrBuffer = FileMap.ReadLine()) != null)
            {
                StrTemp = StrBuffer.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < Digital[2] ; i++) MatrixMap[Row, i] = Convert.ToInt32(StrTemp[i]);
                Row += 1;
            }
            FileMap.Close();
        }
        
        public MainGame(iPlay _iPlay,string MapName, Game game):base(_iPlay,game)
        {
           ID = STATEGAME.MAINGAME;
           _MapName = MapName;
        }

        public override void Init()
        {
            #region Play Audio
            Audio.Instance().StopAllBack();
            Random r = new Random();
            int s = r.Next(2);
            switch (s)
            {
                case 0:
                    Audio.Instance().Play("b_game1");
                    break;
                case 1:
                    Audio.Instance().Play("b_game2");
                    break;
            }
            #endregion Play Audio

            // Init Menu In Game 
            m_iRunning = true;
            m_Menu = new MenuInGame(Play, Game, this);
            m_Menu.Init();

            m_iRunning1 = true;
            m_Win = new WinDialog(Play, Game, this);
            m_Win.Init();

            m_iRunning2 = true;
            m_Lose = new LoseDialog(Play, Game, this);
            m_Lose.Init();

            oldKeyboardState = Keyboard.GetState();
            TimeGame = new Timer();

            icon = new Sprite(Game, @"Image/icon", new Point(21, 22), 1, Color.White);
            icon.Position = new Vector2(40,20);
            icon.Scale = new Vector2(2.0f, 2.0f);
            Font = Game.Content.Load<SpriteFont>("SpriteFont1");
            listScore = new List<RenderScore>();

            ReadFile();
            Level = Digital[4];
            time = Digital[5];

            _coin = new Coin(Game, 450, 20, 50, 50, 6, 1);
            mario = new Mario(Game,100,100, 50, 100, 5,bullet);
            Objs = new QuadTree(new Rectangle(0, 0, Digital[2] *50, Digital[3] * 50));
            CameraContent = new List<MyObject>();
            Cam2D = new Camera2D(Game, new Rectangle(-600,-600, 2000, 2000));
            CameraContent.Add(mario);

            for (int i = 0; i < Digital[3]; i++)
            {
                for (int j = 0; j < Digital[2]; j++)
                {
                    if (MatrixMap[i, j] == 1)
                    {
                        Objs.Insert(new Title(Game, j * 50, i * 50, 50, 72, 5, 1));
                    }

                    if (MatrixMap[i, j] == 2)
                    {
                        Objs.Insert(new MushroomToxic(Game, j * 50, i * 50, 50, 50, 3));
                    }

                    if (MatrixMap[i, j] == 3)
                    {
                        Objs.Insert(new Base_LeftOn(Game, j * 50, i * 50, 50, 50, 2));
                    }

                    if (MatrixMap[i, j] == 5)
                    {
                         Objs.Insert(new Base_On(Game, j * 50, i * 50, 50, 50, 2));                        
                    }

                    if (MatrixMap[i, j] == 7)
                    {
                         Objs.Insert(new Base_RightOn(Game, j * 50, i * 50, 50, 50, 2));
                    }

                    if (MatrixMap[i, j] == 4)
                    {
                        Objs.Insert(new Base_LeftUnder(Game, j * 50, i * 50, 50, 50, 2));  
                    }

                    if (MatrixMap[i, j] == 6)
                    {
                       Objs.Insert(new Base_Under(Game, j * 50, i * 50, 50, 50, 2));  
                    }

                    if (MatrixMap[i, j] == 8)
                    {
                        Objs.Insert(new Base_RightUnder(Game, j * 50, i * 50, 50, 50, 2));                      
                    }

                    if (MatrixMap[i, j] == 9)
                    {
                       Objs.Insert(new PipeTL(Game, j * 50, i * 50, 50, 50, 2));        
                    }

                    if (MatrixMap[i, j] == 11)
                    {
                        Objs.Insert(new PipeTR(Game, j * 50, i * 50, 50, 50, 2));  
                    }

                    if (MatrixMap[i, j] == 10)
                    {
                        Objs.Insert(new PipeBL(Game, j * 50, i * 50, 50, 50, 2));   
                    }

                    if (MatrixMap[i, j] == 12)
                    {
                        Objs.Insert(new PipeBR(Game, j * 50, i * 50, 50, 50, 2)); 
                    }

                    if (MatrixMap[i, j] == 13)
                    {
                        Objs.Insert(new BrickHard(Game, j * 50, i * 50, 50, 50, 2));   
                    }

                    if (MatrixMap[i, j] == 14)
                    {
                        Objs.Insert(new BrickBreak(Game, j * 50, i * 50, 50, 50, 2));  
                    }

                    if (MatrixMap[i, j] == 15)
                    {
                        Objs.Insert(new Coin(Game, j * 50, i * 50, 50, 50, 6, 1));
                    }

                    if (MatrixMap[i, j] == 16)
                    {
                        temp = new Flower(Game, j * 50, i * 50, 50, 50, 4);
                        Objs.Insert(temp);
                        Objs.Insert(new Question(Game, j * 50, i * 50, 50, 50, 2, temp));                     
                    }

                    if (MatrixMap[i, j] == 17)
                    {
                        temp = new Mushroom1Up(Game, j * 50, i * 50, 50, 50, 2);
                        Objs.Insert(temp);
                        Objs.Insert(new Question(Game, j * 50, i * 50, 50, 50, 2, temp)); 
                    }

                    if (MatrixMap[i, j] == 18)
                    {
                        temp = new MushroomBig(Game, j * 50, i * 50, 50, 50, 2);
                        Objs.Insert(temp);
                        Objs.Insert(new Question(Game, j * 50, i * 50, 50, 50, 2, temp));                     
                    }

                    if (MatrixMap[i, j] == 19)
                    {
                        temp = new Coin(Game, j * 50, i * 50, 50, 50, 6, 2);
                        Objs.Insert(temp);
                        Objs.Insert(new Question(Game, j * 50, i * 50, 50, 50, 2, temp));
                    }

                    if (MatrixMap[i, j] == 20)
                    {
                        Objs.Insert(new MountainSmall(Game, j * 50, i * 50 - 150, 500, 200, 1, (float)((Game1)Game).rnd.Next(0,200) / 10000));
                    }

                    if (MatrixMap[i, j] == 21)
                    {
                        Objs.Insert(new MountainBig(Game, j * 50, i * 50 - 350, 828, 400, 1, (float)((Game1)Game).rnd.Next(0, 300) / 10000));
                    }

                    if (MatrixMap[i, j] == 22)
                    {
                        Objs.Insert(new Cloud(Game, j * 50, i * 50, 100, 80, 3, (float)((Game1)Game).rnd.Next(0, 100) / 10000));
                    }

                    if (MatrixMap[i, j] == 23)
                    {
                        Objs.Insert(new Grass(Game, j * 50, i * 50, 100, 50, 3, (float)((Game1)Game).rnd.Next(0, 200) / 10000));
                    }

                    if (MatrixMap[i, j] == 24)
                    {
                        building = new Building(Game, j * 50, i * 50 - 500, 500, 550, 1);
                        Objs.Insert(building);
                    }

                    if (MatrixMap[i, j] == 25)
                    {
                        Objs.Insert(new Fence(Game, j * 50, i * 50, 50, 50, 6, 0));
                    }

                    if (MatrixMap[i, j] == 26)
                    {
                        Objs.Insert(new Fence(Game, j * 50, i * 50, 50, 50, 6, 1));
                    }

                    if (MatrixMap[i, j] == 27)
                    {
                        Objs.Insert(new Fence(Game, j * 50, i * 50, 50, 50, 6, 2));
                    }

                    if (MatrixMap[i, j] == 28)
                    {
                        Objs.Insert(new Fence(Game, j * 50, i * 50, 50, 50, 6, 3));
                    }

                    if (MatrixMap[i, j] == 29)
                    {
                        Objs.Insert(new Fence(Game, j * 50, i * 50, 50, 50, 6, 4));
                    }

                    if (MatrixMap[i, j] == 30)
                    {
                        Objs.Insert(new Fence(Game, j * 50, i * 50, 50, 50, 6, 5));
                    }

                    if (MatrixMap[i, j] == 31)
                    {
                        Objs.Insert(new Pole(Game, j * 50, i * 50, 50, 50, 6, 0));
                    }

                    if (MatrixMap[i, j] == 32)
                    {
                        Objs.Insert(new Pole(Game, j * 50, i * 50, 50, 50, 6, 1));
                    }

                    if (MatrixMap[i, j] == 33)
                    {
                        Objs.Insert(new Pole(Game, j * 50, i * 50, 50, 50, 6, 2));
                    }

                    if (MatrixMap[i, j] == 34)
                    {
                        Objs.Insert(new Pole(Game, j * 50, i * 50, 50, 50, 6, 3));
                    }

                    if (MatrixMap[i, j] == 35)
                    {
                        Objs.Insert(new Pole(Game, j * 50, i * 50, 50, 50, 6, 4));
                    }

                    if (MatrixMap[i, j] == 36)
                    {
                        Objs.Insert(new Pole(Game, j * 50, i * 50, 50, 50, 6, 5));
                    }

                    if (MatrixMap[i, j] == 37)
                    {
                        Objs.Insert(new Post(Game, j * 50, i * 50, 50, 50, 3, 0));
                    }

                    if (MatrixMap[i, j] == 38)
                    {
                        Objs.Insert(new Post(Game, j * 50, i * 50, 50, 50, 3, 1));
                    }

                    if (MatrixMap[i, j] == 39)
                    {
                        Objs.Insert(new Post(Game, j * 50, i * 50, 50, 50, 3, 2));
                    }

                    if (MatrixMap[i, j] == 40)
                    {
                        win_pole = new WinPole(Game, j * 50, i * 50, 50, 450, 1);
                        Objs.Insert(win_pole);
                    }
                    if (MatrixMap[i, j] == 42)
                    {
                        Boss = new Boss(Game, j * 50, i * 50, 100, 110, 4);
                        Objs.Insert(Boss);
                    }
                }
            }
            _Blood = new Sprite[Boss.Blood / 10];
            for (int i=0; i < Boss.Blood / 10; i++)
            {
                _Blood[i] = new Sprite(RSManager.Instance(Game).SPRITE(MyID.BLOOD));
                _Blood[i].Position = new Vector2(850 - (Boss.Blood - 102)/2 + i * 10, 100);
            }
        }
        public override void Update(GameTime gameTime)
        {
            // Update Menu In game
            if (!m_iRunning)
            {
                m_Menu.Update(gameTime);
                return;
            }

            if (!m_iRunning1)
            {
                m_Win.Update(gameTime);
                return;
            }

            if (!m_iRunning2)
            {
                m_Lose.Update(gameTime);
                return;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                //Game.IsActive = false;
            }
            if (mario.STATUS == MyStatus.ACTIVE || mario.STATUS == MyStatus.DOWN || mario.STATUS == MyStatus.UP||mario.STATUS==MyStatus.INVI||mario.STATUS==MyStatus.CHANGE)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    mario.ACCEL = new Vector3(-0.0015f, mario.ACCEL.Y, 0);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    mario.ACCEL = new Vector3(0.0015f, mario.ACCEL.Y, 0);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    if (mario.VELOC.Y == 0)
                    {
                        Audio.Instance().Play("e_jump");

                        mario.VELOC = new Vector3(mario.VELOC.X, -1.5f, 0);
                        if (mario.VELOC.X != 0)
                        {
                            if (mario.VELOC.X > 0)
                                mario.VELOC = new Vector3(mario.VELOC.X, mario.VELOC.Y - mario.VELOC.X / 4, 0);
                            else
                                mario.VELOC = new Vector3(mario.VELOC.X, mario.VELOC.Y + mario.VELOC.X / 4, 0);
                        }
                    }

                }

                ///////////////////////////////////////////////////////////////////////

                KeyboardState keyBoardState = Keyboard.GetState();
                if (keyBoardState.IsKeyDown(Keys.Z) && oldKeyboardState.IsKeyUp(Keys.Z))
                {
                    if (mario.ID == MyID.MARIO_SUPER && mario.STATUS == MyStatus.ACTIVE)
                    {
                        if (mario.bullets > 0)
                        {
                            Audio.Instance().Play("e_shot");
                            mario.bullets -= 1;
                            mario.STATUS = MyStatus.SHOT;
                            if (mario.dri > 0)
                                mario.Visible = new Bullet(Game, (int)mario.POSITION.X + 40, (int)mario.POSITION.Y + 40, 26, 26, 10, mario.dri);
                            //bullet = new Bullet(Game, (int)mario.POSITION.X + 40, (int)mario.POSITION.Y + 40, 26, 26, 10, mario.dri);
                            else
                                mario.Visible = new Bullet(Game, (int)mario.POSITION.X, (int)mario.POSITION.Y + 40, 26, 26, 10, mario.dri);
                            //bullet = new Bullet(Game, (int)mario.POSITION.X, (int)mario.POSITION.Y + 40, 26, 26, 10, mario.dri);
                            Objs.Insert(mario.Visible);
                            //Objs.Insert(bullet);

                            mario.Visible.STATUS = MyStatus.ACTIVE;
                            mario.Visible.VELOC = new Vector3(mario.dri * 0.6f, 0, 0);
                            mario.Visible.ACCEL = new Vector3(0, 0.001f, 0);
                            //bullet.STATUS = MyStatus.ACTIVE;
                            //bullet.VELOC = new Vector3(mario.dri * 0.6f, 0, 0);
                            //bullet.ACCEL = new Vector3(0, 0.001f, 0);
                        }
                    }
                }
                oldKeyboardState = keyBoardState;
            }

            if (mario.DirectionCollision(win_pole) != DIR.NONE)
            {
                Audio.Instance().StopAllBack();
                Audio.Instance().Play("e_slide");
                if (TimeGame.StopWatch(100, gameTime))
                {
                    score += 200;
                    listScore.Add(new RenderScore(Game, Font, (int)mario.POSITION.X, (int)mario.POSITION.Y, 200));
                }
                if (mario.STATUS != MyStatus.WIN)
                {
                    mario.POSITION = new Vector3(win_pole.POSITION.X, mario.POSITION.Y, mario.POSITION.Z);
                    if (mario.VELOC.Y == 0)
                    {
                        Audio.Instance().Stop("e_slide");
                        Audio.Instance().Play("e_win");
                        mario.STATUS = MyStatus.WIN;
                    }
                }
            }

            if (mario.POSITION.X > building.POSITION.X + 250)
            {
                mario.STATUS = MyStatus.DEATH;
                timeUpdate2 += gameTime.ElapsedGameTime.Milliseconds;
                if (timeUpdate2 > 3000)
                {
                    timeUpdate2 -= 3000;
                    Audio.Instance().Stop("e_win");
                    Audio.Instance().Play("b_win_state");
                    m_iRunning1 = false;
                }
            }

            if (Boss.STATUS == MyStatus.SHOT)
            {
                if (TimeGame.StopWatch(300,gameTime))
                {
                    if (Boss.Direct > 0)
                        bulletBoss = new BulletBoss(Game, (int)Boss.POSITION.X + 70, (int)Boss.POSITION.Y + 20, 89, 50, 3, Boss.Direct);
                    else
                        bulletBoss = new BulletBoss(Game, (int)Boss.POSITION.X - 20, (int)Boss.POSITION.Y + 20, 89, 50, 3, Boss.Direct);
                    Objs.Insert(bulletBoss);

                    bulletBoss.STATUS = MyStatus.ACTIVE;
                    bulletBoss.VELOC = new Vector3(Boss.Direct * 0.6f, 0, 0);
                }
            }
            /************************************************************************/
            /* Quad Tree And Camera                                                                     */
            /************************************************************************/

            for (int i = 0; i < CameraContent.Count; )
            {
                if (CameraContent[i].STATUS == MyStatus.DEATH)
                {
                    CameraContent.Remove(CameraContent[i]);
                    continue;
                }
                if (Cam2D.CameraRect.Intersects(CameraContent[i].RECT) == false)
                {
                    Objs.Insert(CameraContent[i]);
                    CameraContent.Remove(CameraContent[i]);
                    continue;
                }
                i++;
            }

            CameraContent.AddRange(Objs.Query(Cam2D.CameraRect));

            /************************************************************************/
            /*                                                                      */
            /************************************************************************/

            for (int i = 0; i < CameraContent.Count; i++)
            {
                CameraContent[i].Update(gameTime);
                for (int j = i + 1; j < CameraContent.Count; j++)
                {
                    if (CameraContent[i].DirectionCollision(CameraContent[j]) != DIR.NONE)
                    {
                        MyObject t = new MyObject(CameraContent[i]);
                        CameraContent[i].ActionCollision(CameraContent[j]);
                        CameraContent[j].ActionCollision(t);
                    }
                }
            }
            /**************************************************************************/
            /*                                                                        */
            /**************************************************************************/
            if ((mario.STATUS==MyStatus.BEFORE_DEATH1) && (life >= 1))
            {
                timeUpdate1 += gameTime.ElapsedGameTime.Milliseconds;
                if (timeUpdate1 > 4000)
                {
                    timeUpdate1 -= 4000;
                    life -= 1;
                    Play.CurrentState.ID = STATEGAME.LOADINGGAME;
                    Play.NextState = new MainGame(Play,_MapName, Game);
                }
            }

            if ((mario.STATUS == MyStatus.BEFORE_DEATH1) && (life < 1))
            { 
                timeUpdate1 += gameTime.ElapsedGameTime.Milliseconds;
                if (timeUpdate1 > 4000)
                {
                    timeUpdate1 -= 4000;
                    Audio.Instance().StopAllBack();
                    Audio.Instance().Play("b_lose_state");
                    m_iRunning2 = false;
                }
            }
            /**************************************************************************/
            /*                                                                        */
            /**************************************************************************/
            timeUpdate += gameTime.ElapsedGameTime.Milliseconds;
            if (timeUpdate > 1000)
            {
                timeUpdate -= 1000;
                time -= 1;
                if (time < 0)
                {
                    time = 0;
                    mario.ID = MyID.MARIO_SMALL;
                    mario.SPRITE = RSManager.Instance(Game).SPRITE(mario.ID);
                    mario.SIZE = new Vector3(mario.SIZE.X, 50, 0);
                    mario.POSITION = new Vector3(mario.POSITION.X, mario.POSITION.Y + 50, mario.POSITION.Z);
                    mario.VELOC = new Vector3(0, -0.9f, 0);

                    mario.STATUS = MyStatus.BEFORE_DEATH1;
                }
            }
            /**************************************************************************/
            /*                                                                        */
            /**************************************************************************/
            for (int i = 0; i < CameraContent.Count; i++)
            {
                if (CameraContent[i].ID == MyID.COIN && CameraContent[i].STATUS == MyStatus.BEFORE_DEATH1
                    && (CameraContent[i].OLDSTATUS == MyStatus.ACTIVE || CameraContent[i].OLDSTATUS == MyStatus.START))
                {
                    Audio.Instance().Play("e_coin");
                    mario.bullets += 2;
                    numberCoin += 1;
                    score += 200;
                    listScore.Add(new RenderScore(Game, Font, (int)CameraContent[i].POSITION.X, (int)CameraContent[i].POSITION.Y, 200));
                }

                if (CameraContent[i].ID == MyID.MUSHROOM_TOXIC && (CameraContent[i].STATUS == MyStatus.BEFORE_DEATH1
                    || CameraContent[i].STATUS == MyStatus.BEFORE_DEATH2) && (CameraContent[i].OLDSTATUS == MyStatus.ACTIVE))
                {
                    score += 100;
                    listScore.Add(new RenderScore(Game, Font, (int)CameraContent[i].POSITION.X, (int)CameraContent[i].POSITION.Y, 100));
                }

                if (CameraContent[i].ID == MyID.TITLE && CameraContent[i].STATUS == MyStatus.BEFORE_DEATH1
                    && (CameraContent[i].OLDSTATUS == MyStatus.ACTIVE || CameraContent[i].OLDSTATUS == MyStatus.RUN || CameraContent[i].OLDSTATUS == MyStatus.STATIC))
                {
                    score += 100;
                    listScore.Add(new RenderScore(Game, Font, (int)CameraContent[i].POSITION.X, (int)CameraContent[i].POSITION.Y, 100));
                }

                if (CameraContent[i].ID == MyID.TITLE && CameraContent[i].STATUS == MyStatus.STATIC
                    && CameraContent[i].OLDSTATUS == MyStatus.ACTIVE)
                {
                    score += 200;
                    listScore.Add(new RenderScore(Game, Font, (int)CameraContent[i].POSITION.X, (int)CameraContent[i].POSITION.Y, 200));
                }

                if (((CameraContent[i].ID == MyID.FLOWER) || (CameraContent[i].ID == MyID.MUSHROOM_1UP) || (CameraContent[i].ID == MyID.MUSHROOM_BIG))
                    && CameraContent[i].STATUS == MyStatus.DEATH)
                {
                    score += 1000;
                    mario.bullets += 5;
                    listScore.Add(new RenderScore(Game, Font, (int)CameraContent[i].POSITION.X, (int)CameraContent[i].POSITION.Y, 1000));
                    if (CameraContent[i].ID == MyID.MUSHROOM_1UP)
                    {
                        life += 1;
                    }
                }

                if ((CameraContent[i].ID == MyID.MARIO_BIG || CameraContent[i].ID == MyID.MARIO_SMALL || CameraContent[i].ID == MyID.MARIO_SUPER)
                    && ((CameraContent[i].STATUS == MyStatus.UP || CameraContent[i].STATUS == MyStatus.DOWN
                    || CameraContent[i].STATUS == MyStatus.INVI || CameraContent[i].STATUS == MyStatus.CHANGE)
                    && CameraContent[i].OLDSTATUS == MyStatus.ACTIVE))
                {
                    Audio.Instance().Play("e_grow");
                }

                if (CameraContent[i].ID == MyID.MUSHROOM_1UP && CameraContent[i].STATUS == MyStatus.DEATH && CameraContent[i].OLDSTATUS == MyStatus.ACTIVE)
                {
                    Audio.Instance().Play("e_1up");
                }

                if ((CameraContent[i].ID == MyID.MUSHROOM_TOXIC && CameraContent[i].OLDSTATUS == MyStatus.ACTIVE
                    && CameraContent[i].STATUS == MyStatus.BEFORE_DEATH2) || (CameraContent[i].ID == MyID.TITLE && (CameraContent[i].OLDSTATUS == MyStatus.ACTIVE
                    ||CameraContent[i].OLDSTATUS == MyStatus.RUN||CameraContent[i].OLDSTATUS == MyStatus.STATIC)
                    && CameraContent[i].STATUS == MyStatus.BEFORE_DEATH1))
                {
                    Audio.Instance().Play("e_death_shot");
                }

                if ((CameraContent[i].ID == MyID.MARIO_BIG || CameraContent[i].ID == MyID.MARIO_SMALL || CameraContent[i].ID == MyID.MARIO_SUPER) &&
                    (CameraContent[i].STATUS == MyStatus.BEFORE_DEATH1 && CameraContent[i].OLDSTATUS == MyStatus.ACTIVE))
                {
                    Audio.Instance().Play("e_death");
                }

                if (CameraContent[i].ID == MyID.BRICK_BREAK && CameraContent[i].STATUS == MyStatus.BEFORE_DEATH3 && CameraContent[i].OLDSTATUS == MyStatus.ACTIVE)
                {
                    Audio.Instance().Play("e_broken");
                }

                if ((CameraContent[i].ID == MyID.MUSHROOM_1UP || CameraContent[i].ID == MyID.MUSHROOM_BIG || CameraContent[i].ID == MyID.FLOWER) &&
                CameraContent[i].OLDSTATUS == MyStatus.START && CameraContent[i].STATUS == MyStatus.RUN)
                {
                    Audio.Instance().Play("e_up");
                }

                if ((CameraContent[i].ID == MyID.MUSHROOM_TOXIC && CameraContent[i].OLDSTATUS == MyStatus.ACTIVE && CameraContent[i].STATUS == MyStatus.BEFORE_DEATH1)
                    || (CameraContent[i].ID == MyID.TITLE && (CameraContent[i].OLDSTATUS == MyStatus.ACTIVE || CameraContent[i].OLDSTATUS == MyStatus.RUN)
                    && CameraContent[i].STATUS == MyStatus.STATIC))
                {
                    Audio.Instance().Play("e_mush_die");
                }

                if (CameraContent[i].ID == MyID.TITLE && CameraContent[i].OLDSTATUS == MyStatus.STATIC && CameraContent[i].STATUS == MyStatus.RUN)
                {
                    Audio.Instance().Play("e_touch_tirtle");
                }

                if (CameraContent[i].ID == MyID.BRICK_BREAK && CameraContent[i].STATUS == MyStatus.RUN && CameraContent[i].OLDSTATUS == MyStatus.ACTIVE)
                {
                    Audio.Instance().Play("e_brick_up");
                }
                if (CameraContent[i].ID == MyID.BOSS && CameraContent[i].STATUS == MyStatus.HURT && (CameraContent[i].OLDSTATUS == MyStatus.ACTIVE||CameraContent[i].OLDSTATUS == MyStatus.SHOT))
                {
                    Audio.Instance().Play("e_boss_hurt");
                    Boss.Blood -= 10;
                    if (Boss.Blood <= 0)
                    {
                        Boss.STATUS = MyStatus.BEFORE_DEATH1;
                    }
                }
                if (CameraContent[i].ID == MyID.BOSS && CameraContent[i].STATUS == MyStatus.BEFORE_DEATH1 && (CameraContent[i].OLDSTATUS == MyStatus.HURT || CameraContent[i].OLDSTATUS == MyStatus.ACTIVE || CameraContent[i].OLDSTATUS == MyStatus.SHOT))//ACTIVE||CameraContent[i].OLDSTATUS == MyStatus.SHOT))
                {
                    Audio.Instance().Play("e_boss_before_die");
                }
                if (CameraContent[i].ID == MyID.BOSS && CameraContent[i].STATUS == MyStatus.BEFORE_DEATH3 && CameraContent[i].OLDSTATUS == MyStatus.BEFORE_DEATH1)//ACTIVE||CameraContent[i].OLDSTATUS == MyStatus.SHOT))
                {
                    Audio.Instance().Play("e_boss_die");
                }
                if (CameraContent[i].ID == MyID.BOSS && CameraContent[i].STATUS == MyStatus.SHOT && (CameraContent[i].OLDSTATUS == MyStatus.ACTIVE||CameraContent[i].OLDSTATUS == MyStatus.HURT))//ACTIVE||CameraContent[i].OLDSTATUS == MyStatus.SHOT))
                {
                    Audio.Instance().Play("e_boss_fire");
                }
                CameraContent[i].OLDSTATUS = CameraContent[i].STATUS;
            }
            /**************************************************************************/
            /*                                                                        */
            /**************************************************************************/
            if (score % 50000 == 0 && score != 0)
            {
                life += 1;
            }
            /**************************************************************************/
            /*                                                                        */
            /**************************************************************************/
            //update render score
            if (listScore.Count != 0)
            {
                for (int i = 0; i < listScore.Count; i++)
                {
                    listScore[i].Update(gameTime);
                }
            }
            for (int i = 0; i < listScore.Count; )
            {
                if (listScore[i].status == 2)
                {
                    listScore.Remove(listScore[i]);
                    continue;
                }
                i++;
            }

            _coin.Update(gameTime);
            //Update camera
            Cam2D.UpdateMove(mario);
        }
        public override void Render(GameTime gameTime,SpriteBatch _SpriteBatch)
        {
            this.Game.GraphicsDevice.Clear(Color.LightBlue);
            _SpriteBatch.Begin(SpriteSortMode.FrontToBack,
                BlendState.AlphaBlend,
                null,
                null,
                null,
                null,
                Cam2D.get_transformation(this.Game.GraphicsDevice));

            foreach (MyObject i in CameraContent)
            {
                i.Render(_SpriteBatch);
            }

            for (int i = 0; i < listScore.Count; i++)
            {
                listScore[i].Render(_SpriteBatch);
            }
                
            _SpriteBatch.End();

            _SpriteBatch.Begin(SpriteSortMode.FrontToBack,
               BlendState.AlphaBlend);

            icon.Render(_SpriteBatch);
            _coin.Render(_SpriteBatch);
            _SpriteBatch.DrawString(Font, " X " + numberCoin.ToString()
            , new Vector2(500, 20), Color.Red, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 1.0f);
            _SpriteBatch.DrawString(Font, " X " + life.ToString()
            , new Vector2(75, 20), Color.Red, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 1.0f);
            _SpriteBatch.DrawString(Font, "LEVEL:" + Level.ToString()
            , new Vector2(460, 70), Color.Red, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 1.0f);
            _SpriteBatch.DrawString(Font, "Score: " + score.ToString()
            , new Vector2(35, 55), Color.Red, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 1.0f);
            //if (mario.ID == MyID.MARIO_SUPER && mario.STATUS == MyStatus.ACTIVE)
            //{
                _SpriteBatch.DrawString(Font, "Bullets:" + mario.bullets.ToString()
                , new Vector2(35, 88), Color.Red, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 1.0f);
            //}
            _SpriteBatch.DrawString(Font, "Time(s)\n " + time.ToString()
            , new Vector2(850, 20), Color.Red, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 1.0f);
            if (Math.Abs(Boss.POSITION.X - mario.POSITION.X) <= Game.GraphicsDevice.Viewport.Width && Boss.STATUS!=MyStatus.DEATH)
            {
                _SpriteBatch.DrawString(Font, "BOSS:" + Boss.Blood.ToString() + "cc"
                , new Vector2(850, 97), Color.Blue, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                for (int i = 0; i < Boss.Blood / 10; i++)
                {
                    _Blood[i].Render(_SpriteBatch);
                }
            }

            _SpriteBatch.End();

            if (!m_iRunning)
            {
                m_Menu.Render(gameTime, _SpriteBatch);
            }

            if (!m_iRunning1)
            {
                m_Win.Render(gameTime,_SpriteBatch);
            }

            if (!m_iRunning2)
            {
                m_Lose.Render(gameTime, _SpriteBatch);
            }
        }
        public override void Destroy()
        {            
        }
        public STATEGAME m_ID { get; set; }
    }
}
