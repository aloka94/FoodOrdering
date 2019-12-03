using FoodOrdering.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Classes
{
    public class FoodItem:INotifyPropertyChanged
    {
        public int foodId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int  quantity { get; set; }
        public decimal price { get; set; }
        public bool isAvailable { get; set; }
        public string foodType { get; set; }
        public string category { get; set; }
        public string imagePath {
            get { return imagePath_; }
            set
            {
                imagePath_ = value;
                OnPropertyChanged("imagePath");
            }
        }
        private string imagePath_;
        public List<AddOn> addOns { get; set; }
        private string visibilityAdd_;
        public string visibilityAdd
        {
            get { return visibilityAdd_; }
            set
            {
                visibilityAdd_ = value;
                OnPropertyChanged("visibilityAdd");
            }
        }
        private string visibility_;
        public string visibility
        {
            get { return visibility_; }
            set
            {
                visibility_ = value;
                OnPropertyChanged("visibility");
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

// Not being used currently
    public class AddOn
    {
        public int addOnId { get; set; }
        public string addOnName { get; set; }
        public float addOnPrice { get; set; }
    }


