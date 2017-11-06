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
        private int bookingRef = 1;
        private int chaletID;
        private double cost;
        private int bookref;

       /*
        * 
        * Method for generating booking ref
        * 
        */

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
            //    bookingRef = generateBookingRef();
            }
        }





    }
}
