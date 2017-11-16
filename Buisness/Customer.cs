using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public class Customer : Guest
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
