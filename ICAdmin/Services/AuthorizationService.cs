using ICAdmin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace ICAdmin.Services
{
    class AuthorizationService
    {
        private static Server server = new Server();
        public static async Task<User> GetUser(string Login, string Password)
        {
            try
            {
                WebRequest request = WebRequest.Create($"{server.Domain}:{server.Port}/api/Account/LoginUser?Login={Login}&Password={Password}");
                request.Method = "GET";
                WebResponse response = await request.GetResponseAsync();
                string answer = string.Empty;
                User? user = null;
                using (Stream s = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {

                        user = await JsonSerializer.DeserializeAsync<User>(reader.BaseStream);
                    }
                }
                response.Close();
                return user;
            }
            catch (WebException ex)
            {
                MessageBox.Show("Ошибка", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
    }
}
