using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnCK.RS.Implement
{
    public interface IAppraiseAlgorthim
    {
         double RMSE(int[][] Reality, int[][] prediction);
    }
}