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

namespace FrameWork.FrameWork.QuadTree
{
    class Camera2D
    {
        public Matrix transform; // Matrix Transform

        private Game Game;
        private float _Y;

        Vector2 pos; // Camera Position
        Vector2 distance; //distance between pos camera and pos rect
        Rectangle rect; //camera rect;

        public Camera2D()
        {
            Pos = Vector2.Zero;
            CameraRect = new Rectangle();             
        }

        public Camera2D(Game _game, Rectangle _rect)
        {
            Game = _game;
            CameraRect = _rect;
            //Pos = new Vector2(Game.Window.ClientBounds.Width /2,Game.Window.ClientBounds.Height /2);
            Pos = new Vector2(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2);
            Distance = new Vector2(Pos.X-CameraRect.X,Pos.Y-CameraRect.Y);
            _Y = Pos.Y;
        }

        public Vector2 Distance
        {
            get { return distance;}
            set { distance = value; }
        }

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public Rectangle CameraRect
        {
            get { return rect; }
            set { rect = value; }
        }

        public void UpdateMove(MyObject Obj)
        {
            //if (Obj.POSITION.X > Game.Window.ClientBounds.Width / 2)
            if (Obj.POSITION.X > Game.GraphicsDevice.Viewport.Width / 2)
            {
                Pos = new Vector2(Obj.POSITION.X, Pos.Y);
            }
            if (Obj.POSITION.Y < 0)
            {
                Pos = new Vector2(Pos.X, _Y - Math.Abs(Obj.POSITION.Y));
            }
            else
                Pos = new Vector2(Pos.X, _Y);
            CameraRect = new Rectangle((int)(Pos.X - Distance.X), (int)(Pos.Y - Distance.Y), CameraRect.Width, CameraRect.Height);
        }

        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            transform =
                    Matrix.Identity *
                    Matrix.CreateTranslation(new Vector3(-Pos.X, -Pos.Y, 0)) *
                    Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f,graphicsDevice.Viewport.Height * 0.5f, 0));

            return transform;
        }
    }
}

