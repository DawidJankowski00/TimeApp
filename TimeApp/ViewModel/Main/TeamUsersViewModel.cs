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

        private ObservableCollection<TeamUser> _users = new ObservableCollection<TeamUser>();
        public ObservableCollection<TeamUser> Users
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
                    TeamUser usr = new(user);
                    if (App.ActualTeam.LeaderId == usr.Id) {
                        usr.Rank = "Leader"; 
                    }
                    else if (App.ActualTeam.Moderators.Contains(usr.Id))
                    {
                        usr.Rank = "Moderator";
                    }
                    else
                    {
                        usr.Rank = "Użytkownik";
                    }
                    Users.Add(usr);
                }
            }
        }

        [RelayCommand]
        async void AddUserToTeam()
        {
            if (App.ActualTeam.MembersIds.Contains(_id))
            {
                await Shell.Current.DisplayAlert("Error!", $"Użytkownik należy już do zespołu", "OK");
                return;
            }
            var user = await userService.GetUserById(_id);
            if (user == null) { return; }
            if (user.GroupsId == null)
            {
                user.GroupsId = new List<int>();
            }
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

        public string Rank
        {
            get
            {
                return "Mod";
            }
        }
    }
}
