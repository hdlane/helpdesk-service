using System.Net;
using System.Net.NetworkInformation;

namespace ComputerInfo;

public class Computer
{
    public string ComputerName { get; set; }
    public string? MacAddress { get; set; }
    public string UserName { get; set; }
    public string LastContact { get; set; }
    public string IpAddress { get; set; }

    public Computer()
    {
        this.ComputerName = Environment.MachineName;
        this.MacAddress = GetMac();
        this.UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        this.IpAddress = GetIp();
        this.LastContact = GetTime();
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
            //IPInterfaceProperties properties = adapter.GetIPProperties();
            //Console.WriteLine();
            //Console.WriteLine(adapter.Description);
            //Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));
            //Console.WriteLine("  Interface type ............................ : {0}", adapter.NetworkInterfaceType);
            PhysicalAddress address = adapter.GetPhysicalAddress();
            List<string> macAddress = [];
            byte[] bytes = address.GetAddressBytes();
            for (int i = 0; i < bytes.Length; i++)
            {
                macAddress.Add($"{bytes[i].ToString("X2")}");
            }
            string mac = String.Join("-", macAddress);
            //Console.WriteLine("  Physical address ............................ : {0}", mac);
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

    public string GetTime()
    {
        DateTime dateTime = DateTime.Now;
        return dateTime.ToString("MM/dd/yy H:mm:ss:ff");
    }

    public void GetInfo()
    {
        this.ComputerName = Environment.MachineName;
        this.MacAddress = GetMac();
        this.UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        this.IpAddress = GetIp();
        this.LastContact = GetTime();
    }
}