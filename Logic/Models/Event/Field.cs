using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Event
{
    public class Field
    {
        List<Row> rows = new List<Row>();

        public string FieldLetter = string.Empty;

        public bool IsFull = false;

        public int RowCapacity = 3;

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
        public bool AddRow(int amountofSeats, bool childFriendly)
        {
            Row row = new Row(amountofSeats, FieldLetter);

            row.ChildFriendlyRow = childFriendly;
            
            bool success = false;
            if(RowsAdded < RowCapacity)
            {
                RowsAdded++;
                row.Number = RowsAdded;
                this.rows.Add(row);
                success = true;
            }
            return success;
        }
    }
}
