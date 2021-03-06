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
    /// Interaction logic for NewCustomer.xaml
    /// </summary>
    public partial class NewCustomer : Window
    {
        public NewCustomer()
        {
            InitializeComponent();
        }

        private void btn_addNewCust_Click(object sender, RoutedEventArgs e)
        {
            if (txt_newCustName.Text == "" || txt_newCustAddress.Text == "")
            {
                MessageBox.Show("Please enter a name and an address");
            }
            else
            {
                RefGeneratorSingleton generator = RefGeneratorSingleton.Generator;

                int customerRef = generator.generateCustomerRef();
                string name = txt_newCustName.Text;
                string address = txt_newCustAddress.Text;

                BuisnessFacade addcust = new BuisnessFacade();

                addcust.addCustomer(customerRef, name, address);

                this.Close();
                Window newwin = new AddBooking(customerRef);
                newwin.ShowDialog();
            }
        }
    }
}
