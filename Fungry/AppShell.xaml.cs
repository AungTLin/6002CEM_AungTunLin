using Fungry.Services;
using Fungry.Themes;

//Routing.RegisterRoute(nameof(Signin), typeof(Signin)); // you need to do for every page instead of that you can do diff as shown below using foreach 
//Routing.RegisterRoute(nameof(Signup), typeof(Signup));

namespace Fungry
{
    public partial class AppShell : Shell
    {
        public AppShell(AuthService authService)
        {
            InitializeComponent();

            RegisterRoutes();
            _authService = authService;
           
        }

        private readonly static Type[] _routePtype =
            [
            typeof(Signin), // you can add more type of page if you creating the another new page for routing.
            typeof(Signup),
            typeof(order),
            typeof(OrdDetail),
            typeof(Alldetails),
            ];
      private readonly AuthService _authService;

        private static void RegisterRoutes()
        {
            foreach (var pagetype in _routePtype)
            {
                Routing.RegisterRoute(pagetype.Name, pagetype);
            }

            
        }

        private async void FlyoutFooter_Tapped(object sender, TappedEventArgs e)
        {
            await Launcher.OpenAsync("https://boxicons.com/?query=arrow");
        }

        private async void Signout_Clicked(object sender, EventArgs e)
        {
            _authService.Signout();
            await Shell.Current.GoToAsync($"//{nameof(Onbopage)}");
        }
    }


}
