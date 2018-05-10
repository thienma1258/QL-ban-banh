using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnCK.Models
{
    public static class HelperClass
    {
        public static string GetIPHelper()
        {
            string ip = HttpContext.Current.Request.UserHostAddress;
            return ip;
        }
    }
}