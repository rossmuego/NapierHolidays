using System;

namespace Buisness
{

    /*
 *  Ross Muego
 *  40280659
 *  Class containing the propeties and values of the Customer object. Validation carried out to make sure the properties
 *  are valid.
 *  No design patterns used. 
 *  Last Modified -- 08/12/2017
 */
    public class Customer
    {
        private string name;
        private string address;
        private int customerRef;

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
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                if(value == "")
                {
                    throw new ArgumentException("Invalid Address");
                }
                address = value;
            }
        }

        public int CustomerRef
        {
            get
            {
                return customerRef;
            } 
            set
            {
                if(value.ToString() == "" || value < 0)
                {
                    throw new ArgumentException("Invalid CustomerRef");
                }
                customerRef = value;
            }
        }


    }
}
