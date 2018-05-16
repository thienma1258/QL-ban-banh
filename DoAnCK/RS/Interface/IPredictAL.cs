using DoAnCK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCK.RS.Interface
{
    public interface IPredictAL
    {
        List<int> GetListItemNeibour(Neighbour[] nei);
        double[][] MatrixNeibour(List<int> list);
        double sumproduct(double[][] a, Neighbour[] b, int n);
        double sumif(double[][] a, Neighbour[] b, int n);
        List<double> ListPredit(double[][] a, Neighbour[] b);
        double[][] MatrixNeibourPreCal(List<int> list);
        List<double> ListAverage();
        List<double> ListPreDictPreCall(double[][] a, Neighbour[] b, string id);
        List<double> CollaborativeFiltering(string id);
    }
}
