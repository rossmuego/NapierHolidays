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

        private void btn_cancelSearch_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_searchRefCont_Click(object sender, RoutedEventArgs e)
        {
            int bookingref = Convert.ToInt32(txt_bookRefSearch.Text);
            BuisnessFacade searchbook = new BuisnessFacade();

            ArrayList results = searchbook.searchBooking(bookingref);

            Customer searchescust = (Customer)results[0];
            Console.WriteLine(searchescust.Name);
            Booking searchedBooking = (Booking)results[1];
            Console.WriteLine(searchedBooking.Car);
            List<Guest> searchedGuests = (List<Guest>)results[2];
            foreach(Guest i in searchedGuests)
            {
                Console.WriteLine(i.GuestID);
            }

            Window newwin = new BookingSearchResults(results);
            newwin.ShowDialog();
        }
    }
}
