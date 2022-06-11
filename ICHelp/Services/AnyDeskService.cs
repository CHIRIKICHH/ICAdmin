﻿using System;
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
    internal class AnyDeskService
    {
        public string GetId()
        {
            if (StartService("AnyDesk"))
            {
                return GetAnyDeskId();
            }
            else if (File.Exists($"{Environment.CurrentDirectory}//progs//AnyDesk.exe")
            {
                 StartProgram("");
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
            using (var process = new Process()) {
                process.StartInfo.FileName = $"{Environment.CurrentDirectory}//progs//AnyDesk.exe";
                process.StartInfo.Arguments = argument;
                process.StartInfo.UseShellExecute = false; //Отключаем любой инферфейс у процесса, чтобы небыло никаких окон
                process.StartInfo.CreateNoWindow = true; //отключаем также отображение на панели задач
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
            return process;
            }
        }

        private string GetAnyDeskId()
        {
            using (var anyDeskProcess = StartProgram("--get-id"))
            {
                StreamReader stream = anyDeskProcess.StandardOutput;
                string result = stream.ReadToEndAsync().Result;
                return result;
            }
        }
        private async void DownloadAnyDesk()
        {
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFileAsync(new Uri("https://download.anydesk.com/AnyDesk.exe"), $"{Environment.CurrentDirectory}//progs//AnyDesk.exe");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "AnyDeskSerive Error");
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
