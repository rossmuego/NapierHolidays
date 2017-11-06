using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    class Guest
    {
        private string name;
        private int passportNum;
        private int age;


        public string Name
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

        public int PassportNumber
        {
            get
            {
                return passportNum;
            }
            set
            {
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
                age = value;
            }
        }


    }
}
