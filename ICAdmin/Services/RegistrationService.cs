using DevExpress.Mvvm;
using ICAdmin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ICAdmin.Services
{
    class RegistrationService : BindableBase
    {
        
        public async Task<bool> RegistrationAsync(string Login, string Password)
        {
            try
            {
                var client = new HttpClient();
                string resultJson = await client.GetStringAsync($"{Server.Domain}:{Server.Port}/api/Account/CreateUser?Login={Login}&Password={Password}");
                User user = JsonConvert.DeserializeObject<User>(resultJson);
                if (user.ErrorCode == null)
                {
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
