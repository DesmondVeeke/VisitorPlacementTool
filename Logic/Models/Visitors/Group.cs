using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Visitors
{
    public class Group
    {
        public int GroupNumber { get; set; }

        public List<Visitor> Members = new List<Visitor>();

        public Group()
        {

        }

        public bool ContainsChildren()
        {
            bool hasChild = false;

            foreach (Visitor visitor in Members)
            {
                if (!visitor.OlderThan12())
                {
                    hasChild = true;
                }
            }
            return hasChild;
        }

        public int ChildrenToSeat()
        {
            int toSeat = 0;

            foreach (Visitor visitor in Members)
            {
                if (!visitor.Seated && !visitor.OlderThan12())
                {
                    toSeat++;
                }
            }
            return toSeat;
        }
    }
}

