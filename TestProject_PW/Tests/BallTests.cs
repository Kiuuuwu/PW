using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using Logic;

namespace TestProject_PW
{
    [TestClass]
    public class BallTests
    {
        [TestMethod]
        public void CreateBall_RandomWithinPlane_IsInsideThePlane()
        {
            BallManager ballManager = new BallManager();
            Ball ball = ballManager.CreateBall();
            Plane plane = new Plane(400, 250);

            Assert.IsTrue(ball.XCoordinate <= (plane.Width) / 2 - ball.Radius);
            Assert.IsTrue(ball.YCoordinate <= (plane.Height) / 2 - ball.Radius);
        }

        [TestMethod]
        public void MoveBall_ValidAmmount_IsMovedInsideThePlane()
        {
            BallManager ballManager = new BallManager();
            Ball ball = ballManager.CreateBall();
            Plane plane = new Plane(400, 250);

            Ball ball1 = ballManager.MoveBall(ball, 5);

            Assert.AreNotEqual(ball.XCoordinate, ball1.XCoordinate);
            Assert.AreNotEqual(ball.YCoordinate, ball1.YCoordinate);

            Assert.IsTrue(ball.XCoordinate <= (plane.Width) / 2 - ball.Radius);
            Assert.IsTrue(ball.YCoordinate <= (plane.Height) / 2 - ball.Radius);
        }
    }
}