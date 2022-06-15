using DevExpress.Mvvm;
using ICAdmin.Commands;
using ICAdmin.Inventorization.Models;
using ICAdmin.Models;
using ICAdmin.Services;
using ICAdmin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ICAdmin.ViewModels
{
    public class MainMenuViewModel : BindableBase
    {
        private ObservableCollection<UserMachine> _userMachines;
        public ObservableCollection<UserMachine> UserMachines { get => _userMachines; set { _userMachines = value; RaisePropertiesChanged("UserMachines"); } }
        public UserMachine SelectedUserMachine { get; set; }

        private readonly PageService _pageService;
        private readonly MessageBus _messageBus;
        private readonly AuthorizationService _authorizationService;
        private readonly UserMachineService _userMachineService;
        private readonly AnyDeskService _anyDeskService;

        public MainMenuViewModel(PageService pageService, MessageBus meessageBus, AnyDeskService anyDeskService, AuthorizationService authorizationService, UserMachineService userMachineService)
        {
            _pageService = pageService;
            _messageBus = meessageBus;
            _authorizationService = authorizationService;
            _userMachineService = userMachineService;
            _anyDeskService = anyDeskService;

            UserMachines = new ObservableCollection<UserMachine>();
            _authorizationService.OnAuthorized += delegate { GetAllMachines(); };

            OverlayService.GetInstance().Show = (str) =>
            {
                OverlayService.GetInstance().Text = str;
            };
        }

        private ICommand connectAnyDeskCommand;
        public ICommand ConnectAnyDeskCommand
        {
            get
            {
                return connectAnyDeskCommand ??
                    (connectAnyDeskCommand = new AsyncCommand(async ()=> _anyDeskService.ConnectToAnyDesk(SelectedUserMachine)));
            }
        }
        
        private ICommand fileTransferAnyDeskCommand;
        public ICommand FileTransferAnyDeskCommand
        {
            get
            {
                return fileTransferAnyDeskCommand ??
                    (fileTransferAnyDeskCommand = new AsyncCommand(async ()=> _anyDeskService.ConnectToAnyDesk(SelectedUserMachine, "--file-transfer")));
            }
        }
        private ICommand openChatPage;
        public ICommand OpenChatCommand
        {
            get
            {
                return openChatPage ??
                    (openChatPage = new RelayCommand( obj => _pageService.ChangePage(new ChatPage())));
            }
        }

        private ICommand checkOverlayCommand;
        public ICommand CheckOverlayCommand
        {
            get
            {
                return checkOverlayCommand ??
                    (checkOverlayCommand = new RelayCommand(param => OverlayService.GetInstance().Show("Функционал находится в разработке, следите за обновлениями!")));
            }
        }

        
        private async void GetAllMachines()
        {
            var machines = await _userMachineService.GetUserMachinesAsync(_authorizationService.CurrentUser.Login, _authorizationService.CurrentUser.Password);
            foreach (var item in machines)
                UserMachines.Add(item);
        }

    }
}
