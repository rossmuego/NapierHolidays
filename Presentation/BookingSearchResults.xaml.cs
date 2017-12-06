using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
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
        int bookingid;
        public BookingSearchResults(ArrayList x)
        {
            InitializeComponent();
            foundBooking = x;

            if (foundBooking.Count == 0)
            {
                MessageBox.Show("Booking does not exist");
            }
            else
            {
                CostCalculator calc = new CostCalculator();

                Customer customer = (Customer)foundBooking[0];
                txt_resultsID.Text = customer.CustomerRef.ToString();
                txt_resultsName.Text = customer.Name;
                txt_resultsAddress.Text = customer.Address;
                Booking booking = (Booking)foundBooking[1];
                bookingid = booking.BookingRef;
                dtp_arrival.DisplayDateStart = DateTime.Today;
                dtp_arrival.SelectedDate = booking.ArrivalDate;

                dtp_departure.DisplayDateStart = booking.ArrivalDate;
                dtp_departure.SelectedDate = booking.DepartureDate;

                dt_carArrival.DisplayDateStart = dtp_arrival.SelectedDate;
                dt_carArrival.DisplayDateEnd = dtp_departure.SelectedDate;

                dt_carEnd.DisplayDateStart = dtp_arrival.SelectedDate;
                dt_carEnd.DisplayDateEnd = dtp_departure.SelectedDate;

                txt_totalCost.Text = "£" + calc.calculateCost(booking).ToString();

                cmb_chalets.Items.Add(booking.Chalet);
                cmb_chalets.SelectedIndex = 0;

                if (booking.Breakfast == true)
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

                foreach (Guest y in guests)
                {
                    lst_displayGuests.Items.Add(y);
                }


                if(foundBooking.Count == 3)
                {
                    btn_addCar.Visibility = Visibility.Visible;
                }
                else
                {
                    btn_addCar.Visibility = Visibility.Hidden;
                }




                try
                {
                    Car carhire = (Car)foundBooking[3];

                    txt_namedDriver.Text = carhire.Name;
                    dt_carArrival.Text = carhire.Start.ToLongDateString();
                    dt_carEnd.Text = carhire.End.ToLongDateString();
                }
                catch
                {
                    return;
                }
                
            }
        }

        private void lst_displayGuests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            Guest selected = (Guest)lst_displayGuests.SelectedItem;

            txt_guestName.Text = selected.Name;
            txt_guestPP.Text = selected.PassportNumber;
            txt_guestAge.Text = selected.Age.ToString();
        }

        private void btn_guestUpdate_Click(object sender, RoutedEventArgs e)
        {
            Guest selected = (Guest)lst_displayGuests.SelectedItem;

            if (selected.GuestID.ToString() != "")
            {
                int guestUpdateID = selected.GuestID;

                BuisnessFacade update = new BuisnessFacade();
                update.updateGuest(guestUpdateID, txt_guestName.Text, txt_guestPP.Text, Convert.ToInt32(txt_guestAge.Text));

                foreach(Guest x in guests)
                {
                    if(x.GuestID == guestUpdateID)
                    {
                        x.Name = txt_guestName.Text;
                        x.PassportNumber = txt_guestPP.Text;
                        x.Age = Convert.ToInt32(txt_guestAge.Text);
                        lst_displayGuests.Items.Refresh();
                    }
                }

                MessageBox.Show("Guest Updated");
            }
        }

        private void btn_viewCustomer_Click(object sender, RoutedEventArgs e)
        {
            int custref = Convert.ToInt32(txt_resultsID.Text);

            Window newwin = new SearchCustomerRef(custref);
        }

        private void btn_checkAvailibility_Click(object sender, RoutedEventArgs e)
        {
            int current = Convert.ToInt32(cmb_chalets.SelectedItem.ToString());

            cmb_chalets.Items.Clear();

            DateTime arrival = dtp_arrival.SelectedDate.Value;
            DateTime departure = dtp_departure.SelectedDate.Value;

            BuisnessFacade check = new BuisnessFacade();

            ArrayList dates = check.chaletAvailible(arrival, departure);

            foreach(string x in dates)
            {
                cmb_chalets.Items.Add(x);
            }
        }

        private void btn_updateBooking_Click(object sender, RoutedEventArgs e)
        {
            int bfast;
            int evening;

            DateTime arrival = Convert.ToDateTime(dtp_arrival.SelectedDate);
            DateTime departure = Convert.ToDateTime(dtp_departure.SelectedDate);
            int chaletid = Convert.ToInt32(cmb_chalets.SelectedValue.ToString());
            int totalguests = lst_displayGuests.Items.Count;
            if(check_breakfast.IsChecked == true)
            {
                bfast = 1;
            }
            else
            {
                bfast = 0;
            }

            if(check_evening.IsChecked == true)
            {
                evening = 1;
            }
            else
            {
                evening = 0;
            }

            BuisnessFacade update = new BuisnessFacade();

            update.updateBooking(bookingid, arrival, departure, bfast, evening, chaletid, totalguests);

            CostCalculator updatecost = new CostCalculator();

            txt_totalCost.Text = "£" + updatecost.calculateCost((Booking)update.searchBooking(bookingid)[1]).ToString();
        }

        private void btn_deleteGuests_Click(object sender, RoutedEventArgs e)
        {
            BuisnessFacade remove = new BuisnessFacade();

            Guest removeguest = (Guest)lst_displayGuests.SelectedItem;
            CostCalculator updatecost = new CostCalculator();

            int guestid = removeguest.GuestID;

            remove.removeGuest(guestid, guests.Count - 1, bookingid);

            txt_totalCost.Text = "£" + updatecost.calculateCost((Booking)remove.searchBooking(bookingid)[1]).ToString();

            List<Guest> guestCopy = guests;

            foreach (Guest x in guestCopy)
            {
                if (x.GuestID == guestid)
                {
                    if (lst_displayGuests.Items.Count == 1)
                    {
                        MessageBox.Show("Cannot remove all guests");
                    }
                    else
                    {
                        if (lst_displayGuests.SelectedIndex < lst_displayGuests.Items.Count - 1)
                        {
                            lst_displayGuests.SelectedIndex = lst_displayGuests.SelectedIndex + 1;
                        }
                        else if (lst_displayGuests.SelectedIndex > 0)
                        {
                            lst_displayGuests.SelectedIndex = lst_displayGuests.SelectedIndex - 1;
                        }
                        guests.Remove(x);
                        lst_displayGuests.Items.Remove(x);
                        lst_displayGuests.Items.Refresh();
                        break;
                    }
                }
            }
        }

        private void dt_carArrival_CalendarOpened(object sender, RoutedEventArgs e)
        {
            dt_carArrival.DisplayDateStart = dtp_arrival.SelectedDate;
            dt_carArrival.DisplayDateEnd = dtp_departure.SelectedDate;
        }

        private void dt_carEnd_CalendarOpened(object sender, RoutedEventArgs e)
        {
            dt_carEnd.DisplayDateStart = dtp_arrival.SelectedDate;
            dt_carEnd.DisplayDateEnd = dtp_departure.SelectedDate;
        }

        private void btn_deleteBooking_Click(object sender, RoutedEventArgs e)
        {
            BuisnessFacade remove = new BuisnessFacade();

            remove.removeBooking(bookingid);
            this.Close();
        }

        private void btn_generateInvoice_Click(object sender, RoutedEventArgs e)
        {
            Window gen = new Invoice(foundBooking);
            gen.ShowDialog();
        }

        private void btn_addNewGuest_Click(object sender, RoutedEventArgs e)
        {
            if (guests.Count < 6)
            {
                BuisnessFacade update = new BuisnessFacade();
                CostCalculator updatecost = new CostCalculator();

                AddGuest newwin = new AddGuest(bookingid, guests.Count + 1);
                newwin.ShowDialog();

                txt_totalCost.Text = "£" + updatecost.calculateCost((Booking)update.searchBooking(bookingid)[1]).ToString();

                guests.Add(newwin.guest);
                lst_displayGuests.Items.Add(newwin.guest);
                lst_displayGuests.Items.Refresh();            
            }
            else
            {
                MessageBox.Show("You are only allowed 6 guests!");
            }
        }

        private void btn_addCar_Click(object sender, RoutedEventArgs e)
        {
            CarHire add = new CarHire(Convert.ToDateTime(dtp_arrival.SelectedDate), Convert.ToDateTime(dtp_departure.SelectedDate));
            add.ShowDialog();

            BuisnessFacade addcar = new BuisnessFacade();

            addcar.addCar(add.hire, bookingid);
        }

        private void btn_updateCar_Click(object sender, RoutedEventArgs e)
        {
            if (txt_namedDriver.Text != "" && dt_carArrival.Text != "" && dt_carEnd.Text != "")
            {

                Car hire = new Car();

                hire.Name = txt_namedDriver.Text;
                hire.Start = Convert.ToDateTime(dt_carArrival.Text);
                hire.End = Convert.ToDateTime(dt_carEnd.Text);

                BuisnessFacade car = new BuisnessFacade();
                CostCalculator updatecost = new CostCalculator();

                car.updateCar(hire, bookingid);

                txt_totalCost.Text = "£" + updatecost.calculateCost((Booking)car.searchBooking(bookingid)[1]).ToString();

            }
            else
            {
                MessageBox.Show("please enter all details");
            }
        }
    }
}
