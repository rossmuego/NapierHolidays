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
                SelectedDatesCollection Dates = selectDatePicker.SelectedDates;
                int breakfast;
                int evening;
                int customerref = cusr;

                if (check_bfast.IsChecked == true)
                {
                    breakfast = 1;
                }
                else
                {
                    breakfast = 0;
                }

                if (check_evening.IsChecked == true)
                {
                    evening = 1;
                }
                else
                {
                    evening = 0;
                }

                DateTime arrival = Dates[0];
                DateTime departure = Dates[Dates.Count - 1];

                int chaletid = Convert.ToInt32(cmb_ChaletId.Text);

                BuisnessFacade buisnessFacade = new BuisnessFacade();
                int totalGuests = currentGuests.Count;

                var result = MessageBox.Show("Are you sure you want to make this booking?", "Application Exit", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    buisnessFacade.addBooking(currentGuests, arrival, departure, breakfast, evening, customerref, totalGuests, carHire, chaletid);
                }
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

        private void selectDatePicker_LostFocus(object sender, RoutedEventArgs e)
        {

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
        }
    }
}
