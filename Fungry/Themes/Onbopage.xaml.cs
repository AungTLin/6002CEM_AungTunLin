using Fungry.Services;

namespace Fungry.Themes;

public partial class Onbopage : ContentPage

    
{
    private readonly AuthService _authService;  
	public Onbopage(AuthService authService)
	{
		InitializeComponent();
        _authService = authService;
	}

   // private async void Button_Clicked(object sender, EventArgs e)
    //{
		//await Shell.Current.GoToAsync($"//{nameof(home)}");
   // }

    protected  async override void OnAppearing()
    {
        if(_authService.User is not null
            && _authService.User.Id != default
            && !string.IsNullOrWhiteSpace(_authService.Token))

        {
            //user is already logged in 

            await Shell.Current.GoToAsync($"//{nameof(home)}");
        }
    }
    private async void Signin_btn(object sender, EventArgs e)

    {
        await Shell.Current.GoToAsync(nameof(Signin));
    }

    private async void Signup_btn(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Signup));
    }
}