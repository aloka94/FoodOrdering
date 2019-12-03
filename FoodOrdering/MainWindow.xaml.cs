using FoodOrdering.Classes;
using FoodOrdering.Screens;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<FoodItem> foodItems;
        private bool storeData;
        string filter = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_home_Click(object sender, RoutedEventArgs e)
        {
            Storage.WriteXml<ObservableCollection<FoodItem>>(foodItems, "FoodItems.xml");
            var win = new CustomerMainWindow();
            win.Owner = this;
            win.Show();
            Visibility = Visibility.Hidden;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foodItems = Storage.ReadXml<ObservableCollection<FoodItem>>("FoodItems.xml");
            if (foodItems == null)
            {
                foodItems = GenerateFoodItems(2);
            }

            Lbx_fooditems.ItemsSource=foodItems;
        }

        
        private ObservableCollection<FoodItem> GenerateFoodItems(int cnt)
        {
            var lst = new ObservableCollection<FoodItem>();
            var addOn = new List<AddOn>();
            addOn.Add(new AddOn { addOnId = 1, addOnName = "Sauce", addOnPrice = 1.32f });
            for (int i = 0; i < cnt; i++)
            {
                lst.Add(new FoodItem { foodId = i, description = "bla bla", foodType = "veg", quantity = i, price = 1.45M, isAvailable = true, name = "food item" + i, category ="Appetizers", addOns = addOn });
            }

            return lst;
        }

        private void Btn_Image_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                var btn = sender as Button;
                string newFileName= System.IO.Path.GetFullPath(System.IO.Path.Combine(@"..\..\Food Images\" , System.IO.Path.GetFileName(op.FileName)));
                File.Copy(op.FileName, newFileName, true);
                FoodItem selectedItem=(FoodItem)Lbx_fooditems.SelectedItem as FoodItem;
                selectedItem.imagePath = newFileName;

            }
            storeData = true;
        }

        private void Tbx_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            filter = Tbx_filter.Text.ToLower();
            if (filter == "")
            {
                Lbx_fooditems.ItemsSource = foodItems;
            }
            else
            {
                var results = from s in foodItems where s.name.ToLower().Contains(filter) select s;
                Lbx_fooditems.ItemsSource = results;

            }
        }

        private void Tbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            storeData = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (storeData)
            {
                Storage.WriteXml<ObservableCollection<FoodItem>>(foodItems, "FoodItems.xml");
               
            }
        }

        private void Cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            storeData = true;
        }

        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            var item = new FoodItem() { visibility="Collapsed", visibilityAdd="Visible", isAvailable=true, foodType= "Non-Veg", quantity=1,imagePath= "pack://application:,,,/Food Images/upload_image.jpg", category="Main Menu"};
            foodItems.Add(item);
            Lbx_fooditems.SelectedItem = item;
            Lbx_fooditems.ScrollIntoView(item);
        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            if (Lbx_fooditems.SelectedItem == null)
            {
                MessageBox.Show("Please select an item to be deleted first!", "Error");
                return;
            }

            foodItems.Remove(Lbx_fooditems.SelectedItem as FoodItem);
            Tbx_filter.Text = "";
        }

        private void Lbx_fooditems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Lbx_fooditems.Items.Refresh();
        }
    }
}
