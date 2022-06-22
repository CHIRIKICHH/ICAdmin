using DevExpress.Mvvm;
using ICHelp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ICHelp.Services
{
    public class MessageService : BindableBase
    {
        private static readonly HttpClient _Client = new HttpClient();
        CheckConnectionService connection = CheckConnectionService.GetInstance();
        public MessageService()
        {
            
        }


        public async Task<bool> SendMessageAsync(Message message)
        {
            if (message != null)
            {
                string url = $"{Server.Domain}:{Server.Port}/api/Message/PostMessage";
                var json = System.Text.Json.JsonSerializer.Serialize(message);
                var response = await Request(HttpMethod.Post, url, json, new Dictionary<string, string>());
                string responseText = await response.Content.ReadAsStringAsync();
                bool result = JsonConvert.DeserializeObject<bool>(responseText);
                return result;
            }
            else
                return false;
        }

        public async Task<List<Message>> GetMessages(User user, string MotherBoardSerialNumber)
        {
            try
            {
                if (connection.IsServerConnected)
                {
                    var client = new HttpClient();
                    string resultJson = await client.GetStringAsync($"{Server.Domain}:{Server.Port}/api/Message/GetMessages?Login={user.Login}&Password={user.Password}&Name={user.Name}&MotherBoardSerialNumber={MotherBoardSerialNumber}");
                    var result = JsonConvert.DeserializeObject<List<Message>>(resultJson);
                    return result;
                }
                else
                {
                    await Task.Delay(5000);
                    return GetMessages(user, MotherBoardSerialNumber).Result;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка в сервисе получения!");
                return null;
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
