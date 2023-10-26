using TimeApp.ViewModel.Main;

namespace TimeApp.View.Main;

public partial class MainPage : ContentPage
{
    public MainPage() : this(new MainPageViewModel())
    {

    }

    public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}