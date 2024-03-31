using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Fungry.Models
{
   public partial class FoodOption: ObservableObject

    {
        public string Taste {  get; set; }

        public string Extra { get; set; }

        [ObservableProperty]
        private bool _isSelected;
    }
}
