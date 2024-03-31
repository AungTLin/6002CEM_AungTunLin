using System.Runtime.CompilerServices;

using Fungry.ViewMo;


namespace Fungry.Themes;

public partial class home : ContentPage
{
	private readonly HomeViewModel _homeViewModel;
	public home(HomeViewModel homeViewModel)
	{
		InitializeComponent();
        _homeViewModel = homeViewModel;
		BindingContext = _homeViewModel;
    }
	
	protected async override void OnAppearing()
	{
		await _homeViewModel.InitializeAsync();
	}
   
}