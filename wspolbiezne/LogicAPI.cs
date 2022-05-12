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
        public abstract ObservableCollection<DataAPI> getCollection();
        public abstract void CreateBall(int NrOfBalls);
        public abstract void IsCollisionAndHandleCollision(ObservableCollection<DataAPI> CurrentBalls);
        public abstract void MoveBall(DataAPI ball);
        public abstract void BounceBall(DataAPI ball1, DataAPI ball2);
        public abstract void FindNewBallPosition(DataAPI ball);
        public abstract void BallsMovement();
    }
}
