using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

/*
 *  Ross Muego
 *  40280659
 *  30/11/2017
 *  No Design Patterns
 *  
 *  Database class for handling all of my SQL queries adding, removing and updating information in my database. This communicates with my facade
 *  in my Buisness layer.
 *  
 */


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

        public void addBooking(int bookingref, DateTime arrival, DateTime departure, int breakfast, int evening, int car, int customerid, int totalGuests, int chaletid)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";


            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO Bookings(booking_id, arrivalDate, departureDate, chalet_id, customer_id, breakfast, evening, car_days, total_guests) VALUES(@bookingref, @arrival, @departure, @chaletid, @customerid, @breakfast, @evening, @car, @totalguests)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@bookingref", SqlDbType.VarChar, 50).Value = bookingref;
                    cmd.Parameters.Add("@arrival", SqlDbType.Date, 10).Value = arrival;
                    cmd.Parameters.Add("@departure", SqlDbType.Date, 10).Value = departure;
                    cmd.Parameters.Add("@chaletid", SqlDbType.Int, 10).Value = chaletid;
                    cmd.Parameters.Add("@customerid", SqlDbType.Int, 10).Value = customerid;
                    cmd.Parameters.Add("@breakfast", SqlDbType.Int, 10).Value = breakfast;
                    cmd.Parameters.Add("@evening", SqlDbType.Int, 10).Value = evening;
                    cmd.Parameters.Add("@car", SqlDbType.Int, 10).Value = car;
                    cmd.Parameters.Add("@totalguests", SqlDbType.Int, 10).Value = totalGuests;

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

        public ArrayList getCustomerBookings(int customerRef)
        {
            ArrayList list = new ArrayList();

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT booking_id, arrivalDate, departureDate, chalet_id, breakfast, evening, car_days, total_guests FROM Bookings WHERE customer_id =@ref", conn);
            command.Parameters.AddWithValue("@ref", customerRef);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var foundbooking = new string[8];
                    foundbooking[0] = reader["booking_id"].ToString();
                    foundbooking[1] = reader["arrivalDate"].ToString();
                    foundbooking[2] = reader["departureDate"].ToString();
                    foundbooking[3] = reader["chalet_id"].ToString();
                    foundbooking[4] = reader["breakfast"].ToString();
                    foundbooking[5] = reader["evening"].ToString();
                    foundbooking[6] = reader["car_days"].ToString();
                    foundbooking[7] = reader["total_guests"].ToString();
                    list.Add(foundbooking);
                }
            }

            conn.Close();

            return list;

        }
        public string[] getBooking(int refrence)
        {
            var foundbooking = new string[9];

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT booking_id, arrivalDate, departureDate, chalet_id, customer_id, breakfast, evening, car_days, total_guests FROM Bookings WHERE booking_id =@ref", conn);
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
                    foundbooking[6] = reader["car_days"].ToString();
                    foundbooking[7] = reader["total_guests"].ToString();
                    foundbooking[8] = reader["booking_id"].ToString();
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
        public void AmmendBooking(int bookingid, DateTime arrival, DateTime departure, int bfast, int evening, int chalet, int totalguests)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";


            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "UPDATE Bookings SET arrivalDate = @arrival, departureDate = @depart, chalet_id = @chalet, breakfast = @bfast, evening = @evening, total_guests = @totalguests WHERE booking_id = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = bookingid;
                    cmd.Parameters.Add("@arrival", SqlDbType.Date, 50).Value = arrival;
                    cmd.Parameters.Add("@depart", SqlDbType.Date, 10).Value = departure;
                    cmd.Parameters.Add("@bfast", SqlDbType.Int, 10).Value = bfast;
                    cmd.Parameters.Add("@evening", SqlDbType.Int, 10).Value = evening;
                    cmd.Parameters.Add("@chalet", SqlDbType.Int, 10).Value = chalet;
                    cmd.Parameters.Add("@totalguests", SqlDbType.Int, 10).Value = totalguests;

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

        public void AmmendCar(int bookingid, string name, DateTime start, DateTime end)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";


            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "UPDATE CarHire SET name = @name, start_date = @arrival, end_date = @departure WHERE booking_id = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = bookingid;
                    cmd.Parameters.Add("@arrival", SqlDbType.Date, 50).Value = start;
                    cmd.Parameters.Add("@departure", SqlDbType.Date, 10).Value = end;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = name;


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

        public void AmmendCustomer(int custref, string name, string address)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";


            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "UPDATE Customers SET name = @name, address = @address WHERE customer_id = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = custref;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = name;
                    cmd.Parameters.Add("@address", SqlDbType.VarChar, 10).Value = address;

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

        public void AmmendGuest(int guestid, string name, string ppnum, int age)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";


            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "UPDATE Guests SET name = @name, passport_num = @ppnum, age = @age WHERE guest_id = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = guestid;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = name;
                    cmd.Parameters.Add("@ppnum", SqlDbType.VarChar, 10).Value = ppnum;
                    cmd.Parameters.Add("@age", SqlDbType.Int, 20).Value = age;

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

        public void AmmendChalet(int bookingid, int chaletid, DateTime arrive, DateTime depart)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";


            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "UPDATE ChaletAvali SET chalet_id = @chalet, arrival = @arrive, depart = @depart WHERE booking_id = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = bookingid;
                    cmd.Parameters.Add("@chalet", SqlDbType.Int, 50).Value = chaletid;
                    cmd.Parameters.Add("@arrive", SqlDbType.Date, 10).Value = arrive;
                    cmd.Parameters.Add("@depart", SqlDbType.Date, 20).Value = depart;

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

        public void addCarHire(int bookref, string name, DateTime start, DateTime finish)
        {

            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";


            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO CarHire(booking_id, name, start_date, end_date) VALUES(@bookingid, @name, @start_date, @end_date)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@bookingid", SqlDbType.Int).Value = bookref;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = name;
                    cmd.Parameters.Add("@start_date", SqlDbType.Date, 20).Value = start;
                    cmd.Parameters.Add("@end_date", SqlDbType.Date, 20).Value = finish;

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

        public ArrayList getCarHire(int refrence)
        {
            ArrayList hire = new ArrayList();

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT name, start_date, end_date FROM CarHire WHERE booking_id =@ref", conn);
            command.Parameters.AddWithValue("@ref", refrence);
            // int result = command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var found = new string[3];
                    found[0] = reader["name"].ToString();
                    found[1] = reader["start_date"].ToString();
                    found[2] = reader["end_date"].ToString();
                    hire.Add(found);
                }
            }

            conn.Close();

            return hire;
        }

        public ArrayList getCustomer(int refrence)
        {
            ArrayList customers = new ArrayList();

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT name, address FROM Customers WHERE customer_id =@ref", conn);
            command.Parameters.AddWithValue("@ref", refrence);
            // int result = command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var found = new string[3];
                    found[0] = refrence.ToString();
                    found[1] = reader["name"].ToString();
                    found[2] = reader["address"].ToString();
                    customers.Add(found);
                }
            }

            conn.Close();

            return customers;
        }

        public ArrayList getChalets(DateTime arrive, DateTime depart)
        {
            ArrayList list = new ArrayList();

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT chalet_id FROM Chalets WHERE chalet_id NOT IN (SELECT chalet_id FROM ChaletAvali WHERE ((arrival BETWEEN @start AND @end) OR (depart BETWEEN @start AND @end) OR (@start >= arrival AND @end <= depart)))", conn);

            command.Parameters.AddWithValue("@start", arrive);
            command.Parameters.AddWithValue("@end", depart);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(reader["chalet_id"].ToString());
                }
            }

            conn.Close();


            return list;
        }

        public void addChaletBook(DateTime arrival, DateTime departure, int bookingid, int chaletid)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";


            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO ChaletAvali(booking_id, chalet_id, arrival, depart) VALUES(@bookingid, @chaletid, @start_date, @end_date)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@bookingid", SqlDbType.Int).Value = bookingid;
                    cmd.Parameters.Add("@chaletid", SqlDbType.VarChar, 50).Value = chaletid;
                    cmd.Parameters.Add("@start_date", SqlDbType.Date, 20).Value = arrival;
                    cmd.Parameters.Add("@end_date", SqlDbType.Date, 20).Value = departure;

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

        public void removeBooking(int bookingid)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE Bookings WHERE booking_id = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = bookingid;

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

        public void removeSingleGuest(int guestid)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE Guests WHERE guest_id = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = guestid;

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

        public void removeGuestsBooking(int bookingid)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE Guests WHERE booking_id = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = bookingid;

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

        public void removeCustomer(int customerid)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE Customers WHERE customer_id = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = customerid;

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

        public void removeCustomerBookings(int customerid)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE Bookings WHERE customer_id = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = customerid;

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

        public void removeChaletAvali(int bookingref)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE ChaletAvali WHERE booking_id = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = bookingref;

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

        public void removeCar(int bookingref)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE CarHire WHERE booking_id = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = bookingref;

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

        public void updateTotalGuests(int guests, int bookingref)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";


            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "UPDATE Bookings SET total_guests = @guests WHERE booking_id = @ref";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@guests", SqlDbType.Int).Value = guests;
                    cmd.Parameters.Add("@ref", SqlDbType.Int).Value = bookingref;

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

        public void updateCarDays(int days, int bookingid)
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|NapierHolidaysDB.mdf;Integrated Security=True;Connect Timeout=30";


            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "UPDATE Bookings SET car_days = @days WHERE booking_id = @ref";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // set up parameters
                    cmd.Parameters.Add("@days", SqlDbType.Int).Value = days;
                    cmd.Parameters.Add("@ref", SqlDbType.Int).Value = bookingid;

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
    }
}
