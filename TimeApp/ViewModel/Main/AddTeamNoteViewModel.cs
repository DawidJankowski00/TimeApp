using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Models;
using TimeApp.Services;
using TimeAppRestApi.Models;

namespace TimeApp.ViewModel.Main
{
    public partial class AddTeamNoteViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _title;
        [ObservableProperty]
        private string _description;
        [ObservableProperty]
        public DateTime _startDate;
        [ObservableProperty]
        public DateTime _deadLine;

        [ObservableProperty]
        public List<string> _myUsersValues;

        [ObservableProperty]
        private string _selectedUserValue;



        UserService userService = new();
        TeamService teamService = new();

        public AddTeamNoteViewModel()
        {
            _startDate = DateTime.Today;
            _deadLine = DateTime.Today;

            LoadUsers();
        }

        public async void LoadUsers()
        {
            MyUsersValues = new List<string>(await GroupUsersAsync());
        }

        public async Task<List<string>> GroupUsersAsync()
        {

            var users = new List<string>();
            if (App.ActualTeam.MembersIds != null)
            {
                foreach (int id in App.ActualTeam.MembersIds)
                {
                    var user = await userService.GetUserById(id);
                    TeamUser usr = new(user);
                    
                    users.Add(usr.Name);
                }
            }
            
            return users;
        }

        [RelayCommand]
        async void Save()
        {
            if (_title == null)
            {
                await Shell.Current.DisplayAlert("Error!", $"Pole title jest puste", "OK");
                return;
            }
            if (_description == null)
            {
                _description = "";
            }

            else if (_startDate == null)
            {
                await Shell.Current.DisplayAlert("Error!", $"Pole przypomnienia jest puste", "OK");
                return;
            }
            else if (_deadLine == null)
            {
                await Shell.Current.DisplayAlert("Error!", $"Pole dedline jest puste", "OK");
                return;
            }

            

            Note note = new Note();
            note.Id = 0;
            note.Title = _title;
            note.Description = _description;
            note.StartDate = _startDate;
            note.Deadline = _deadLine;
            note.AuthorId = App.User.Id;
            note.Status = TimeAppRestApi.Models.TaskStatus.NotStarted;
            TeamNote teamNote = new TeamNote();
            teamNote.Note = note;
            teamNote.UserIds = new List<int>();

            Team team = App.ActualTeam;

            int i = 0;
            foreach (string usr in MyUsersValues)
            {
                if (usr == null) { continue; }
                else
                {
                    if (usr == SelectedUserValue)
                    {
                        teamNote.UserIds.Add(team.MembersIds[i]);
                    }
                }
                i++;
            }

            if (team.TeamNotes == null)
            {
                team.TeamNotes = new List<TeamNote>();
            }
            team.TeamNotes.Add(teamNote);
            var response = await teamService.AddTeamNote(team, teamNote);

            if (response)
            {
                App.ActualTeam = team;
                await Shell.Current.DisplayAlert("Gratulacje!", $"Pomyślnie dodano!", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Gratulacje!", $"Nie dodano!", "OK");
            }

        }
    }
}
