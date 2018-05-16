using DAL.Interface;
using DAL.Models;
using DoAnCK.Models;
using DoAnCK.RS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnCK.RS.Implement
{
    public class PredictAL : IPredictAL
    {


        IRateResposibitory irate;
        IMatrixParse imatrix;
        IBakeryReposibitory ibake;
        public PredictAL(IRateResposibitory irate,  IMatrixParse imatrix,IBakeryReposibitory ibake)
        {
            this.ibake = ibake;
            this.irate = irate;
            this.imatrix = imatrix;
        }
        public Neighbour[] FindNeighBour(string IdUser, int n=5)
        {
            var BakeryRow = this.imatrix.getBakeryRow();
            Neighbour[] neighbours = new Neighbour[n];
            var ColUser = this.imatrix.getUserCol();
            int k = -1;
            //prefix
            //find position in a list;
            for (int i = 0; i < ColUser.Count; i++)
            {
                if (ColUser[i].Id == IdUser)
                {
                    k = i;
                }
            }
            if (k == -1)
                return null;

            List<double> listsimilaritem = new List<double>();
            var UserMatrixItem = this.imatrix.UserItemMatrix();
            //double[] b = new double[UserMatrixItem.GetLength(0)];
            //for (int j = 0; j < UserMatrixItem.GetLength(0); j++)
            //{
            //    b[j] = UserMatrixItem[j][k];
            //}
            //trung binh rate cua user dang xet
            var averageCurrentUser = this.imatrix.average(UserMatrixItem[k]);
            for (int i = 0; i < ColUser.Count; i++)
            {


                //double[] a = new double[UserMatrixItem.GetLength(0)];
                //for (int j = 0; j < UserMatrixItem.GetLength(0); j++)
                //{
                //    a[j] = UserMatrixItem[j][i];
                //}

                // tb cua user rate check similar
                var averageUser = this.imatrix.average(UserMatrixItem[i]);
                double predictemp = 0;
                double TS = 0;
                double MS1 = 0;
                double MS2 = 0;
                for (int j = 0; j < UserMatrixItem[i].GetLength(0); j++)
                {

                    //    predictemp += ((UserMatrixItem[j][i] - averageBakery) * (UserMatrixItem[j][k] - averageCurrentBakery)) / (Math.Sqrt(Math.Pow((UserMatrixItem[j][i] - averageBakery), 2) * (Math.Pow(UserMatrixItem[j][i] - averageBakery, 2))));
                    TS += (UserMatrixItem[i][j] - averageUser) * (UserMatrixItem[k][j] - averageCurrentUser);
                    MS1 += Math.Pow((UserMatrixItem[i][j] - averageUser), 2);
                    MS2 += Math.Pow((UserMatrixItem[k][j] - averageCurrentUser), 2);

                }
                if (MS1 * MS2 != 0)
                {
                    predictemp = TS / Math.Sqrt(MS1 * MS2);
                }
                listsimilaritem.Add(predictemp);
            }
            int t = 0;

            for (int i = 0; i < n; i++)
            {

                double temp = -100;
                int x = -1;
                for (int j = 0; j < listsimilaritem.Count; j++)
                {
                    if (temp < listsimilaritem[j] && listsimilaritem[j] != 1)
                    {

                        x = j;
                        temp = listsimilaritem[j];
                    }
                }
                if (ColUser[x].Id == IdUser)
                    continue;
                // lay ra n highest correl
                Neighbour neighbour = new Neighbour
                {
                    id = ColUser[x].Id,
                    correl = listsimilaritem[x],

                };
                neighbours[t++] = neighbour;

                listsimilaritem[x] = -100;


            }


            return neighbours;
        }
        //Lấy ra list vị trí của neibour trong MatrixUserItem
        public List<int> GetListItemNeibour(Neighbour[] nei)
        {
            List<int> neibour = new List<int>();
            //double[][] matrix = imatrix.UserItemMatrix();
            List<BakeryUser> listuser = imatrix.getUserCol();
            for (int j = 0; j < 5; j++)
            {
                var temp1 = nei[j].id;
                for (int i = 0; i < listuser.Count(); i++)
                {
                    if (listuser[i].Id == temp1)
                    {
                        neibour.Add(i);
                        break;
                    }


                }
            }
            return neibour;
        }
        //tối giản ma trận
        public double[][] MatrixNeibour(List<int> list)
        {
            double[][] b = imatrix.UserItemMatrix();
            double[][] a = new double[100][];
            //a[0] = b[0];

            for (int i = 0; i < 5; i++)
            {
                a[i] = b[list[i]];
            }

            return a;
        }
        public List<double> ListAverage()
        {
            var result = new List<double>();
            
            double[][] a = imatrix.UserItemMatrix();
            var rows = imatrix.getBakeryRow();
            var cols = imatrix.getUserCol();
          
            for (int i = 0; i < cols.Count; i++)
            {
                double temp = 0;
                double score = 0;
                for (int j = 0; j < rows.Count; j++)
                {
                    temp += a[i][j];
                }
                score = temp / rows.Count;
                result.Add(score);
            }
            return result;
        }
        //chuẩn hóa ma trận
        public double[][] MatrixNeibourPreCal(List<int> list)
        {
            var rows = imatrix.getBakeryRow();
            var cols = imatrix.getUserCol();
            double[][] result = MatrixNeibour(list);
            var listaverage = ListAverage();
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < rows.Count; j++)
                {
                    if(result[i][j]>0)
                    result[i][j] = result[i][j] - listaverage[list[i]];
                }
            }
            return result;
        }
        public int FindPosAverag(string id)
        {
            int pos = 0;
            List<BakeryUser> list = imatrix.getUserCol();
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == id)
                {
                    pos = i;
                    break;
                }
            }
            return pos;
        }
        //chuẩn hóa Predict
        public List<double> ListPreDictPreCall(double[][] a,Neighbour[] b,string id)
        {
            List<double> listpre = new List<double>();
            int rows = imatrix.getBakeryRow().Count();
            var listaverage = ListAverage();
            int pos = FindPosAverag(id);
            double temp1 = 0;
            double temp2 = 0;
            double temp3 = 0;
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    temp1 += a[i][j] * b[i].correl;
                    if (a[i][j] != 0)
                    {
                        temp2 += b[i].correl;
                    }


                }
                if (temp1 == 0 || temp2 == 0)
                {
                    temp3 = 0;
                }
                else
                {
                    temp3 = temp1 / temp2 + listaverage[pos];
                }
                listpre.Add(temp3);
                temp1 = 0;
                temp2 = 0;
                temp3 = 0;


            }
            return listpre;


        }
        // cal sumproduct
        public double sumproduct(double[][] a,Neighbour[] b,int n)
        {
            double result = 0;
            for(int i = 0; i < 5; i++)
            {
                result += a[i][n] * b[i].correl;
                
            }
            return result;
        }
       
        //cal sumif
        public double sumif(double[][] a,Neighbour[] b,int n)
        {
            double result = 0;
            for(int i = 0; i < 5; i++)
            {
                if (a[i][n] != 0)
                {
                    result += b[i].correl;
                }
            }
            return result;
        }
        //cal Prsdit
        public List<double> ListPredit(double[][] a ,Neighbour[] b)
        {
            
            //------------------------------------------------
            List<double> listpre = new List<double>();
            int rows = imatrix.getBakeryRow().Count();
            double temp1 = 0;
            double temp2 = 0;
            double temp3 = 0;
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    temp1 += a[i][j] * b[i].correl;
                    if (a[i][j] != 0)
                    {
                        temp2 += b[i].correl;
                    }


                }
                if (temp1 == 0 || temp2 == 0)
                {
                    temp3 = 0;
                }
                else
                {
                    temp3 = temp1 / temp2;
                }
                listpre.Add(temp3);
                temp1 = 0;
                temp2 = 0;
                temp3 = 0;


            }
            return listpre;
        }
        public List<double> CollaborativeFiltering(string id)
        {
            
            //Protogtype Cal Correl From id User
            //Prototype Create Neiber Array from List Correl

            Neighbour[] ListNeibour = new Neighbour[1000];
            ListNeibour = FindNeighBour(id);
            //Prototype ListNeibour=NeibourArray
            //List chỉ số của neibour trong matrix user item
            List<int> ListPos = GetListItemNeibour(ListNeibour);
            //tối giản matrixuseritem
            double[][] ListNeibourFromListPos = MatrixNeibourPreCal(ListPos);
            //List predict cần tìm
            List<double> ListPreDict = ListPreDictPreCall(ListNeibourFromListPos, ListNeibour, id);

            return ListPreDict;
        }
    }
}