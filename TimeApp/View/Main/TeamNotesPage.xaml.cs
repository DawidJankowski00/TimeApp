using TimeApp.ViewModel.Main;

namespace TimeApp.View.Main;

public partial class TeamNotesPage : ContentPage
{
	public TeamNotesPage(TeamNotesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	public TeamNotesPage(): this(new())
	{

	}
}