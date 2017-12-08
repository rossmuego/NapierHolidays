using System;

namespace Buisness

/*
*  Ross Muego
*  40280659
*  Class for the guest object, setting properties and values and providing validation that the age and passport
*  number are within range.
*  No design patterns used. 
*  Last Modified -- 08/12/2017
*/
{
    public class Guest
    {
        private string name;
        private string passportNum;
        private int age;
        private int guestid;

        public int GuestID
        {
            get
            {
                return guestid;
            }
            set
            {
                if(value.ToString() == "" || value < 0)
                {
                    throw new ArgumentException("Invalid GuestID");
                }
                guestid = value;
            }
        }

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

        public string PassportNumber
        {
            get
            {
                return passportNum;
            }
            set
            {
                if(value == "" || value.Length < 0 || value.Length > 10)
                {
                    throw new ArgumentException("Invalid Passport Number");
                }
                passportNum = value;
            }
        }

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if(value.ToString() == "" || value < 0 || value > 101)
                {
                    throw new ArgumentException("Invalid Age");
                }
                age = value;
            }
        }
    }
}
