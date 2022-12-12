using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Models.Visitors;
using Logic.Models.Event;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class SeatArranger
    {
        public ArrangementModel ArrangeSeating(VisitorList visitorList)
        {
            ArrangementModel arrangedSeating = new ArrangementModel();

            int leftToPlace = visitorList.Individuals.Count();

            int fieldCount = leftToPlace / 30;
            
            if(fieldCount != 0)
            {
                arrangedSeating.CreateFullSizeFields(fieldCount);
            }

            leftToPlace = leftToPlace  % 30;

            while (leftToPlace > 0)
            {
                if(leftToPlace < 3)
                {
                    arrangedSeating.CreateField(1, 3);
                    leftToPlace = 0;
                }
                else if(leftToPlace <= 10)
                {
                    arrangedSeating.CreateField(1, leftToPlace);
                    leftToPlace = 0;
                }
                else if(leftToPlace % 3 == 0)
                {
                    arrangedSeating.CreateField(3, (leftToPlace / 3));
                    leftToPlace = 0;
                }
                else if(leftToPlace % 2 == 0 && ((leftToPlace / 2.0) <= 10))
                {
                    arrangedSeating.CreateField(2, leftToPlace / 2);
                    leftToPlace = 0;
                }
                else
                {
                    leftToPlace++;
                }
            }
            return arrangedSeating;
        }
        
    }
}
