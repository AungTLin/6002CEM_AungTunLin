using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using fungry.lib.Dtos;
using Fungry.Services;
using Fungry.Themes;


namespace Fungry.ViewMo
{
    public partial class  AuthViewModel(IAuthApi authApi, AuthService authService) : BaseViewModel

    {
        
        private readonly IAuthApi _authApi =authApi;
        private readonly AuthService _authService =authService;

        //[ObservableProperty]
        //private bool _isBusy;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _name;


        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignin)), NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _email;

        [ObservableProperty ,NotifyPropertyChangedFor(nameof(CanSignin)), NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _password;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _address;

        public bool CanSignin => !string.IsNullOrEmpty(Email)
                                    && !string.IsNullOrEmpty(Password);

        public bool CanSignup => CanSignin
                                        && !string.IsNullOrEmpty(Password)
                                        && !string .IsNullOrEmpty(Address);

        [RelayCommand]

        private async Task SignupAsync()
        {
            IsBusy = true;
            try
            {
                var signupDto =new SignupRequestDto(Name, Email, Password,Address);
                var result =await _authApi.SignupAsync(signupDto);

                if(result.IsSuccess)
                {
                    _authService.Signin(result.Data);
                    
                    await GoToAsync($"//{nameof(home)}", animate: true);
                }
                else
                {
                    await ShowErrorAlertAsync(result.ErrorMessage ??"Error in sign up" );
                }

            }
            catch(Exception ex)
            {
                await ShowErrorAlertAsync(ex.Message);
            }

            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task SigninAsync()
        {
            IsBusy = true;
            try
            {
                var signinDto = new SigninRequestDto(Email, Password);
                var result = await _authApi.SigninAsync(signinDto);

                if (result.IsSuccess)
                {
                    _authService.Signin(result.Data);
                    await GoToAsync($"//{nameof(home)}", animate: true);
                }
                else
                {
                    await ShowErrorAlertAsync(result.ErrorMessage ?? "Error in sign in");
                }

            }
            catch (Exception ex)
            {
                await ShowErrorAlertAsync(ex.Message);
            }

            finally
            {
                IsBusy = false;
            }
        }
    }
}
