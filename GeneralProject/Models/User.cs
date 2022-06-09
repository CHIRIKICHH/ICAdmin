using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAdmin.Models
{
    internal class User
    {
        private static User currentUser;
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        public string? ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }

    }
}
