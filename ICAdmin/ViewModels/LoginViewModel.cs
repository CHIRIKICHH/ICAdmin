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
        private readonly CheckConnectionService _checkConnectionService;
        private readonly AuthorizationService _authorizationService;

        public LoginViewModel(PageService pageService, CheckConnectionService connectionService, AuthorizationService authorizationService)
        {
            _pageService = pageService;
            _checkConnectionService = connectionService;
            _authorizationService = authorizationService;
            _authorizationService.OnAuthorized += () => ChangePage.Execute(null);
        }

        public bool IsNotLogged
        {
            get => GetValue<bool>();
            set => SetValue(value);
            
        }
        public string Login { get => GetValue<string>(); set => SetValue(value); }
         
        
        public string Password { get => GetValue<string>(); set => SetValue(value); }

        private ICommand loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return loginCommand ??
                    (loginCommand = new AsyncCommand(async () => {
                        OverlayService.GetInstance().Show("Авторизация...");
                        bool result = await _authorizationService.AuthorizationAsync(Login, Password);
                        IsNotLogged = result;
                        OverlayService.GetInstance().Close();
                    } , () => CanAuth()));
            }
        }

        private bool CanAuth()
        {
            if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password) && _checkConnectionService.IsServerConnected)
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
