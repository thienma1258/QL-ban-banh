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
         List<Bakery> getlist(int countnumber = 0,int partition=1);
            
        Bakery find(string id);
        void AddBakery(Bakery bakery);
        bool AddBakery(string name,Category category,ImageModel image,int price,float VAT,int count);
        void EditBakery(Bakery bakery, string nameimage);
        void DeleteBakery(string id);
        List<Bakery> getlist( Category category, int countnumber = 0);
    }
}
