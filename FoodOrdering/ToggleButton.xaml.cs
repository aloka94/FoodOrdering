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

namespace FoodOrdering
{
    /// <summary>
    /// Interaction logic for ToggleButton.xaml
    /// </summary>
    public partial class ToggleButton : UserControl
    {
        Thickness thicknessLeft = new Thickness(0, 0, 58, 0);
        Thickness thicknessRight = new Thickness(58, 0, 0, 0);
        SolidColorBrush brushOff = new SolidColorBrush(Color.FromRgb(160, 160, 160));
        SolidColorBrush brushOn = new SolidColorBrush(Color.FromRgb(100, 100, 250));
        bool toggled = false;

        public ToggleButton()
        {
            InitializeComponent();
            Back.Fill = brushOff;
            toggled = false;
            Dot.Margin = thicknessLeft;
        }

        public bool Toggled { get => toggled; set => toggled = value; }

        private void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!toggled) 
            {
                Back.Fill = brushOn;
                toggled = true;
                Dot.Margin = thicknessRight;

            }
            else
            {
                Back.Fill = brushOff;
                toggled = false;
                Dot.Margin = thicknessLeft;

            }
        }
    }
}
