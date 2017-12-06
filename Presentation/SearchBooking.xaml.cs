using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Buisness;
using System.Collections;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for SearchBooking.xaml
    /// </summary>
    public partial class SearchBooking : Window
    {
        public SearchBooking()
        {
            InitializeComponent();
        }

        public int bookingref;

        public SearchBooking(int refr)
        {
            bookingref = refr;

            BuisnessFacade searchbook = new BuisnessFacade();

            ArrayList results = searchbook.searchBooking(bookingref);

            Customer searchescust = (Customer)results[0];
            Booking searchedBooking = (Booking)results[1];
            List<Guest> searchedGuests = (List<Guest>)results[2];
            
            Window newwin = new BookingSearchResults(results);
            newwin.ShowDialog();
        }

        private void btn_seearchRefCont_Click(object sender, RoutedEventArgs e)
        {
            int value;
            if (txt_bookRefSearch.Text == "" || !int.TryParse(txt_bookRefSearch.Text, out value))
            {
                MessageBox.Show("Please enter a refrence number");
            }
            else
            {
                bookingref = Convert.ToInt32(txt_bookRefSearch.Text);
                BuisnessFacade searchbook = new BuisnessFacade();

                ArrayList results = searchbook.searchBooking(bookingref);

                Customer searchescust = (Customer)results[0];
                Booking searchedBooking = (Booking)results[1];
                List<Guest> searchedGuests = (List<Guest>)results[2];

                if(searchedBooking.BookingRef == 0)
                {
                    MessageBox.Show("Booking does not exist");
                    txt_bookRefSearch.Text = "";
                }
                else
                {
                    Window newwin = new BookingSearchResults(results);
                    newwin.ShowDialog();
                }
            }
        }

        private void btn_refCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
