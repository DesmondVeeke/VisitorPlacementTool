using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Models.Visitors;
using Logic.Models.Event;
using System.Globalization;

namespace Logic
{
    public class SeatArranger
    {
        char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        public ArrangementModel ArrangeSeating(VisitorList visitorList)
        {
            ArrangementModel arrangedSeating = new ArrangementModel();

            int leftToPlace = visitorList.Individuals.Count();

            int fieldsCreated = arrangedSeating.fields.Count();

            bool needNewField = true;

            Field currentField = new Field("empty field");



            Group chunk = new Group();

            while(leftToPlace > 0)
            {
                leftToPlace = visitorList.Individuals.Count();

                if (visitorList.Individuals.Count() > 30)
                {
                    chunk.Members = visitorList.Individuals.GetRange(0, 10);
                    bool chunkPlaced = false;

                    while (!chunkPlaced)
                    {

                        if (needNewField)
                        {
                            currentField = new Field(alphabet[fieldsCreated].ToString());
                            needNewField = false;
                        }


                        if (currentField.RowsAdded == 0)
                        {
                            currentField.AddRow(currentField.RowCapacity, true);
                            chunkPlaced = true;
                        }
                        else if (currentField.RowsAdded == 3)
                        {
                            arrangedSeating.fields.Add(currentField);
                            fieldsCreated = arrangedSeating.fields.Count();

                            needNewField = true;
                        }
                        if (!chunkPlaced && currentField.RowsAdded <3 && currentField.RowsAdded != 0)
                        {
                            currentField.AddRow(currentField.RowCapacity, false);
                            chunkPlaced = true;
                        }
                    }
                    visitorList.Individuals.RemoveRange(0, 10);
                    leftToPlace = visitorList.Individuals.Count();
                }

                if(leftToPlace == 0)
                {
                    return arrangedSeating;
                }


                if(leftToPlace < 30 || leftToPlace == 30)
                {

                    if(currentField.RowsAdded != 0) 
                    {
                        arrangedSeating.fields.Add(currentField);
                    } 

                    currentField = new Field(alphabet[fieldsCreated].ToString());


                    bool restLeftToPlace = true;
                    bool placeHolderNeeded = false;

                    while (restLeftToPlace)
                    {

                        if (placeHolderNeeded)
                        {
                            visitorList.Individuals.Add(new Visitor());
                        }

                        if (visitorList.Individuals.Count() < currentField.RowCapacity)
                        {

                            if (visitorList.Individuals.Count() < 3)
                            {
                                currentField.AddRow(3, true);
                                visitorList = CleanupRest(visitorList);
                            }
                        }
                        if (visitorList.Individuals.Count() % (currentField.RowCapacity) == 0 && visitorList.Individuals.Count() > 0)
                        {
                            currentField = CreateThreeRows(visitorList, currentField);
                            visitorList = CleanupRest(visitorList);
                        }


                        if (visitorList.Individuals.Count() % (currentField.RowCapacity - 1) == 0 && visitorList.Individuals.Count() > 1)
                        {
                            var seatsPerRow = visitorList.Individuals.Count() / 2;


                            if(seatsPerRow <= currentField.RowCapacity)
                            {
                                currentField = CreateTwoRows(visitorList, currentField);
                                visitorList = CleanupRest(visitorList);

                            }
                            else
                            {
                                placeHolderNeeded = true;
                            }

                        }

                        restLeftToPlace = visitorList.AnyVisitorsLeft();

                    }
                    arrangedSeating.fields.Add(currentField);
                }
                leftToPlace = visitorList.Individuals.Count();
            }


            return arrangedSeating;
        }

        private VisitorList CleanupRest(VisitorList visitorList)
        {
            var list = visitorList;
            list.Individuals.RemoveRange(0, list.Individuals.Count());

            return list;
        }

        private Field CreateTwoRows(VisitorList list, Field field)
        {
            var currentField = field;

            var visitorList = list;

            var seatsPerRow = visitorList.Individuals.Count() / 2;

            if (seatsPerRow <= currentField.RowCapacity)
            {
                currentField.AddRow(seatsPerRow, true);
                currentField.AddRow(seatsPerRow, false);
            }

            return currentField;
        }

        private Field CreateThreeRows(VisitorList list, Field field)
        {
            var currentField = field;
            var visitorList = list;

            var seatsPerRow = visitorList.Individuals.Count() / 3;

            for(int i = 0; i < currentField.RowCapacity; i++)
            {
                if(i == 0)
                {
                    currentField.AddRow(seatsPerRow, true);
                }
                else
                {
                    currentField.AddRow(seatsPerRow, false);
                }
            }
            return currentField;
        }

        
    }
}
