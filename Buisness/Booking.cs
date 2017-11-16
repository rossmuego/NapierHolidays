using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness;


namespace Buisness
{
    public class Booking
    {
        private DateTime arrivalDate;
        private DateTime departureDate;
        private int bookingRef;
        private double cost;
        private int chaletID;
        private int customerID;
        private bool breakfast;
        private bool evening;
        private bool car;
       
        public bool Breakfast
        {
            get
            {
                return breakfast;
            }
            set
            {
                breakfast = value;
            }
        }

        public bool Evening
        {
            get
            {
                return evening;
            }
            set
            {
                evening = value;
            }
        }

        public bool Car
        {
            get
            {
                return car;
            }
            set
            {
                car = value;
            }
        }
        public int Chalet
        {
            get
            {
                return chaletID;
            }
            set
            {
                chaletID = value;
            }
        }
        public int CustomerID
        {
            get
            {
                return customerID;
            }
            set
            {
                customerID = value;
            }
        }
        public int BookingRef
        {
            get
            {
                return bookingRef;
            }
            set
            {
                bookingRef = value;
            }

        }

        public DateTime ArrivalDate
        {
            get
            {
                return arrivalDate;
            }
            set
            {
                arrivalDate = value;
            }
        }

        public DateTime DepartureDate
        {
            get
            {
                return departureDate;
            }
            set
            {
                departureDate = value;
            }
        }

        public int BookingRefrence
        {
            get
            {
                return bookingRef;
            }
            set
            {
                bookingRef = value;
            }
        }

        public double Cost
        {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
            }
        }



    }
}
