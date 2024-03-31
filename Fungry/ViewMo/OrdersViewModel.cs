using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using fungry.lib.Dtos;
using Fungry.Services;
using Refit;

namespace Fungry.ViewMo
{
    public partial class OrdersViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly IOrderApi _orderApi;

        public OrdersViewModel(AuthService authService, IOrderApi orderApi)
        {
            _authService = authService;
            _orderApi = orderApi;
        }

        [ObservableProperty]
        private OrderDto[] _orders = [];

        public async Task InitializeAsync() => await LoadOrdersAsync();

        [RelayCommand]
        private async Task LoadOrdersAsync()

        {
            IsBusy = true;
            try
            {
                Orders = await _orderApi.GetMyOrdersAsync();
                if(Orders.Length == 0)
                {
                    await ShowToastAsync("No orders found");
                }
            }
            catch (ApiException ex) 
            {

                await HandleApiExceptionAsync(ex, () => _authService.Signout());
            }
            finally
            {
                IsBusy=false;
            }
        }
        

    }
}
