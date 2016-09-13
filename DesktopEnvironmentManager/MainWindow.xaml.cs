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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopEnvironmentManager
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

        //declaring variables

        
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // getting the values 
            var productTextBox = int.Parse(ProductText.Text);
            var priceTextBox = int.Parse(PriceText.Text);

            //show the value 
            // MessageBox.Show("This is your total: " + (priceTextBox * productTextBox));
            MessageBox.Show("amount calculated " );

            this.TotalTextBox.Text = "Your total:" + (priceTextBox * productTextBox);

        }
    }
}
