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

        public static int customerRef;

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
            Window nextStage = new AddBooking();
            nextStage.ShowDialog();

        }


        private void txt_refSearch_KeyUp(object sender, KeyEventArgs e)
        {
            int refsearch = Convert.ToInt32(txt_refSearch.Text);

            BuisnessFacade search = new BuisnessFacade();

            Customer found = search.SearchCustomer(refsearch);

            lst_refrenceSearch.Items.Add(found);

        }

        private void lst_refrenceSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
          
        }

        private void btn_newCustomer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window newwin = new NewCustomer();
            newwin.ShowDialog();


        }
    }
}
