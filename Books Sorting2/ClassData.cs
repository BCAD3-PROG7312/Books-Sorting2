using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Sorting2
{
    public class ClassData
    {
        public string CallNumber { get; set; }
        public string Description { get; set; }

        public ClassData()
        {

        }
        public ClassData(string callnumber, string description)
        {
            CallNumber = callnumber;
            Description = description;
        }
    }
}
