using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    class Customer : Guest
    {
        private string address;
        private int customerRef;


        public string Address
        {
            get
            {
                return address;
            }
            set
            {
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
                customerRef = value;
            }
        }


    }
}
