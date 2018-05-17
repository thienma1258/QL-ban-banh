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
        IPredictAL ipredict;
        public AppraiseAlgorthim(IRateResposibitory irate, IMatrixParse imatrix, IPredictAL ipredict)
        {
            this.irate = irate;
            this.ipredict = ipredict;
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


        public double recall(double ss)
        {
            var coluser = this.imatrix.getUserCol();
            var rowbakery = this.imatrix.getBakeryRow();
            double[][] a = new double[coluser.Count][];
            for (int i = 0; i < coluser.Count; i++)
            {




                List<double> t = this.ipredict.CollaborativeFilteringz(coluser[i].Id);
                a[i] = new double[rowbakery.Count];



                for (int j = 0; j < rowbakery.Count; j++)
                {
                    a[i][j] = t[j];
                }

            }
            var userItemMatrix = this.imatrix.UserItemMatrix();
            int dtest = 0;
            int reconmededCreate = 0;
            for (int i = 0; i < userItemMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < userItemMatrix[i].GetLength(0); j++)
                {
                    if (userItemMatrix[i][j] > 0 && a[i][j] > 0)
                    {

                        reconmededCreate++;
                        if (Math.Abs(userItemMatrix[i][j] - a[i][j]) < ss)
                            dtest++;
                    }
                }

            }
            double result=((double)dtest / reconmededCreate);
            return result;


        }
        public double User_KNN()
        {
            var coluser = this.imatrix.getUserCol();
            var rowbakery = this.imatrix.getBakeryRow();
            double[][] a = new double[coluser.Count][];
            for (int i = 0; i < coluser.Count; i++)
            {


                

                List<double> t = this.ipredict.CollaborativeFilteringz(coluser[i].Id);
                a[i] = new double[rowbakery.Count];
              

              
                for(int j = 0; j < rowbakery.Count; j++)
                {
                    a[i][j] = t[j];
                }
                
            }
            var userItemMatrix = this.imatrix.UserItemMatrix();

            return RMSE(userItemMatrix,  a);
        }
        public double ItemAvaeage()
        {
            var list = this.irate.getlist();

           
      //      var avarage = list.Where(p => p.bakery.ID == IdBakery).Average(p => p.ratestar);
            var userItemMatrix = this.imatrix.UserItemMatrix();
            var collist = this.imatrix.getUserCol();
            var rowlist = this.imatrix.getBakeryRow();
            var n = collist.Count;
            var m = rowlist.Count;
            var itemAverageMatrix = new double[n][];
            for (int i = 0; i < n; i++)
                itemAverageMatrix[i] = new double[m];
            for(int i = 0; i < m; i++)
            {
                double average = 0;

                var check = list.Where(p => p.bakery.ID == rowlist[i].ID).ToList();
                if (check.Count != 0)
                {
                    average = check.Average(p => p.ratestar);
                }
                for(int j = 0; j < n; j++)
                {
                    itemAverageMatrix[j][i] = average;
                }
            }
          

            //     var itemavaragematrix = this.imatrix.Matrixglobal(avarage, m, n);

               var RMSEUserAvarage = this.RMSE(userItemMatrix, itemAverageMatrix);
            return RMSEUserAvarage;
        }
        public double UserAverage()
        {
            var list = this.irate.getlist();
       
            var userItemMatrix = this.imatrix.UserItemMatrix();
            var collist = this.imatrix.getUserCol();
            var rowlist = this.imatrix.getBakeryRow();
            var n = collist.Count;
            var m = rowlist.Count;
            var UserAverageMatrix = new double[n][];
            for (int i = 0; i < n; i++)
            {
                double average = 0;


                var check = list.Where(p => p.User.Id == collist[i].Id);
                if (check.Count() != 0)
                {
                    average = check.Average(p => p.ratestar);
                }
                UserAverageMatrix[i] = new double[m];
                double[] a = new double[m];
                for (int j = 0; j < m; j++)
                {
                    a[j] = average;
                }
                UserAverageMatrix[i] = a;

            }
            //    var useravaragematrix = this.imatrix.Matrixglobal(avarage, m, n);
         
            var RMSEUserAvarage = this.RMSE(userItemMatrix, UserAverageMatrix);
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
                    if (Reality[i][j] > 0&&prediction[i][j]>0)
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