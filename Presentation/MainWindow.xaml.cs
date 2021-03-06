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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_addBooking_Click(object sender, RoutedEventArgs e)
        {
            Window addBooking = new SelectCustomer();
            addBooking.Show();
        }

        private void btn_searchBooking_Click(object sender, RoutedEventArgs e)
        {
            Window searchBooking = new SearchBooking();
            searchBooking.ShowDialog();
        }

        private void btn_searchCustomer_Click(object sender, RoutedEventArgs e)
        {
            Window searchCust = new SearchCustomerRef();
            searchCust.ShowDialog();
        }
    }
}
