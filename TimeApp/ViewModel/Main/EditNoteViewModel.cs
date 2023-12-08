using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeAppRestApi.Models;

namespace TimeApp.ViewModel.Main
{
    [QueryProperty("Note", "Note")]
    public partial class EditNoteViewModel : BaseViewModel
    {
        public EditNoteViewModel() {
            
        }

        [ObservableProperty]
        Note note;
    }
}
