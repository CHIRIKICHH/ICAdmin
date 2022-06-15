using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm;
using ICHelp.Models;
using Newtonsoft.Json;

namespace ICHelp.Services
{
    public class RegistrationService : BindableBase
    {
        private bool isRegistrated;
        public bool IsRegistrated { get => isRegistrated; set { isRegistrated = value; RaisePropertiesChanged("IsRegistrated"); } }
        CheckConnectionService connection = CheckConnectionService.GetInstance();
        private static readonly HttpClient _Client = new HttpClient();

        public RegistrationService()
        {

        }

        public async void Start()
        {
            if (connection.IsServerConnected)
            {
                await Task.Run(() =>
                {
                    if (CheckRegistrationAsync().Result)
                    {
                        IsRegistrated = true;
                    }
                    else
                    {
                        RegistrationAsync();
                    }
                });
            }
            else
            {
                await Task.Delay(5000);
                Start();
            }
        }
        public async Task<bool> CheckRegistrationAsync()
        {
            try
            {
                var client = new HttpClient();
                string resultJson = await client.GetStringAsync($"{Server.Domain}:{Server.Port}/api/UserMachines/IsMachineRegistrated?MotherBoardSerialNumber={SystemInformationService.GetWMIInfo("Win32_BaseBoard", "SerialNumber").Trim()}");
                bool result = JsonConvert.DeserializeObject<bool>(resultJson);
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка регистрации!");
                return false;
            }
        }

        public async void RegistrationAsync()
        {

            var info = new SystemInformationService();
            var machine = await info.GetSystemInfoAsync();
            if (machine != null)
            {
                string url = $"http://{Server.Domain}:{Server.Port}/api/UserMachines/RegistrationMachine";
                var json = System.Text.Json.JsonSerializer.Serialize(machine);
                var response = await Request(HttpMethod.Post, url, json, new Dictionary<string, string>());
                string responseText = await response.Content.ReadAsStringAsync();
                IsRegistrated = bool.Parse(responseText);
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
