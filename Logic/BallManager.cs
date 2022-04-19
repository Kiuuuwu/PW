using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class BallManager : INotifyPropertyChanged
    {

        private ObservableCollection<Ball> _currentBalls;
        public ObservableCollection<Ball> CurrentBalls
        {
            get
            {
                return _currentBalls;
            }
        }
        public ObservableCollection<Ball> CreateBall(ObservableCollection<Ball> CurrentBalls)
        {
            Random random = new Random();

            Ball ball = new Ball(random.Next(-100, 100), random.Next(-50, 50), random.NextDouble(), random.NextDouble()*10, random.Next(-100, 100), 100);
            CurrentBalls.Add(ball);

            return CurrentBalls;
        }

        public Ball MoveBall(Ball ball, double nrOfSteps)
        {
            double distanceX = ball.DestinationPlaneX - ball.XCoordinate;
            double distanceY = ball.DestinationPlaneY - ball.YCoordinate;
            ball.XCoordinate += distanceX/nrOfSteps;
            ball.YCoordinate += distanceY/nrOfSteps;

            return ball;
        }

        public Ball BounceBall(Ball ball)
        {
            if (ball.DestinationPlaneY == 125 || ball.DestinationPlaneY == -125)
            {
                ball.XCoordinate = ball.DestinationPlaneX + ball.DestinationPlaneX - ball.XCoordinate;
            }
            else if (ball.DestinationPlaneX == 200 || ball.DestinationPlaneY == -200)
            {
                ball.YCoordinate = ball.DestinationPlaneY + ball.DestinationPlaneY - ball.YCoordinate;
            }
            return ball;
        }

        private void OnPropertyChanged([CallerMemberName]  string property = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
