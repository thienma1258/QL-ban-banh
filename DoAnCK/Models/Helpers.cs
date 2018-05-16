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
            //localhost
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                   return  Convert.ToString(IP);
                }
            }
            //wlan 
            String ip =
             HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ip))
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            else
                ip = ip.Split(',')[0];

            return ip;
        }

       
    }

 }

