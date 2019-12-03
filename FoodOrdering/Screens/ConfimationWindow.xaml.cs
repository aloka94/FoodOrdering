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

namespace FoodOrdering.Screens
{
    /// <summary>
    /// Interaction logic for ConfimationWindow.xaml
    /// </summary>
    public partial class ConfimationWindow : Window
    {
        public ConfimationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var customerMainWindow = new CustomerMainWindow();
            customerMainWindow.Window_Loaded(sender,e);
            customerMainWindow.Spl_orders.Visibility = Visibility.Collapsed;
            Visibility = Visibility.Collapsed;
            
        }
    }
}
