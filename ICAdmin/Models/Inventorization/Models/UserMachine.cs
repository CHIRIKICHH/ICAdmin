using DevExpress.Mvvm;
using ICAdmin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAdmin.Inventorization.Models
{
    public class UserMachine:BindableBase
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("CurrentUser")]
        public User CurrentUser { get; set; }
        [JsonProperty("InventoryName")]
        public string InventoryName { get; set;  }
        [JsonProperty("WindowsAccountName")]

        public string WindowsAccountName { get; set; }
        public DateTime LastActivity { get; set; }
        public bool IsConnected { get; set; } 
        public InventorizationData InventorizationData { get; set; }
    }
}
