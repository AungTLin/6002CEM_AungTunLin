using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using fungry.lib.Dtos;
using Fungry.Models;

namespace Fungry.ViewMo
{
    [QueryProperty(nameof(Food),nameof(Food))]
    public partial class DetailsViewModel : BaseViewModel
    {

        public DetailsViewModel(CartVIewModel cartVIewModel)
        {
            _cartVIewModel= cartVIewModel;
        }
        [ObservableProperty]
        private FoodDto? _food;

        [ObservableProperty]
        private int _quant;

        [ObservableProperty]
        private FoodOption[] _options = [];
        private CartVIewModel _cartVIewModel;

        partial void OnFoodChanged(FoodDto? value)
        {
            Options = [];
            if (value is null)
                return;

            Options =value.Options.Select(o=> new  FoodOption
            {
                Taste =o.Taste,
                Extra = o.Extra,
                IsSelected =false
            })
                .ToArray();

            Quant = _cartVIewModel.GetstoreCartCount(value.Id);
        }

        [RelayCommand]
        private void IncreaseQuantity() => Quant++;

        [RelayCommand]
        private void DecreaseQuantity()
        {
            if (Quant > 1)
                Quant--;
        }

        [RelayCommand]
        private async Task GoBackAsync() => await GoToAsync("..", animate: true);

        [RelayCommand]
        private void SelectOption(FoodOption newOption)
        {
            var newIsSelected = !newOption.IsSelected;
            Options = [..Options.Select(o => { o.IsSelected = false;
            return o; })];
            newOption.IsSelected = newIsSelected;
        }

        [RelayCommand]
        private async Task  AddToCartAsync()
        {
            var selectedOption = Options.FirstOrDefault(o => o.IsSelected) ?? Options[0];
            _cartVIewModel.AddItemToCart(Food!, Quant, selectedOption.Taste, selectedOption.Extra);
            await GoBackAsync();
        }
    }

}
