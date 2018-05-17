using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IRateResposibitory
    {
        List<Rating> getlist(int countnumber = 0);
        Rating find(string id);
        void AddRates(Rating rates);
        //double countrate(string IDrate);
        //double sumrate(string ID);
        Rating check(Bakery bakery, BakeryUser user);
       
      
    }
}
