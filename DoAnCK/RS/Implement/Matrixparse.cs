using DAL.Interface;
using DAL.Models;
using DoAnCK.RS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnCK.RS.Implement
{
    public class MatrixParse:IMatrixParse
    {
        IBakeryReposibitory ibakeryrepository;
        IRateResposibitory iraterepository;
       
        public MatrixParse(IBakeryReposibitory ibakeryrepository, IRateResposibitory iraterepository)
        {
            this.ibakeryrepository = ibakeryrepository;
            this.iraterepository = iraterepository;
            
        }
        public  double[][] Matrixglobal(double avarage, int m, int n)
        {
            double[][] myArray = new double[m][];
            for (int i = 0; i < m; i++)
            {
                myArray[i] = new double[n];
                for (int j = 0; j < n; j++)
                {
                    myArray[i][j] = avarage;
                }
            }
            return myArray;
        }
        public  double[][] UserItemMatrix()
        {
            var listbakery = ibakeryrepository.getlist();
            var listrate = iraterepository.getlist();
            var listuniqueuser = listrate.GroupBy(p => new { p.User }).ToList();
            int col = listuniqueuser.Count();
            int row = listbakery.Count;
            double[][] useritemMatrix = new double[col][];
           for (int i = 0; i < col; i++)
            {
                useritemMatrix[i] = new double[row];
                for( int j = 0; j < row; j++)
                {
                    var check = listuniqueuser[i].Where(p => p.bakery == listbakery[j]).FirstOrDefault();
                    if (check == null)
                    {
                        useritemMatrix[i][j] = 0;
                    }
                    else
                    {
                        useritemMatrix[i][j] = check.ratestar;
                    }
                  
                    
                }
            }
            return useritemMatrix;

        }
        public List<BakeryUser> getUserCol(){
            List<BakeryUser > listuser=new List<BakeryUser>();
                     var list = iraterepository.getlist().GroupBy(p => new { p.User }).ToList();
            foreach(var a in list)
            {
                listuser.Add(a.Key.User);
            }
            return listuser;
        }
        public List<Bakery> getBakeryRow()
        {
            return ibakeryrepository.getlist();
        }


        public double average(double[] a)
        {
            int m = a.GetLength(0);
            double temp = 0;
            int z = 0;
            for (int i = 0; i < m; i++)
            {
                
                temp += a[i];
               
            }
            return temp / m;
            

        }
        public List<double> correlSimilarItem(string BakeryId)
        {
            //var bakery = this.ibakeryrepository.find(BakeryId);
            var BakeryRow = this.getBakeryRow();
           
            int k = -1;
            //prefix
            //find position in a list;
            for(int i=0;i<BakeryRow.Count;i++)
            {
                if (BakeryRow[i].ID == BakeryId)
                {
                    k = i;  
                }
            }
            if (k == -1)
                return null;

            List<double> listsimilaritem = new List<double>();
            var UserMatrixItem = this.UserItemMatrix();
            double[] b = new double[UserMatrixItem.GetLength(0)];
            for (int j = 0; j < UserMatrixItem.GetLength(0); j++)
            {
                b[j] = UserMatrixItem[j][k];
            }
            //trung binh cua banh dang xet
            var averageCurrentBakery = average(b);
            for (int i = 0; i < BakeryRow.Count; i++)
            {


                double[] a = new double[UserMatrixItem.GetLength(0)];
                for (int j = 0; j < UserMatrixItem.GetLength(0); j++)
                {
                    a[j] = UserMatrixItem[j][i];
                }

                // tb cua banh check similar
                var averageBakery = average(a);
                double predictemp = 0;
                double TS = 0;
                double MS1 = 0;
                double MS2 = 0;
                for(int j = 0; j < UserMatrixItem.GetLength(0); j++)
                {
                    
                        //    predictemp += ((UserMatrixItem[j][i] - averageBakery) * (UserMatrixItem[j][k] - averageCurrentBakery)) / (Math.Sqrt(Math.Pow((UserMatrixItem[j][i] - averageBakery), 2) * (Math.Pow(UserMatrixItem[j][i] - averageBakery, 2))));
                        TS += (UserMatrixItem[j][i] - averageBakery) * (UserMatrixItem[j][k] - averageCurrentBakery);
                        MS1 += Math.Pow((UserMatrixItem[j][i] - averageBakery), 2);
                        MS2 += Math.Pow((UserMatrixItem[j][k] - averageCurrentBakery), 2);
                    
                 }
                if (MS1*MS2 != 0)
                {
                    predictemp = TS /Math.Sqrt( MS1*MS2);
                }
                listsimilaritem.Add(predictemp);
            }
            return listsimilaritem ;
        }
        public List<Bakery> getHighNbyonvalue(List<Bakery> listbakery, ref List<double> score, int n,ref List<int> position)
        {
            List<double> newScore = new List<double>();
            List<Bakery> listnewBakery = new List<Bakery>();
            for (int i = 0; i < n; i++)
            {

                double temp = -100;
                int k = -1;
                for (int j = 0; j < score.Count; j++)
                {
                    if (temp < score[j] && score[j] != 1)
                    {

                        k = j;
                        temp = score[j];
                    }
                }

                newScore.Add(score[k]);
                listnewBakery.Add(listbakery[k]);
                position.Add(k);
                score[k] = -100;


            }
            score = newScore;
            return listnewBakery;
        }
        public double[][] GetHighestItemSimilar(List<double> correl,out List<double> newCorrel,double[][] usermatrixitem,ref List<Bakery> ListBakery,int n,int numberUser)
        {
            double[][] a = new double[numberUser][];
            List<Bakery> listnewBakery = new List<Bakery>();
              newCorrel = new List<double>();
            List<int> listpos = new List<int>();
           listnewBakery= getHighNbyonvalue(ListBakery,ref correl, n, ref listpos);
            //for(int i = 0; i < n; i++)
            //{

            //    double temp = -100;
            //    int k = -1;
            //    for(int j = 0; j < correl.Count; j++)
            //    {
            //        if (temp < correl[j]&&correl[j]!=1)
            //        {

            //            k = j;
            //            temp = correl[j];
            //        }
            //    }
            //   // lay ra n highest correl

            //    newCorrel.Add(correl[k]);
            //    listnewBakery.Add(ListBakery[k]);
            //    listpos.Add(k);
            //    correl[k] = -100;


            //}
            newCorrel = correl;
            //initialise matrix
            for (int i = 0; i < numberUser; i++)
            {
                a[i] = new double[n];
           
            }


            //toi gian ma tran;
            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < numberUser; i++)
                {
                    a[i][k] = usermatrixitem[i][listpos[k]];
                }
            }
            ListBakery = listnewBakery;
            return a;
        }
       


    }
}