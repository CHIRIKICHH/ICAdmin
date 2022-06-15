using ICHelp.Inventorization.Models;
using ICHelp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ICHelp.Services
{
    public class SystemInformationService
    {
        public SystemInformationService()
        {

        }

        public async Task<UserMachine> GetSystemInfoAsync()
        {
            try
            {
                return await Task.Run(() =>
                 new UserMachine()
                 {
                     CurrentUser = new User()
                     {
                         Name = Environment.UserName,
                         Login = Environment.UserName,
                         Password = "eoJG0vaXdSaO",
                         AnyDesk = new AnyDesk()
                         {
                             AnyDeskID = new AnyDeskService().GetId(),
                             Password = "eoJG0vaXdSaO"
                         }
                     },
                     InventorizationData = GetInventorizationData(),
                     InventoryName = GetInventorizationName().Result,
                     WindowsAccountName = Environment.UserName,
                 });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка получения информации о системе");
                return null;
            }
        }


        private InventorizationData GetInventorizationData()
        {
            try
            {
                var Invent = new InventorizationData()
                {
                    InventoryName = GetInventorizationName().Result,
                    WindowsVersion = GetWMIInfo("Win32_OperatingSystem", "Caption", "Version"),
                    SetupOperationSystemDate = DateTime.Parse(GetWMIInfo("Win32_OperatingSystem", "InstallDate").Insert(4, "-").Insert(7, "-").Remove(10)),
                    MotherBoard = GetWMIInfo("Win32_BaseBoard", "Product"),
                    MotherBoardSerialNumber = GetWMIInfo("Win32_BaseBoard", "SerialNumber").Trim(),
                    CPU = GetWMIInfo("Win32_Processor", "Name"),
                    RAM = GetRam(),
                    Disks = GetDisks(),
                    NetworkInterfaces = GetNetworkInterfaces(),
                    GraphicsAdapters = GetGrapchickAdapters(),
                };
                return Invent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка инвенторизации");
                return null;
            }
        }
        private async Task<string> GetInventorizationName()
        {
            try
            {
                var client = new HttpClient();
                string resultJson = await client.GetStringAsync($"{Server.Domain}:{Server.Port}/api/UserMachines/GetInventoryName");
                if (resultJson.Length != 6)
                    return resultJson;
                else
                    return "";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка получения инвентарного номера");
                return "Error";
            }
        }
        public static string GetWMIInfo(string TableName, params string[] ObjectName)
        {
            string result = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM {TableName}");

            foreach (ManagementObject obj in searcher.Get())
            {
                for (int i = 0; i < ObjectName.Length; i++)
                    try
                    {
                        result += obj[ObjectName[i]]?.ToString() + " ";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
            }
            return result;
        }

        private List<Disk> GetDisks()
        {
            ManagementObjectSearcher searcher =
            new ManagementObjectSearcher("root\\CIMV2",
            "SELECT * FROM Win32_Volume");
            List<Disk> disks = new List<Disk>();
            foreach (ManagementObject queryObj in searcher.Get())
            {
                disks.Add(new Disk()
                {
                    Caption = queryObj["Caption"].ToString(),
                    Capacity = long.Parse(queryObj["Capacity"].ToString()),
                    FreeSpace = long.Parse(queryObj["FreeSpace"].ToString()),
                    DriveLetter = queryObj["DriveLetter"] != null ? queryObj["DriveLetter"].ToString() : "",
                    DriveType = (DriveType)int.Parse(queryObj["DriveType"].ToString()),
                    FileSystem = queryObj["FileSystem"].ToString()
                });
            }
            return disks;
        }

        private List<Ram> GetRam()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");
            List<Ram> ramList = new List<Ram>();

            foreach (ManagementObject queryObj in searcher.Get())
            {
                ramList.Add(new Ram()
                {
                    BankLevel = queryObj["BankLabel"].ToString(),
                    Capacity = Math.Round(Convert.ToDouble(queryObj["Capacity"]) / 1024 / 1024 / 1024, 2),
                    Speed = int.Parse(queryObj["Speed"].ToString())
                });
            }
            return ramList;


        }

        private List<NetworkInterface> GetNetworkInterfaces()
        {
            List<NetworkInterface> interfaces = new List<NetworkInterface>();
            ManagementObjectSearcher searcher =
   new ManagementObjectSearcher("root\\CIMV2",
   "SELECT * FROM Win32_NetworkAdapterConfiguration");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                string Caption = queryObj["Caption"].ToString();
                string DefaultIPGateway = "";
                string DNSServerSearchOrder = "";
                string IPAdress = "";
                string IPSubnet = "";
                string MACAdress = "";
                string ServiceName = "";
                if (queryObj["DefaultIPGateway"] == null)
                    DefaultIPGateway = "";
                else
                {
                    String[] arrDefaultIPGateway = (String[])(queryObj["DefaultIPGateway"]);
                    foreach (String arrValue in arrDefaultIPGateway)
                    {
                        DefaultIPGateway += arrValue + Environment.NewLine;
                    }
                }

                if (queryObj["DNSServerSearchOrder"] == null)
                    DNSServerSearchOrder = "";
                else
                {
                    String[] arrDNSServerSearchOrder = (String[])(queryObj["DNSServerSearchOrder"]);
                    foreach (String arrValue in arrDNSServerSearchOrder)
                    {
                        DNSServerSearchOrder += arrValue + Environment.NewLine;
                    }
                }

                if (queryObj["IPAddress"] == null)
                    IPAdress = "";
                else
                {
                    String[] arrIPAddress = (String[])(queryObj["IPAddress"]);
                    foreach (String arrValue in arrIPAddress)
                    {
                        IPAdress += arrValue + Environment.NewLine;
                    }
                }

                if (queryObj["IPSubnet"] == null)
                    IPSubnet = "";
                else
                {
                    String[] arrIPSubnet = (String[])(queryObj["IPSubnet"]);
                    foreach (String arrValue in arrIPSubnet)
                    {
                        IPSubnet += arrValue + Environment.NewLine;
                    }
                }
                MACAdress = queryObj["MACAddress"] != null ? queryObj["MACAddress"].ToString() : "";
                ServiceName = queryObj["ServiceName"] != null ? queryObj["ServiceName"].ToString() : "";
                interfaces.Add(new NetworkInterface()
                {
                    Caption = Caption,
                    DefaultIPGateway = DefaultIPGateway,
                    DNSServerSearchOrder = DNSServerSearchOrder,
                    IPAdress = IPAdress,
                    IPSubnet = IPSubnet,
                    MACAdress = MACAdress,
                    ServiceName = ServiceName
                });
            }
            return interfaces;
        }

        private List<GraphicsAdapter> GetGrapchickAdapters()
        {
            List<GraphicsAdapter> adapters = new List<GraphicsAdapter>();
            ManagementObjectSearcher searcher11 =
    new ManagementObjectSearcher("root\\CIMV2",
    "SELECT * FROM Win32_VideoController");

            foreach (ManagementObject queryObj in searcher11.Get())
            {
                adapters.Add(new GraphicsAdapter()
                {
                    AdapterRam = int.Parse(queryObj["AdapterRAM"].ToString()),
                    Caption = queryObj["Caption"].ToString(),
                    Description = queryObj["Description"].ToString(),
                    VideoProcessor = queryObj["VideoProcessor"].ToString()
                });

            }
            return adapters;
        }
    }
}
