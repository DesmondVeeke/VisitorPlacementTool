using System.Reflection.Metadata.Ecma335;

namespace Logic.Models.Visitors
{
    public class Visitor
    {
        public int ID;
        public DateTime DoB;
        public int GroupNumber;
        public bool Seated = false;

        public bool OlderThan12()
        {
            bool flag = false;
            var today = DateTime.Today;

            DateTime requirement = today.AddYears(-12);

            if (this.DoB <= requirement)
            {
                flag = true;
            }
            return flag;
        }
        public int Group()
        {
            int group = 0;

            group = GroupNumber;

            return group;
        }
    }
}