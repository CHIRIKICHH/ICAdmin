using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAdmin.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public SenderType Sender { get; set; }
        public DateTime SendTime { get; set; }


    }
    public enum SenderType
    {
        Employee = 0,
        SysAdmin = 1
    }
    
}
