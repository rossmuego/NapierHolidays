﻿using System;
using System.Collections;
using System.Collections.Generic;
using Data;

namespace Buisness
{

    /*
*  Ross Muego
*  40280659
*  Class containing a number of methods that communicate with my database in my data layer. 
*  Last Modified -- 08/12/2017
*/
    public class BuisnessFacade
    {

        private Database _database;

        public BuisnessFacade()
        {
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

        public int addBooking(Booking newBooking, List<Guest> guests, int customerRef, Car carhire)
        {
            RefGeneratorSingleton generator = RefGeneratorSingleton.Generator;

            DateTime arrival = newBooking.ArrivalDate;
            DateTime departure = newBooking.DepartureDate;
            int breakfast = Convert.ToInt32(newBooking.Breakfast);
            int evening = Convert.ToInt32(newBooking.Evening);
            int totalGuests = newBooking.TotalGuests;
            int chaletid = newBooking.Chalet;
            

            int bookingRef = generator.generateBookingRef();
            int car_days = (carhire.End - carhire.Start).Days + 1;

            _database.addBooking(bookingRef, arrival, departure, breakfast, evening, car_days, customerRef, totalGuests, chaletid);
            _database.addChaletBook(arrival, departure, bookingRef, chaletid);

            foreach (Guest x in guests)
            {
                _database.addGuest(x.Name, x.PassportNumber, x.Age, bookingRef);
            }

            if(carhire != null)
            {
                _database.addCarHire(bookingRef, carhire.Name, carhire.Start, carhire.End);
            }

            return bookingRef;
        }

        public void addGuest(Guest add, int bookingref, int total)
        {
            _database.addGuest(add.Name, add.PassportNumber, add.Age, bookingref);
            _database.updateTotalGuests(total, bookingref);
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

        public void updateBooking(int bookingid, DateTime arrival, DateTime departure, int breakfast, int evening, int chaletid, int totalguests)
        {
            _database.AmmendBooking(bookingid, arrival, departure, breakfast, evening, chaletid, totalguests);
            _database.AmmendChalet(bookingid, chaletid, arrival, departure);
        }

        public void removeBooking(int bookingid)
        {
            _database.removeBooking(bookingid);
            _database.removeGuestsBooking(bookingid);
            _database.removeChaletAvali(bookingid);
            _database.removeCar(bookingid);
        }

        public void removeGuest(int guestid, int guests, int bookingid)
        {
            _database.removeSingleGuest(guestid);
            _database.updateTotalGuests(guests, bookingid);
        }

        public void removeCustomers(int customerID)
        {
            _database.removeCustomer(customerID);
            _database.removeCustomerBookings(customerID);
        }

        public void addCar(Car hire, int bookingid)
        {
            _database.addCarHire(bookingid, hire.Name, hire.Start, hire.End);
        }

        public void updateCar(Car hire, int bookingid)
        {
            ArrayList test = _database.getCarHire(bookingid);
            Car testcar = new Car();
            int totaldays;

            foreach(string[] x in test)
            {
                testcar.Name = x[0];
                testcar.Start = Convert.ToDateTime(x[1]);
                testcar.End = Convert.ToDateTime(x[2]);
            }

            totaldays = (testcar.End - testcar.Start).Days + 1;

            if (testcar.Name == "")
            {
                _database.addCarHire(bookingid, hire.Name, hire.Start, hire.End);
                _database.updateCarDays(totaldays, bookingid);
            }
            else
            {
                _database.AmmendCar(bookingid, hire.Name, hire.Start, hire.End);
                _database.updateCarDays(totaldays, bookingid);
            }
        }
    }
}
