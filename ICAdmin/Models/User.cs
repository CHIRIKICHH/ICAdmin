using ICAdmin.Inventorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAdmin.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsConnected { get; set; }
        public DateTime LastConnection { get; set; }
        public string? ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
        public List<Message>? Messages { get; set; }
        public AnyDesk? AnyDesk { get; set; }

    }
}
