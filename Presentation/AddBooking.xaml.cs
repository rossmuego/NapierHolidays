﻿using System;
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
    /// Interaction logic for AddBooking.xaml
    /// </summary>
    public partial class AddBooking : Window
    {

        List<Guest> currentBooking = new List<Guest>();

        public AddBooking()
        {
            InitializeComponent();
        }

        private void btn_addGuest_Click(object sender, RoutedEventArgs e)
        {
            if (lst_guestView.Items.Count < 6)
            {
                Guest newGuest = new Guest();

                newGuest.Name = txt_guestName.Text;
                newGuest.PassportNumber = txt_guestPassport.Text;
                newGuest.Age = Convert.ToInt32(txt_guestAge.Text);

                currentBooking.Add(newGuest);
                lst_guestView.Items.Add(newGuest);

            }
            else
            {
                MessageBox.Show("Can only have 6 guests");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            BuisnessFacade buisnessFacade = new Buisness.BuisnessFacade();


            buisnessFacade.addBooking(currentBooking);
        }
    }
}
