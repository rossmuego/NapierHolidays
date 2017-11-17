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
    /// Interaction logic for CarHire.xaml
    /// </summary>
    public partial class CarHire : Window
    {
        public CarHire()
        {
            InitializeComponent();
        }

        public Car hire = new Car();

        private void btn_addCar_Click(object sender, RoutedEventArgs e)
        {
            SelectedDatesCollection dates = cal_carDates.SelectedDates;

            hire.Name = txt_carName.Text;
            hire.Start = dates[0];
            hire.End = dates[dates.Count - 1];

            this.Close();
        }
    }
}
