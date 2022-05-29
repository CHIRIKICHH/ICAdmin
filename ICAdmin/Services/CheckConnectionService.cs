using ICAdmin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ICAdmin.Services
{
    internal class CheckConnectionService : BaseVM
    {
        Server server = new Server();
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
                    WebRequest request = WebRequest.Create($"{server.Domain}:{server.Port}/api");
                    request.Method = "Get";
                    WebResponse response = await request.GetResponseAsync();
                    string answer = string.Empty;
                    using (Stream s = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            answer = await reader.ReadToEndAsync();
                        }
                    }
                    response.Close();
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
    }
}
