using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Models;
using TimeApp.Services;

namespace TimeApp.ViewModel.Main
{
    public partial class TeamUsersViewModel : BaseViewModel
    {
        [ObservableProperty]
        private int _id;

        UserService userService = new UserService();
        TeamService teamService = new TeamService();

        private ObservableCollection<User> _users = new ObservableCollection<User>();
        public ObservableCollection<User> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        public UserService UserService = new UserService();

        public TeamUsersViewModel() 
        {
            LoadUsers();
        }

        async void LoadUsers()
        {
            if (App.ActualTeam.MembersIds != null)
            {
                foreach (int id in App.ActualTeam.MembersIds)
                {
                    var user = await UserService.GetUserById(id);
                    Users.Add(user);
                }
            }
        }

        [RelayCommand]
        async void AddUserToTeam()
        {
            
            var user = await userService.GetUserById(_id);
            if (user == null) { return; }
            user.GroupsId.Add(App.ActualTeam.Id);
            var res = await userService.UpdateUser(user);
            App.ActualTeam.MembersIds.Add(_id);
            var res2 = await teamService.UpdateTeam(App.ActualTeam);
            if (res && res2)
            {
                await Shell.Current.DisplayAlert("Error!", $"Dodano", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error!", $"Nie dodano", "OK");
            }  
        }
        [RelayCommand]
        async void DelateUserFromTeam(User user)
        {
            await Shell.Current.DisplayAlert("Error!", $"test", "OK");
            user.GroupsId.Remove(App.ActualTeam.Id);
            var res = await userService.UpdateUser(user);
            App.ActualTeam.MembersIds.Remove(user.Id);
            var res2 = await teamService.UpdateTeam(App.ActualTeam);

            if (res && res2)
            {
                await Shell.Current.DisplayAlert("Error!", $"Usunieto", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error!", $"Nie usunieto", "OK");
            }
        }

        public string IsMod(User user)
        {
            if (App.ActualTeam.Moderators.Contains(App.User.Id))
            {
                return "Tak";
            }
            return "Nie";
            
        }
    }
}
