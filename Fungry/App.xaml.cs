using Fungry.Services;
using Fungry.ViewMo;

namespace Fungry;

public partial class App : Application
{
    private readonly CartVIewModel _cartVIewModel;

    public App(AuthService authService, CartVIewModel cartVIewModel)
    {
        InitializeComponent();

        authService.Initialize();

        MainPage = new AppShell(authService);
        _cartVIewModel = cartVIewModel;
    }
    protected override async void OnStart()
    {
        await _cartVIewModel.InitializeCartAsync();
    }
}
