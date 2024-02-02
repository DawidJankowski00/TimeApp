using CommunityToolkit.Mvvm.Input;
using Food.ViewModel;
using TimeApp.View;
using TimeApp.View.Main;

namespace TimeApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = new AppShellViewModel();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
            Routing.RegisterRoute(nameof(AddNotePage), typeof(AddNotePage));
            Routing.RegisterRoute(nameof(SetingsPage), typeof(SetingsPage));
            Routing.RegisterRoute(nameof(EditNotePage), typeof(EditNotePage));
            Routing.RegisterRoute(nameof(TeamsPage), typeof(TeamsPage));
            Routing.RegisterRoute(nameof(AddTeamPage), typeof(AddTeamPage));
            Routing.RegisterRoute(nameof(DetailsTeamPage), typeof(DetailsTeamPage));
            Routing.RegisterRoute(nameof(TeamUsersPage), typeof(TeamUsersPage));
            Routing.RegisterRoute(nameof(TeamChatPage), typeof(TeamChatPage));
            Routing.RegisterRoute(nameof(TeamNotesPage), typeof(TeamNotesPage));
            Routing.RegisterRoute(nameof(AddTeamNotePage), typeof(AddTeamNotePage));
            Routing.RegisterRoute(nameof(MyTeamNotesPage), typeof(MyTeamNotesPage));
        }
    }
}