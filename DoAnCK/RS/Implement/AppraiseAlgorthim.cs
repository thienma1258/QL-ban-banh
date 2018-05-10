using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnCK.RS.Implement
{
    public class AppraiseAlgorthim: IAppraiseAlgorthim
    {
        public double RMSE(int [][]Reality,int [][]prediction)
        {
            double rootmeansqaureerror = 0;
           for (int i = 0; i < Reality.GetLength(0);i++)
            {
                for (int j= 0; j < Reality.GetLength(1); j++)
                {
                    if (Reality[i][j] > 0)
                    {
                        rootmeansqaureerror += Math.Pow(Reality[i][j] - prediction[i][j], 2);
                    }
                }
            
            }
              

            
            
            
            return Math.Sqrt(rootmeansqaureerror);
        }
    }
}