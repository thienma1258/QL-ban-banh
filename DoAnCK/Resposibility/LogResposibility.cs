using DAL.Context;
using DAL.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnCK.Resposibility
{
    public class LogResposibility
    {
         public static BakeryContext Bakerycontext
        {
            get
            {
                return  HttpContext.Current.GetOwinContext().Get<BakeryContext>();
            }
          
        }
        public static void LogLogin(BakeryUser user)
        {
            LogUser loguser = new LogUser()
            {
                Id = Guid.NewGuid().ToString(),
                bakeryuser = user,
                datetime = DateTime.Now,
                logstring = user.UserName + " Vừa Đăng nhập vào lúc"+DateTime.Now
            };
            Bakerycontext.logs.Add(loguser);
            Bakerycontext.SaveChanges();
        }
      public static void Log(System.Security.Principal.IPrincipal user,string Log)
        {
            var username = user.Identity.Name;
            var userlog = Bakerycontext.Users.SingleOrDefault(p => p.UserName == username);
            LogUser loguser = new LogUser()
            {
                Id=Guid.NewGuid().ToString(),
                bakeryuser = userlog,
                datetime = DateTime.Now,
                logstring=username+" Vừa "+Log
            };
            Bakerycontext.logs.Add(loguser);
            Bakerycontext.SaveChanges();


        }


    }
}