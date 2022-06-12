using DevExpress.Mvvm;
using ICAdmin.Commands;
using ICAdmin.Models;
using ICAdmin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAdmin.ViewModels
{
    public class MainMenuViewModel: BindableBase
    {
        public ObservableCollection<UserMachine> UserMachines { get; set; }

        private readonly PageService _pageService;
        private readonly MessageBus _messageBus;
        private readonly AuthorizationService _authorizationService;
        private readonly AnyDeskService _anyDeskService;

        public MainMenuViewModel(PageService pageService, MessageBus meessageBus, AuthorizationService authorizationService, AnyDeskService anyDeskService)
        {
            _pageService = pageService;
            _messageBus = meessageBus;
            _authorizationService = authorizationService;
            _anyDeskService = anyDeskService;
            UserMachines = new ObservableCollection<UserMachine>();

            Random rnd = new Random();
            for (int i = 0; i < 120; i++)
            {
                UserMachine userMachine = new UserMachine() { Id = i, InventoryName = $"СБ{i}", IsConnected = rnd.Next(0, 2) == 1 ? true : false };
                UserMachines.Add(userMachine);
            }
        }

        private RelayCommand connectAnyDeskCommand;
        public RelayCommand ConnectAnyDeskCommand
        {
            get
            {
                return connectAnyDeskCommand ??
                    (connectAnyDeskCommand = new RelayCommand(param => ConnectAnyDesk(param as UserMachine)));
            }
        }

        private void ConnectAnyDesk(UserMachine userMachine)
        {
            if(userMachine != null)
            {
                _anyDeskService.GetCurrentAnyDeskId();
            }
        }


    }
}
