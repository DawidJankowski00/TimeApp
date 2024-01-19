using TimeApp.ViewModel.Main;

namespace TimeApp.View.Main;

public partial class TeamsPage : ContentPage
{
	public TeamsPage(): this(new TeamsPageViewModel())
	{

	}


	public TeamsPage(TeamsPageViewModel viewModel)
	{
        InitializeComponent();
		BindingContext = viewModel;
	}
}