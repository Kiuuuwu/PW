using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using System.Linq;
using Data;
using System.Drawing;
using System.Collections.Generic;

namespace TestProject_PW
{
    [TestClass]
    public class LogicTests
    {
        [TestMethod]
        public void MoveBall_ValidAmmount_IsBallMoved()
        {
            Logger logger = new Logger();
            LogicAPI ballManager = LogicAPI.CreateAPI();
            ballManager.CreateBall(1);

            DataAPI ball = new Ball(1, 10, 200, 25, 15, 100, 0, 20, new PointF(2, 5), logger);

            ballManager.FindNewBallPosition(ball);
            double tmp_x = ball.XCoordinate;
            double tmp_y = ball.YCoordinate;
            ballManager.MoveBall(ball);

            Assert.AreNotEqual(ball.XCoordinate, tmp_x);
            Assert.AreNotEqual(ball.YCoordinate, tmp_y);
        }

        [TestMethod]
        public void CreateBall_Valid_IsBallCreated()
        {
            LogicAPI ballManager = LogicAPI.CreateAPI();
            ballManager.CreateBall(1);
            Assert.AreEqual(ballManager.getCollection().Count(), 1);
        }

        [TestMethod]
        public void CreateBall_RandomWithinPlane_IsInsideThePlane()
        {
            Canvas canvas = new Canvas(new Point(0, 0), new Point(640, 360));
            LogicAPI ballManager = LogicAPI.CreateAPI();
            ballManager.CreateBall(1);

            Assert.IsTrue(ballManager.getCollection()[0].XCoordinate <= canvas.RightDownCorner.X - ballManager.getCollection()[0].Diameter &&
                ballManager.getCollection()[0].XCoordinate >= canvas.LeftUpCorner.X - ballManager.getCollection()[0].Diameter);
            Assert.IsTrue(ballManager.getCollection()[0].YCoordinate <= canvas.RightDownCorner.Y - ballManager.getCollection()[0].Diameter &&
                ballManager.getCollection()[0].YCoordinate >= canvas.LeftUpCorner.Y - ballManager.getCollection()[0].Diameter);
        }
    }
}