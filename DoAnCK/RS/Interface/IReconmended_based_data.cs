using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCK.RS.Interface
{
    public interface IReconmended_based_data
    {
        List<Bakery> Reconmended_bought_also_bought(string IDBakery, int number);
    }
}
