using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IBranchResponsibility
    {
        List<Shop> getlist(int countnumber = 0);
        Shop find(string id);
        void AddBranch(Shop branch);
        void EditBranch(Shop branch);
        
        void DeleteBranch(string id);
    }
}
