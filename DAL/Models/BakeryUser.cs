using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
   public class BakeryUser: IdentityUser
    {
        public virtual ImageModel representImage { get; set; }
          
        public virtual List<LogUser> loguser { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<BakeryUser> manager,BakeryUser user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            userIdentity.AddClaim(new Claim(ClaimTypes.Role, user.Roles.ToString()));
            return userIdentity;
        }
    }
}
