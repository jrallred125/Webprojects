using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FlappyBirdsDemo.Web.Models
{
    public class GameManager : INotifyPropertyChanged
    {
        private readonly int _gravity;
        private readonly int _speed;

        private readonly int _width;
        public int Width { get { return _width; } }
        private readonly int _height;
        public int Height { get { return _height; } }
        public int SkyHeight { get { return Height / 73 * 58; } }
        public int GroundHeight { get { return Height - SkyHeight; } }
        private readonly int _border;
        public int Border { get { return _border; } }

        public bool IsRunning { get; private set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public BirdModel Bird { get; private set; }
        public ObservableCollection<PipeModel> Pipes { get; private set; }




        public GameManager(int gameWidth, int gameHeight)
        {
            _width = gameWidth;
            _height = gameHeight;
            _border = _height * 8 / 73;

            _gravity = gameHeight * 2 / 730;
            _speed = gameHeight * 2 / 730;

            ResetGame();
        }
        public void StartGame()
        {
            if (!IsRunning)
            {
                ResetGame();
                MainLoop();
            }
        }
        public async void MainLoop()
        {
            IsRunning = true;

            while (IsRunning)
            {
                MoveObjects();
                CheckforCollisions();
                ManagePipes();

                await Task.Delay(20);
            }
        }
        private void CheckforCollisions()
        {
            if (Bird.IsOnGround())
                GameOver("Brid collided with the ground.");

            var centeredPipe = GetCenteredPipe();

            if (centeredPipe != null)
            {
                if (Bird.DistanceFromGround < centeredPipe.DistanceFromGround)
                    GameOver($"Bird collided with lower pipe.\n Bird Bottom: {Bird.DistanceFromGround}\n Pipe Top: {centeredPipe.DistanceFromGround}");

                else if (Bird.DistanceFromGround > centeredPipe.DistanceFromGround + centeredPipe.Gap - 45)
                    GameOver($"Bird collided with upper pipe.\n Bird Top: {Bird.DistanceFromGround - 45}\n Pipe Bottom: {centeredPipe.DistanceFromGround + centeredPipe.Gap}");
            }
        }
        private void ManagePipes()
        {
            if (!Pipes.Any() || Pipes.Last().DistanceFromLeft <= 250)
                GeneratePipe();

            if (Pipes.First().IsOffScreen()) 
                Pipes.Remove(Pipes.First());
          
        }
        private void GameOver(string message = null)
        {
            IsRunning = false;
            if (message != null)
                Console.WriteLine(message);
        }

        private void GeneratePipe()
        {
            Pipes.Add(new(Width,Height,GroundHeight));
        }

        private void ResetGame()
        {
            Bird = new(Width, Height);
            Pipes = new();
            Pipes.CollectionChanged += (o, e) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pipes)));
        }

        public void Jump()
        {
            if (IsRunning)
            {
                Bird.Jump();
            }
        }

        private void MoveObjects()
        {
            Bird.Fall(_gravity);
            foreach (var pipe in Pipes)
            {
                pipe.Move(_speed);
            }
        }

        private PipeModel GetCenteredPipe()
        {
            return Pipes.FirstOrDefault(p => p.IsCentered(Bird.Width));
        }
    }
}
