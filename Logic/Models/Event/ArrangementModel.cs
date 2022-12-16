using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Event
{
    public class ArrangementModel
    {
        char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        public List<Field> fields = new List<Field>();


        public void CreateFullSizeFields(int fieldNumber)
        {
            if(fieldNumber == 0)
            {
                return;
            }

            int rowDepth = 3;

            if (fieldNumber == 0)
            {
                return;
            }
            for (int i = 0; i < fieldNumber; i++)
            {
                Field field = new Field(GetLetter());;

                for(int j = 0; j < rowDepth; j++)
                {
                    field.RowDepth = rowDepth;
                    field.AddRow(10);
                }

                fields.Add(field);
            }
        }

        public void CreateField(int numberOfRows, int seatsPerRow)
        {
            int rowDepth = numberOfRows;

            Field field = new Field(GetLetter());

            for(int i = 0; i < rowDepth; i++)
            {
                field.RowDepth = rowDepth;
                field.AddRow(seatsPerRow);
            }

            fields.Add(field);
        }

        private string GetLetter()
        {
            string letter = "";

            letter = alphabet[(fields.Count())].ToString();

            return letter;
        }

        public void AddRemainder(List<Field> remainingFields)
        { 
            this.fields.AddRange(remainingFields);
        }
    }
}
