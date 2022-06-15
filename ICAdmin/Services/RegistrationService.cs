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
    public class RegistrationService : BindableBase
    {
        private static readonly HttpClient _Client = new HttpClient();
        public async Task<bool> RegistrationAsync(string Login, string Password)
        {
            try
            {
               User user = new User() { Login = Login, Password = Hash.HashPassword(Password), Name = Login };
                if (user != null)
                {
                    string url = "http://chirikichh.ru:32000/api/Account/CreateUser";
                    var json = System.Text.Json.JsonSerializer.Serialize(user);
                    var response = await Request(HttpMethod.Post, url, json, new Dictionary<string, string>());
                    string responseText = await response.Content.ReadAsStringAsync();
                    return bool.Parse(responseText);
                }
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


        static async Task<HttpResponseMessage> Request(HttpMethod pMethod, string pUrl, string pJsonContent, Dictionary<string, string> pHeaders)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = pMethod;
            httpRequestMessage.RequestUri = new Uri(pUrl);
            foreach (var head in pHeaders)
            {
                httpRequestMessage.Headers.Add(head.Key, head.Value);
            }
            switch (pMethod.Method)
            {
                case "POST":
                    HttpContent httpContent = new StringContent(pJsonContent, Encoding.UTF8, "application/json");
                    httpRequestMessage.Content = httpContent;
                    break;

            }

            return await _Client.SendAsync(httpRequestMessage);
        }
    }


}
