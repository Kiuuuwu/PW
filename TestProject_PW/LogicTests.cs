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
            LogicAPI ballManager = LogicAPI.CreateAPI();
            ballManager.CreateBall(1);
            double tmp_x = ballManager.getCollection()[0].XCoordinate;
            double tmp_y = ballManager.getCollection()[0].YCoordinate;
            ballManager.MoveBall(ballManager.getCollection()[0], 5, 3, new System.Drawing.PointF(10, 10));

            Assert.AreNotEqual(ballManager.getCollection()[0].XCoordinate, tmp_x);
            Assert.AreNotEqual(ballManager.getCollection()[0].YCoordinate, tmp_y);
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
            LogicAPI ballManager = LogicAPI.CreateAPI();
            ballManager.CreateBall(1);

            Assert.IsTrue(ballManager.getCollection()[0].XCoordinate <= 680 - ballManager.getCollection()[0].Radius);
            Assert.IsTrue(ballManager.getCollection()[0].YCoordinate <= 400 - ballManager.getCollection()[0].Radius);
        }

        
    }
}