using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Models.Visitors;

namespace Logic.Models.Event
{
    public class Event
    {
        public DateTime StartDate { get; set; }
        public int Capacity { get; set; }
        public List<Visitor> signups { get; set; }


        public Event(DateTime startDate, int capacity, List<Visitor> visitors)
        {
            StartDate = startDate;
            Capacity = capacity;
            signups = visitors;
        }
    }
}
