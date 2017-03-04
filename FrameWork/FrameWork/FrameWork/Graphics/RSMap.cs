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

namespace FrameWork.FrameWork.Graphics
{
    class RSMap
    {
        private Title _title;
        private MushroomToxic _toxic;
        private Base_LeftOn _baseOnLeft;
        private Base_LeftUnder _baseUnderLeft;
        private Base_On _baseOn;
        private Base_Under _baseUnder;
        private Base_RightOn _baseOnRight;
        private Base_RightUnder _baseUnderRight;
        private PipeTL _pipeUpLeft;
        private PipeBL _pipeUnderLeft;
        private PipeTR _pipeUpRight;
        private PipeBR _pipeUnderRight;
        private BrickHard _brickHard;
        private BrickBreak _brickBreak;
        private Coin _coin;
        private Coin _coinInvi;
        private Flower _flower;
        private Mushroom1Up _up;
        private MushroomBig _big;
        private Question _quesFlower;
        private Question _quesUp;
        private Question _quesBig;
        private Question _quesCoin;

        public MyObject OBJECTS(Game _Game,int _X, int _Y, int _index)
        {
            if (_index == 1)
            {
                _title = new Title(_Game, _X, _Y, 50, 72, 5, 1);
                return _title;
            }

            if (_index == 2)
            {
                _toxic = new MushroomToxic(_Game, _X, _Y, 45, 50, 3);
                return _toxic;
            }

            if (_index == 3)
            {
                _baseOnLeft = new Base_LeftOn(_Game, _X, _Y, 50, 50, 2);
                return _baseOnLeft;
            }

            if (_index == 5)
            {
                _baseOn = new Base_On(_Game, _X, _Y, 50, 50, 2);
                return _baseOn;
            }

            if (_index == 7)
            {
                _baseOnRight = new Base_RightOn(_Game, _X, _Y, 50, 50, 2);
                return _baseOnRight;
            }

            if (_index == 4)
            {
                _baseUnderLeft = new Base_LeftUnder(_Game, _X, _Y, 50, 50, 2);
                return _baseUnderLeft;
            }

            if (_index == 6)
            {
                _baseUnder = new Base_Under(_Game, _X, _Y, 50, 50, 2);
                return _baseUnder;
            }

            if (_index == 8)
            {
                _baseUnderRight = new Base_RightUnder(_Game, _X, _Y, 50, 50, 2);
                return _baseUnderRight;
            }

            if (_index == 9)
            {
                _pipeUpLeft = new PipeTL(_Game, _X, _Y, 50, 50, 2);
                return _pipeUpLeft;
            }

            if (_index == 11)
            {
                _pipeUpRight = new PipeTR(_Game, _X, _Y, 50, 50, 2);
                return _pipeUpRight;
            }

            if (_index == 10)
            {
                _pipeUnderLeft = new PipeBL(_Game, _X, _Y, 50, 50, 2);
                return _pipeUnderLeft;
            }

            if (_index == 12)
            {
                _pipeUnderRight = new PipeBR(_Game, _X, _Y, 50, 50, 2);
                return _pipeUnderRight;
            }

            if (_index == 13)
            {
                _brickHard = new BrickHard(_Game, _X, _Y, 50, 50, 2);
                return _brickHard;
            }

            if (_index == 14)
            {
                _brickBreak = new BrickBreak(_Game, _X, _Y, 50, 50, 2);
                return _brickBreak;
            }

            if (_index == 15)
            {
                _coin = new Coin(_Game, _X, _Y, 50, 50, 6, 1);
                return _coin;
            }

            if (_index == 20)
            {
                _coinInvi = new Coin(_Game, _X, _Y, 50, 50, 6, 2);
                return _coinInvi;
            }

            if (_index == 21)
            {
                _flower = new Flower(_Game, _X, _Y, 45, 50, 4);
                return _flower;
            }

            if (_index == 22)
            {
                _up = new Mushroom1Up(_Game, _X, _Y, 45, 50, 2);
                return _up;
            }

            if (_index == 23)
            {
                _big = new MushroomBig(_Game, _X, _Y, 45, 50, 2);
                return _big;
            }

            if (_index == 16)
            {
                _quesFlower = new Question(_Game, _X, _Y, 50, 50, 2, new Flower(_Game, _X, _Y, 45, 50, 4));
                return _quesFlower;
            }

            if (_index == 17)
            {
                _quesUp = new Question(_Game, _X, _Y, 50, 50, 2, new Mushroom1Up(_Game, _X, _Y, 45, 50, 2));
                return _quesUp;
            }

            if (_index == 18)
            {
                _quesBig = new Question(_Game, _X, _Y, 50, 50, 2, new MushroomBig(_Game, _X, _Y, 45, 50, 2));
                return _quesBig;
            }

            if (_index == 19)
            {
                _quesCoin = new Question(_Game, _X, _Y, 50, 50, 2, new Coin(_Game, _X, _Y, 50, 50, 6, 2));
                return _quesCoin;
            }

            return null;
        }
    }
}
