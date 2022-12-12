using Logic;
using Logic.Models.Event;
using Logic.Models.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestClass]
    public class RowTests
    {
        [TestMethod]
        public void RowCanFitChild_Even_Success()
        {
            //Arrange

            Row row = new Row(10, "a", 1);

            //Act

            bool result = row.CanFitChild();

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]

        public void RowCanFitChild_Even_Fail()
        {
            //Arrange
            Row row = new Row(10, "a", 1);
            Populator populator= new Populator();



            var visitors = populator.Create(1);

            for (int i = 0; i < visitors.Individuals.Count(); i++)
            {
                row.seats[i].visitor = visitors.Individuals[i];
            }
            
            //Act

            bool result = row.CanFitChild();    
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]

        public void RowCanFitChild_Uneven_Success()
        {
            //Arrange
            Row row = new Row(9, "a", 1);
            Populator populator = new Populator();

            var visitors = populator.Create(9);

            for (int i = 0; i < visitors.Individuals.Count(); i++)
            {
                row.seats[i].visitor = visitors.Individuals[i];
            }

            //Act
            bool result = row.CanFitChild();

            Assert.IsFalse(result);
        }
    }
}
