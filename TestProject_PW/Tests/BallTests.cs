using Microsoft.VisualStudio.TestTools.UnitTesting;
using wspolbiezne1.Data;
using wspolbiezne1.Logic;

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

            Assert.IsTrue(ball.XCoordinate < (plane.Width) / 2 - ball.Radius);
            Assert.IsTrue(ball.YCoordinate < (plane.Height) / 2 - ball.Radius);
        }
    }
}