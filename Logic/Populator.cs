using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Models.Visitors;

namespace Logic
{
    public class Populator
    {
        public VisitorList Create(int signups)
        {
            List<Visitor> visitors = new List<Visitor>();
            VisitorList list = new VisitorList();

            var today = new DateTime(2022, 11, 30);
            var adult = today.AddYears(-20);

            for (int i = 0; i < signups; i++)
            {
                visitors.Add(new Visitor()
                {
                    ID = i,
                    DoB = adult,
                    GroupNumber = 0
                });
            }

            list.Individuals = visitors;

            return list;
        }

        public VisitorList Create(int signups, int children)
        {
            List<Visitor> visitors = new List<Visitor>();
            VisitorList list = new VisitorList();

            var today = new DateTime(2022, 11, 30);
            var adult = today.AddYears(-20);
            var child = today.AddYears(-5);
            int created = 0;

           

            for (int i = 0; i < signups; i++)
            {

                if (created < children)
                {
                    visitors.Add(new Visitor()
                    {
                        ID = created,
                        DoB = child,
                        GroupNumber = 0
                    });
                    created++;
                }
                else
                {
                    visitors.Add(new Visitor()
                    {
                        ID = created,
                        DoB = adult,
                        GroupNumber = 0
                    });
                    created++;
                }

            }
            list.Individuals = visitors;
            list.Children = children;

            return list;
        }

        public VisitorList CreateGroups(int signups, int groups)
        {
            Group g = new Group();
            VisitorList vList = new VisitorList();

            var groupNum = 1;
            double rest = signups % groups;
            var groupSize = signups / groups;

            g.GroupNumber = groupNum;

            var dob = new DateTime(2000, 12, 17);

            for (int visitorID = 1; visitorID <= signups; visitorID++)
            {
                Visitor visitor = new Visitor()
                {
                    ID = visitorID,
                    DoB = dob,
                    GroupNumber = groupNum
                };
                g.Members.Add(visitor);
                vList.Individuals.Add(visitor);
                
                bool groupCompleted = GroupCompleted(visitorID, groupSize);


                if (groupCompleted)
                {
                    vList.Groups.Add(g);
                    
                    if (groupNum < groups)
                    {
                        groupNum++;

                        g = new Group()
                        {
                            GroupNumber = groupNum,
                        };
                    }
                    else
                    {
                        if (rest > 0)
                        {
                            for (int j = 0; j < rest; j++)
                            {
                                Visitor restVisitor = new Visitor()
                                {
                                    ID = (groupSize + j),
                                    DoB = dob,
                                    GroupNumber = vList.Groups[j].GroupNumber
                                };

                                vList.Groups[j].Members.Add(restVisitor);
                                vList.Individuals.Add(restVisitor);
                                
                                //Account for rest added related to the total signups required.
                                visitorID++;
                            }
                        }
                    }
                }


            }

            return vList;
        }

        public VisitorList CreateGroupsWithChildren(int signups, int groups, int numberofChildrenInGroup)
        {
            VisitorList visitorList = new VisitorList();

            visitorList = CreateGroups(signups, groups);

            foreach (Group group in visitorList.Groups)
            {
                var childrenToPlace = numberofChildrenInGroup;

                for(int i = 0; i < childrenToPlace; i++)
                {
                    group.Members[i] = ChangeToChild(group.Members[i]);
                    visitorList.Children++;
                }
            }

            return visitorList;
        }

        private Visitor ChangeToChild(Visitor visitor)
        {
            var youngVisitor = visitor;
            var today = new DateTime(2022, 11, 30);
            var child = today.AddYears(-5);

            youngVisitor.DoB = child;

            return youngVisitor;
        }

        private bool GroupCompleted(int currentVisitor, int groupSize)
        {
            bool needed = false;

            if (currentVisitor % groupSize == 0 && currentVisitor != 0)
            {
                needed = true;
            }

            return needed;
        }
        
    }
}
