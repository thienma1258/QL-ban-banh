using DAL.Context;
using DAL.Enum;
using DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Startup
    {

      public   BakeryContext bakerycontext;
       public Startup(BakeryContext bakerycontext)
        {
            this.bakerycontext = bakerycontext;
        }

        public  void initialisedatabase()
        {
            
          
            var UserManager = new UserManager<BakeryUser>(new UserStore<BakeryUser>(bakerycontext));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(bakerycontext));

            var listoles = bakerycontext.Roles.ToList();
            if (!listoles.Any())
            {
                IdentityResult roleResult;

                if (!RoleManager.RoleExists(AdminRoles.Admin.ToString()))
                {
                    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    role.Name = AdminRoles.Admin.ToString();
                    roleResult = RoleManager.Create(role);
                }
                if (!RoleManager.RoleExists(AdminRoles.Editor.ToString()))
                {
                    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    role.Name = AdminRoles.Editor.ToString();
                    roleResult = RoleManager.Create(role);
                }
            }
            var listuser = bakerycontext.Users.ToList();
            if (!listuser.Any())
            {
                var user = new BakeryUser();
                user.UserName = "Admin";
                user.Email = "cpud1258@gmail.com";
                UserManager.Create(user,"123456");
                UserManager.AddToRole(user.Id, AdminRoles.Admin.ToString());
                var editoruser = new BakeryUser();
                editoruser.UserName = "Editor";
                editoruser.Email = "thienma1258z@gmail.com";
                UserManager.Create(editoruser, "123456");
                UserManager.AddToRole(editoruser.Id, AdminRoles.Editor.ToString());

            }
        }
    }
}
