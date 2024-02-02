using TimeApp.ViewModel.Main;

namespace TimeApp.View.Main;

public partial class MyTeamNotesPage : ContentPage
{
	public MyTeamNotesPage(MyTeamNotesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	public MyTeamNotesPage() : this(new())
	{

	}
}