using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ICHelp.Services
{
    public class AnyDeskService : BindableBase
    {
        private string anyDeskId;
        public string AnyDeskId { get => anyDeskId; set { anyDeskId = value; RaisePropertiesChanged("AnyDeskId"); } }

        public AnyDeskService()
        {
            GetIdAsync();
        }

        public async void GetIdAsync()
        {
           AnyDeskId = await Task.Run(() => GetId()
        );
        }
        public string GetId()
        {
            if (StartService("AnyDesk"))
            {
                string AnyDeskId = GetAnyDeskIdAsync().Result;
                if (AnyDeskId == "0")
                    AnyDeskId = GetAnyDeskIdAsync().Result;
                return AnyDeskId;
            }
            else if (File.Exists($"{Environment.CurrentDirectory}\\progs\\AnyDesk.exe"))
            {
                var process = StartProgram($"--install {Environment.CurrentDirectory}\\AnyDesk --silent");
                process.Start();
                process.WaitForExit();
                return GetId();
            }
            else
            {
                DownloadAnyDesk();
                return GetId();
            }
        }
        private Process StartProgram(string argument)
        {
            if (!File.Exists($"{Environment.CurrentDirectory}\\progs\\AnyDesk.exe"))
            {
                DownloadAnyDesk();
            }
            var process = new Process();
            process.StartInfo.FileName = $"{Environment.CurrentDirectory}\\progs\\AnyDesk.exe";
            process.StartInfo.Arguments = argument;
            process.StartInfo.UseShellExecute = false; //Отключаем любой инферфейс у процесса, чтобы небыло никаких окон
            process.StartInfo.CreateNoWindow = true; //отключаем также отображение на панели задач
            process.StartInfo.RedirectStandardOutput = true;
            return process;

        }

        private async Task<string> GetAnyDeskIdAsync()
        {
            using (var anyDeskProcess = StartProgram("--get-id"))
            {
                anyDeskProcess.Start();
                StreamReader stream = anyDeskProcess.StandardOutput;
                string result = await stream.ReadToEndAsync();
                return result;
            }
        }

        private void DownloadAnyDesk()
        {
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(new Uri("https://download.anydesk.com/AnyDesk.exe"), $"{Environment.CurrentDirectory}\\progs\\AnyDesk.exe");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        // Запуск службы
        private bool StartService(string serviceName)
        {
            try
            {
                ServiceController service = new ServiceController(serviceName);
                // Проверяем не запущена ли служба
                if (service.Status != ServiceControllerStatus.Running)
                {
                    // Запускаем службу
                    service.Start();
                    // В течении минуты ждём статус от службы
                    service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMinutes(1));
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
