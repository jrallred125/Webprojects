using System;
using System.ComponentModel;

namespace FlappyBirdsDemo.Web.Models
{
    public class BirdModel : INotifyPropertyChanged
    {
        private readonly int _gameHeight;
        private readonly int _height;
        public int Height{get { return _height; } }
        private readonly int _width;
        public int Width { get { return _width; } }
        private readonly int _distanceFromLeft;
        public int DistanceFromLeft { get { return _distanceFromLeft; } }
        private int _distanceFromGround;
        public int DistanceFromGround { get { return _distanceFromGround; } private set
            {
                _distanceFromGround = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DistanceFromGround)));
            }
        }
        private int _jumpStrength;
        public int JumpStrength
        { 
            get { return _jumpStrength; }
            set { _jumpStrength = value; }
        }

        public BirdModel(int gameWidth, int gameHeight)
        {
            _gameHeight = gameHeight;
            _width = gameHeight * 6 / 73;
            _height = _width * 3 / 4;
            _distanceFromLeft = (gameWidth = _width) / 2;
            _distanceFromGround = gameHeight * 10 / 73;
            _jumpStrength = gameHeight * 5 / 73;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Fall(int gravity)
        {
            DistanceFromGround -= Math.Min(gravity, DistanceFromGround);
        }

        public void Jump()
        {
            if (DistanceFromGround <= _gameHeight) DistanceFromGround += JumpStrength;
        }

        public bool IsOnGround()
        {
            return DistanceFromGround <= 0;
        }
    }
}
