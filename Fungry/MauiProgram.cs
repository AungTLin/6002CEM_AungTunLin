using CommunityToolkit.Maui;

using Fungry.Services;
using Fungry.Themes;
using Fungry.ViewMo;
using Microsoft.Extensions.Logging;
using Refit;

#if ANDROID
using System.Net.Security;
using Xamarin.Android.Net;
#elif IOS
using Security;
#endif

namespace Fungry
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMauiCommunityToolkit()
                .ConfigureMauiHandlers(h=>
                {

#if ANDROID || IOS

                    h.AddHandler<Shell, Tabbar>();
#endif
                });

#if DEBUG
    		builder.Logging.AddDebug();

#endif
            builder.Services.AddSingleton<DatabaseService>();

            builder.Services.AddTransient<AuthViewModel>()
                            .AddTransient<Signup>()
                            .AddTransient<Signin>();

            builder.Services.AddSingleton<AuthService>();

            builder.Services.AddTransient<Onbopage>();
            
            builder.Services.AddSingleton<HomeViewModel>()
                            .AddSingleton<home>();

            builder.Services.AddTransient<DetailsViewModel>()
                            .AddTransient<Alldetails>();

            builder.Services.AddSingleton<CartVIewModel>()
                             .AddTransient<storecart>();

            builder.Services.AddTransient<ProfileViewModel>()
                            .AddTransient<Account>();

            builder.Services.AddTransient<OrdersViewModel>()
                            .AddTransient<order>();

            builder.Services.AddTransient<Vieworddtl>()
                            .AddTransient<OrdDetail>();

            ConfigureRefit(builder.Services);
            return builder.Build();
        }

        private static void ConfigureRefit(IServiceCollection services)
        {
            
            services.AddRefitClient<IAuthApi>(GetRefitSettings)
                .ConfigureHttpClient(SetHttpClient);

            services.AddRefitClient<IFoodsAPi>(GetRefitSettings)
                .ConfigureHttpClient(SetHttpClient);

            services.AddRefitClient<IOrderApi>(GetRefitSettings)
            .ConfigureHttpClient(SetHttpClient);



            static void SetHttpClient(HttpClient httpClient)
            {
                var baseUrl = DeviceInfo.Platform == DevicePlatform.Android
                                   ? "https://10.0.2.2:7156"
                                   : "https://localhost:7156";

                if(DeviceInfo.DeviceType ==DeviceType.Physical)
                {
                    baseUrl = " https://dgzw1rgm-7156.uks1.devtunnels.ms"; //this is one is fake becoz my https is not working
                }
           



                httpClient.BaseAddress = new Uri(baseUrl);
            }
        }
        
        static RefitSettings GetRefitSettings(IServiceProvider serviceProvider)
        {
            var authService = serviceProvider.GetRequiredService<AuthService>();
            var refitSettings = new RefitSettings
            {
                HttpMessageHandlerFactory = () =>
                {


                    //return http handler 
#if ANDROID
                    return new AndroidMessageHandler
                    {
                        ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, chain, sslPolicyErrors) =>
                        {
                            return certificate?.Issuer == "CN=localhost" || sslPolicyErrors == SslPolicyErrors.None;
                        }

                    };
#elif IOS
                    return new NSUrlSessionHandler
                    {
                        TrustOverrideForUrl = (NSUrlSessionHandler sender, string url, SecTrust trust) =>
                        url.StartsWith("https://localhost")
                    };
#endif
                    return null;
                },
                AuthorizationHeaderValueGetter = (_, __) =>
                Task.FromResult(authService.Token ?? string.Empty)
            };
            return refitSettings;
        }
    }
    


}
