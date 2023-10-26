using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Isopoh.Cryptography.Argon2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TimeApp.Models;
using TimeApp.Services;
using TimeAppRestApi.Models;

namespace TimeApp.ViewModel
{
    public partial class RegisterViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private string _email;
        [ObservableProperty]
        private string _password;
        UserService userService = new UserService();


        [RelayCommand]
        async void AddUser()
        {
            if (_name == null)
            {
                await Shell.Current.DisplayAlert("Error!", $"Pole nazwa jest puste", "OK");
                return;
            }
            else if(_email == null){
                await Shell.Current.DisplayAlert("Error!", $"Pole email jest puste", "OK");
                return;
            }
            else if (_password == null)
            {
                await Shell.Current.DisplayAlert("Error!", $"Pole haslo jest puste", "OK");
                return;
            }
            if (IsPasswordValid(_password)){
                var users = await userService.GetUser("try");
                
                
                

                _password = Argon2.Hash(_password);
                _email = Argon2.Hash(_email.ToLower());
                User user = new();
                user.Name = _name;
                user.Email = _email;
                user.Password = _password;
                user.Notes = new List<Note>();
                user.GroupsId = new List<int>();

                var response = await userService.AddUser(user);
                if (response)
                {
                    await Shell.Current.DisplayAlert("Gratulacje!", $"Pomyślnie zarejstrowano!", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error!", $"Wystąpił błąd", "OK");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error!", $"Podane hasło musi zawierać conajmniej 8 znaków, 1 znak specjalny, 1 małą literę oraz 1 dużą literę", "OK");
                return;
            } 
        }

        static bool IsPasswordValid(string password)
        {
            if (password.Length < 8)
            {
                return false;
            }

            if (!password.Any(char.IsDigit))
            {
                return false;
            }

            if (!password.Any(char.IsUpper))
            {
                return false;
            }

            if (!password.Any(char.IsLower))
            {
                return false;
            }

            if (!Regex.IsMatch(password, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]"))
            {
                return false;
            }

            return true;
        }
    }
}
