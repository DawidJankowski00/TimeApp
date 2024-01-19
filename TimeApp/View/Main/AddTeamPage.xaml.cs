using TimeApp.ViewModel.Main;

namespace TimeApp.View.Main;

public partial class AddTeamPage : ContentPage
{
	public AddTeamPage(AddTeamViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	public AddTeamPage() : this(new AddTeamViewModel())
	{
        InitializeComponent();
    }
}