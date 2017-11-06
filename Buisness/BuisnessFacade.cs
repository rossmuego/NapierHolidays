using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Buisness
{
    public class BuisnessFacade
    {

        private Guest _guest;
        private Customer _customer;
        private Booking _booking;
        private Database _database;

        public BuisnessFacade()
        {
            _guest = new Guest();
            _customer = new Customer();
            _booking = new Booking();
            _database = new Database();
        }


        public void addBooking(List<Guest> guests)
        {
            foreach (Guest x in guests)
            {
                _database.addGuest(x.Name, x.PassportNumber, x.Age);
            } 
        }
        
    }
}
