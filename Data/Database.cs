using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;


namespace Data
{
    public class Database
    {

        public void addGuest(string name, string ppnum, int age, int bookingRef)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";

            
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO Guests(name, passport_num, age, booking_id) VALUES(@name, @ppnum, @age, @bookingref)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = name;
                    cmd.Parameters.Add("@ppnum", SqlDbType.VarChar, 10).Value = ppnum;
                    cmd.Parameters.Add("@age", SqlDbType.Int, 10).Value = age;
                    cmd.Parameters.Add("@bookingref", SqlDbType.Int, 10).Value = bookingRef;

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex);
                    }
                }
            }
        }

        public void addCustomer(int customerid, string name, string address)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";


            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO Customers(customer_id, name, address) VALUES(@customerid, @name, @address)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@customerid", SqlDbType.Int).Value = customerid;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = name;
                    cmd.Parameters.Add("@address", SqlDbType.VarChar, 100).Value = address;

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex);
                    }
                }
            }
        }

        public void addBooking(int bookingref, DateTime arrival, DateTime departure, int breakfast, int evening, int car, int customerid)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";


            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO Bookings(booking_id, arrivalDate, departureDate, chalet_id, customer_id, breakfast, evening, car) VALUES(@bookingref, @arrival, @departure, @chaletid, @customerid, @breakfast, @evening, @car)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@bookingref", SqlDbType.VarChar, 50).Value = bookingref;
                    cmd.Parameters.Add("@arrival", SqlDbType.Date, 10).Value = arrival;
                    cmd.Parameters.Add("@departure", SqlDbType.Date, 10).Value = departure;
                    cmd.Parameters.Add("@chaletid", SqlDbType.Int, 10).Value = 1;
                    cmd.Parameters.Add("@customerid", SqlDbType.Int, 10).Value = customerid;
                    cmd.Parameters.Add("@breakfast", SqlDbType.Int, 10).Value = breakfast;
                    cmd.Parameters.Add("@evening", SqlDbType.Int, 10).Value = evening;
                    cmd.Parameters.Add("@car", SqlDbType.Int, 10).Value = car;


                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex);
                    }
                }
            }
        }

        public string[] getBooking(int refrence)
        {
            var foundbooking = new string[7];

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT arrivalDate, departureDate, chalet_id, customer_id, breakfast, evening, car FROM Bookings WHERE booking_id =@ref", conn);
            command.Parameters.AddWithValue("@ref", refrence);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    foundbooking[0] = reader["arrivalDate"].ToString();
                    foundbooking[1] = reader["departureDate"].ToString();
                    foundbooking[2] = reader["chalet_id"].ToString();
                    foundbooking[3] = reader["customer_id"].ToString();
                    foundbooking[4] = reader["breakfast"].ToString();
                    foundbooking[5] = reader["evening"].ToString();
                    foundbooking[6] = reader["car"].ToString();
                }
            }

            conn.Close();

            return foundbooking;
        }

        public ArrayList getGuest(int refrence)
        {
            ArrayList guestlist = new ArrayList();

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT guest_id, name, passport_num, age FROM Guests WHERE booking_id = @ref", conn);
            command.Parameters.AddWithValue("@ref", refrence);
            // int result = command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var guests = new string[4];
                    guests[0] = reader["guest_id"].ToString();
                    guests[1] = reader["name"].ToString();
                    guests[2] = reader["age"].ToString();
                    guests[3] = reader["passport_num"].ToString();
                    guestlist.Add(guests);
                }
            }

            conn.Close();

            return guestlist;
        }
        public int AmmendBooking()
        {
            return 0;
        }

        public string[] getCustomer(int refrence)
        {
            var found = new string[3];

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT name, address FROM Customers WHERE customer_id =@ref", conn);
            command.Parameters.AddWithValue("@ref", refrence);
            // int result = command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    found[0] = refrence.ToString();
                    found[1] = reader["name"].ToString();
                    found[2] = reader["address"].ToString();

                }
            }

            conn.Close();

            return found;
        }
    }
}
