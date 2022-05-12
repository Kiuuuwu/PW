using Data;
using System.Collections.ObjectModel;
using System.Drawing;
using Model;

namespace Logic
{
    public class BallManager : LogicAPI
    {
        private ObservableCollection<DataAPI> _currentBalls = new ObservableCollection<DataAPI>();
        private ObservableCollection<BallModel> _currentBallsModel = new ObservableCollection<BallModel>();
        private Canvas _canvas = new Canvas(new Point(0, 0), new Point(640, 360));

        public ObservableCollection<DataAPI> CurrentBalls => _currentBalls;

        public override ObservableCollection<BallModel> getCollection() => _currentBallsModel;

        public override async Task CreateBall(int NrOfBalls)
        {
            _currentBalls.Clear();
            Random random = new Random();
            for (int i = 0; i < NrOfBalls; i++)
            {
                PointF vector = new PointF(0, 0);
                int diameter = random.Next(40) + 20;
                DataAPI ball = new Ball(
                    random.Next(_canvas.LeftUpCorner.X, _canvas.RightDownCorner.X - diameter),
                    random.Next(_canvas.LeftUpCorner.Y, _canvas.RightDownCorner.Y - diameter),
                    random.Next(20, 30), diameter,
                    0,
                    0,
                    random.NextDouble() + 0.1,
                    vector);
                _currentBalls.Add(ball);
                _currentBallsModel.Add(new BallModel(ball.XCoordinate, ball.YCoordinate, ball.Diameter));
            }
        }

        private void UpdateBallsModel()
        {
            for (int i = 0; i < CurrentBalls.Count; i++)
            {
                var ball = CurrentBalls[i];
                _currentBallsModel[i].Update(ball.XCoordinate, ball.YCoordinate, ball.Diameter);
            }
        }

        public override void MoveBall(DataAPI ball)
        {
            ball.Move();
            Thread.Sleep(50);
        }

        public override void BounceBall(DataAPI ball1, DataAPI ball2)  // odbijanie pilek
        {
            PointF tmp = ball1.Vector;
            double tmpX = ball1.DestinationPlaneX;
            double tmpY = ball1.DestinationPlaneY;
            double temp = ball1.NrOfFrames + ((2 * ball2.Mass) / (ball1.Mass + ball2.Mass));
            double temp2 = ball2.NrOfFrames + ((2 * ball1.Mass) / (ball1.Mass + ball2.Mass));
            ball1.UpdateMovement(ball2.DestinationPlaneX, ball2.DestinationPlaneY, ball2.Vector, temp);
            ball2.UpdateMovement(tmpX, tmpY, tmp, temp2);
        }
        public override void IsCollisionAndHandleCollision(ObservableCollection<DataAPI> CurrentBalls) // czy pilka zderza sie z inna pilka
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
                UpdateBallsModel();

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

        public override void FindNewBallPosition(DataAPI ball)
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
        }

        public override async Task BallsMovement()
        {
            foreach (DataAPI ball in _currentBalls)
            {
                Task task = new Task(() =>
                {
                    bool hitWall = false;
                    FindNewBallPosition(ball);
                    while (true)
                    {
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
