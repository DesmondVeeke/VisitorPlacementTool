using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Models.Visitors;
using Logic.Models.Event;
using System.Globalization;
using System.Runtime.CompilerServices;
using Logic.Placement;
using System.Xml.Schema;

namespace Logic
{
    public class SeatArranger
    {
        public ArrangementModel ArrangeSeating(VisitorList visitorList)
        {
            ArrangementModel arrangedSeating = new ArrangementModel();

            //Set initial values
            int leftToPlace = visitorList.Individuals.Count();
            int children = visitorList.ChildrenLeftToPlace();
            
            int maxAdultsPerField = 24;
            int maxChildrenPerField = 6;
            int maxVisitorsPerField = 30;
            int childFields = 0;

            //Set requirement values
            var adultsPerField = 30.0;
            var adultsPerFieldRest = 0.0;
            int childrenPerField = 0;

            //Calculated values
            int adults = leftToPlace - children;
            var fieldsWithRest = 0;
            var fieldsWithoutRest = 0;


            //At least one field with full row of children?
            if (children > 6)
            {
                //Calculate amount of full fields with children
                childFields = (int)Math.Ceiling(children / 6.0);
                adultsPerField = ((double)adults / (double)childFields);

                //Use decimal value to determine how many  fields should be created with rest adults per field before removing 1 and creating the remaining fields
                fieldsWithRest = (int)Math.Ceiling((adultsPerField - (int)adultsPerField) * 10);
                adultsPerFieldRest = (int)Math.Ceiling((double)adults / (double)childFields);
                adultsPerField = (int)Math.Floor((double)adults / (double)childFields);
                childrenPerField = maxChildrenPerField;
            }

            //More adults than needed to fill fields that contain children.
            if (adultsPerField >= maxAdultsPerField)
            {
                //Create a fields for each full group of 30
                var adultsInChildFields = childFields * maxAdultsPerField;
                var adultsLeft = adults - adultsInChildFields;
                var adultFields = adultsLeft / maxVisitorsPerField;
                
                arrangedSeating.CreateFullSizeFields(childFields + adultFields);

                //Calculate amount of rows and seats for the remaining visitors
                adultsLeft = adultsLeft % 30;
                var updatedFields = CreateFieldForRemainder(arrangedSeating, adultsLeft);

                arrangedSeating.fields = updatedFields;
            }
            //Less adults than needed to fill fields that contain children.
            else
            {
                //Calculate how many fields without rest.
                fieldsWithoutRest = childFields - fieldsWithRest;

                //Create fields with rest visitor
                var totalPerField = (int)adultsPerFieldRest + childrenPerField;
                var updatedFields = CreateCustomFields(arrangedSeating, totalPerField, fieldsWithRest);
                arrangedSeating.fields = updatedFields;

                //Create fields without rest visitors
                totalPerField = (int)adultsPerField + childrenPerField;
                updatedFields = CreateCustomFields(arrangedSeating, totalPerField, fieldsWithoutRest);
                arrangedSeating.fields = updatedFields;

            }
            return arrangedSeating;
        }

        //Create a field for the last visitors below 30.
        private List<Field> CreateFieldForRemainder(ArrangementModel arrangement, int visitors)
        {
            var updatedSeating = arrangement;
            var leftToPlace = visitors;

            while (leftToPlace > 0)
            {
                //Create a field with one row with the remainder
                if (leftToPlace < 3)
                {
                    updatedSeating.CreateField(1, 3);
                    leftToPlace = 0;
                }
                //Create a field with 1 row with the remainder
                else if (leftToPlace <= 10)
                {
                    updatedSeating.CreateField(1, leftToPlace);
                    leftToPlace = 0;
                }
                //Create a field with 3 rows, split rest over three rows
                else if (leftToPlace % 3 == 0)
                {
                    updatedSeating.CreateField(3, (leftToPlace / 3));
                    leftToPlace = 0;
                }
                //Create a field with 2 rows, split rest over two rows
                else if (leftToPlace % 2 == 0 && ((leftToPlace / 2.0) <= 10))
                {
                    updatedSeating.CreateField(2, leftToPlace / 2);
                    leftToPlace = 0;
                }
                else
                //Add a placeholder visitor to make the rows equal in size
                {
                    leftToPlace++;
                }
            }
            return updatedSeating.fields;
        }

        //Create fields that are not at max capacity.
        private List<Field> CreateCustomFields(ArrangementModel arrangement, int visitorsPerField, int createNumberOfFields)
        {

            ArrangementModel updatedSeating = arrangement;

            //Set initial values
            var totalPerField = visitorsPerField;
            var fieldsToCreate = createNumberOfFields;

            //Set loop condition
            int fieldsCreated = 0;
            int seatsPerRow = 0;
            int storedVisitors = 0;


            //Begin sorting
            while (fieldsCreated < fieldsToCreate)
            {
                //Try to place in field with 3 rows
                if (totalPerField % 3 == 0)
                {
                    seatsPerRow = (totalPerField / 3);
                    arrangement.CreateField(3, seatsPerRow);
                    fieldsCreated++;
                }
                //Try to place in field with 2 rows
                else if (totalPerField % 2 == 0 && (totalPerField / 2) <= 10)
                {
                    seatsPerRow = (totalPerField / 2);
                    arrangement.CreateField(2, seatsPerRow);
                    fieldsCreated++;
                }
                //Remove a visitor and place it in a seperate group.
                else
                {
                    totalPerField--;
                    storedVisitors = storedVisitors + (1 * fieldsToCreate);
                }
            }

            //Account for stored visitors
            //Create full fields if possible with the stored visitors.
            if(storedVisitors > 30)
            {
                var fullFieldsToCreate = (storedVisitors / 30);

                arrangement.CreateFullSizeFields(fullFieldsToCreate);

                storedVisitors = storedVisitors % 30;

                AccountForRemainingVisitors();
            }
            else
            //Place remaining stored visitors
            {
                AccountForRemainingVisitors();
            }

            //End function.
            return updatedSeating.fields;




            //Supporting functions

            void AccountForRemainingVisitors()
            {
                //See if the remainder can be split in 3 or 2 rows.
                var canSplitIn3 = storedVisitors % 3 == 0;
                var canSplitin2 = storedVisitors % 2 == 0;


                //Can the total amount of rest visitors be divided over the fields already created?
                var canFit3InCurrentFields = (storedVisitors / 3) <= arrangement.fields.Count();
                var canFit2InCurrentFields = (storedVisitors / 2) <= arrangement.fields.Count();

                while (storedVisitors > 0)
                {
                    var suitableField = FindFieldNotAtMaxCapacity(3);
                    canSplitIn3 = storedVisitors % 3 == 0;
                    canSplitin2 = storedVisitors % 2 == 0;

                    if (canSplitin2)
                    {
                        suitableField = FindFieldNotAtMaxCapacity(2);
                    }
                    else
                    {
                        suitableField = FindFieldNotAtMaxCapacity(3);
                    }

                    if (suitableField is null)
                    {
                        var updateWithRemainder = CreateFieldForRemainder(arrangement, storedVisitors);
                        storedVisitors = 0;
                        arrangement.fields = updateWithRemainder;
                    }
                    else if(canSplitIn3 && canFit3InCurrentFields)
                    {
                        suitableField.AddSeatToEachRow();
                        storedVisitors = storedVisitors - 3;
                    }
                    else if(canSplitin2 && canFit2InCurrentFields)
                    {
                        suitableField.AddSeatToEachRow();
                        storedVisitors = storedVisitors - 2;
                    }
                }

                //Find a field where a visitors can be placed with appropriate row depth?
                Field? FindFieldNotAtMaxCapacity(int rowDepth)
                {
                    var totalFieldCapacity = rowDepth * 10;

                    foreach (Field field in arrangement.fields)
                    {
                        //Does the field have room for more seats evenly distributed among all rows?
                        if (field.RowDepth == rowDepth && ((rowDepth * field.RowWidth) < totalFieldCapacity))
                        {
                            return field;
                        }
                    }
                    return null;
                }
            }



        }
    }
}
