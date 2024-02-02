using TimeApp.ViewModel.Main;

namespace TimeApp.View.Main;

public partial class TeamChatPage : ContentPage
{
	public TeamChatPage(TeamChatViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	public TeamChatPage(): this(new TeamChatViewModel())
	{

	}
}