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
    /// Interaction logic for SearchCustomerRef.xaml
    /// </summary>
    public partial class SearchCustomerRef : Window
    {
        public SearchCustomerRef()
        {
            InitializeComponent();
        }

        private void btn_custSerch_Click(object sender, RoutedEventArgs e)
        {
            int customerref = Convert.ToInt32(txt_customerRefSearch.Text);
            BuisnessFacade searchcust = new BuisnessFacade();
            ArrayList results = searchcust.searchCustomerBookings(customerref);
            this.Close();
            Window newwin = new CustomerSearchResults(results);
            newwin.ShowDialog();
        }
    }
}
