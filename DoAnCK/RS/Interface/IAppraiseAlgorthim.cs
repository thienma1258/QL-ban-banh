using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnCK.RS.Implement
{
    public interface IAppraiseAlgorthim
    {
         double RMSE(double [][] Reality, double[][] prediction);
        double GlobalAvarage();
        double ItemAvaeage(string IdBakery);
        double UserAverage(string idUser);
    }
}