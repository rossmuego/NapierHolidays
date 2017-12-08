using System;
using System.Data.SqlClient;

namespace Buisness
{

    /*
 *  Ross Muego
 *  40280659
 *  Class containing methods to generate booking ID's and customer ID numbers. When initilised it gets the 
 *  highest id's of the respective variables from the database, then everytime the method is called to generate
 *  and ID number, 1 is added to the respective number.
 *  This class uses a singleton design pattern in order to not create multiple instances of it, therefor allowign 
 *  confusion and inconsistencies between ID numbers. 
 *  Last Modified -- 08/12/2017
 */
    public class RefGeneratorSingleton
    {
        // The singleton aspect of the class, setting it to private and only allowing generation
        // once through the property Generator.
        private RefGeneratorSingleton() { }

        private static RefGeneratorSingleton generator;

        public static RefGeneratorSingleton Generator
        {
            get
            {
                if(generator == null)
                {
                    generator = new RefGeneratorSingleton();
                }
                return generator;
            }
        }

        // assigning the base ID's when the class is first instanciated by calling the below methods.

        private int bookingRef = baseBookRef();
        private int customerRef = baseCustRef();

        //  Method called to generate a new booking refrence
        public int generateBookingRef()
        {
            bookingRef++;

            return bookingRef;
        }

        //  Method called to generate new customer refrence
        public int generateCustomerRef()
        {
            customerRef++;

            return customerRef;
        }

        //  Method to obtain the highest booking ref in the system when the class is first instanciated. 
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

        //  Method to obtain the highest customer ref in the system when the class is first instanciated. 

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
