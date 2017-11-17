using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public class CostCalculator
    {

        public int calculateCost(Booking booking, List<Guest> guests)
        {
            int cost = 0;
            int perNight = 60;
            int perGuest = 25;
            int extrasCost = 0;
            int breakfastCost = 5;
            int eveningCost = 10;

            int days = Convert.ToInt32((booking.DepartureDate - booking.ArrivalDate).TotalDays);
            int nights = Convert.ToInt32(((booking.DepartureDate - booking.ArrivalDate).TotalDays) - 1);
            int totalguests = guests.Count;

            int nightlyCost = perNight * nights;
            int guestCost = perGuest * nights;

            bool breakfast = booking.Breakfast;
            bool evening = booking.Evening;
            bool car = booking.Car;

            if(breakfast == true)
            {
                extrasCost += breakfastCost * days;
            }

            if(evening == true)
            {
                extrasCost += eveningCost * nights;
            }

            cost = nightlyCost + guestCost + extrasCost;
           
            return cost;
        }



    }
}
