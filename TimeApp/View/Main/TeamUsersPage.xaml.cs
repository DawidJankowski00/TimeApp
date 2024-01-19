using TimeApp.ViewModel.Main;

namespace TimeApp.View.Main;

public partial class TeamUsersPage : ContentPage
{
	public TeamUsersPage(TeamUsersViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	public TeamUsersPage(): this(new TeamUsersViewModel())
	{

	}


}