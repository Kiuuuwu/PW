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
            ObservableCollection<Ball> BallList = new ObservableCollection<Ball>();
            BallManager ballManager = new BallManager();
            BallList = ballManager.CreateBall(BallList);
            Plane plane = new Plane(400, 250);

            Assert.IsTrue(BallList[0].XCoordinate <= (plane.Width) / 2 - BallList[0].Radius);
            Assert.IsTrue(BallList[0].YCoordinate <= (plane.Height) / 2 - BallList[0].Radius);
        }

        [TestMethod]
        public void MoveBall_ValidAmmount_IsMovedInsideThePlane()
        {
            ObservableCollection<Ball> BallList = new ObservableCollection<Ball>();
            BallManager ballManager = new BallManager();
            BallList = ballManager.CreateBall(BallList);
            Plane plane = new Plane(400, 250);

            BallList.Add(ballManager.MoveBall(BallList[0], 5));

            Assert.AreNotEqual(BallList[0].XCoordinate, BallList[1].XCoordinate);
            Assert.AreNotEqual(BallList[0].YCoordinate, BallList[1].YCoordinate);

            Assert.IsTrue(BallList[1].XCoordinate <= (plane.Width) / 2 - BallList[1].Radius);
            Assert.IsTrue(BallList[1].YCoordinate <= (plane.Height) / 2 - BallList[1].Radius);
        }
    }
}