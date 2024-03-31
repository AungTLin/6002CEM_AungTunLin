using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Fungry.Models
{
    public partial class Storecart : ObservableObject
    {
        public int Id { get; set; }

        public int FoodId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string TasteName { get; set; }

        public string ExtraName { get; set; }

        [ObservableProperty, NotifyPropertyChangedFor(nameof(TotatPrice))]
        private int _quant;

        public double TotatPrice => Price * Quant;
    }

}
