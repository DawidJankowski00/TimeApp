using TimeApp.ViewModel.Main;
using TimeAppRestApi.Models;

namespace TimeApp.View.Main;

public partial class EditNotePage : ContentPage
{
	public EditNotePage(EditNoteViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
	public EditNotePage() : this(new EditNoteViewModel()) 
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {

        base.OnNavigatedTo(args);
    }
}