using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Models.Visitors;

namespace Logic.Models.Event
{
    public class Row
    {
        public List<Seat> seats = new List<Seat>();

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

        public Row(int numberofSeats, string fieldLetter, int rowNumber) 
        {
            List<Seat> seats = new List<Seat>();

            this.FieldLetter = fieldLetter;
            this.Number = rowNumber;
            this.SeatCapacity = numberofSeats;

            for(int i = 0; i < numberofSeats; i++)
            {
                Seat seat = new Seat((FieldLetter + "-" + Number.ToString()+ "-" + i));
                this.seats.Add(seat);
            }
            if(rowNumber == 1)
            {
                ChildFriendlyRow = true;
            }

        }

        public bool AddSeat()
        {
            Seat seat = new Seat((FieldLetter + "-" + Number.ToString() + "-" + seats.Count()));
            seats.Add(seat);
            return true;
        }

        public bool AddVisitor(Visitor visitor)
        {
            bool success = false;
            foreach(Seat seat in seats)
            {
                if (!seat.IsFull())
                {
                    seat.visitor = visitor;
                    success = true;
                    break;
                }
            }
            return success;
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

        public bool CanFitChild()
        {
            bool canFitChild = false;
            var visitorCount = seats.Count();


            if (seats[visitorCount - 1].visitor!= null)
            {
                return false;
            }

            if (this.ChildFriendlyRow)
            {
                if (!seats[0].IsFull())
                {
                    canFitChild = true;
                }
                else if(this.seats.Count() % 2 == 0)
                {
                    bool even = true;
                    canFitChild = NextSeatNeedsChildOrAdult(even);
                }
                else
                {
                    bool uneven = false;
                    canFitChild = NextSeatNeedsChildOrAdult(uneven);
                }
            }
            return canFitChild;
        }

        private bool NextSeatNeedsChildOrAdult(bool Even)
        {
            int placeID = 0;
            bool childNeeded = true;
            int cycleCount = 0;
            

            for (int i = 0; i < seats.Count(); i++)
            {
                if (!seats[i].IsFull())
                {
                    break;
                }
                
                if (seats[i].IsFull())
                {
                    placeID++;
                }
                
                if(placeID == 3)
                {
                    placeID = 0;
                    cycleCount++;
                    if(cycleCount == 3 && Even)
                    {
                        placeID = 1;
                    }
                }

            }
            if(placeID == 1)
            {
                childNeeded = false;
            }
            return childNeeded;
        }



    }
}
