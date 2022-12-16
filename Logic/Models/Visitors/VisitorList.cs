using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Visitors
{
    public class VisitorList
    {
        public int Children = 0;

        public List<Group> Groups = new List<Group>();

        public List<Visitor> Individuals = new List<Visitor>();


        public bool AnyVisitorsLeft()
        {
            bool visitorsLeft = false;

            foreach (Group group in this.Groups)
            {
                if(group.Members.Count() > 0)
                {
                    visitorsLeft = true;
                }
            }
            if(Individuals.Count() > 0)
            {
                visitorsLeft = true;
            }

            return visitorsLeft;
        }

        public bool ChildrenToPlace()
        {
            var childrenLeft = ChildrenLeftToPlace();

            if(childrenLeft == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int ChildrenLeftToPlace()
        {
            int children = 0;
            foreach(Visitor visitor in Individuals)
            {
                if (!visitor.OlderThan12() && !visitor.Seated)
                {
                    children++;
                }
            }
            this.Children = children;
            return children;
        }
    }
}
