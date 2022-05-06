using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class LogicAPI
    {
        public static LogicAPI CreateAPI()
        {
            return new BallManager();
        }
        public abstract ObservableCollection<Ball> getCollection();
        public abstract void CreateBall(int NrOfBalls);
        public abstract void MoveBall(Ball ball, double nrOfFrames, double duration, PointF vector);
        public abstract Ball BounceBall(Ball ball);
        public abstract PointF FindNewBallPosition(Ball ball, int nrOfFrames, PointF vector);
        public abstract void BallsMovement();
    }
}
