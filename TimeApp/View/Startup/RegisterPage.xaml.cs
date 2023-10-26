using TimeApp.ViewModel;

namespace TimeApp.View;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}
}