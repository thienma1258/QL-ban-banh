using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
  public  interface IBillReposibility
    {
        List<Bill> getlist(int countnumber = 0);
        Bill getbill(string id);
        void AddBill(Bill bill);
        void ConfirmBill(string id);
        void DeleteBill(string id);

    }
}
