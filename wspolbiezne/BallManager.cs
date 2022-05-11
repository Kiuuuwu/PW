using Data;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Logic
{
    public class BallManager : LogicAPI
    {
        private ObservableCollection<Ball> _currentBalls = new ObservableCollection<Ball>();
        private Canvas _canvas = new Canvas(new Point(0, 0), new Point(640, 360));

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
                PointF vector = new PointF(0, 0);
                int diameter = random.Next(40) + 20;
                Ball ball = new Ball(
                    random.Next(_canvas.LeftUpCorner.X, _canvas.RightDownCorner.X - diameter), 
                    random.Next(_canvas.LeftUpCorner.Y, _canvas.RightDownCorner.Y - diameter), 
                    random.Next(20, 30), diameter,
                    0, 
                    0, 
                    random.NextDouble() + 0.1, 
                    vector);
                _currentBalls.Add(ball);
            }
        }

        public override void MoveBall(Ball ball)
        {
            //ball.XCoordinate += ball.Vector.X;
            //ball.YCoordinate += ball.Vector.Y;
            //ball.Speed = (int)((duration / nrOfFrames) * 100);
            ball.Move();
            //Thread.Sleep((int)(ball.Speed));
            Thread.Sleep(50);
        }

        public override void BounceBall(Ball ball1, Ball ball2)  // odbijanie pilek
        {
            // te wzory nie sa poprawne ale to chyba nie jest zbyt wazne
            //ball1.Speed = ball1.Speed - ((2 * ball2.Mass) / ball1.Mass + ball2.Mass);
            //ball2.Speed = ball2.Speed - ((2 * ball1.Mass) / ball1.Mass + ball2.Mass);

            PointF tmp = ball1.Vector;  // czy ten wektor jest gdziekolwiek uzywany? chyba nie
            //ball1.Vector = ball2.Vector;
            //ball2.Vector = tmp;

            double tmpX = ball1.DestinationPlaneX;
            double tmpY = ball1.DestinationPlaneY;
            //ball1.DestinationPlaneX = ball2.DestinationPlaneX;
            //ball1.DestinationPlaneY = ball2.DestinationPlaneY;
            //ball2.DestinationPlaneX = tmpX;
            //ball2.DestinationPlaneY = tmpY;
            double temp = ball1.NrOfFrames + ((2 * ball2.Mass) / (ball1.Mass + ball2.Mass));
            //if (temp < 0) temp = 25;
            double temp2 = ball2.NrOfFrames + ((2 * ball1.Mass) / (ball1.Mass + ball2.Mass));
            //if (temp2 < 0) temp2 = 25;
            ball1.UpdateMovement(ball2.DestinationPlaneX, ball2.DestinationPlaneY, ball2.Vector, temp);
            ball2.UpdateMovement(tmpX, tmpY, tmp, temp2);
        }
        public override /*async*/ void IsCollisionAndHandleCollision(ObservableCollection<Ball> CurrentBalls) // czy pilka zderza sie z inna pilka
        {
            double distanceX;
            double distanceY;

            Dictionary<(int, int), bool> bouncesDict = new Dictionary<(int, int), bool>();
            // na poczatku nie mamy zadnych zarejestrowanych odbic - wrzucamy wszedzie false, zeby nam potem nie krzyczal, że Key does not exist
            for (int i = 0; i < CurrentBalls.Count; i++)
            {
                for (int j = i + 1; j < CurrentBalls.Count; j++)
                {
                    bouncesDict[(i, j)] = false;
                }
            }

            while (true) // wykrywamy zderzenia przez caly czas dzialania programu
            {
                for (int i = 0; i < CurrentBalls.Count; i++)
                {
                    for (int j = i + 1; j < CurrentBalls.Count; j++)
                    {
                        distanceX = CurrentBalls[i].XCoordinate - CurrentBalls[j].XCoordinate;
                        distanceY = CurrentBalls[i].YCoordinate - CurrentBalls[j].YCoordinate;
                        if (Math.Sqrt(distanceX * distanceX + distanceY * distanceY) <= CurrentBalls[i].Radius + CurrentBalls[j].Radius)
                        {
                            // jezeli obsluzylismy juz odbicie dla tej pary kulek, to pomijamy Bounce
                            if (bouncesDict[(i, j)]) continue;

                            //Console.WriteLine($"COLLISION DETECTED between:\n{CurrentBalls[i].Details}\nand\n\n{CurrentBalls[j].Details}\n");
                            BounceBall(CurrentBalls[i], CurrentBalls[j]);
                            bouncesDict[(i, j)] = true; // jezeli zrobilismy Bounce, to ustawiamy flage na true, zeby wiedziec, ze to odbicie juz zostalo obsluzone
                        }
                        else bouncesDict[(i, j)] = false; // jezeli kulki sie nie stykaja to ustawiamy flage na false, zeby bylo mozna obsluzyc kolejne zderzenie dla tej pary kulek
                    }
                }
            }
        }

        public override void FindNewBallPosition(Ball ball)
        {
            // losowe miejsce na ktorejs ze scianek jako destination point

            double lastDestinationX = ball.DestinationPlaneX;
            double lastDestinationY = ball.DestinationPlaneY;


            Random random = new Random();
            var values = new[] { 0, 1, 2, 3 };
            int result = values[random.Next(values.Length)];        // to zwroci cyfre od 0 do 3
            switch (result)
            {
                case 0: //sciana lewa
                    ball.DestinationPlaneX = _canvas.LeftUpCorner.X;
                    ball.DestinationPlaneY = random.Next(_canvas.LeftUpCorner.Y, _canvas.RightDownCorner.Y - (int)ball.Diameter);
                    ball.Vector = new PointF
                    {
                        X = -1 * Math.Abs(ball.Vector.X),
                        Y = ball.Vector.Y
                    };
                    break;
                case 1: //sciana prawa
                    ball.DestinationPlaneX = _canvas.RightDownCorner.X - (int)ball.Diameter;
                    ball.DestinationPlaneY = random.Next(_canvas.LeftUpCorner.Y, _canvas.RightDownCorner.Y - (int)ball.Diameter);
                    ball.Vector = new PointF
                    {
                        X = Math.Abs(ball.Vector.X),
                        Y = ball.Vector.Y
                    };
                    break;
                case 2: //sciana gorna
                    ball.DestinationPlaneX = random.Next(_canvas.LeftUpCorner.X, _canvas.RightDownCorner.X - (int)ball.Diameter);
                    ball.DestinationPlaneY = _canvas.LeftUpCorner.Y;
                    ball.Vector = new PointF
                    {
                        X = ball.Vector.X,
                        Y = -1 * Math.Abs(ball.Vector.Y)
                    };
                    break;
                case 3: //sciana dolna
                    ball.DestinationPlaneX = random.Next(_canvas.LeftUpCorner.X, _canvas.RightDownCorner.X - (int)ball.Diameter);
                    ball.DestinationPlaneY = _canvas.RightDownCorner.Y - (int)ball.Diameter;
                    ball.Vector = new PointF
                    {
                        X = ball.Vector.X,
                        Y = Math.Abs(ball.Vector.Y)
                    };
                    break;
            }

            // jeżeli wylosujemy wspolrzedna, w ktorej juz znajduje sie kulka, to przerzucamy cel na przeciwlegla sciane
            if (lastDestinationX == ball.DestinationPlaneX)
            {
                if (ball.DestinationPlaneX == _canvas.LeftUpCorner.X)
                    ball.DestinationPlaneX = _canvas.RightDownCorner.X - ball.Diameter;
                else if (ball.DestinationPlaneX == _canvas.RightDownCorner.X - ball.Diameter)
                    ball.DestinationPlaneX = _canvas.LeftUpCorner.X;
            }

            if (lastDestinationY == ball.DestinationPlaneY)
            {
                if (ball.DestinationPlaneY == _canvas.LeftUpCorner.Y)
                    ball.DestinationPlaneY = _canvas.RightDownCorner.Y - ball.Diameter;
                else if (ball.DestinationPlaneY == _canvas.RightDownCorner.Y - ball.Diameter)
                    ball.DestinationPlaneY = _canvas.LeftUpCorner.Y;
            }

            // wtedy kiedy vektor jest (0, 0), czyli jeszcze przed rozpoczeciem ruchu kulek tworzymy wektor
            if (ball.Vector.X == _canvas.LeftUpCorner.X && ball.Vector.Y == _canvas.LeftUpCorner.Y)
            {
                ball.Vector = new PointF
                {
                    X = (float)((ball.DestinationPlaneX - ball.XCoordinate) / ball.NrOfFrames),
                    Y = (float)((ball.DestinationPlaneY - ball.YCoordinate) / ball.NrOfFrames)
                };
            }

            //return new PointF
            //{
            //    X = (float)((ball.DestinationPlaneX - ball.XCoordinate) / ball.NrOfFrames),
            //    Y = (float)((ball.DestinationPlaneY - ball.YCoordinate) / ball.NrOfFrames)
            //};
        }

        public override void BallsMovement()
        {
            foreach (Ball ball in _currentBalls)
            {
                Task task = new Task(() =>
                {
                    bool hitWall = false;
                    FindNewBallPosition(ball);
                    while (true)
                    {
                        // todo: pilka znajduje nowy wektor w momencie gdy sie odbije od sciany lub innej pilki
                        //if ((ball.Vector.X > 0 && ball.Vector.Y > 0 && ball.XCoordinate >= ball.DestinationPlaneX && ball.YCoordinate >= ball.DestinationPlaneY) ||
                        //(ball.Vector.X > 0 && ball.Vector.Y < 0 && ball.XCoordinate >= ball.DestinationPlaneX && ball.YCoordinate <= ball.DestinationPlaneY) ||
                        //(ball.Vector.X < 0 && ball.Vector.Y < 0 && ball.XCoordinate <= ball.DestinationPlaneX && ball.YCoordinate <= ball.DestinationPlaneY) ||
                        //(ball.Vector.X < 0 && ball.Vector.Y > 0 && ball.XCoordinate <= ball.DestinationPlaneX && ball.YCoordinate >= ball.DestinationPlaneY))
                        //{
                        //    FindNewBallPosition(ball);
                        //}

                        if (!hitWall && (ball.XCoordinate <= _canvas.LeftUpCorner.X || ball.XCoordinate >= _canvas.RightDownCorner.X - ball.Diameter))
                        {
                            hitWall = true;
                            ball.Vector = new PointF
                            {
                                X = -ball.Vector.X,
                                Y = ball.Vector.Y
                            };
                        }

                        if (!hitWall && (ball.YCoordinate <= _canvas.LeftUpCorner.Y || ball.YCoordinate >= _canvas.RightDownCorner.Y - ball.Diameter))
                        {
                            hitWall = true;
                            ball.Vector = new PointF
                            {
                                X = ball.Vector.X,
                                Y = -ball.Vector.Y
                            };
                        }

                        MoveBall(ball);
                        hitWall = false;
                    }
                });
                task.Start();
            }
            Task task1 = new Task(() => IsCollisionAndHandleCollision(_currentBalls));
            task1.Start();
        }
    }
}
