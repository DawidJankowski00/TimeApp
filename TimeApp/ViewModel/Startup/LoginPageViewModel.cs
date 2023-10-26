using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Isopoh.Cryptography.Argon2;
using TimeApp.View;
using TimeApp.View.Main;
using TimeApp.Services;

namespace TimeApp.ViewModel
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _email;
        [ObservableProperty]
        private string _password;
        UserService userService = new UserService();

        [RelayCommand]
        async void Login()
        {
        
            if (IsBusy) { return; }
            if (string.IsNullOrWhiteSpace(Email) && string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Error!", $"Pola są puste", "OK");
                return;
            }
            try
            {

                IsBusy = true;
                var user = await userService.GetUser(Email);
                if (user == null) { return; }
                
                if (Argon2.Verify(user.Password, Password))
                {

                    user.Email = Email;
                    if (Preferences.ContainsKey(nameof(App.User)))
                    {
                        Preferences.Remove(nameof(App.User));
                    }
                    string UserStr = JsonConvert.SerializeObject(user);
                    Preferences.Set(nameof(App.User), UserStr);
                    App.User = user;

                    await Shell.Current.GoToAsync($"{nameof(MainPage)}");

                }
                else
                {
                    await Shell.Current.DisplayAlert("Error!", "Niepoprawne dane", "OK");
                }
                return;
               
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to Login: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }


        }

        [RelayCommand]
        async void GoToRegister() 
        {
            if (IsBusy) { return; }
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");

        }
    }
}
