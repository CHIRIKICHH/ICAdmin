using ICAdmin.Commands;
using ICAdmin.Models;
using ICAdmin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAdmin.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private string login;
        private string password;
        private bool isLogged;

        public bool IsLogged
        {
            get { return isLogged; }
            set
            {
                isLogged = value;
                OnPropertyChanged("IsLogged");
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
                OnPropertyChanged("Login");
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
                OnPropertyChanged("Password");
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
            User user = await AuthorizationService.AuthorizationAsync(Login, Password);

            if (user != null)
                IsLogged = true;
            else
                IsLogged = false;
        }

        private bool CanAuth(object param)
        {
            if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password))
                return true;
            return false;
        }
    }
}
