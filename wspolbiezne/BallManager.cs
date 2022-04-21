using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data;

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
        public void CreateBall()
        {
            Random random = new Random();

            Ball ball = new Ball(random.Next(0, 540), random.Next(5, 260), random.NextDouble(), random.NextDouble() * 100, random.Next(0, 540), random.Next(0, 260));
            _currentBalls.Add(ball);
        }

        public Ball MoveBall(Ball ball, double nrOfFrames, double duration)
        {
            PointF vector = new PointF();
            vector.X = (float)((ball.DestinationPlaneX - ball.XCoordinate)/nrOfFrames);
            vector.Y = (float)((ball.DestinationPlaneY - ball.YCoordinate)/nrOfFrames);
            //ball.XCoordinate += distanceX / nrOfFrames;
            //ball.YCoordinate += distanceY / nrOfFrames;
            for (int i = 0; i < nrOfFrames; i++)
            {

                ball.XCoordinate += vector.X;
                ball.YCoordinate += vector.Y;

                Thread.Sleep((int)((duration / nrOfFrames) * 1000));
            }

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

        public Ball FindNewBallPosition(Ball ball)
        {
            Random random = new Random();
            ball.XCoordinate = ball.DestinationPlaneX;
            ball.YCoordinate = ball.DestinationPlaneY;
            ball.DestinationPlaneX = random.Next(0, 540);
            ball.DestinationPlaneY = random.Next(0, 260);
            return ball;
        }
    }
}
