using TimeApp.ViewModel.Main;

namespace TimeApp.View.Main;

public partial class DetailsTeamPage : ContentPage
{
	public DetailsTeamPage(DetailsTeamPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
	public DetailsTeamPage(): this(new DetailsTeamPageViewModel())
	{
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {

        base.OnNavigatedTo(args);
    }
}