using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeApp;
using TimeApp.View;
using TimeApp.ViewModel;

namespace Food.ViewModel
{
    public partial class AppShellViewModel : BaseViewModel
    {
        [RelayCommand]
        async void SignOut()
        {
            if (Preferences.ContainsKey(nameof(App.User))) 
            {
                Preferences.Remove(nameof(App.User));
                App.User = null;
            }
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

    }
}
