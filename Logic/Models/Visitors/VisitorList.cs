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

            foreach (Group group in Groups)
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
    }
}
