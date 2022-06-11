using DevExpress.Mvvm;
using ICAdmin.Commands;
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
    class RegistrationViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private readonly CheckConnectionService _checkConnectionService;
        private readonly RegistrationService _registrationService;

        public RegistrationViewModel(PageService pageService, CheckConnectionService connectionService, RegistrationService registrationservice)
        {
            _pageService = pageService;
            _checkConnectionService = connectionService;
            _registrationService = registrationservice;
        }

        private string login;
        private string password;
        private string repeatPassword;
        public string Login { get => login; set { login = value; RaisePropertiesChanged("Login"); } }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }

        private RelayCommand registrationCommand;
        public RelayCommand RegistrationCommand
        {
            get
            {
                return registrationCommand ??
                    (registrationCommand = new RelayCommand(param => Registration(param), param => CanReg(param)));
            }
        }

        private async void Registration(object param)
        {
            bool result = await _registrationService.RegistrationAsync(login, Password);
            if (result)
            {
               ChangeLoginPage.Execute(null);
            }
            else
            {

            }
        }

        private bool CanReg(object param)
        {
            if (Login != String.Empty && Password == RepeatPassword && Password.Length > 4)
            {
                if (_checkConnectionService.IsServerConnected)
                    return true;
            }
            return false;
        }

        public ICommand ChangeLoginPage => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new LoginPage());
        });
    }
}
