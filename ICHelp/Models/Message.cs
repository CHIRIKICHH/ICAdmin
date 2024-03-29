﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICHelp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public SenderType Sender { get; set; }
        public DateTime SendTime { get; set; }
        public User User { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj is Message message)
                return message.Body == Body && message.Sender == Sender && message.SendTime.ToString() == SendTime.ToString();
            return false;
        }
    }

    public enum SenderType
    {
        Employee = 0,
        SysAdmin = 1
    }
}
