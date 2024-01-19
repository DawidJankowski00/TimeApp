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
using TimeApp.View.Main;

namespace TimeApp.ViewModel.Main
{
    public partial class TeamsPageViewModel : BaseViewModel
    {
        private ObservableCollection<Team> _teams = new ObservableCollection<Team>();
        public ObservableCollection<Team> Teams
        {
            get => _teams;
            set => SetProperty(ref _teams, value);
        }

        [ObservableProperty]
        bool isRefreshing;

        public TeamService TeamService = new TeamService();
        public UserService UserService = new UserService();

        public TeamsPageViewModel()
        {
            Title = "Teams";

            LoadTeams();
        }

        async void LoadTeams()
        {
            if (App.User.GroupsId != null) {
                foreach (int id in App.User.GroupsId)
                {
                    var team = await TeamService.GetTeam(id);
                    Teams.Add(team);
                }
            }
        }
        [RelayCommand]
        async void GoToAddTeam()
        {
            
            await Shell.Current.GoToAsync($"{nameof(AddTeamPage)}");
        }

        [RelayCommand]
        async Task DetailTeamAsync(Team team)
        {
            try
            {
                if (team is null) { return; }
                App.ActualTeam = team;
                await Shell.Current.GoToAsync($"{nameof(DetailsTeamPage)}", true,
                    new Dictionary<string, object>
                    {
                    {"Team",team }
                    });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get products: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        async void Refresh()
        {
            IsRefreshing = true;
            try
            {
                var tmp = App.User.Email;
                var user = await UserService.GetUser(tmp);
                App.User.GroupsId = user.GroupsId;

                // Czyszczenie i ponowne załadowanie notatek
                Teams.Clear();
                var i = 0;
                foreach (int id in App.User.GroupsId)
                {
                    i = i + 1;
                    var team = await TeamService.GetTeam(id);
                    Teams.Add(team);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                // Rozważ wyświetlenie komunikatu o błędzie użytkownikowi
            }
            finally
            {
                IsRefreshing = false;
            }
        }
        
    }
}
