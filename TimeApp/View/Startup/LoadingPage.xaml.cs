using TimeApp.ViewModel;

namespace TimeApp.View;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}
}