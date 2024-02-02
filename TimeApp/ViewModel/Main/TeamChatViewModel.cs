using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Models;
using TimeApp.Services;

namespace TimeApp.ViewModel.Main
{
    public partial class TeamChatViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _myMsg;

        [ObservableProperty]
        bool isRefreshing;

        private ObservableCollection<Message> _messages = new ObservableCollection<Message>();
        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set => SetProperty(ref _messages, value);
        }


        TeamService teamService = new TeamService();


        public TeamChatViewModel()
        {
            Title = "Chat";
            LoadMessages();
        }

        async void LoadMessages()
        {
            App.ActualTeam = await teamService.GetTeam(App.ActualTeam.Id);
            if (App.ActualTeam != null)
            {
                if (App.ActualTeam.Messages != null) 
                {
                    Messages.Clear();
                    foreach(Message m in App.ActualTeam.Messages)
                    {
                        Messages.Add(m);
                    }
                }
            }
        }


        [RelayCommand]
        async void AddMsgTeam()
        {
            Team team = App.ActualTeam;
            Message msg = new();
            msg.Id = team.GetNextMsgId();
            msg.Msg = _myMsg;
            msg.User = App.User;

            team.Messages.Add(msg);

            App.ActualTeam = team;
            var res = await teamService.UpdateTeam(team);
            if (!res)
            {
                await Shell.Current.DisplayAlert("Error!", $"Nie dodano {_myMsg}", "OK");
            }
            Refresh();
        }

        [RelayCommand]
        async void Refresh()
        {
            IsRefreshing = true;
            try
            {
                LoadMessages();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                // Rozważ wyświetlenie komunikatu o błędzie użytkownikowi
            }
            finally
            {
                IsRefreshing = false;
            }
        }
    }
}
