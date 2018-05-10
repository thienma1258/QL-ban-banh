using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace DoAnCK.Models
{
    public static class HelperClass
    {
        public static string GetIPHelper()
        {
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
           //  string IPCLIENTADDRESS;
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                 return Convert.ToString(IP);
                }
            }
            return null;

        }
    }
}