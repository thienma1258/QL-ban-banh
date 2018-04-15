using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
   public  interface IBakeryReposibitory
    {
         List<Bakery> getlist(int countnumber = 0);
        Bakery find(string id);
        void AddBakery(Bakery bakery);
        void EditBakery(Bakery bakery, string nameimage);
        void DeleteBakery(string id);
        List<Bakery> getlist( Category category, int countnumber = 0);
    }
}
