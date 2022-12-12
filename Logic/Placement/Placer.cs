using Logic.Models.Event;
using Logic.Models.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Placement
{
    public class Placer
    {

        public SeatedVisitorModel SeatVisitors(VisitorList visitorList, ArrangementModel arrangementModel)
        {
            SeatedVisitorModel seatedVisitorModel = new SeatedVisitorModel();


            Sorter sorter = new Sorter();

            var children = sorter.ChildrenOnly(visitorList);

            var adults = sorter.AdultsOnly(visitorList);

            foreach(Field field in arrangementModel.fields)
            {
                var totalToPlace = (field.RowDepth * field.RowWidth);

                while(totalToPlace> 0)
                {
                    var nextChild = children.FirstOrDefault();

                    var nextAdult = adults.FirstOrDefault();

                    if(nextAdult == null)
                    {
                        break;
                    }

                    if (field.NeedsChild() && visitorList.ChildrenLeftToPlace() > 0)
                    {
                        field.AddChild(nextChild);
                        children.Remove(nextChild);
                        totalToPlace--;
                    }
                    else
                    {
                        field.AddAdult(nextAdult);
                        adults.Remove(nextAdult);
                        totalToPlace--;
                    }
                }
                seatedVisitorModel.AddField(field);
            }
            
            return seatedVisitorModel;
        }



    }
}
