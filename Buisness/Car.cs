using System;

namespace Buisness
{

    /*
     *  Ross Muego
     *  40280659
     *  Car object propeties and values. Checking that the name is not left blank along with the dates being correct
     *  No design patterns used. 
     */
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
                if(value.ToString() == "" || value < start)
                {
                    throw new ArgumentException("Invalid Departure Date");
                }
                end = value;
            }
        }
    }
}
