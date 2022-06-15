using ICAdmin.Inventorization.Models;
using ICAdmin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace ICAdmin.Services
{
    public class UserMachineService
    {
        public UserMachineService()
        {

        }

        public async Task<List<UserMachine>> GetUserMachinesAsync(string Login, string Password)
        {
            try
            {
                var client = new HttpClient();
                string resultJson = await client.GetStringAsync($"{Server.Domain}:{Server.Port}/api/UserMachines/AllMachines?" + HttpUtility.UrlEncode($"Login={Login}&Password={Password}"));
                var userMachines = JsonConvert.DeserializeObject<List<UserMachine>>(resultJson);
                return userMachines;
            }
            catch (WebException ex)
            {
                OverlayService.GetInstance().Show("Ошибка при получении списка компьютеров"+ Environment.NewLine + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                OverlayService.GetInstance().Show("Ошибка при получении списка компьютеров" + Environment.NewLine + ex.Message);
                return null;
            }
        }
    }
}
