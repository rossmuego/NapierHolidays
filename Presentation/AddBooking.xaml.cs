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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Buisness;
using System.Collections;
using System.Drawing.Text;
using System.Windows.Media;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for AddBooking.xaml
    /// </summary>
    public partial class AddBooking : Window
    {

        int cusr;
        List<Guest> currentGuests = new List<Guest>();
        Car carHire = new Car();
        public AddBooking(int customerref)
        {
            InitializeComponent();
            this.cusr = customerref;
            selectDatePicker.DisplayDateStart = DateTime.Today;
            selectDatePicker.BlackoutDates.AddDatesInPast();

            txt_guestName.Text = "Name";
            txt_guestName.Foreground = new SolidColorBrush(Colors.Gray);
            txt_guestPassport.Text = "Passport Number";
            txt_guestPassport.Foreground = new SolidColorBrush(Colors.Gray);
            txt_guestAge.Text = "Age";
            txt_guestAge.Foreground = new SolidColorBrush(Colors.Gray);         
        }

        private void btn_addGuest_Click(object sender, RoutedEventArgs e)
        {
            if (lst_guestView.Items.Count < 6)
            {
                try
                {
                    Guest newGuest = new Guest();

                    newGuest.Name = txt_guestName.Text;
                    newGuest.PassportNumber = txt_guestPassport.Text;
                    newGuest.Age = Convert.ToInt32(txt_guestAge.Text);
                    currentGuests.Add(newGuest);
                    lst_guestView.Items.Add(newGuest);

                    txt_guestAge.Text = "";
                    txt_guestName.Text = "";
                    txt_guestPassport.Text = "";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }   
            }
            else
            {
                MessageBox.Show("Can only have 6 guests");
            }
        }

        private void btn_createBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Booking newBooking = new Booking();

                SelectedDatesCollection Dates = selectDatePicker.SelectedDates;

                int customerref = cusr;

                newBooking.CustomerID = cusr;

                if (check_bfast.IsChecked == true)
                {
                    newBooking.Breakfast = true;
                }
                else
                {
                    newBooking.Breakfast = false;
                }

                if (check_evening.IsChecked == true)
                {
                    newBooking.Evening = true;
                }
                else
                {
                    newBooking.Evening = false;
                }

              
                newBooking.ArrivalDate = Dates[0];
                newBooking.DepartureDate = Dates[Dates.Count - 1];


                newBooking.Chalet = Convert.ToInt32(cmb_ChaletId.Text);

                BuisnessFacade buisnessFacade = new BuisnessFacade();

                newBooking.TotalGuests = currentGuests.Count;

                int bookingid = buisnessFacade.addBooking(newBooking, currentGuests, customerref, carHire);

                CostCalculator invoice = new CostCalculator();

                MessageBox.Show("Price of stay: £" + invoice.calculateCost(newBooking).ToString() + "\n \n \n" + "Booking refrence: " + bookingid);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btn_addCarHire_Click(object sender, RoutedEventArgs e)
        {
            SelectedDatesCollection Dates = selectDatePicker.SelectedDates;

            if (Dates.Count == 0)
            {
                MessageBox.Show("Please select holiday dates first");
            }
            else
            {
                DateTime start = Dates[0];
                DateTime end = Dates[Dates.Count - 1];

                CarHire carHireWin = new CarHire(start, end);
                carHireWin.ShowDialog();

                carHire = carHireWin.hire;
            }
        }

        private void selectDatePicker_MouseUp(object sender, MouseButtonEventArgs e)
        {
            cmb_ChaletId.Items.Clear();
            cmb_ChaletId.Items.Refresh();

            BuisnessFacade chalet = new BuisnessFacade();
            SelectedDatesCollection Dates = selectDatePicker.SelectedDates;
            DateTime start = Dates[0];
            DateTime end = Dates[Dates.Count - 1];

            ArrayList freechalets = chalet.chaletAvailible(start, end);

            foreach (string x in freechalets)
            {
                cmb_ChaletId.Items.Add(Convert.ToInt32(x));
            }

            cmb_ChaletId.SelectedIndex = 0;
        }

        private void txt_guestName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_guestName.Text == "Name")
            {
                txt_guestName.Text = "";
                txt_guestName.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void txt_guestName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_guestName.Text == "")
            {
                txt_guestName.Text = "Name";
                txt_guestName.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void txt_guestAge_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_guestAge.Text == "Age")
            {
                txt_guestAge.Text = "";
                txt_guestAge.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void txt_guestAge_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_guestAge.Text == "")
            {
                txt_guestAge.Text = "Age";
                txt_guestAge.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void txt_guestPassport_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_guestPassport.Text == "Passport Number")
            {
                txt_guestPassport.Text = "";
                txt_guestPassport.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void txt_guestPassport_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_guestPassport.Text == "")
            {
                txt_guestPassport.Text = "Passport Number";
                txt_guestPassport.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}
