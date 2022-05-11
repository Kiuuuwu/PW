using Data;
using System.Collections.ObjectModel;
using System.Drawing;

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
        public abstract void IsCollisionAndHandleCollision(ObservableCollection<Ball> CurrentBalls);
        public abstract void MoveBall(Ball ball);
        public abstract void BounceBall(Ball ball1, Ball ball2);
        public abstract PointF FindNewBallPosition(Ball ball);
        public abstract void BallsMovement();
    }
}
