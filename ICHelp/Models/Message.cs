using System;
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
        public User? Sender { get; set; }
        public User? Recipient { get; set; }
    }
}
