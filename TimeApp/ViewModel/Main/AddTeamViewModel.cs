using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Models;
using TimeApp.Services;

namespace TimeApp.ViewModel.Main
{
    public partial class AddTeamViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _name;

        UserService userService = new UserService();
        TeamService teamService = new TeamService();

        public AddTeamViewModel()
        {
            Title = "AddTeam";
        }


        [RelayCommand]
        async void CreateTeam()
        {
            if (Name == null)
            {
                await Shell.Current.DisplayAlert("Error!", $"Pole nazwa jest puste", "OK");
                return;
            }
            Team team = new Team();
            team.Name = Name;
            team.LeaderId = App.User.Id;
            
            team.Moderators = new();
            team.Moderators.Add(App.User.Id);
            team.MembersIds = new();
            team.MembersIds.Add(App.User.Id);
            team.TeamNotes = new();
            team.Messages = new();

            var response2 = await teamService.AddTeam(team);
            if (response2)
            {
                await Shell.Current.DisplayAlert("Gratulacje!", $"Pomyślnie utworzono!", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Gratulacje!", $"No nie!", "OK");
            }

        }
    }

    
}
