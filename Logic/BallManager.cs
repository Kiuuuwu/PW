﻿using System;
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
    }
}