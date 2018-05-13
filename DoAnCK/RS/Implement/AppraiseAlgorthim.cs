using DAL.Interface;
using DoAnCK.RS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnCK.RS.Implement
{
    public class AppraiseAlgorthim: IAppraiseAlgorthim
    {

        IRateResposibitory irate;
        IMatrixParse imatrix;
        public AppraiseAlgorthim(IRateResposibitory irate, IMatrixParse imatrix)
        {
            this.irate = irate;
            this.imatrix = imatrix;
        }
        public double GlobalAvarage()
        {
            var list = this.irate.getlist();
            var avarage = list.Average(p => p.ratestar);
            var userItemMatrix = this.imatrix.UserItemMatrix();
            var collist = this.imatrix.getUserCol();
            var rowlist = this.imatrix.getBakeryRow();
            var m = collist.Count;
            var n = rowlist.Count;

            var globalaveragematrix = this.imatrix.Matrixglobal(avarage, m, n);

            var RMSEGLobalavarage = this.RMSE(userItemMatrix, globalaveragematrix);
            return RMSEGLobalavarage;

        }
        public double ItemAvaeage(string IdBakery)
        {
            var list = this.irate.getlist();
            var avarage = list.Where(p => p.bakery.ID == IdBakery).Average(p => p.ratestar);
            var userItemMatrix = this.imatrix.UserItemMatrix();
            var collist = this.imatrix.getUserCol();
            var rowlist = this.imatrix.getBakeryRow();
            var m = collist.Count;
            var n = rowlist.Count;

            var itemavaragematrix = this.imatrix.Matrixglobal(avarage, m, n);

            var RMSEUserAvarage = this.RMSE(userItemMatrix, itemavaragematrix);
            return RMSEUserAvarage;
        }
        public double UserAverage(string idUser)
        {
            var list = this.irate.getlist();
            var avarage = list.Where(p=>p.User.Id==idUser).Average(p => p.ratestar);
            var userItemMatrix = this.imatrix.UserItemMatrix();
            var collist = this.imatrix.getUserCol();
            var rowlist = this.imatrix.getBakeryRow();
            var m = collist.Count;
            var n = rowlist.Count;

            var useravaragematrix = this.imatrix.Matrixglobal(avarage, m, n);

            var RMSEUserAvarage = this.RMSE(userItemMatrix, useravaragematrix);
            return RMSEUserAvarage;
        }

        public double RMSE(double [][]Reality,double [][]prediction)
        {
            double rootmeansqaureerror = 0;
            int dtest = 0;
           for (int i = 0; i < Reality.GetLength(0);i++)
            {
                for (int j= 0; j < Reality[i].GetLength(0); j++)
                {
                    if (Reality[i][j] > 0)
                    {
                        rootmeansqaureerror = rootmeansqaureerror+ Math.Pow(Reality[i][j] - prediction[i][j], 2);
                        dtest++;
                    }
                }
            
            }
              

            
            
            
            return Math.Sqrt(rootmeansqaureerror/dtest);
        }
    }
}