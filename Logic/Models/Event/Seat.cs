using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Models.Visitors;

namespace Logic.Models.Event
{
    public class Seat
    {
        Visitor? visitor = null;

        public string? seatID = null;

        public Seat(Visitor visitor, string seatID)
        {
            this.visitor = visitor;
            this.seatID = seatID;
        }

        public Seat(string seatID)
        {
            this.seatID = seatID;
        }

        public bool IsFull()
        {
            if (this.visitor == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void SetID(string id)
        {
            this.seatID = id;
        }
    }
}
