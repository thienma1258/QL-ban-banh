using DAL.Context;
using DAL.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace DoAnCK.Resposibility
{
    public class UserReposibility
    {

        public static BakeryContext Bakerycontext
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Get<BakeryContext>();
            }

        }
        public static BakeryUser getcurrentUser(System.Security.Principal.IPrincipal user)
        {
            var current = Bakerycontext.Users.SingleOrDefault(p => p.UserName == user.Identity.Name);
            return current;


        }
        public static void setImageuser(System.Security.Principal.IPrincipal user,string idimageuser)
        {
            var image = Bakerycontext.images.SingleOrDefault(p => p.Id == idimageuser);
            var currentuser = UserReposibility.getcurrentUser(user);
            currentuser.representImage = image;
            Bakerycontext.SaveChanges();
        }
     
        public static void Logout()
        {

        }
        public static string getrolesuser(System.Security.Principal.IPrincipal user)
        {
            var opl = (ClaimsIdentity)user.Identity;
            var claims = opl.Claims.FirstOrDefault(p=>p.Type==ClaimTypes.Role);
          
          
            return claims.Value;
        }
    }

    
}