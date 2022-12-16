using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Models.Event;

namespace Logic.Models.Visitors
{
    public class VisitorsSeatedModel
    {
        public List<Field> fields = new List<Field>();
        public void AddField(Field field)
        {
            fields.Add(field);
        }
    }
}
