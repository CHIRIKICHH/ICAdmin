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
                    (loginCommand = new RelayCommand(obj =>
                    {
                        User user = AuthorizationService.GetUser(Login, Password).Result;
                        if (user != null)
                            IsLogged = true;
                        else
                            IsLogged = false;
                    }));
            }
        }
    }
}
