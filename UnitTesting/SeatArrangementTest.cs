using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Models.Visitors;
using Logic.Models.Event;

namespace UnitTesting
{
    [TestClass]
    public class SeatArrangementTest
    {
        [TestMethod]

        public void CreateArrangement_200Visitors_Success()
        {
            //Arrange
            Populator populator = new Populator();

            VisitorList visitorList = populator.Create(200);

            SeatArranger seatArranger = new SeatArranger();

            ArrangementModel arrangement = new ArrangementModel();

            //Act
            arrangement = seatArranger.ArrangeSeating(visitorList);

            //Assert

            Assert.AreEqual(7, arrangement.fields.Count());
        }

        [TestMethod]

        public void CreateArrangement_232Visitors_Success()
        {
            //Arrange
            Populator populator = new Populator();

            VisitorList visitorList = populator.Create(232);

            SeatArranger seatArranger = new SeatArranger();

            ArrangementModel arrangement = new ArrangementModel();

            //Act
            arrangement = seatArranger.ArrangeSeating(visitorList);

            //Assert

            Assert.AreEqual(8, arrangement.fields.Count());
        }

        [TestMethod]

        public void CreateArrangement_1Visitor_Success()
        {
            //Arrange
            Populator populator = new Populator();

            VisitorList visitorList = populator.Create(1);

            SeatArranger seatArranger = new SeatArranger();

            ArrangementModel arrangement = new ArrangementModel();

            //Act
            arrangement = seatArranger.ArrangeSeating(visitorList);

            //Assert

            Assert.AreEqual(1, arrangement.fields.Count());
        }
    }
}
