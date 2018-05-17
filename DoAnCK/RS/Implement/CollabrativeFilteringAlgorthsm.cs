using DoAnCK.Models;
using DoAnCK.RS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnCK.RS.Implement
{
    public class CollabrativeFilteringAlgorthsm : ICollabrativeFilteringAlgorthsm
    {
        public double Nearest(double[] X, double[] Y)
        {
            {
                if (X.Length != Y.Length)
                    throw new ArgumentException("values must be the same length");

                var avg1 = X.Average();
                var avg2 = Y.Average();

                var sum1 = X.Zip(Y, (x1, y1) => (x1 - avg1) * (y1 - avg2)).Sum();

                var sumSqr1 = X.Sum(x => Math.Pow((x - avg1), 2.0));
                var sumSqr2 = Y.Sum(y => Math.Pow((y - avg2), 2.0));

                var result = sum1 / Math.Sqrt(sumSqr1 * sumSqr2);

                return result;
            }
        }
       
    }  
}