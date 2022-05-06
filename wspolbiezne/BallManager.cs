using System.Collections.ObjectModel;
using System.Drawing;

namespace Logic
{
    public class BallManager : LogicAPI
    {
        private ObservableCollection<Ball> _currentBalls = new ObservableCollection<Ball>();
        public ObservableCollection<Ball> CurrentBalls
        {
            get
            {
                return _currentBalls;
            }
        }
        public override ObservableCollection<Ball> getCollection()
        {
            return CurrentBalls;
        }
        public override void CreateBall(int NrOfBalls)
        {
            _currentBalls.Clear();
            Random random = new Random();
            for (int i = 0; i < NrOfBalls; i++)
            {
                Ball ball = new Ball(random.Next(0, 580), random.Next(2, 300), random.NextDouble() / 15, random.NextDouble() * 50 + 10, random.Next(0, 540), random.Next(0, 260));
                _currentBalls.Add(ball);
            }

        }

        public override void MoveBall(Ball ball, double nrOfFrames, double duration, PointF vector)
        {
            ball.XCoordinate += vector.X;
            ball.YCoordinate += vector.Y;
            Thread.Sleep((int)((duration / nrOfFrames) * 100));
        }

        public override Ball BounceBall(Ball ball)  // odbijanie pilki od sciany
        {
            if (ball.YCoordinate <= (int)ball.Radius * 2 || ball.YCoordinate >=  360 - (int)ball.Radius * 2)
            {
                // odbicie sprężyste bez straty energii uwzgledniajace predkosc mase i poprzedni kierunek ruchu kulki
                ball.XCoordinate = ball.DestinationPlaneX + ball.DestinationPlaneX - ball.XCoordinate;
            }
            else if (ball.XCoordinate <= (int)ball.Radius * 2  || ball.XCoordinate >= 640 - (int)ball.Radius * 2)   // niby 0,0 to lewy goorny rog i kolko tam nie powinno wejsc w sciane ale nw
            {
                // odbicie sprężyste bez straty energii uwzgledniajace predkosc mase i poprzedni kierunek ruchu kulki
                ball.YCoordinate = ball.DestinationPlaneY + ball.DestinationPlaneY - ball.YCoordinate;
            }
            return ball;
        }

        public override PointF FindNewBallPosition(Ball ball, int nrOfFrames, PointF oldVector)
        {
            if (oldVector.X == 0 && oldVector.Y == 0)
            {
                // losowe miejsce na ktorejs ze scianek jako destination point
                Random random = new Random();
                var values = new[] {0, 1, 2, 3};
                int result = values[random.Next(values.Length)];        // to zwroci cyfre od 1 do 4?
                switch (result)
                {
                    case 0: //sciana lewa
                        ball.DestinationPlaneX = 0; // czy powinnam dodac tu (int)ball.Radius * 2? rczej nie
                        ball.DestinationPlaneY = random.Next(0 + (int)ball.Radius * 2, 360 - (int)ball.Radius * 2);
                        break;
                    case 1: //sciana prawa
                        ball.DestinationPlaneX = 640 - (int)ball.Radius * 2;
                        ball.DestinationPlaneY = random.Next(0 + (int)ball.Radius * 2, 360 - (int)ball.Radius * 2);
                        break;
                    case 2: //sciana gorna
                        ball.DestinationPlaneX = 0; // powinno byc 0 + (int)ball.Radius * 2,  ??
                        ball.DestinationPlaneY = random.Next(0 + (int)ball.Radius * 2, 360 - (int)ball.Radius * 2);
                        break;
                    case 3: //sciana dolna
                        ball.DestinationPlaneX = 640 - (int)ball.Radius * 2;
                        ball.DestinationPlaneY = random.Next(0 + (int)ball.Radius * 2, 360 - (int)ball.Radius * 2);
                        break;
                }
                PointF vector = new PointF();
                vector.X = (float)((ball.DestinationPlaneX - ball.XCoordinate) / nrOfFrames);
                vector.Y = (float)((ball.DestinationPlaneY - ball.YCoordinate) / nrOfFrames);
                return vector;
            }
            else
            {
                // losowe miejsce na ktorejs ze scianek jako destination point
                Random random = new Random();
                var values = new[] { 0, 1, 2, 3 };
                int result = values[random.Next(values.Length)];        // to zwroci cyfre od 1 do 4?
                switch (result)
                {
                    case 0: //sciana lewa
                        ball.DestinationPlaneX = 0 + (int)ball.Radius * 2; // czy powinnam dodac tu (int)ball.Radius * 2? rczej nie
                        ball.DestinationPlaneY = random.Next(0 + (int)ball.Radius * 2, 360 - (int)ball.Radius * 2);
                        break;
                    case 1: //sciana prawa
                        ball.DestinationPlaneX = 640 - (int)ball.Radius * 2;
                        ball.DestinationPlaneY = random.Next(0 + (int)ball.Radius * 2, 360 - (int)ball.Radius * 2);
                        break;
                    case 2: //sciana gorna
                        ball.DestinationPlaneX = 0 + (int)ball.Radius * 2; // powinno byc 0 + (int)ball.Radius * 2,  ??
                        ball.DestinationPlaneY = random.Next(0 + (int)ball.Radius * 2, 360 - (int)ball.Radius * 2);
                        break;
                    case 3: //sciana dolna
                        ball.DestinationPlaneX = 640 - (int)ball.Radius * 2;
                        ball.DestinationPlaneY = random.Next(0 + (int)ball.Radius * 2, 360 - (int)ball.Radius * 2);
                        break;
                }
                PointF vector = new PointF();
                vector.X = (float)((ball.DestinationPlaneX - ball.XCoordinate) / nrOfFrames);
                vector.Y = (float)((ball.DestinationPlaneY - ball.YCoordinate) / nrOfFrames);
                return vector;
            }
            
        }

        public override void BallsMovement()
        {
            foreach (Ball ball in _currentBalls)
            {
                Thread thread = new Thread(() =>
                {
                    PointF vector = new PointF(0, 0);
                    vector = FindNewBallPosition(ball, 25, vector);
                    while(true)
                    {
                        // todo: pilka znajduje nowy wektor w momencie gdy sie odbije od sciany lub innej pilki
                        if((vector.X > 0 && vector.Y > 0 && ball.XCoordinate >= ball.DestinationPlaneX && ball.YCoordinate >= ball.DestinationPlaneY) ||
                        (vector.X > 0 && vector.Y < 0 && ball.XCoordinate >= ball.DestinationPlaneX && ball.YCoordinate <= ball.DestinationPlaneY) ||
                        (vector.X < 0 && vector.Y < 0 && ball.XCoordinate <= ball.DestinationPlaneX && ball.YCoordinate <= ball.DestinationPlaneY) ||
                        (vector.X < 0 && vector.Y > 0 && ball.XCoordinate <= ball.DestinationPlaneX && ball.YCoordinate >= ball.DestinationPlaneY))
                        {
                            vector = FindNewBallPosition(ball, 25, vector);
                        }
                        MoveBall(ball, 7, 4, vector);
                    }
                });
                thread.Start();
            }
        }
    }
}
