using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IUserRepository
    {
        BakeryUser getcurrentUser(System.Security.Principal.IPrincipal user);
        void setImageuser(System.Security.Principal.IPrincipal user, string idimageuser);
        string getrolesuser(System.Security.Principal.IPrincipal user);

    }
}
