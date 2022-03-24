using System.ComponentModel;
using System.Threading.Tasks;

namespace FlappyBirdsDemo.Web.Models
{
    public class GameManager :INotifyPropertyChanged
    {
        private readonly int _gravity = 2;

        public event PropertyChangedEventHandler PropertyChanged;

        public BirdModel Bird { get; private set; }
        public PipeModel Pipe { get; private set; }
        public bool IsRunning { get; private set; } = false;



        public GameManager()
        {
            Bird = new();
            Pipe = new();
        }

        public async void MainLoop()
        {
            IsRunning = true;
            while (IsRunning)
            {
                Bird.Fall(_gravity);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bird)));

                Pipe.Move();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pipe)));
                if (Bird.DistanceFromGround <= 0) GameOver();
                await Task.Delay(20);
            }
        }
        public void StartGame()
        {
            if (!IsRunning)
            {
                Bird = new();
                MainLoop();
            }
        }

        public void GameOver()
        {
            IsRunning = false;
        }
    }
}
