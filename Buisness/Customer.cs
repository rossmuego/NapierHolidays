using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public class Customer
    {
        private string name;
        private string address;
        private int customerRef;

        public new string Name
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
                if(value.ToString() == "")
                {
                    throw new ArgumentException("Invalid CustomerRef");
                }
                customerRef = value;
            }
        }


    }
}
