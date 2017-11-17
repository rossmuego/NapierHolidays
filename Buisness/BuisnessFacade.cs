﻿using System;
using System.Collections;
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

        public void addBooking(List<Guest> guests, DateTime arrival, DateTime departure, int breakfast, int evening, int customerRef, int totalGuests, Car carhire)
        {
            RefGenerator generator = RefGenerator.Generator;

            int bookingRef = generator.generateBookingRef();
            int car_days = Convert.ToInt32((carhire.End - carhire.Start).TotalDays);

            _database.addBooking(bookingRef, arrival, departure, breakfast, evening, car_days, customerRef, totalGuests);


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
    
            Customer foundCustomer = SearchCustomer(bookingref);

            booking.Add(foundCustomer);

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

            return booking;
        }
    }
}
