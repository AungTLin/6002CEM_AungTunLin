using Fungry.ViewMo;

namespace Fungry.Themes;

public partial class Signin : ContentPage
{
	public Signin(AuthViewModel authViewModel)
	{
		InitializeComponent();
		BindingContext =authViewModel;
	}

    private async void SignupLabel_Tapped(object sender, TappedEventArgs e)
    {
		await Shell.Current.GoToAsync(nameof(Signup));
    }
}