using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICHelp.Inventorization.Models

{
    public class Disk
    {
        public int Id { get; set; }
        public string? Caption { get; set; }
        public long Capacity { get; set; }
        public long FreeSpace { get; set; }
        public string? FileSystem { get; set; }
        public string? DriveLetter { get; set; }
        public DriveType DriveType { get; set; }
    }

    public enum DriveType
    {
        Unknown = 0,
        NoRootDirectory = 1,
        Removable = 2,
        Fixed = 3,
        Network = 4,
        CDRom = 5,
        Ram = 6
    }
}

