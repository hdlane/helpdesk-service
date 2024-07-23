// using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace ComputerInfo;

public class Computer
{
    public string ComputerName { get; set; }
    public string? MacAddress { get; set; }
    public string UserName { get; set; }
    public string LastContact { get; set; }
    public string IpAddress { get; set; }
    public string? Uptime { get; set; }
    public string? Os { get; set; }
    public string? Drive { get; set; }
    public string? Model { get; set; }

    public Computer()
    {
        this.ComputerName = Environment.MachineName;
        this.MacAddress = GetMac();
        this.UserName = Environment.UserName;
        this.IpAddress = GetIp();
        this.LastContact = DateTimeOffset.Now.ToString("MM/dd/yy HH:mm:ss");
        this.Uptime = GetUptime();
        this.Os = GetOs();
        this.Model = GetModel();
        this.Drive = GetDrive();
        Console.WriteLine(this.Model);
    }

    public string GetMac()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        List<string> macAddresses = [];
        if (nics == null || nics.Length < 1)
        {
            Console.WriteLine("No interfaces found");
            return "None";
        }

        foreach (NetworkInterface adapter in nics)
        {
            PhysicalAddress address = adapter.GetPhysicalAddress();
            List<string> macAddress = [];
            byte[] bytes = address.GetAddressBytes();
            for (int i = 0; i < bytes.Length; i++)
            {
                macAddress.Add($"{bytes[i].ToString("X2")}");
            }
            string mac = String.Join("-", macAddress);
            macAddresses.Add(mac);
        }

        return macAddresses[0];
    }

    public string GetIp()
    {
        List<string> computerIps = [];
        IPAddress[]? IpAddresses = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
        foreach (var ip in IpAddresses)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                computerIps.Add(ip.ToString());
            }
        }
        if (computerIps == null)
        {
            return "No IP Address";
        }
        return computerIps[0];
    }

    public string GetUptime()
    {
        var ms = Environment.TickCount64;
        TimeSpan time = TimeSpan.FromMilliseconds(ms);
        double days = Math.Floor(time.TotalDays);
        return days == 0 ? $"{days.ToString()} day" : $"{days.ToString()} days";
    }

    public static string GetOs()
    {
        //string os = Environment.OSVersion.ToString();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return $"Windows";
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return $"Linux";
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return $"MacOs";
        }
        else
        {
            return "Unknown";
        }
    }

    public string GetDrive()
    {
        DriveInfo[] allDrives = DriveInfo.GetDrives();

        if (allDrives[0].IsReady)
        {
            var drive = allDrives[0];
            long freeSpace = drive.AvailableFreeSpace / 1024 / 1024 / 1024;
            return $"{freeSpace.ToString()}GB";
        }
        return "Unknown";
    }

    public string GetModel()
    {
        // Only returning the Name field and not Manufacturer or Model
        // string model = "";
        // string wmiQuery = "SELECT * FROM Win32_ComputerSystem";
        // ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQuery);
        // ManagementObjectCollection results = searcher.Get();
        // foreach (ManagementObject result in results)
        // {
        //     Console.WriteLine(result.ToString());
        //     model += result.ToString();
        // }
        // return model;
        return "";
    }

    public void GetInfo()
    {
        this.ComputerName = Environment.MachineName;
        this.MacAddress = GetMac();
        this.UserName = Environment.UserName;
        this.IpAddress = GetIp();
        this.LastContact = DateTimeOffset.Now.ToString("MM/dd/yy HH:mm:ss");
        this.Uptime = GetUptime();
        this.Os = GetOs();
        this.Model = GetModel();
        this.Drive = GetDrive();
    }
}
