using DevExpress.Mvvm;
using ICAdmin.Commands;
using ICAdmin.Models;
using ICAdmin.Services;
using ICAdmin.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ICAdmin.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private readonly MessageBus _messageBus;
        private readonly CheckConnectionService _checkConnectionService;
        private readonly AuthorizationService _authorizationService;

        private string login;
        private string password;
        private bool isNotLogged;

        public LoginViewModel(PageService pageService, MessageBus meessageBus, CheckConnectionService connectionService, AuthorizationService authorizationService)
        {
            _pageService = pageService;
            _messageBus = meessageBus;
            _checkConnectionService = connectionService;
            _authorizationService = authorizationService;
        }

        public bool IsNotLogged
        {
            get { return isNotLogged; }
            set
            {
                isNotLogged = value;
                RaisePropertyChanged();
            }
        }
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }

        }

        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                    (loginCommand = new RelayCommand(param => Authorization(param), param => CanAuth(param)));
            }
        }

        private async void Authorization(object param)
        {
            bool result = await _authorizationService.AuthorizationAsync(login, password);
            if (result)
            {
                ChangePage.Execute(null);
            }
            else
            {
                IsNotLogged = true;
            }
        }

        private bool CanAuth(object param)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password) && _checkConnectionService.IsServerConnected)
                return true;
            return false;
        }

        public ICommand ChangePage => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new MainMenuPage());
        });

        public ICommand ChangeRegistrationPage => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new RegistrationPage());
        });
    }
}
