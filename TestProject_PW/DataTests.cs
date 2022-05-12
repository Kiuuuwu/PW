using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject_PW
{
    [TestClass]
    public class DataTests
    {
        [TestMethod]
        public void UpdateMovement_Valid_ArePropertiesChanged()
        {
            DataAPI ball = new Ball(10, 200, 25, 15, 100, 0, 20, new PointF(0, 0));

            double oldX = ball.DestinationPlaneX;
            double oldY = ball.DestinationPlaneY;
            double oldNrOfFrames = ball.NrOfFrames;
            PointF oldVector = ball.Vector;

            ball.UpdateMovement(0, 20, new PointF(5, 10), 30);

            Assert.AreNotEqual(oldX, ball.DestinationPlaneX);
            Assert.AreNotEqual(oldY, ball.DestinationPlaneY);
            Assert.AreNotEqual(oldNrOfFrames, ball.NrOfFrames);
            Assert.AreNotEqual(oldVector, ball.Vector);


        }
        [TestMethod]
        public void Move_Valid_ArePropertiesChanged()
        {
            DataAPI ball = new Ball(10, 200, 25, 15, 100, 0, 20, new PointF(5, 10));

            double oldX = ball.XCoordinate;
            double oldY = ball.YCoordinate;

            ball.Move();

            Assert.AreNotEqual(oldX, ball.XCoordinate);
            Assert.AreNotEqual(oldY, ball.YCoordinate);
        }
    }
}
