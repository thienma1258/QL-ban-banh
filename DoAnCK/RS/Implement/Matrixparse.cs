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
            var listuniqueuser = listrate.GroupBy(p => new { p.IPADDRESS }).ToList();
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
        public List<String> getUserCol(){
            List<string > listuser=new List<string>();
                     var list = iraterepository.getlist().GroupBy(p => new { p.IPADDRESS }).ToList();
            foreach(var a in list)
            {
                listuser.Add(a.Key.IPADDRESS);
            }
            return listuser;
        }
        public List<Bakery> getBakeryRow()
        {
            return ibakeryrepository.getlist();
        }

       
    }
}