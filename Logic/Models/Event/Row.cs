using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Event
{
    public class Row
    {
        List<Seat> seats = new List<Seat>();

        public int Number = 1;

        public string FieldLetter = string.Empty;

        public int SeatCapacity = 10;

        public bool ChildFriendlyRow = false;

        public bool IsFull = false;

        public Row(List<Seat> seats, int number, bool childFriendlyRow, bool isFull)
        {
            this.seats = seats;
            Number = number;
            ChildFriendlyRow = childFriendlyRow;
            IsFull = isFull;
        }

        public Row(int numberofSeats, string fieldLetter) 
        {
            List<Seat> seats = new List<Seat>();

            this.FieldLetter = fieldLetter;

            for(int i = 0; i < numberofSeats; i++)
            {
                Seat seat = new Seat((FieldLetter + Number.ToString() + i));
                this.seats.Add(seat);
            }

        }

        public bool AddSeat(Seat seat)
        {
            seats.Add(seat);
            return true;
        }

        public int EmptySeats()
        {
            int emptySeatCount = 0;
            foreach(Seat seat in seats)
            {
                if (!seat.IsFull())
                {
                    emptySeatCount++;
                }
            }
            if (emptySeatCount == 0)
            {
                IsFull = true;
            }
            return emptySeatCount;
        }

        public bool SayIfFull()
        {
            bool full = true;
            foreach(Seat seat in seats)
            {
                if (!seat.IsFull())
                {
                    full = false;
                    IsFull = false;
                    break;
                }
            }
            if (full)
            {
                IsFull = true;
            }
            return full;
        }

        public bool CanFitChildren()
        {
            return this.ChildFriendlyRow;
        }
    }
}
