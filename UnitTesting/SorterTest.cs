using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using Logic.Models.Visitors;
using Logic.Placement;

namespace UnitTesting
{
    [TestClass]
    public class SorterTest
    {
        [TestMethod]
        public void GroupSort_List_Success()
        {
            //Arrange
            Populator pop = new Populator();

            Sorter sorter = new Sorter();

            //Act
            var testList = pop.CreateGroups(200, 8);

            testList = sorter.FormGroups(testList);

            //Assert
            Assert.AreEqual(8, testList.Groups.Count());
            Assert.AreEqual(200, testList.Individuals.Count());

        }
        [TestMethod]
        public void GroupSort_2ChildrenFirstOptimalList_Success()
        {
            //Arrange
            Populator pop = new Populator();
            Sorter sorter = new Sorter();

            var children = 2;
            var signups = 232;
            var groups = 11;

            //Act
            var testList = pop.CreateGroupsWithChildren(signups, groups, children);

            testList = sorter.OptimalList(testList);

            //Assert

            foreach(Group group in testList.Groups)
            {
                Assert.AreEqual(children, group.ChildrenToSeat());
            }
            Assert.AreEqual(groups, testList.Groups.Count());
            Assert.AreEqual(signups, testList.Individuals.Count());

        }

        [TestMethod]
        public void GroupSort_6ChildrenFirstOptimalList_Success()
        {
            //Arrange
            Populator pop = new Populator();
            Sorter sorter = new Sorter();

            var children = 6;
            var signups = 237;
            var groups = 7;

            //Act
            var testList = pop.CreateGroupsWithChildren(signups, groups, children);

            testList = sorter.OptimalList(testList);

            //Assert

            foreach (Group group in testList.Groups)
            {
                Assert.AreEqual(children, group.ChildrenToSeat());
            }
            Assert.AreEqual(groups, testList.Groups.Count());
            Assert.AreEqual(signups, testList.Individuals.Count());

        }
    }
}
