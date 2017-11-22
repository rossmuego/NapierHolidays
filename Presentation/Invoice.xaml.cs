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
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class Invoice : Window
    {
        public Invoice(ArrayList foundBooking)
        {
            InitializeComponent();


            Customer customer = (Customer)foundBooking[0];

            lblCustomerName.Content = customer.Name;
            lbl_customerAddress.Content = customer.Address;
            lbl_customerRef.Content = customer.CustomerRef;

            Booking booking = (Booking)foundBooking[1];

            lbl_bookingNum.Content = "Booking #" + booking.BookingRef;

            DateTime arrival = booking.ArrivalDate;
            DateTime depart = booking.DepartureDate;
            int totaldays = (depart - arrival).Days + 1;
            int nights = totaldays - 1;
            int extras = 0;

            lbl_checkIn.Content = booking.ArrivalDate.ToLongDateString();
            lbl_checkOut.Content = booking.DepartureDate.ToLongDateString();
            lbl_chalet_id.Content = booking.Chalet;
            

            if (booking.Breakfast == true)
            {
                check_breakfast.IsChecked = true;
                lbl_bfastCost.Content = "£" + 5 * totaldays;
                extras += totaldays * 5;
            }
            else
            {
                check_breakfast.IsChecked = false;
                lbl_bfastCost.Content = "£0";
            }

            if (booking.Evening == true)
            {
                check_evening.IsChecked = true;
                lbl_eveningCost.Content = "£" + 10 * nights;
                extras += nights * 10;
            }
            else
            {
                check_evening.IsChecked = false;
                lbl_eveningCost.Content = "£0";

            }

            List<Guest> guests = (List<Guest>)foundBooking[2];

            foreach (Guest y in guests)
            {
                lst_guests.Items.Add(y);
            }

            if (foundBooking.Count != 3)
            {
                Car carhire = (Car)foundBooking[3];

                if (booking.Car > 0)
                {
                    check_car.IsChecked = true;
                    lbl_hireEnd.Visibility = Visibility.Visible;
                    lbl_hireName.Visibility = Visibility.Visible;
                    lbl_hireStart.Visibility = Visibility.Visible;

                    lbl_hireNameResult.Visibility = Visibility.Visible;
                    lbl_hireNameResult.Content = carhire.Name;

                    lbl_hireStartResult.Visibility = Visibility.Visible;
                    lbl_hireStartResult.Content = carhire.Start;

                    lbl_hireEndResult.Visibility = Visibility.Visible;
                    lbl_hireEndResult.Content = carhire.End;

                    lbl_carCost.Content = "£" + (((carhire.End - carhire.Start).Days + 1) * 50);

                    extras += ((carhire.End - carhire.Start).Days + 1) * 50;
                }
            }
   
            CostCalculator cost = new CostCalculator();

            lbl_totalCost.Content = "£" + cost.calculateCost(booking);

            lbl_totalExtras.Content = "£" + extras;

            lbl_costpernight.Content = "£" + (((nights * 60) + (guests.Count * 25 * nights)) / nights) + " (£" + ((nights * 60) + (guests.Count * 25 * nights)) + " total)";

        }
    }
}
