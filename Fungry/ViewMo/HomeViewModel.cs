using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using fungry.lib.Dtos;
using Fungry.Services;
using Fungry.Themes;


namespace Fungry.ViewMo
{
    public partial class HomeViewModel(IFoodsAPi foodsAPi, AuthService authService) : BaseViewModel  
    {
        private readonly IFoodsAPi _foodsApi = foodsAPi;
        private readonly AuthService _authService = authService;

        [ObservableProperty]
        private FoodDto[] _foods = [];

        [ObservableProperty]
        private string _userName = string.Empty;

        private bool _isInitialized;

        public async Task InitializeAsync()

        {
            UserName = _authService.User!.Name;
            if (_isInitialized) 
                return;

            IsBusy = true;
            try
            {
                //fetch foods form service 
                _isInitialized   = true;
                Foods =await _foodsApi.GetFoodsAsync();
            }
            catch(Exception ex)
            {
                _isInitialized= false;
                await ShowErrorAlertAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
        [RelayCommand]
        private async Task GoToAlldetailsAsync(FoodDto food)
        {
            var parameter = new Dictionary<string, object>
            {
                [nameof(DetailsViewModel.Food)] = food,
            };
            await GoToAsync(nameof(Alldetails), animate:true,parameter);
        }
    }
}
