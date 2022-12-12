using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Models.Visitors;


namespace Logic.Placement
{
    public class Sorter
    {
        public VisitorList VisitorList = new VisitorList();

        public VisitorList SortedList = new VisitorList();

        public VisitorList FormGroups(VisitorList visitorList)
        {

            VisitorList sortedList = new VisitorList();

            List<int> groupIds = FindGroupIDS(visitorList);

            List<Group> groupList = SortIntoGroups(groupIds, visitorList.Individuals);

            sortedList.Groups = groupList;
            sortedList.Individuals = visitorList.Individuals;

            return sortedList;
        }



        public List<Group> ChildrenInGroupsFirst(VisitorList visitorList)
        {

            List<Group> sortedList = visitorList.Groups;

            sortedList = sortedList.OrderBy(o => o.ContainsChildren()).ToList();

            foreach(Group group in sortedList)
            {
                group.Members = group.Members.OrderBy(o => o.OlderThan12()).ToList();
            }

            return sortedList;
        }

        public List<Visitor> ChildrenOnly(VisitorList visitorList)
        {
            List<Visitor> sortedList = visitorList.Individuals;

            sortedList = sortedList.Where(o => !o.OlderThan12()).ToList();

            return sortedList;
        }

        public List<Visitor> AdultsOnly(VisitorList visitorList)
        {
            List<Visitor> sortedList = visitorList.Individuals;

            sortedList = sortedList.Where(o => o.OlderThan12()).ToList();

            return sortedList;
        }



        public VisitorList OptimalList(VisitorList visitorList)
        {
            VisitorList optimalList = new VisitorList();

            optimalList = FormGroups(visitorList);

            optimalList.Groups = ChildrenInGroupsFirst(optimalList);

            return optimalList;
        }

        public int GroupsLeft(VisitorList visitorList)
        {
            int groupCount = 0;

            foreach(Group group in visitorList.Groups)
            {
                groupCount++;
            }
            return groupCount;
        }


        private List<int> FindGroupIDS(VisitorList visitorList)
        {
            List<int> foundIds = new List<int>();

            foreach (Visitor visitor in visitorList.Individuals)
            {
                if (!foundIds.Contains(visitor.GroupNumber))
                {
                    foundIds.Add(visitor.GroupNumber);
                }
            }
            return foundIds;
        }

        private List<Group> SortIntoGroups(List<int> groupIds, List<Visitor> visitors)
        {
            
            List<Group> groups = new List<Group>();

            foreach (int groupId in groupIds)
            {
                var groupMembers = visitors.Where(x => x.GroupNumber == groupId).ToList();

                Group group = new Group()
                {
                    GroupNumber = groupId,
                };

                group.Members = groupMembers;

                groups.Add(group);
            }
            return groups;
        }
    }
}
