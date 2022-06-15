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
    public class RegistrationViewModel : BindableBase
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

        public string Login { get => GetValue<string>(); set => SetValue(value); }
        public string Password { get => GetValue<string>(); set => SetValue(value); }
        public string RepeatPassword { get => GetValue<string>(); set => SetValue(value); }
        public bool IsNotRegistered { get => GetValue<bool>(); set => SetValue(value); }

        private ICommand registrationCommand;
        public ICommand RegistrationCommand
        {
            get
            {
                return registrationCommand ??
                    (registrationCommand = new AsyncCommand(async () => {

                        OverlayService.GetInstance().Show("Регистрация...");
                        bool result = await _registrationService.RegistrationAsync(Login, Password);
                        if (result)
                        {
                            ChangeLoginPage.Execute(null);
                        }
                        else
                        {
                            IsNotRegistered = true;
                        }
                        OverlayService.GetInstance().Close();
                    }, () => CanReg()));
            }
        }

        private bool CanReg()
        {
            if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password) && Login.Length > 4 && Password == RepeatPassword && Password.Length > 8)
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
