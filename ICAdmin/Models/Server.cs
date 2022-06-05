using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ICAdmin.Models
{
    internal class Server : BaseVM
    {
        public static readonly string Domain = "http://chirikichh.ru";
        public static readonly int Port = 32000;
        public static bool IsConnected { get; set; }
        public Server()
        {

        }

    }
}
