using TimeApp.ViewModel.Main;

namespace TimeApp.View.Main;

public partial class AddTeamNotePage : ContentPage
{
	public AddTeamNotePage(AddTeamNoteViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
	public AddTeamNotePage() : this(new())
	{

	}
}