using TimeApp.ViewModel;

namespace TimeApp.View;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}
}