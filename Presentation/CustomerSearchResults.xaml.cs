using System;
using System.Collections;
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

namespace Presentation
{
    /// <summary>
    /// Interaction logic for CustomerSearchResults.xaml
    /// </summary>
    public partial class CustomerSearchResults : Window
    {
        ArrayList customerResults = new ArrayList();
        public CustomerSearchResults(ArrayList x)
        {
            InitializeComponent();
            customerResults = x;

            Customer found = (Customer)customerResults[0];

            txt_customerRefNum.Text = found.CustomerRef.ToString();
            txt_customerName.Text = found.Name;
            txt_customerAddress.Text = found.Address;

            List<Booking> bookings = (List<Booking>)customerResults[1];
            txt_totalBookings.Text = bookings.Count.ToString();

            foreach (Booking y in bookings)
            {
                lst_customerBooks.Items.Add(y.BookingRef);
            }
        }

        private void lst_customerBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int bookingref = Convert.ToInt32(lst_customerBooks.SelectedValue.ToString());
            List<Booking> booking = (List<Booking>)customerResults[1];

            foreach (Booking z in booking)
            {
                if(z.BookingRef == bookingref)
                {
                    CostCalculator cost = new CostCalculator();

                    txt_arrivalDate.Text = z.ArrivalDate.ToLongDateString();
                    txt_departureDate.Text = z.DepartureDate.ToLongDateString();
                    txt_bookingChalet.Text = z.Chalet.ToString();
                    txt_totalGuests.Text = z.TotalGuests.ToString();
                    txt_bookingCost.Text = cost.calculateCost(z).ToString();

                    if(z.Breakfast == true)
                    {
                        chk_breakfast.IsChecked = true;
                    }
                    else
                    {
                        chk_breakfast.IsChecked = false;
                    }

                    if (z.Evening == true)
                    {
                        chk_evening.IsChecked = true;
                    }
                    else
                    {
                        chk_evening.IsChecked = false;
                    }

                    if (z.Car == 0)
                    {
                        chk_car.IsChecked = false;
                    }
                    else
                    {
                        chk_car.IsChecked = true;
                    }
                }
            }

        }

        private void btn_updateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (txt_customerRefNum.Text != "")
            {
                int custref = Convert.ToInt32(txt_customerRefNum.Text);

                BuisnessFacade update = new BuisnessFacade();
                update.updateCustomer(custref, txt_customerName.Text, txt_customerAddress.Text);

                MessageBox.Show("Customer Updated");
            }
        }
    }
}
