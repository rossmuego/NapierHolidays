using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.Windows;
using System.Windows.Controls;
using Buisness;

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

        public ArrayList searchCustomerBookings(int customerref)
        {
            ArrayList results = new ArrayList();


            Customer customerDetails = SearchCustomer(customerref);
            results.Add(customerDetails);

            List<Booking> custBookings = new List<Booking>();

            ArrayList bookings = _database.getCustomerBookings(customerref);
            List<Booking> list = new List<Booking>();

            foreach (string[] i in bookings)
            {
                Booking found = new Booking();
                found.BookingRef = Convert.ToInt32(i[0]);
                found.ArrivalDate = Convert.ToDateTime(i[1]);
                found.DepartureDate = Convert.ToDateTime(i[2]);
                found.Chalet = Convert.ToInt32(i[3]);
                found.Breakfast = Convert.ToBoolean(i[4]);
                found.Evening = Convert.ToBoolean(i[5]);
                found.Car = Convert.ToInt32(i[6]);
                found.TotalGuests = Convert.ToInt32(i[7]);
                list.Add(found);
                Console.WriteLine(found.TotalGuests);
            }

            results.Add(list);

            return results;
        }

        public void addBooking(List<Guest> guests, DateTime arrival, DateTime departure, int breakfast, int evening, int customerRef, int totalGuests, Car carhire, int chaletid)
        {
            RefGenerator generator = RefGenerator.Generator;
            Booking newBooking = new Booking();

            int bookingRef = generator.generateBookingRef();
            int car_days = (carhire.End - carhire.Start).Days;

            _database.addBooking(bookingRef, arrival, departure, breakfast, evening, car_days, customerRef, totalGuests, chaletid);
            _database.addChaletBook(arrival, departure, bookingRef, chaletid);

            foreach (Guest x in guests)
            {
                _database.addGuest(x.Name, x.PassportNumber, x.Age, bookingRef);
            }

            _database.addCarHire(bookingRef, carhire.Name, carhire.Start, carhire.End);
        }

        public List<Customer> SearchCustomerList(int refrence)
        {
            ArrayList found = _database.getCustomer(refrence);
            List<Customer> customers = new List<Customer>();
        
            foreach(string[] x in found)
            {
                Customer foundCustomer = new Customer();
                foundCustomer.Name = x[1];
                foundCustomer.Address = x[2];
                foundCustomer.CustomerRef = Convert.ToInt16(x[0]);
                customers.Add(foundCustomer);
            }          

            return customers;
        }

        public Customer SearchCustomer(int refrence)
        {
            ArrayList found = _database.getCustomer(refrence);

            Customer foundCustomer = new Customer();

            foreach (string[] x in found)
            {
                foundCustomer.Name = x[1];
                foundCustomer.Address = x[2];
                foundCustomer.CustomerRef = Convert.ToInt16(x[0]);
            }
           

            return foundCustomer;
        }

        public void addCustomer(int customerid, string name, string address)
        {
            _database.addCustomer(customerid, name, address);
        }
        
        public ArrayList searchBooking(int bookingref)
        {
            ArrayList booking = new ArrayList();

            string[] bookingArray = _database.getBooking(bookingref);
            Booking found = new Booking();
            found.ArrivalDate = Convert.ToDateTime(bookingArray[0]);
            found.DepartureDate = Convert.ToDateTime(bookingArray[1]);
            found.Chalet = Convert.ToInt32(bookingArray[2]);
            found.CustomerID = Convert.ToInt32(bookingArray[3]);
            found.Breakfast = Convert.ToBoolean(bookingArray[4]);
            found.Evening = Convert.ToBoolean(bookingArray[5]);
            found.Car = Convert.ToInt32(bookingArray[6]);
            found.TotalGuests = Convert.ToInt32(bookingArray[7]);
            found.BookingRef = Convert.ToInt32(bookingArray[8]);

            Customer foundCustomer = SearchCustomer(found.CustomerID);

            booking.Add(foundCustomer);

            booking.Add(found);

            List<Guest> guestList = new List<Guest>();

            ArrayList guest = _database.getGuest(bookingref);

            foreach(string[] i in guest)
            {
                Guest foundGuest = new Guest();
                foundGuest.GuestID = Convert.ToInt32(i[0]);
                foundGuest.Name = i[1];
                foundGuest.Age = Convert.ToInt32(i[2]);
                foundGuest.PassportNumber = i[3];
                guestList.Add(foundGuest);
            }

            booking.Add(guestList);

            Car carHire = new Car();

            ArrayList carHireList = _database.getCarHire(bookingref);

            foreach(string[] x in carHireList)
            {
                carHire.Name = x[0];
                carHire.Start = Convert.ToDateTime(x[1]);
                carHire.End = Convert.ToDateTime(x[2]);
                booking.Add(carHire);
            }

            return booking;
        }

        public ArrayList chaletAvailible(DateTime start, DateTime end)
        {
            ArrayList availible = _database.getChalets(start, end);

            return availible;
        }

        public void updateGuest(int guestID, string name, string ppnum, int age)
        {

            _database.AmmendGuest(guestID, name, ppnum, age);
           
        }

        public void updateCustomer(int custid, string name, string address)
        {
            _database.AmmendCustomer(custid, name, address);
        }

        public void updateBooking(int bookingid, DateTime arrival, DateTime departure, int breakfast, int evening, int chaletid, int totalguests, Car hire)
        {
            int totalCarDays = (hire.End - hire.Start).Days;
            DateTime carstart = hire.Start;
            DateTime carend = hire.End;
            string named = hire.Name;

            _database.AmmendBooking(bookingid, arrival, departure, breakfast, evening, chaletid, totalguests, totalCarDays);
            _database.AmmendCar(bookingid, named, carstart, carend);
            _database.AmmendChalet(bookingid, chaletid, arrival, departure);
        }

        public void removeBooking(int bookingid)
        {
            _database.removeBooking(bookingid);
            _database.removeGuestsBooking(bookingid);
        }

        public void removeGuest(int guestid)
        {
            _database.removeSingleGuest(guestid);
        }

        public void removeCustomers(int customerID)
        {
            _database.removeCustomer(customerID);
            _database.removeCustomerBookings(customerID);
        }
    }
}
