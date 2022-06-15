using DevExpress.Mvvm;
using ICAdmin.Inventorization.Models;
using ICAdmin.Models;
using Newtonsoft.Json;
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

namespace ICAdmin.Services
{
    public class AnyDeskService : BindableBase
    {

        //public async Task<string> GetCurrentAnyDeskId()
        //{
        //    try
        //    {
        //        var client = new HttpClient();
        //        string resultJson = await client.GetStringAsync($"{Server.Domain}:{Server.Port}/api");
        //        string anyDeskId = JsonConvert.DeserializeObject<string>(resultJson);
        //        return anyDeskId;
        //    }
        //    catch (WebException ex)
        //    {
        //        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return "";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return "";
        //    }
        //}

        public void ConnectToAnyDesk(UserMachine machine, string mode = "--plain")
        {
            if (File.Exists($"{Environment.CurrentDirectory}\\progs\\AnyDesk.exe"))
            {
                var info = new ProcessStartInfo();
                info.UseShellExecute = false;
                info.CreateNoWindow = true;
                info.Arguments = "/C " + $"echo {machine.CurrentUser.AnyDesk.Password} | {Environment.CurrentDirectory}\\progs\\AnyDesk.exe {machine.CurrentUser.AnyDesk.AnyDeskId} --with-password {mode}";
                info.FileName = "cmd.exe";
                Process.Start(info);
            }
            else
            {
                DownloadAnyDesk();
                ConnectToAnyDesk(machine);
            }
        }

        public AnyDeskService()
        {
            SetPassword();
        }

        public async void SetPassword()
        {
            await Task.Run(async () =>
            {
                if (File.Exists($"{Environment.CurrentDirectory}\\progs\\AnyDesk.exe"))
                {
                    var info = new ProcessStartInfo();
                    info.UseShellExecute = false;
                    info.CreateNoWindow = true;
                    info.Arguments = "/C " + $"echo eoJG0vaXdSaO | {Environment.CurrentDirectory}\\progs\\AnyDesk.exe --set-password _unattended_access";
                    info.FileName = "cmd.exe";
                    Process.Start(info);
                }
                else
                {
                    DownloadAnyDesk();
                    SetPassword();
                }
            });
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
