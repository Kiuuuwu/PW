using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using Logic;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TestProject_PW
{
    [TestClass]
    public class BallTests
    {
        [TestMethod]
        public void CreateBall_RandomWithinPlane_IsInsideThePlane()
        {
            BallManager ballManager = new BallManager();
            ballManager.CreateBall(1);

            Assert.IsTrue(ballManager.CurrentBalls[0].XCoordinate <= 540 - ballManager.CurrentBalls[0].Radius);
            Assert.IsTrue(ballManager.CurrentBalls[0].YCoordinate <= 260 - ballManager.CurrentBalls[0].Radius);
        }

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
    }
}