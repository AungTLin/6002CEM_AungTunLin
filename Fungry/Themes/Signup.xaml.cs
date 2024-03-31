using Fungry.ViewMo;

namespace Fungry.Themes;

public partial class Signup : ContentPage
{
    public Signup(AuthViewModel authViewModel)
    {
        InitializeComponent();
        BindingContext = authViewModel;
    }

    private async void SigninLabel_Tapped(object sender, TappedEventArgs e)
    {
		await Shell.Current.GoToAsync(nameof(Signin));
    }
}