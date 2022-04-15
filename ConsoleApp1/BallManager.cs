using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class BallManager
    {
        public Ball CreateBall()
        {
            Random random = new Random();

            Ball ball = new Ball(random.Next(-100, 100), random.Next(-50, 50), random.NextDouble(), random.NextDouble()*10, random.Next(-100, 100), 100);
            return ball;
        }

        public Ball MoveBall(Ball ball)
        {
            //bierzemy wspolrzedne poczatkowe pilki, wspolrzedne destination i przesuwamy
            throw new NotImplementedException();
            //return ball;
        }

        public Ball BounceBall(Ball ball)
        {
            return MoveBall(ball);
        }
    }
}
