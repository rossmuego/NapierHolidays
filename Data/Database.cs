using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Database
    {

        public void addGuest(string name, string ppnum, int age)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\NapierHolidays.mdf;Integrated Security=True;Connect Timeout=30";

            string query = "INSERT INTO Guests(name, passport_number, age, booking_refrence) VALUES(@name, @ppnum, @age, 1)";

            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand myCmd = new SqlCommand(query, conn))
            {
                // set up parameters
                myCmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = name;
                myCmd.Parameters.Add("@ppnum", SqlDbType.VarChar, 10).Value = ppnum;
                myCmd.Parameters.Add("@age", SqlDbType.Int, 10).Value = age;

                try
                {
                    conn.Open();
                    myCmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
            }

        }

        public void addCustomer()
        {

        }

        public void addBooking()
        {

        }

        public static int getBooking()
        {

            return 0;
        }

        public static int AmmendBooking()
        {
            return 0;
        }

    }
}
