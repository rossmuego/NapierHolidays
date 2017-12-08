using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Buisness;

namespace Testing
{
    [TestClass]
    public class BookingTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void testID()
        {

            Booking bookingId = new Booking();
            bookingId.BookingRef = -5;

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void testCustomerId()
        {
            Booking customerid = new Booking();
            customerid.CustomerID = -1;
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void testArrival()
        {
            Booking arrival = new Booking();
            arrival.ArrivalDate = Convert.ToDateTime("01/12/1990");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void testDepart()
        {
            Booking departure = new Booking();
            departure.ArrivalDate = DateTime.Today;
            departure.DepartureDate = departure.ArrivalDate.AddDays(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void testGuests()
        {
            Booking guests = new Booking();
            guests.TotalGuests = 67;
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void testCar()
        {
            Booking car = new Booking();
            car.Car = -6;
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void testChalet()
        {
            Booking chalet = new Booking();
            chalet.Chalet = 45433;
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void bookingIdTest()
        {
            Booking bookingid = new Booking();
            bookingid.BookingRef = -23;
        }
    }
}
