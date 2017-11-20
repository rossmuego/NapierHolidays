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
        List<Guest> guests = new List<Guest>();
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

            guests = (List<Guest>)foundBooking[2];
            
            foreach(Guest y in guests)
            {
                lst_displayGuests.Items.Add(y);
            }
        }

        private void lst_displayGuests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Guest selected = new Guest();

            selected = (Guest)lst_displayGuests.SelectedItem;

            txt_guestName.Text = selected.Name;
            txt_guestPP.Text = selected.PassportNumber;
            txt_guestAge.Text = selected.Age.ToString();


        }
    }
}
