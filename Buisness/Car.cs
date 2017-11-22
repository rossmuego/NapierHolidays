using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public class Car
    {

        private string name;
        private DateTime start;
        private DateTime end;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if(value == "")
                {
                    throw new ArgumentException("Invalid Name");
                }
                name = value;
            }
        }
        public DateTime Start
        {
            get
            {
                return start;
            }
            set
            {
                if(value.ToString() == "")
                {
                    throw new ArgumentException("Invalid Arrival Date");

                }
                start = value;
            }
        }

        public DateTime End
        {
            get
            {
                return end;
            }
            set
            {
                if(value.ToString() == "")
                {
                    throw new ArgumentException("Invalid Departure Date");
                }
                end = value;
            }
        }
    }
}
