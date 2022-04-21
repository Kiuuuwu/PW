using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using System.Linq;

namespace TestProject_PW
{
    [TestClass]
    public class LogicTests
    {
        [TestMethod]
        public void MoveBall_ValidAmmount_IsBallMoved()
        {
            BallManager ballManager = new BallManager();
            ballManager.CreateBall(1);
            double tmp_x = ballManager.CurrentBalls[0].XCoordinate;
            double tmp_y = ballManager.CurrentBalls[0].YCoordinate;
            ballManager.MoveBall(ballManager.CurrentBalls[0], 5, 3, new System.Drawing.PointF(10, 10));

            Assert.AreNotEqual(ballManager.CurrentBalls[0].XCoordinate, tmp_x);
            Assert.AreNotEqual(ballManager.CurrentBalls[0].YCoordinate, tmp_y);
        }

        [TestMethod]
        public void CreateBall_Valid_IsBallCreated()
        {
            BallManager ballManager = new BallManager();
            ballManager.CreateBall(1);
            Assert.AreEqual(ballManager.CurrentBalls.Count(), 1);
        }

        [TestMethod]
        public void CreateBall_RandomWithinPlane_IsInsideThePlane()
        {
            BallManager ballManager = new BallManager();
            ballManager.CreateBall(1);

            Assert.IsTrue(ballManager.CurrentBalls[0].XCoordinate <= 580 - ballManager.CurrentBalls[0].Radius);
            Assert.IsTrue(ballManager.CurrentBalls[0].YCoordinate <= 300 - ballManager.CurrentBalls[0].Radius);
        }

        
    }
}