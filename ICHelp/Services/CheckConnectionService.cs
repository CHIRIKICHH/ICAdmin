using ICHelp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ICHelp.Services
{
    internal class CheckConnectionService : BaseVM
    {
        private static CheckConnectionService instance;
        private bool isServerConnected;

        public bool IsServerConnected
        {
            get { return isServerConnected; }
            set
            {
                isServerConnected = value;
                OnPropertyChanged("IsServerConnected");
            }
        }

        public CheckConnectionService()
        {
            CheckServerAsync();
        }

        private async Task CheckServerAsync()
        {
            while (true)
            {
                try
                {
                    var client = new HttpClient();
                    string resultJson = await client.GetStringAsync($"{Server.Domain}:{Server.Port}/api");
                    bool result = JsonConvert.DeserializeObject<bool>(resultJson);
                    if (!IsServerConnected)
                        IsServerConnected = true;
                }
                catch
                {
                    IsServerConnected = false;
                }
                await Task.Delay(5000);
            }
        }

        public static CheckConnectionService GetInstance()
        {
            if(instance == null)
            {
                instance = new CheckConnectionService();
            }
            return instance;
        }
    }
}
