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
        //private List<Task> _tasksList = new List<Task>();   
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
                //Ball ball = new Ball(random.Next(0, 540), random.Next(5, 260), random.NextDouble(), random.NextDouble() * 100, random.Next(0, 540), random.Next(0, 260));
                Ball ball = new Ball(random.Next(0, 0), random.Next(0, 0), random.NextDouble(), 40, random.Next(0, 540), random.Next(0, 260));
                _currentBalls.Add(ball);
                Console.WriteLine($"ball x, y: {ball.XCoordinate}, {ball.YCoordinate}");
            }
                
        }







        public void MoveBall(Ball ball, double nrOfFrames, double duration, PointF vector)
        {

            //ball.XCoordinate += distanceX / nrOfFrames;
            //ball.YCoordinate += distanceY / nrOfFrames;
            //Console.WriteLine("elo");
            //for (int i = 0; i < nrOfFrames; i++)
            //{
            //    ball.XCoordinate += vector.X;
            //    ball.YCoordinate += vector.Y;
            //    Console.WriteLine($"ball MoveBall x, y: {ball.XCoordinate}, {ball.YCoordinate}");
            //    //RaisePropertyChanged();

            //    Thread.Sleep((int)((duration / nrOfFrames) * 1000));
            //}
            Console.WriteLine($"ball BEFORE x, y: {ball.XCoordinate}, {ball.YCoordinate}");
            Console.WriteLine($"VECTORS x, y: {vector.X}, {vector.Y}");
            ball.XCoordinate += vector.X;
            ball.YCoordinate += vector.Y;
            Console.WriteLine($"ball AFTER x, y: {ball.XCoordinate}, {ball.YCoordinate}");

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
            Console.WriteLine("FindNewBallPosition");
            Random random = new Random();
            //ball.XCoordinate = ball.DestinationPlaneX;
            //ball.YCoordinate = ball.DestinationPlaneY;
            ball.DestinationPlaneX = random.Next(0, 540);
            ball.DestinationPlaneY = random.Next(0, 260);
            Console.WriteLine($"new x y {ball.DestinationPlaneX} ; {ball.DestinationPlaneY}");
            PointF vector = new PointF();
            vector.X = (float)((ball.DestinationPlaneX - ball.XCoordinate) / nrOfFrames);
            vector.Y = (float)((ball.DestinationPlaneY - ball.YCoordinate) / nrOfFrames);
            return vector;
        }

        public void BallsMovement(int NrOfBalls)
        {
            Console.WriteLine("BallsMovement");
            //for(int i=0; i<NrOfBalls; i++)
            //{
            //    CreateBall();
            //}
            //for (int j = 0; j < 5; j++)
            //{
                foreach (Ball ball in _currentBalls)
                {
                    //Thread thread = new Thread(()=>MoveBall(ball, 24, 14));
                    //thread.Start();
                    //Task tmp = new Task(() => MoveBall(ball, 3, 5));
                    //_tasksList.Add(tmp);
                    //tmp.Start();
                    PointF vector = FindNewBallPosition(ball, 5);
                    MoveBall(ball, 3, 5, vector);
                    Console.WriteLine($"ball w foreach x, y: {ball.XCoordinate}, {ball.YCoordinate}");
                }
            //}
            
            
        }
    }
}
