using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAdmin.Inventorization.Models
{
    public class NetworkInterface
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string DefaultIPGateway { get; set; }
        public string DNSServerSearchOrder { get; set; }
        public string IPAdress { get; set; }
        public string IPSubnet { get; set; }
        public string MACAdress { get; set; }
        public string ServiceName { get; set; }
    }
}
