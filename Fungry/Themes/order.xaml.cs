using Fungry.ViewMo;

namespace Fungry.Themes;

public partial class order : ContentPage
{
    private readonly OrdersViewModel _ordersViewModel;

    public order(OrdersViewModel ordersViewModel)

	{
		InitializeComponent();
        _ordersViewModel = ordersViewModel;
        BindingContext = ordersViewModel;
    }

    protected override async void OnAppearing()
    {
        await _ordersViewModel.InitializeAsync();
    }
}