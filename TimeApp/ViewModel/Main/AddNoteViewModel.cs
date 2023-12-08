using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Services;
using TimeAppRestApi.Models;

namespace TimeApp.ViewModel.Main
{
    public partial class AddNoteViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _title;
        [ObservableProperty]
        private string _description;
        [ObservableProperty]
        public DateTime _startDate;
        [ObservableProperty]
        public DateTime _deadLine;
        UserService userService = new UserService();

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
            note.Title = _title;
            note.Description = _description;
            note.StartDate = _startDate;
            note.AuthorId = App.User.Id;
            var response = await userService.AddNote(note);
            if (response)
            {
                await Shell.Current.DisplayAlert("Gratulacje!", $"Pomyślnie dodano!", "OK");
            }
            
        }
    }
}
