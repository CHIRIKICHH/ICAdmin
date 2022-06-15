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
        private static readonly HttpClient _Client = new HttpClient();

        public event Action OnAuthorized;

        private User _currentUser;
        public string AuthorizationToken { get; set; }
        public User CurrentUser { get => _currentUser; set { _currentUser = value; OnAuthorized.Invoke(); RaisePropertyChanged("CurrentUser"); } }
        public async Task<bool> AuthorizationAsync(string Login, string Password)
        {
            try
            {
                
                    string url = "http://chirikichh.ru:32000/api/Account/LoginUser";
                    var json = JsonConvert.SerializeObject(new {Login = Login, Password = Hash.HashPassword(Password)});
                    var response = await Request(HttpMethod.Post, url, json, new Dictionary<string, string>());
                    User user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
                if (user.ErrorCode == null)
                {
                    CurrentUser = user;
                    return false;
                }
                else
                    return true;
            }
            catch (WebException ex)
            {
                OverlayService.GetInstance().Show("Ошибка Авторизации" + Environment.NewLine + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                OverlayService.GetInstance().Show("Ошибка Авторизации" + Environment.NewLine + ex.Message);
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
