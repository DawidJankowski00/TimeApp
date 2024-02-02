using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Models;
using TimeApp.View.Main;

namespace TimeApp.ViewModel.Main
{
    [QueryProperty("Team", "Team")]
    public partial class DetailsTeamPageViewModel : BaseViewModel
    {
        public DetailsTeamPageViewModel() { }

        [ObservableProperty]
        Team team;

        public bool IsMod
        {
            get { return App.ActualTeam.Moderators.Contains(App.User.Id); }
        }

        [RelayCommand]
        async void GoToTeamUsers()
        {
            
            await Shell.Current.GoToAsync($"{nameof(TeamUsersPage)}");
        }

        [RelayCommand]
        async void GoToMyTeamNotes()
        {

            await Shell.Current.GoToAsync($"{nameof(MyTeamNotesPage)}");
        }

        [RelayCommand]
        async void GoToTeamNotes()
        {

            await Shell.Current.GoToAsync($"{nameof(TeamNotesPage)}");
        }

        [RelayCommand]
        async void GoToTeamChat()
        {

            await Shell.Current.GoToAsync($"{nameof(TeamChatPage)}");
        }
    }
}
