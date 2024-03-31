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
using Refit;

namespace Fungry.ViewMo
{
    [QueryProperty(nameof(OrderId),nameof(OrderId))]
    public partial class Vieworddtl : BaseViewModel
    {
        private readonly IOrderApi _orderApi;
        private readonly AuthService _authService;

        public Vieworddtl(AuthService authService,IOrderApi orderApi)
        {
            _authService = authService;
            _orderApi = orderApi;
        }
        [ObservableProperty]
        private long _orderId;

        [ObservableProperty]
        private OrderItemDto[] _orderItems = [];

        [ObservableProperty]
        private string _title = "OrderItems";

        partial void OnOrderIdChanged(long value)
        {
           Title = $"Order #{value}";
            LoadOrderItemsAsync(value);
        }

        private async Task LoadOrderItemsAsync(long orderId)
        {
            IsBusy = true;
            try
            {
                OrderItems = await _orderApi.GetOrderItemsAsync(orderId);
            }
            catch(ApiException ex)
            {
               await HandleApiExceptionAsync(ex, () => _authService.Signout());
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task GoToOrdDetailAsync(long orderId)
        {
            var parameter = new Dictionary<string, object>
            {
                [nameof(Vieworddtl.OrderId)] = orderId,
            };
            await GoToAsync(nameof(OrdDetail), animate: true, parameter);
        }
    }
}
