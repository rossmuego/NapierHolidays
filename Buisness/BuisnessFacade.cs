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

        public void addBooking(List<Guest> guests, DateTime arrival, DateTime departure, int breakfast, int evening, int car, int customerRef)
        {
            RefGenerator generator = RefGenerator.Generator;

            int bookingRef = generator.generateBookingRef();

            _database.addBooking(bookingRef, arrival, departure, breakfast, evening, car, customerRef);

            foreach (Guest x in guests)
            {
                _database.addGuest(x.Name, x.PassportNumber, x.Age, bookingRef);
            } 
        }

        public Customer SearchCustomer(int refrence)
        {
            string[] found = _database.getCustomer(refrence);

            Customer foundCustomer = new Customer();
            foundCustomer.Name = found[1];
            foundCustomer.Address = found[2];
            foundCustomer.CustomerRef = Convert.ToInt16(found[0]);

            return foundCustomer;
        }

        public void addCustomer(int customerid, string name, string address)
        {
            _database.addCustomer(customerid, name, address);
        }
        
    }
}
