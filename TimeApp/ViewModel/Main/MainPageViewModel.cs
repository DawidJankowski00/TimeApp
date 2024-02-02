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
using TimeApp.ViewModel;
using TimeAppRestApi.Models;

namespace TimeApp.ViewModel.Main
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<Note> _notes = new ObservableCollection<Note>();
        public ObservableCollection<Note> Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        [ObservableProperty]
        bool isRefreshing;


        public UserService UserService = new UserService();

        public MainPageViewModel() {
            Title = "Notes";
            LoadNotes();
        }

        private void LoadNotes()
        {

            var sortedNotes = App.User.Notes.OrderBy(note => note.Deadline);

            foreach (Note note in sortedNotes)
            {
                Notes.Add(note);
            }
        }


        [RelayCommand]
        async void GoToAddNote()
        {
            
            await Shell.Current.GoToAsync($"{nameof(AddNotePage)}");
        }

        [RelayCommand]
        async void EditNote(Note note)
        {
            try
            {
                if (note is null) { return; }

                await Shell.Current.GoToAsync($"{nameof(EditNotePage)}", true,
                    new Dictionary<string, object>
                    {
                    {"Note",note }
                    });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get products: {ex.Message}", "OK");
            }
        }


        [RelayCommand]
        async void DeleteNote(Note note)
        {
            var response = await UserService.DeleteNote(note.Id);
            if (response)
            {
                await Shell.Current.DisplayAlert("Gratulacje!", $"Pomyślnie usunięto!", "OK");
                Notes.Remove(note);
            }
        }


        

        [RelayCommand]
        public async void Start()
        {
            await Shell.Current.DisplayAlert("Gratulacje!", $"test", "OK");
        }


        [RelayCommand]
        async void Refresh()
        {
            IsRefreshing = true;

            try
            {
                var tmp = App.User.Email;
                var user = await UserService.GetUser(tmp);
                App.User.Notes = user.Notes;

                // Czyszczenie i ponowne załadowanie notatek
                Notes.Clear();
                foreach (Note note in App.User.Notes.OrderBy(note => note.Deadline))
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
