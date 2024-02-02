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
using TimeAppRestApi.Models;

namespace TimeApp.ViewModel.Main
{
    public partial class TeamNotesViewModel : BaseViewModel
    {

        private ObservableCollection<TeamNote> _notes = new ObservableCollection<TeamNote>();
        public ObservableCollection<TeamNote> Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }
        [ObservableProperty]
        bool isRefreshing;

        public TeamService TeamService = new TeamService();

        public TeamNotesViewModel() 
        {
            LoadNotes();
        }

        private void LoadNotes()
        {

            var sortedNotes = App.ActualTeam.TeamNotes.OrderBy(note => note.Note.Deadline);

            foreach (TeamNote note in sortedNotes)
            {
                Notes.Add(note);
            }
        }

        [RelayCommand]
        async void GoToAddTeamNote()
        {

            await Shell.Current.GoToAsync($"{nameof(AddTeamNotePage)}");
        }

        [RelayCommand]
        async void Refresh()
        {
            IsRefreshing = true;

            try
            {
                var tmp = App.ActualTeam.Id;
                var team = await TeamService.GetTeam(tmp);
                App.ActualTeam.TeamNotes = team.TeamNotes;

                // Czyszczenie i ponowne załadowanie notatek
                Notes.Clear();
                foreach (TeamNote note in App.ActualTeam.TeamNotes.OrderBy(note => note.Note.Deadline))
                {
                    Notes.Add(note);
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
