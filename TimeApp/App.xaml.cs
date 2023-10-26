using TimeApp.Models;

namespace TimeApp
{
    public partial class App : Application
    {

        public static User User { get; set; }

        public static Team ActualTeam { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}