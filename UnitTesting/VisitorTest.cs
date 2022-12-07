using Logic;
using Logic.Models.Visitors;

namespace UnitTesting
{
    [TestClass]
    public class VisitorTest
    {
        [TestMethod]
        public void Visitor_OlderThan12_Success()
        {
            //Arrange
            Visitor v = new Visitor()
            {
                ID = 1,
                DoB = new DateTime(1992, 12, 17),
                GroupNumber = 1,
            };

            bool test = false;

            //Act
            test = v.OlderThan12();


            //Assert
            Assert.IsTrue(test);
        }
        [TestMethod]
        public void Visitor_OlderThan12_Failed()
        {
            //Arrange
            Visitor v = new Visitor()
            {
                ID = 1,
                DoB = new DateTime(2017, 12, 17),
                GroupNumber = 1,
            };

            bool test = false;

            //Act
            test = v.OlderThan12();


            //Assert
            Assert.IsFalse(test);
        }

        [TestMethod]
        public void Visitor_Group_Success()
        {
            //Arrange
            Visitor v = new Visitor()
            {
                ID = 1,
                DoB = new DateTime(2017, 12, 17),
                GroupNumber = 1,
            };

            int expected = 1;
            int result;

            //Act

            result = v.Group();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Visitor_Group_Failed()
        {
            //Arrange
            Visitor v = new Visitor()
            {
                DoB = new DateTime(2017, 12, 17),
                ID = 1,
            };

            int expected = 0;
            int result;

            //Act

            result = v.Group();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Populator_Number_Success()
        {
            Populator populator = new Populator();

            List<Visitor> list = new List<Visitor>();
            int signups = 200;

            var today = new DateTime(2022, 11, 30);
            var adult = today.AddYears(-20);

            for (int i = 0; i < signups; i++)
            {
                list.Add(new Visitor()
                {
                    ID = i,
                    DoB = adult,
                    GroupNumber = 0
                });
            }

            var result = populator.Create(200);

            for (int i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(result.Individuals[i].ID, list[i].ID);
                Assert.AreEqual(result.Individuals[i].DoB, list[i].DoB);
                Assert.AreEqual(result.Individuals[i].GroupNumber, list[i].GroupNumber);
            }
        }

        [TestMethod]
        public void Populator_AmountAndChildren_Success()
        {
            //Arrange
            VisitorList vlist = new VisitorList();
            Populator populator = new Populator();

            int signups = 200;
            int children = 50;

            int expectedChildren = 50;
            int expectedAdults = 150;

            int resultChildren = 0;
            int resultAdults = 0;
            //Act

            


            vlist = populator.Create(signups, children);

            for (int i = 0; i < vlist.Individuals.Count; i++)
            {
                if (vlist.Individuals[i].OlderThan12())
                {
                    resultAdults++;
                }
                else
                {
                    resultChildren++;
                }
            }
            //Assert
            Assert.AreEqual(expectedAdults, resultAdults);
            Assert.AreEqual(expectedChildren, resultChildren);
        }

        [TestMethod]
        public void Populator_SizeAndGroup_Success()
        {
            //Arrange
            VisitorList vlist = new VisitorList();
            Populator populator = new Populator();

            int signups = 201;
            int groups = 11;

            int expectedGroups = 11;
            int resultGroups = 1;
            int foundVisitors = 0;
            //Act

            vlist = populator.CreateGroups(signups, groups);

            //Assert

            for(int i = 0; i < vlist.Groups.Count; i++)
            {
                foundVisitors += vlist.Groups[i].Members.Count();

                if (vlist.Groups[i].GroupNumber == resultGroups)
                {
                    resultGroups++;
                }
            }
            Assert.AreEqual(expectedGroups, (resultGroups - 1));
            Assert.AreEqual(signups, foundVisitors);

        }

        [TestMethod]

        public void Populator_SizeGroupAndChildren_Success()
        {
            //Arrange
            VisitorList vlist = new VisitorList();
            Populator populator = new Populator();

            int signups = 232;
            int groups = 11;
            int childrenPerGroup = 2;
            int expectedTotalChildren = 22;

            int expectedGroups = 11;
            int foundVisitors = 0;
            int foundChildren = 0;
            int resultGroups = 1;


            //Act
            vlist = populator.CreateGroupsWithChildren(signups, groups, childrenPerGroup);

            //Assert

            for (int i = 0; i < vlist.Groups.Count; i++)
            {
                foundVisitors += vlist.Groups[i].Members.Count();

                foundChildren += vlist.Groups[i].ChildrenToSeat();

                if (vlist.Groups[i].GroupNumber == resultGroups)
                {
                    resultGroups++;
                }
            }

            Assert.AreEqual(expectedTotalChildren, foundChildren);
            Assert.AreEqual(expectedGroups, (resultGroups - 1));
            Assert.AreEqual(signups, foundVisitors);
        }
    }
}