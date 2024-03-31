using Fungry.ViewMo;

namespace Fungry.Themes;

public partial class Account : ContentPage
{
    private readonly ProfileViewModel _profileViewModel;

    public Account(ProfileViewModel profileViewModel)
	{
		InitializeComponent();
        BindingContext = profileViewModel;
        _profileViewModel = profileViewModel;
    }

    protected override void OnAppearing()
    {
        _profileViewModel.Initialize();
    }
}