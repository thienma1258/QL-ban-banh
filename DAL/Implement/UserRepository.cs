using DAL.Context;
using DAL.Interface;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implement
{
   public class UserRepository:IUserRepository
    {
        BakeryContext db;
        public UserRepository(BakeryContext db)
        {
            this.db = db;
        }

        public  BakeryUser getcurrentUser(System.Security.Principal.IPrincipal user)
        {
            var opl = (ClaimsIdentity)user.Identity;
            var claims = opl.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email);

            var current = db.Users.SingleOrDefault(p => p.Email == claims.Value);
            return current;


        }
        public  void setImageuser(System.Security.Principal.IPrincipal user, string idimageuser)
        {
            var image = db.images.SingleOrDefault(p => p.Id == idimageuser);
            var currentuser = getcurrentUser(user);
            currentuser.representImage = image;
            db.SaveChanges();
        }

        public static void Logout()
        {

        }
        public  string getrolesuser(System.Security.Principal.IPrincipal user)
        {
            var opl = (ClaimsIdentity)user.Identity;
            var claims = opl.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Role);


            return claims.Value;
        }
    }
}
