using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCK.RS.Interface
{
    public interface IRateDampMeanAl
    {
        double damp_mean(int k, double rui, double ui, double average);
        List<Bakery> topxephang();
    }
}
