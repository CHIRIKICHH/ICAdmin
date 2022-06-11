using DevExpress.Mvvm;
using ICAdmin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace ICAdmin.Services
{
    public class AuthorizationService : BindableBase
    {
        private string _authorizationToken;
        private User _currentUser;
        public User CurrentUser { get => _currentUser; set { _currentUser = value; RaisePropertyChanged("CurrentUser"); } }
        public async Task<bool> AuthorizationAsync(string Login, string Password)
        {
            try
            {
                var client = new HttpClient();
                string resultJson = await client.GetStringAsync($"{Server.Domain}:{Server.Port}/api/Account/LoginUser?Login={Login}&Password={Password}");
                User user = JsonConvert.DeserializeObject<User>(resultJson);
                if (user.ErrorCode == null)
                {
                    CurrentUser = user;
                    return true;
                }
                else
                    return false;
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
