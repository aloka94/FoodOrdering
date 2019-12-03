using FoodOrdering.Classes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FoodOrdering.Screens
{
    public class Order:INotifyPropertyChanged
    {
        public int orderId { get; set; }
        public string custName { get; set; }
        public string additionalInfo { get; set; }
        public decimal totalPrice { get; set; }

        private ObservableCollection<OrderItem>  orderItems_;
        public ObservableCollection<OrderItem> orderItems
        {
            get { return orderItems_; }
            set
            {
                orderItems_ = value;
                OnPropertyChanged("orderItems");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}