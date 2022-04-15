using Microsoft.VisualStudio.TestTools.UnitTesting;
using wspolbiezne1.Data;

namespace TestProject_PW
{
    [TestClass]
    public class BallTests
    {
        [TestMethod]
        public void CreateBall_RandomWithinPlane_IsInsideThePlane()
        {
            Ball ball = new Ball(0, 0, 2, 3, 200, 50);
            Plane plane = new Plane(400, 250);

            Assert.IsTrue(ball.XCoordinate < (plane.Width) / 2 - ball.Radius);
            Assert.IsTrue(ball.YCoordinate < (plane.Height) / 2 - ball.Radius);
        }
    }
}