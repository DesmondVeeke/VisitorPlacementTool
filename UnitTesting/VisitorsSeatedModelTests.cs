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
    public class VisitorsSeatedModelTests
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

        [TestMethod]

        public void PlaceVisitors150Adult30Children_Success()
        {
            //Arrange
            Populator populator = new Populator();

            VisitorList visitorList = populator.Create(200, 30);

            SeatArranger seatArranger = new SeatArranger();

            ArrangementModel arrangement = seatArranger.ArrangeSeating(visitorList);

            Placer placer = new Placer();

            //Act
            var seatedVisitorModel = placer.SeatVisitors(visitorList, arrangement);


            //Assert
            var totalVisitorCount = 0;
            var totalChildrenCount = 0;
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

                            if (!seat.visitor.OlderThan12())
                            {
                                totalChildrenCount++;
                            }
                        }
                    }
                }
            }

            Assert.AreEqual(200, totalVisitorCount);
            Assert.AreEqual(30, totalChildrenCount);
            Assert.IsTrue(allSeated);
        }

        [TestMethod]

        public void PlaceVisitors150Adult50Children_Success()
        {
            //Arrange
            Populator populator = new Populator();

            VisitorList visitorList = populator.Create(200, 50);

            SeatArranger seatArranger = new SeatArranger();

            ArrangementModel arrangement = seatArranger.ArrangeSeating(visitorList);

            Placer placer = new Placer();

            //Act
            var seatedVisitorModel = placer.SeatVisitors(visitorList, arrangement);


            //Assert
            var totalVisitorCount = 0;
            var totalChildrenCount = 0;
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

                            if (!seat.visitor.OlderThan12())
                            {
                                totalChildrenCount++;
                            }
                        }
                    }
                }
            }

            Assert.IsTrue(allSeated);
            Assert.AreEqual(200, totalVisitorCount);
            Assert.AreEqual(50, totalChildrenCount);
        }
    }
}
