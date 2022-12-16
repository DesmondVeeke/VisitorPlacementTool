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
            Assert.AreEqual("a", arrangement.fields[0].FieldLetter);

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
            Assert.AreEqual("a", arrangement.fields[0].FieldLetter);

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
            Assert.AreEqual("a", arrangement.fields[0].FieldLetter);
        }

        [TestMethod]

        public void CreateArrangement_5Visitor_Success()
        {
            //Arrange
            Populator populator = new Populator();

            VisitorList visitorList = populator.Create(5);

            SeatArranger seatArranger = new SeatArranger();

            ArrangementModel arrangement = new ArrangementModel();

            //Act
            arrangement = seatArranger.ArrangeSeating(visitorList);

            //Assert

            Assert.AreEqual(1, arrangement.fields.Count());
            Assert.AreEqual(5, arrangement.fields[0].rows[0].seats.Count());
        }

        [TestMethod]

        public void CreateArrangement_200Adults50Children_Success()
        {
            //Arrange

            Populator populator = new Populator();

            VisitorList visitorList = populator.Create(200, 50);

            SeatArranger arranger = new SeatArranger();

            //Act

            var arrangement = arranger.ArrangeSeating(visitorList);

            //Assert

            var totalSeats = 0;
            foreach(Field field in arrangement.fields)
            {
                foreach(Row row in field.rows)
                {
                    totalSeats += row.SeatCapacity;
                }
            }

            Assert.AreEqual(10, arrangement.fields.Count());
            Assert.IsTrue(totalSeats >= 200);

        }

        [TestMethod]

        public void CreateArrangement_232Adults17Children_Success()
        {
            //Arrange

            Populator populator = new Populator();

            VisitorList visitorList = populator.Create(232, 17);

            SeatArranger arranger = new SeatArranger();

            //Act

            var arrangement = arranger.ArrangeSeating(visitorList);

            //Assert
            var totalSeats = 0;
            foreach (Field field in arrangement.fields)
            {
                foreach (Row row in field.rows)
                {
                    totalSeats += row.SeatCapacity;
                }
            }

            Console.WriteLine("Debug");

            Assert.AreEqual(8, arrangement.fields.Count());
        }


        [TestMethod]

        public void CreateArrangement_30Adults50Children_Success()
        {
            //Arrange

            Populator populator = new Populator();

            VisitorList visitorList = populator.Create(80, 50);

            SeatArranger arranger = new SeatArranger();

            //Act

            var arrangement = arranger.ArrangeSeating(visitorList);

            //Assert

            Assert.AreEqual(9, arrangement.fields.Count());
        }
    }
}
