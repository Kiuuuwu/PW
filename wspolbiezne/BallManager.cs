using System.Collections.ObjectModel;
using System.Drawing;

namespace Logic
{
    public class BallManager
    {
        private ObservableCollection<Ball> _currentBalls = new ObservableCollection<Ball>();
        public ObservableCollection<Ball> CurrentBalls
        {
            get
            {
                return _currentBalls;
            }
        }
        public void CreateBall(int NrOfBalls)
        {
            _currentBalls.Clear();
            Random random = new Random();
            for (int i = 0; i < NrOfBalls; i++)
            {
                Ball ball = new Ball(random.Next(0, 580), random.Next(2, 300), random.NextDouble(), random.NextDouble() * 50 + 10, random.Next(0, 540), random.Next(0, 260));
                _currentBalls.Add(ball);
            }

        }

        public void MoveBall(Ball ball, double nrOfFrames, double duration, PointF vector)
        {
            ball.XCoordinate += vector.X;
            ball.YCoordinate += vector.Y;
            Thread.Sleep((int)((duration / nrOfFrames) * 100));
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

        public PointF FindNewBallPosition(Ball ball, int nrOfFrames)
        {
            Random random = new Random();
            ball.DestinationPlaneX = random.Next(30, 630 - (int)ball.Radius*2);
            ball.DestinationPlaneY = random.Next(20, 360 - (int)ball.Radius*2);
            PointF vector = new PointF();
            vector.X = (float)((ball.DestinationPlaneX - ball.XCoordinate) / nrOfFrames);
            vector.Y = (float)((ball.DestinationPlaneY - ball.YCoordinate) / nrOfFrames);
            return vector;
        }

        public void BallsMovement()
        {
            foreach (Ball ball in _currentBalls)
            {
                Thread thread = new Thread(() =>
                {
                    PointF vector = FindNewBallPosition(ball, 15);
                    while(true)
                    {
                        if((vector.X > 0 && vector.Y > 0 && ball.XCoordinate >= ball.DestinationPlaneX && ball.YCoordinate >= ball.DestinationPlaneY) ||
                        (vector.X > 0 && vector.Y < 0 && ball.XCoordinate >= ball.DestinationPlaneX && ball.YCoordinate <= ball.DestinationPlaneY) ||
                        (vector.X < 0 && vector.Y < 0 && ball.XCoordinate <= ball.DestinationPlaneX && ball.YCoordinate <= ball.DestinationPlaneY) ||
                        (vector.X < 0 && vector.Y > 0 && ball.XCoordinate <= ball.DestinationPlaneX && ball.YCoordinate >= ball.DestinationPlaneY))
                        {
                            vector = FindNewBallPosition(ball, 15);
                        }
                        MoveBall(ball, 7, 4, vector);
                    }
                });
                thread.Start();
            }
        }
    }
}
