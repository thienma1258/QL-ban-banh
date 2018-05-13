using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCK.RS.Interface
{
   public interface IMatrixParse
    {
         double[][] UserItemMatrix();
        double[][] Matrixglobal(double avarage, int m, int n);
        List<BakeryUser> getUserCol();
        List<Bakery> getBakeryRow();
        List<double> correlSimilarItem(string BakeryId);
        double[][] GetHighestItemSimilar(List<double> correl, out List<double> newCorrel, double[][] usermatrixitem, ref List<Bakery> ListBakery, int n, int numberUser);
    }
}
