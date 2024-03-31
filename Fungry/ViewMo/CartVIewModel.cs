using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using fungry.lib.Dtos;
using Fungry.Models;
using Fungry.Services;
using Fungry.Themes;
using Refit;

namespace Fungry.ViewMo
{
    public partial class CartVIewModel : BaseViewModel

      

    {

        private readonly DatabaseService _databaseService;
        private readonly IOrderApi _orderApi;
        private readonly AuthService _authService;

        public CartVIewModel(DatabaseService databaseService, IOrderApi orderApi,AuthService authService) 
        { 
            _databaseService = databaseService;
            _orderApi = orderApi;
            _authService = authService;
        }

        public ObservableCollection<Storecart> Storecarts { get; set; } = [];

        public static int TotalCartCount { get; set; }
        public static event EventHandler <int>? TotalCartCountChanged;

        public async void AddItemToCart(FoodDto food, int quant, string taste,string Extra)
        {
            var existingItem = Storecarts.FirstOrDefault(ci=> ci.FoodId ==food.Id);
            if(existingItem is not null)

            {
                var dbStorecart = await _databaseService.GetStorecartAsync(existingItem.Id);

                if (quant <= 0)
                {
                    await _databaseService.DeleteStorecart(dbStorecart);
                    Storecarts.Remove(existingItem);
                    await ShowToastAsync("Food Item removed from the cart");
                }
                else
                {
                    dbStorecart.Quant = quant;
                    await _databaseService.UpdateStorecart(dbStorecart);
                    existingItem.Quant = quant;
                    await ShowToastAsync("Quantity updated in cart");
                }
            }
            else
            {
                var storecart = new Storecart
                {
                    TasteName = taste,
                    FoodId =food.Id,
                    Name= food.Name,
                    Price = food.Price,
                    Quant = quant,
                    ExtraName = Extra

                };

                
                var entity = new SQL.CartItemEntity(storecart);
                await _databaseService.AddStorecart(entity);

                storecart.Id = food.Id;

                Storecarts.Add(storecart);
                await ShowToastAsync("Food Added to Cart");
            }

            NotifyCartCountChange();
        }
        private void NotifyCartCountChange()
        {
            TotalCartCount = Storecarts.Sum(i => i.Quant);
            TotalCartCountChanged?.Invoke(null, TotalCartCount);
        }
        public int GetstoreCartCount(int foodId)
        {
            var existingItem = Storecarts.FirstOrDefault(i=> i.FoodId == foodId);
            return existingItem?.Quant ?? 0;
        }

        public async Task InitializeCartAsync()

        {
            var dbItems =await _databaseService.GetAllStorecartsAsync();
            foreach(var dbItem in dbItems)
            {
                Storecarts.Add(dbItem.ToStorecartModel());
            }
            NotifyCartCountChange();
        }

        [RelayCommand]
        private  async Task ClearCartAsync()
        {
           await ClearCartInternalAsync(fromPlaceOrder: false);
            
        }
        private async Task ClearCartInternalAsync(bool fromPlaceOrder)
        {
            if (!fromPlaceOrder && Storecarts.Count == 0)
            {
                await ShowAlertAsync("Empty store ", "Not items in the store");
                return;
            }
            if (fromPlaceOrder 
                || await ConfirmAsync("Do u wanna clear all", "yes or no"))
            {
                await _databaseService.ClearCartAsync();
                Storecarts.Clear();

                if(!fromPlaceOrder)
                await ShowAlertAsync("Store cleared");

                NotifyCartCountChange();
            }
        }

        [RelayCommand]

        private async Task RemoveCartItemsAsync(int storeCartId)
        {
            if (await ConfirmAsync("Remove from cart", "just like that "))
            {
                var existingItem =Storecarts.FirstOrDefault(i=> i.Id == storeCartId);
                if (existingItem is null)
                    return;

                Storecarts.Remove(existingItem);

                var dbCartItem = await _databaseService.GetStorecartAsync(storeCartId);
                if (dbCartItem is null)
                    return;

                await _databaseService.DeleteStorecart(dbCartItem);
                
                await ShowAlertAsync("Store cleared");
                NotifyCartCountChange();
            }
        }


        [RelayCommand]
        private async Task PlaceOrderAsync()
        {
            if (Storecarts.Count == 0)
            {
                await ShowAlertAsync("Empty store ", "Add cards before placing it");
                return;
            }
            IsBusy = true;
            try
            {
                var order = new OrderDto(0, DateTime.Now, Storecarts.Sum(i => i.TotatPrice));
                var orderItems = Storecarts.Select(i => new OrderItemDto(0, i.FoodId, i.Name, i.Quant, i.Price, i.TasteName, i.ExtraName)).ToArray();
                var orderPlaceDto = new OrderPlaceDto(order, orderItems);

                var result = await _orderApi.PlaceOrderAsync(orderPlaceDto);
                if (!result.IsSuccess)
                {
                    await ShowErrorAlertAsync(result.ErrorMessage!);
                    return;
                }


                await ShowToastAsync("Order Placed");
                await ClearCartInternalAsync(fromPlaceOrder: true);
            }
            catch (ApiException ex)

            {
                await HandleApiExceptionAsync(ex, () => _authService.Signout());
                /*if(ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await ShowErrorAlertAsync("Session Expired.");
                    _authService.Signout();
                    await GoToAsync($"//{nameof(Onbopage)}");
                    return;
                }
               await ShowErrorAlertAsync(ex.Message);
            */
            }

            finally
            {
                IsBusy = false;
            }
        }
    }

}