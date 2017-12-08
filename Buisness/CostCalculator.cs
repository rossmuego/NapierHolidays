using System;

namespace Buisness
{

    /*
     *  Ross Muego
     *  40280659
     *  Class containing a method to calculate the cost of a booking. Method recieves a booking object
     *  and return an integer value with the total cost of the holiday. 
     *  No design patterns used. 
     *  Last Modified -- 08/12/2017
     */
    public class CostCalculator
    {

        public int calculateCost(Booking booking)
        {
            int cost = 0;
            int perNight = 60;
            int perGuest = 25;
            int extrasCost = 0;
            int breakfastCost = 5;
            int eveningCost = 10;
            int carCost = 50;

            int days = Convert.ToInt32((booking.DepartureDate - booking.ArrivalDate).TotalDays + 1);
            int nights = days - 1;
            int totalGuests = booking.TotalGuests;

            int nightlyCost = perNight * nights;
            int guestCost = perGuest * nights * totalGuests;

            bool breakfast = booking.Breakfast;
            bool evening = booking.Evening;
            int car = booking.Car;

            int totalCarCost = car * carCost;

            if(breakfast == true)
            {
                extrasCost += breakfastCost * days;
            }

            if(evening == true)
            {
                extrasCost += eveningCost * nights;
            }

            cost = nightlyCost + guestCost + extrasCost + totalCarCost;
           
            return cost;
        }
    }
}
