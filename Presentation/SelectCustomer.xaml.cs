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
using System.Data;
using System.Web;
using System.Collections;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for SelectCustomer.xaml
    /// </summary>
    public partial class SelectCustomer : Window
    {
        public SelectCustomer()
        {
            InitializeComponent();
        }

        int customerRef;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txt_refSearch.Text == "" || lst_searchedRef.SelectedItem == null)
            {
                MessageBox.Show("Please select a customer");
            }
            else
            {
                try
                {
                    Customer selected = new Customer();
                    selected = (Customer)lst_searchedRef.SelectedValue;
                    customerRef = selected.CustomerRef;
                    this.Close();
                    Window nextStage = new AddBooking(customerRef);
                    nextStage.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void txt_refSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if(txt_refSearch.Text != "")
            {
                try
                {
                    int refsearch = Convert.ToInt32(txt_refSearch.Text);

                    BuisnessFacade search = new BuisnessFacade();

                    List<Customer> found = search.SearchCustomerList(refsearch);

                    foreach (Customer x in found)
                    {
                        lst_searchedRef.Items.Add(x);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                lst_searchedRef.Items.Clear();
                lst_searchedRef.Items.Refresh();
            }
            
            
        }

        private void btn_newCustomer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window newwin = new NewCustomer();
            newwin.ShowDialog();
        }


        private void lst_searchedRef_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BuisnessFacade fc = new BuisnessFacade();

            Customer current = fc.SearchCustomer(Convert.ToInt32(lst_searchedRef.SelectedValue.ToString()));

        }
    }
}
