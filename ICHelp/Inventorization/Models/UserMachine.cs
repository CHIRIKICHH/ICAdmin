using ICHelp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICHelp.Inventorization.Models
{
    public class UserMachine
    {
        public int Id { get; set; }
        public User CurrentUser { get; set; }
        public string InventoryName { get; set; }
        public string WindowsAccountName { get; set; }
        public DateTime LastActivity { get; set; }
        public bool IsConnected { get; set; }
        public InventorizationData InventorizationData { get; set; }
    }
}
