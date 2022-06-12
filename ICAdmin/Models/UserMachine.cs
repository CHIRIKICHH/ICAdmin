using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAdmin.Models
{
    public class UserMachine
    {
        public int Id { get; set; }
        public string InventoryName { get; set; }
        public User CurrentUser { get; set; }
        public bool IsConnected { get; set; }
    }
}
