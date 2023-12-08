using TimeApp.ViewModel.Main;

namespace TimeApp.View.Main;

public partial class AddNotePage : ContentPage
{
	public AddNotePage(AddNoteViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
    public AddNotePage() : this(new AddNoteViewModel())
    {
        InitializeComponent();
    }
}