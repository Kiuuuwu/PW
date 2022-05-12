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
            LogicAPI ballManager = LogicAPI.CreateAPI();
            ballManager.CreateBall(1);

            DataAPI ball = new Ball(10, 200, 25, 15, 100, 0, 20, new PointF(2, 5));

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

        //[TestMethod]
        //public void IsCollisionAndHandleCollision_NoCollision_DictionaryValueIsFalse()
        //{
        //    LogicAPI ballManager = LogicAPI.CreateAPI();
        //    ballManager.CreateBall(2);

        //    ballManager.getCollection()[0].XCoordinate = 0;
        //    ballManager.getCollection()[0].YCoordinate = 0;
        //    ballManager.getCollection()[0].Diameter = 10;

        //    ballManager.getCollection()[1].XCoordinate = 20;
        //    ballManager.getCollection()[1].YCoordinate = 20;
        //    ballManager.getCollection()[1].Diameter = 10;

        //    Dictionary<(int, int), bool> bouncesDict = new Dictionary<(int, int), bool>();
        //    ballManager.IsCollisionAndHandleCollision(ballManager.getCollection());

        //    Assert.IsFalse(bouncesDict[(0, 1)]);
        //}





    }
}