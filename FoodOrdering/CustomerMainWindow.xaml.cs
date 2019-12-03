using FoodOrdering.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
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
    /// Interaction logic for CustomerMainWindow.xaml
    /// </summary>
    public partial class CustomerMainWindow : Window
    {
        ObservableCollection<FoodItem> foodItems;
        ObservableCollection<OrderItem> orderItems;
        bool storeData;
        string category = "Appetizers";
        string foodType = "All";
        Random rnd = new Random();
        string filter;
        Order order;
        string prevButton="Btn_cat1";
               


        public CustomerMainWindow()
        {
            InitializeComponent();

        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            orderItems = new ObservableCollection<OrderItem>();
            order = new Order();
            order.orderItems = orderItems;
            Spl_orders.Visibility = Visibility.Collapsed;
            foodItems = Storage.ReadXml<ObservableCollection<FoodItem>>("FoodItems.xml");
            if (foodItems == null)
            {
                foodItems = GenerateFoodItems(2);
            }
            var food=from f in foodItems where f.category == category where f.isAvailable == true select f;
            Lbx_fooditems.ItemsSource = food;
            Rbn_All.IsChecked = true;
            Tbk_categoryHeading.Text = category;
        }



        private ObservableCollection<FoodItem> GenerateFoodItems(int cnt)
        {
            var lst = new ObservableCollection<FoodItem>();
            var addOn = new List<AddOn>();
            addOn.Add(new AddOn { addOnId = 1, addOnName = "Sauce", addOnPrice = 1.32f });
            for (int i = 0; i < cnt; i++)
            {
                lst.Add(new FoodItem { foodId = i, description = "bla bla", foodType = "veg", quantity = i, price = 1.45M, isAvailable = true, name = "food item" + i, category = "Appetizers", addOns = addOn });
            }

            return lst;
        }

        
        private void Tbx_info_MouseEnter(object sender, MouseEventArgs e)
        {
            var win = new DescriptionPopUp();
            win.Tbk_name.Text = (Lbx_fooditems.SelectedItem as FoodItem).name;
            win.Tbk_description.Text = (Lbx_fooditems.SelectedItem as FoodItem).description;
            win.Owner = this;
            win.Show();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Btn_category = sender as Button;
            category = ((string)Btn_category.Content);
            Btn_category.Background = Brushes.Ivory;
            Btn_category.Foreground = Brushes.Black;
            if (prevButton.Contains("1"))
            {
                Btn_cat1.Background = Brushes.Crimson;
                Btn_cat1.Foreground = Brushes.GhostWhite;
            }
            else if (prevButton.Contains("2"))
            {
                Btn_cat2.Background = Brushes.Crimson;
                Btn_cat2.Foreground = Brushes.GhostWhite;
            }
            else if (prevButton.Contains("3"))
            {
                Btn_cat3.Background = Brushes.Crimson;
                Btn_cat3.Foreground = Brushes.GhostWhite;
            }
            else if (prevButton.Contains("4"))
            {
                Btn_cat4.Background = Brushes.Crimson;
                Btn_cat4.Foreground = Brushes.GhostWhite;
            }
            else if (prevButton.Contains("5"))
            {
                Btn_cat5.Background = Brushes.Crimson;
                Btn_cat5.Foreground = Brushes.GhostWhite;
            }
            prevButton = Btn_category.Name;

            if (!foodType.Equals("All"))
            {
                var foodPerCategory = from f in foodItems where f.category == category where f.isAvailable == true where f.foodType==foodType select f;
                Lbx_fooditems.ItemsSource = foodPerCategory;
            }
            else
            {
                var foodPerCategory = from f in foodItems where f.category == category where f.isAvailable == true select f;
                Lbx_fooditems.ItemsSource = foodPerCategory;
            }
            Tbk_categoryHeading.Text = category;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var RdBtn_type = sender as RadioButton;
            foodType = (string)RdBtn_type.Content;
            if (!foodType.Equals("All"))
            {
                var foodPerCategory = from f in foodItems where f.category == category where f.isAvailable == true where f.foodType == foodType select f;
                Lbx_fooditems.ItemsSource = foodPerCategory;
            }
            else
            {
                var foodPerCategory = from f in foodItems where f.category == category where f.isAvailable == true select f;
                Lbx_fooditems.ItemsSource = foodPerCategory;
            }


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (storeData)
            {
                Storage.WriteXml<ObservableCollection<FoodItem>>(foodItems, "FoodItems.xml");

            }
        }

        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            Spl_orders.Visibility = Visibility.Visible;
            Grid_main.Margin = new Thickness(0, 0, 355, 0);
            var btn_add = sender as Button;
            FoodItem selectedItem = (from food in foodItems where food.name == btn_add.Tag select food).FirstOrDefault();
            selectedItem.visibility = "Visible";
            selectedItem.visibilityAdd = "Collapsed";
            OrderItem orderItem;
            orderItem = new OrderItem { item = new FoodItem() };
            orderItem.item.name = selectedItem.name;
            orderItem.itemQuantity += 1;
            orderItem.itemTotal = (selectedItem.price * orderItem.itemQuantity);
            orderItems.Add(orderItem);
            Tbx_total.Text = getTotalPrice().ToString();
            Lbx_Orderitems.ItemsSource = orderItems;
            Lbx_Orderitems.ScrollIntoView(orderItems.LastOrDefault());
            Lbx_fooditems.ScrollIntoView(selectedItem);
            Lbx_fooditems.SelectedItem = selectedItem;

        }

        private void Btn_minus2_Click(object sender, RoutedEventArgs e)
        {
            var btn_add = sender as Button;
            FoodItem selectedItem = (from food in foodItems where food.name == btn_add.Tag select food).FirstOrDefault();
            OrderItem existingOrder = (from f in orderItems where f.item.name == selectedItem.name select f).FirstOrDefault();
            if (existingOrder.itemQuantity > 1)
            {
                existingOrder.itemQuantity -= 1;
                existingOrder.itemTotal = (selectedItem.price * existingOrder.itemQuantity);
                Tbx_total.Text = getTotalPrice().ToString();
            }
            else if (existingOrder.itemQuantity == 1)
            {
                selectedItem.visibility = "Collapsed";
                selectedItem.visibilityAdd = "Visible";
                orderItems.Remove(existingOrder);
                Tbx_total.Text =getTotalPrice().ToString();

                
            }
            if (orderItems.Count == 0)
            {
                Spl_orders.Visibility = Visibility.Collapsed;
                Grid_main.Margin = new Thickness(0, 0, 0, 0);

            }


            Lbx_Orderitems.ItemsSource = orderItems;

        }

        private void Btn_plus2_Click(object sender, RoutedEventArgs e)
        {
            var btn_add = sender as Button;
            FoodItem selectedItem = (from food in foodItems where food.name == btn_add.Tag select food).FirstOrDefault();
            OrderItem existingOrder =(from f in orderItems where f.item.name == selectedItem.name select f).FirstOrDefault();
           
            existingOrder.itemQuantity+=1;
            existingOrder.itemTotal = (selectedItem.price * existingOrder.itemQuantity);
            Tbx_total.Text = getTotalPrice().ToString();
            Lbx_Orderitems.ItemsSource = orderItems;

        }



        private decimal getTotalPrice()
        {
            decimal totalPrice=0;
            totalPrice=(decimal)(from orders in orderItems select orders.itemTotal).Sum();
            order.totalPrice=totalPrice;
            return totalPrice;
        }

        private void Tbx_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            filter = Tbx_search.Text.ToLower();
            if (filter == "")
            {
                if (!foodType.Equals("All"))
                {
                    var food = from f in foodItems where f.category == category where f.foodType == foodType where f.isAvailable == true select f;
                    Lbx_fooditems.ItemsSource = food;
                }
                else
                {
                    var food = from f in foodItems where f.category == category  where f.isAvailable == true select f;
                    Lbx_fooditems.ItemsSource = food;
                }
                Tbk_categoryHeading.Text=category;
            }
            else
            {
                if (!foodType.Equals("All"))
                {
                    var results = from f in foodItems where f.name.ToLower().Contains(filter) where f.foodType == foodType where f.isAvailable == true select f;
                    Lbx_fooditems.ItemsSource = results;
                    Tbk_categoryHeading.Text = "Search results for \" " + filter + " \"  in " + foodType + " - " + Lbx_fooditems.Items.Count + " results";

                }
                else {
                    var results = from f in foodItems where f.name.ToLower().Contains(filter) where f.isAvailable == true select f;
                    Lbx_fooditems.ItemsSource = results;
                    Tbk_categoryHeading.Text = "Search results for \" " + filter + " \" - " + Lbx_fooditems.Items.Count + " results";
                }
            }
        }

        private void Btn_confirm_Click(object sender, RoutedEventArgs e)
        {
            var win = new ConfimationWindow();
            int randomValue=rnd.Next(0,500);
            order.orderId = randomValue;
            order.orderItems = orderItems;
            win.Rn_orderId.Text = order.orderId.ToString();
            win.Rn_price.Text = getTotalPrice().ToString();
            win.Owner = this;
            win.Show();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var win = new MainWindow();
            win.Owner = this;
            win.Show();
            Visibility = Visibility.Hidden;
        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
           MessageBoxResult selection= MessageBox.Show("Are you sure you want to cancel your order?", "Confirmation",MessageBoxButton.YesNo);
            if (selection == MessageBoxResult.Yes)
            {
                orderItems.Clear();
                Spl_orders.Visibility = Visibility.Collapsed;
                Grid_main.Margin = new Thickness(0, 0, 0, 0);
                Window_Loaded(sender,e);
            }

        }

      
    }
}