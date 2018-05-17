using DAL.Models;
using DoAnCK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCK.RS.Interface
{
    public interface IRateDampMeanAl
    {
        double damp_mean(double rui, double ui, double average, int k);
        List<BakeryRateMean> topxephang();
        List<BakeryRateMean> topxephangtatca();
        List<BakeryRateMean> topxephangngay();
        List<BakeryRateMean> topxephangnam();
        List<BakeryRateMean> topxephangthang();
    }
}
