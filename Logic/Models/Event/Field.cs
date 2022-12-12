using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Models.Visitors;

namespace Logic.Models.Event
{
    public class Field
    {
        public List<Row> rows = new List<Row>();

        public string FieldLetter = string.Empty;

        public bool IsFull = false;

        public int RowDepth = 0;

        public int RowWidth = 10;

        public int RowsAdded = 0;


        public Field(List<Row> rows, string fieldLetter, bool full)
        {
            this.rows = rows;
            FieldLetter = fieldLetter;
            IsFull = full;
        }
        
        public Field(string fieldLetter)
        {
            this.FieldLetter = fieldLetter;
        }

        public bool SayIfFull()
        {
            bool fieldfull = true;

            if(!this.IsFull)
            {
                foreach (Row row in rows)
                {
                    if (!row.SayIfFull())
                    {
                        fieldfull = false;
                        break;
                    }
                }
                if (fieldfull)
                {
                    IsFull = true;
                }
            }
            return fieldfull;
        }
        public bool AddRow(int amountofSeats)
        {   
            bool success = false;
            
            if(RowsAdded < RowDepth)
            {
                RowsAdded++;

                Row row = new Row(amountofSeats, FieldLetter, RowsAdded);
                this.rows.Add(row);
                this.RowWidth = amountofSeats;
                success = true;
            }
            return success;
        }

        public bool NeedsChild()
        {
            bool needsChild = false;

            foreach(Row row in rows)
            {
                if (row.CanFitChild())
                {
                    needsChild = true;
                    break;
                }
            }
            return needsChild;
        }

        public void AddChild(Visitor childVisitor)
        {
            foreach(Row row in rows)
            {
                if (row.NextRequiresChild && !row.SayIfFull())
                {
                    childVisitor.Seated = true;
                    row.AddVisitor(childVisitor);
                    break;
                }
            }
        }

        public void AddAdult(Visitor adultVisitor)
        {
            foreach(Row row in rows)
            {
                if(!row.SayIfFull())
                {
                    adultVisitor.Seated = true;
                    row.AddVisitor(adultVisitor);
                    break;
                }
            }
        }

        public List<int> RowsWithEmptySeats()
        {
            List<int> suitableRows = new List<int>();

            foreach(Row row in this.rows)
            {
                suitableRows.Add(row.EmptySeats());
            }
            return suitableRows;
        }
    }
}
