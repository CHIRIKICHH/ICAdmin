using ICAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAdmin
{
    public class Message
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public SenderType Sender { get; set; }
        public User User { get; set; }
        public DateTime SendTime { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj is Message message)
                return message.Id == Id && message.Sender == Sender && message.SendTime == SendTime;
            return false;
        }
    }

    public enum SenderType
    {
        Employee = 0,
        SysAdmin = 1
    }
}
