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
       
        public int generateBookingRef()
        {
            bookingRef++;
            int refr = bookingRef++;

            return refr;

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
