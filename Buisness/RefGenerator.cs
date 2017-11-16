using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.Data.SqlClient;

namespace Buisness
{
    public class RefGenerator
    {

        private RefGenerator() { }

        private static RefGenerator generator;

        public static RefGenerator Generator
        {
            get
            {
                if(generator == null)
                {
                    generator = new RefGenerator();
                }
                return generator;
            }
        }

        private int bookingRef = baseBookRef();
        private int customerRef = baseCustRef();

        public int generateBookingRef()
        {
            bookingRef++;

            return bookingRef;
        }

        public int generateCustomerRef()
        {
            customerRef++;

            return customerRef;
        }

        private static int baseBookRef()
        {
            int highbook = 0;

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT booking_id FROM Bookings ORDER BY booking_id DESC", conn);
            highbook = Convert.ToInt32(command.ExecuteScalar());

            conn.Close();

            return highbook;
        }

        private static int baseCustRef()
        {
            int highcust = 0;

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT customer_id FROM Customers ORDER BY customer_id DESC", conn);

            highcust = Convert.ToInt32(command.ExecuteScalar());

            conn.Close();

            return highcust;
        }
    }
}
