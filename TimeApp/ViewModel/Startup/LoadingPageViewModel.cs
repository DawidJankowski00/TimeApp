using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Models;
using TimeApp.View;
using TimeApp.View.Main;

namespace TimeApp.ViewModel
{
    public partial class LoadingPageViewModel
    {
        public LoadingPageViewModel() 
        {
            CheckUserLogin();
        }
        private async void CheckUserLogin()
        {
            string userStr = Preferences.Get(nameof(App.User), "");
            if (string.IsNullOrWhiteSpace(userStr)) 
            {
                await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
            }
            else
            {
                var user = JsonConvert.DeserializeObject<User>(userStr);
                App.User = user;
                await Shell.Current.GoToAsync($"{nameof(MainPage)}");
            }
        }
    }
}
