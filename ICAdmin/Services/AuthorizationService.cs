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
    class AuthorizationService
    {
        private static CheckConnectionService _connectionService = CheckConnectionService.GetInstance();
        private string _authorizationToken;
        public static async Task<User> AuthorizationAsync(string Login, string Password)
        {
            if (_connectionService.IsServerConnected)
                try
                {
                    var client = new HttpClient();
                    string resultJson = await client.GetStringAsync($"{Server.Domain}:{Server.Port}/api/Account/LoginUser?Login={Login}&Password={Password}");
                    User user = JsonConvert.DeserializeObject<User>(resultJson);
                    if (user.ErrorCode == null)
                        return user;
                    else
                        return null;
                }
                catch (WebException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            else
            {
                MessageBox.Show("Нет подключения к серверу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
    }
}
