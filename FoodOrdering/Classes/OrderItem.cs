using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Classes
{
    public class OrderItem : INotifyPropertyChanged
    {
        public FoodItem item { get; set; }
        public List<AddOn> addOns { get; set; }
        public string customization { get; set; }
        public decimal itemTotal
        {
            get { return itemTotal_; }
            set
            {
                itemTotal_ = value;
                OnPropertyChanged("itemTotal");
            }
        }
        private decimal itemTotal_;


        private int itemQuantity_;
        public int itemQuantity 
        {
            get { return itemQuantity_; }
            set
            {
                itemQuantity_ = value;
                OnPropertyChanged("itemQuantity");
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
