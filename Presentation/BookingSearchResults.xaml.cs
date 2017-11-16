using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Buisness;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for BookingSearchResults.xaml
    /// </summary>
    public partial class BookingSearchResults : Window
    {
        ArrayList foundBooking;
        public BookingSearchResults(ArrayList x)
        {
            InitializeComponent();
            foundBooking = x;
            Customer customer = (Customer)foundBooking[0];
            txt_resultsID.Text = customer.CustomerRef.ToString();
            txt_resultsName.Text = customer.Name;
            txt_resultsAddress.Text = customer.Address;

            Booking booking = (Booking)foundBooking[1];
            txt_resultsArrival.Text = booking.ArrivalDate.ToLongDateString();
            txt_resultsDepart.Text = booking.DepartureDate.ToLongDateString();

            if(booking.Breakfast == true)
            {
                check_breakfast.IsChecked = true;
            }
            else
            {
                check_breakfast.IsChecked = false;
            }

            if (booking.Evening == true)
            {
                check_evening.IsChecked = true;
            }
            else
            {
                check_evening.IsChecked = false;
            }

            if (booking.Car == true)
            {
                check__car.IsChecked = true;
            }
            else
            {
                check__car.IsChecked = false;
            }

            List<Guest> guests = (List<Guest>)foundBooking[2];
            
            txt_totalGuest.Text = guests.Count().ToString();
        }
    }
}
