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
using Data;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for AddGuest.xaml
    /// </summary>
    public partial class AddGuest : Window
    {
        int id;
        Guest newg = new Guest();

        public Guest guest
        {
            get
            {
                return newg;
            }
        }

        public AddGuest(int bookingid)
        {
            InitializeComponent();
            id = bookingid;
        }

        private void btn_addGuest_Click(object sender, RoutedEventArgs e)
        {
            newg.Name = txt_GuestName.Text;
            newg.PassportNumber = txt_guestPP.Text;
            newg.Age = Convert.ToInt32(txt_guestAge.Text);

            BuisnessFacade addguest = new BuisnessFacade();
            addguest.addGuest(newg, id);

            this.Close();
        }
    }
}
