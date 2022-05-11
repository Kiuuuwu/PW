using Data;
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
                PointF vector = new PointF(0, 0);
                int diameter = 30;// random.Next(50) + 10;
                Ball ball = new Ball(random.Next(0, 640 - diameter), random.Next(2, 360 - diameter), random.NextDouble() / 10, diameter, 0, 0, random.Next(2, 300), vector);
                _currentBalls.Add(ball);
            }
        }

        public override void MoveBall(Ball ball, double nrOfFrames, double duration)
        {
            //ball.XCoordinate += ball.Vector.X;
            //ball.YCoordinate += ball.Vector.Y;
            //ball.Speed = (int)((duration / nrOfFrames) * 100);
            ball.Move(nrOfFrames, duration);
            Thread.Sleep((int)((duration / nrOfFrames) * 100));
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

            ball1.UpdateMovement(ball2.DestinationPlaneX, ball2.DestinationPlaneY, ball2.Vector, ball1.Speed - ((2 * ball2.Mass) / ball1.Mass + ball2.Mass));
            ball2.UpdateMovement(tmpX, tmpY, tmp, ball2.Speed - ((2 * ball1.Mass) / ball1.Mass + ball2.Mass));
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

        public override PointF FindNewBallPosition(Ball ball, int nrOfFrames)
        {
            // losowe miejsce na ktorejs ze scianek jako destination point
            Random random = new Random();
            var values = new[] { 0, 1, 2, 3 };
            int result = values[random.Next(values.Length)];        // to zwroci cyfre od 1 do 4?
            switch (result)
            {
                case 0: //sciana lewa
                    ball.DestinationPlaneX = 0;
                    ball.DestinationPlaneY = random.Next(0, 360 - (int)ball.Diameter);
                    break;
                case 1: //sciana prawa
                    ball.DestinationPlaneX = 640 - (int)ball.Diameter;
                    ball.DestinationPlaneY = random.Next(0, 360 - (int)ball.Diameter);
                    break;
                case 2: //sciana gorna
                    ball.DestinationPlaneX = random.Next(0, 640 - (int)ball.Diameter);
                    ball.DestinationPlaneY = 0;
                    break;
                case 3: //sciana dolna
                    ball.DestinationPlaneX = random.Next(0, 640 - (int)ball.Diameter);
                    ball.DestinationPlaneY = 360 - (int)ball.Diameter;
                    break;
            }

            // jeżeli wylosujemy wspolrzedna, w ktorej juz znajduje sie kulka, to przerzucamy cel na przeciwlegla sciane
            if (ball.XCoordinate == ball.DestinationPlaneX)
            {
                if (ball.DestinationPlaneX == 0)
                    ball.DestinationPlaneX = 640 - ball.Diameter;
                else if (ball.DestinationPlaneX == 640 - ball.Diameter)
                    ball.DestinationPlaneX = 0;
            }

            if (ball.YCoordinate == ball.DestinationPlaneY)
            {
                if (ball.DestinationPlaneY == 0)
                    ball.DestinationPlaneY = 360 - ball.Diameter;
                else if (ball.DestinationPlaneY == 360 - ball.Diameter)
                    ball.DestinationPlaneY = 0;
            }

            return new PointF
            {
                X = (float)((ball.DestinationPlaneX - ball.XCoordinate) / nrOfFrames),
                Y = (float)((ball.DestinationPlaneY - ball.YCoordinate) / nrOfFrames)
            };
        }

        public override void BallsMovement()
        {
            foreach (Ball ball in _currentBalls)
            {
                Task task = new Task(() =>
                {
                    ball.Vector = FindNewBallPosition(ball, 25);
                    while (true)
                    {
                        // todo: pilka znajduje nowy wektor w momencie gdy sie odbije od sciany lub innej pilki
                        if ((ball.Vector.X > 0 && ball.Vector.Y > 0 && ball.XCoordinate >= ball.DestinationPlaneX && ball.YCoordinate >= ball.DestinationPlaneY) ||
                        (ball.Vector.X > 0 && ball.Vector.Y < 0 && ball.XCoordinate >= ball.DestinationPlaneX && ball.YCoordinate <= ball.DestinationPlaneY) ||
                        (ball.Vector.X < 0 && ball.Vector.Y < 0 && ball.XCoordinate <= ball.DestinationPlaneX && ball.YCoordinate <= ball.DestinationPlaneY) ||
                        (ball.Vector.X < 0 && ball.Vector.Y > 0 && ball.XCoordinate <= ball.DestinationPlaneX && ball.YCoordinate >= ball.DestinationPlaneY))
                        {
                            ball.Vector = FindNewBallPosition(ball, 25);
                        }

                        MoveBall(ball, 7, 4);
                    }
                });
                task.Start();
            }
            Task task1 = new Task(() => IsCollisionAndHandleCollision(_currentBalls));
            task1.Start();
        }
    }
}
