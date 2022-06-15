using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAdmin.Inventorization.Models
{
    public class InventorizationData
    {
        public int Id { get; set; }
        public string InventoryName { get; set; }
        public string WindowsVersion { get; set; }
        public DateTime SetupOperationSystemDate { get; set; }
        public string MotherBoard { get; set; }
        public string MotherBoardSerialNumber { get; set; }
        public List<NetworkInterface> NetworkInterfaces { get; set; }
        public string CPU { get; set; }
        public List<Ram> RAM { get; set; }
        public List<Disk> Disks { get; set; }
        public List<GraphicsAdapter> GraphicsAdapters { get; set; }

    }
}
