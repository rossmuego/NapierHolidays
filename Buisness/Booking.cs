using System;

/*
 * Ross Muego 40280659
 * Booking class containing the getters and setters for a booking along with its attributes.
 * 28/11/2017
 * No design patterns used
 */

namespace Buisness
{
    public class Booking
    {
        // Properties of booking objects

        private DateTime arrivalDate;
        private DateTime departureDate;
        private int bookingRef;
        private double cost;
        private int chaletID;
        private int customerID;
        private bool breakfast;
        private bool evening;
        private int car;
        private int totalGuests;

        public int TotalGuests
        {
            get
            {
                return totalGuests;
            }
            set
            {
                if (value.ToString() == "" || value > 6 || value < 0)
                {
                    throw new Exception("Invalid Total Guests");
                }
                totalGuests = value;
            }
        }

        public bool Breakfast
        {
            get
            {
                return breakfast;
            }
            set
            {
                if(value != true && value != false){
                    throw new Exception("Invalid breakfast option");
                }
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
              if(value.ToString() == "")
                {
                    throw new Exception("Invalid evening option");
                }
                evening = value;
            }
        }

        public int Car
        {
            get
            {
                return car;
            }
            set
            {
                if(value.ToString() == "" || value < 0)
                {
                    throw new Exception("Invalid car hire");
                }
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
                if(value.ToString() == "" || value < 0 || value > 10)
                {
                    throw new Exception("Invalid chalet");
                }
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
                if(value.ToString() == "" || value < 0)
                {
                    throw new Exception("Invalid customerid");
                }
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
                if(value.ToString() == "" || value < 0)
                {
                    throw new Exception("invalid bookinref");
                }
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
                if(value.ToString() == "")
                {
                    throw new Exception("Invalid arrival date");
                }
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
                if(value.ToString() == "" || value < arrivalDate)
                {
                    throw new Exception("Invalid departure date");
                }
                departureDate = value;
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
                if(value.ToString() == "" || value == 0)
                {
                    throw new Exception("invalid cost");
                }
                cost = value;
            }
        }



    }
}
