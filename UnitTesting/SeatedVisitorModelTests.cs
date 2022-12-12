using Logic;
using Logic.Models.Event;
using Logic.Models.Visitors;
using Logic.Placement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestClass]
    public class SeatedVisitorModelTests
    {

        [TestMethod]

        public void PlaceVisitors200AdultsOnly_Success()
        {
            //Arrange
            Populator populator = new Populator();

            VisitorList visitorList = populator.Create(200);

            SeatArranger seatArranger = new SeatArranger();

            ArrangementModel arrangement = seatArranger.ArrangeSeating(visitorList);

            Placer placer = new Placer();

            //Act
            var seatedVisitorModel = placer.SeatVisitors(visitorList, arrangement);


            //Assert
            var totalVisitorCount = 0;
            bool allSeated = true;


            foreach(Field field in seatedVisitorModel.fields)
            {
                foreach(Row row in field.rows)
                {
                    foreach(Seat seat in row.seats)
                    {
                        if(seat.visitor != null)
                        {
                            totalVisitorCount++;
                            allSeated = seat.visitor.Seated;
                        }
                    }
                }
            }

            Assert.AreEqual(200, totalVisitorCount);
            Assert.IsTrue(allSeated);
        }

        [TestMethod]

        public void PlaceVisitors17AdultsOnly_Success()
        {
            //Arrange
            Populator populator = new Populator();

            VisitorList visitorList = populator.Create(17);

            SeatArranger seatArranger = new SeatArranger();

            ArrangementModel arrangement = seatArranger.ArrangeSeating(visitorList);

            Placer placer = new Placer();

            //Act
            var seatedVisitorModel = placer.SeatVisitors(visitorList, arrangement);


            //Assert
            var totalVisitorCount = 0;
            bool allSeated = true;


            foreach (Field field in seatedVisitorModel.fields)
            {
                foreach (Row row in field.rows)
                {
                    foreach (Seat seat in row.seats)
                    {
                        if (seat.visitor != null)
                        {
                            totalVisitorCount++;
                            allSeated = seat.visitor.Seated;
                        }
                    }
                }
            }

            Assert.AreEqual(17, totalVisitorCount);
            Assert.IsTrue(allSeated);
        }

        [TestMethod]

        public void PlaceVisitors1AdultOnly_Success()
        {
            //Arrange
            Populator populator = new Populator();

            VisitorList visitorList = populator.Create(1);

            SeatArranger seatArranger = new SeatArranger();

            ArrangementModel arrangement = seatArranger.ArrangeSeating(visitorList);

            Placer placer = new Placer();

            //Act
            var seatedVisitorModel = placer.SeatVisitors(visitorList, arrangement);


            //Assert
            var totalVisitorCount = 0;
            bool allSeated = true;


            foreach (Field field in seatedVisitorModel.fields)
            {
                foreach (Row row in field.rows)
                {
                    foreach (Seat seat in row.seats)
                    {
                        if (seat.visitor != null)
                        {
                            totalVisitorCount++;
                            allSeated = seat.visitor.Seated;
                        }
                    }
                }
            }

            Assert.AreEqual(1, totalVisitorCount);
            Assert.IsTrue(allSeated);
        }
    }
}
