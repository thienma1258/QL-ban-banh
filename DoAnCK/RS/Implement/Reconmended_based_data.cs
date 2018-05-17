using DAL.Interface;
using DAL.Models;
using DoAnCK.RS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnCK.RS.Implement
{
    public class Reconmended_based_data: IReconmended_based_data
    {
        IRateResposibitory irate;
        IBillReposibility ibill;
        IBakeryReposibitory ibakery;
        IMatrixParse imatrix;
        public Reconmended_based_data(IBakeryReposibitory ibakery,IRateResposibitory irate, IBillReposibility ibill, IMatrixParse imatrix)
        {
            this.irate = irate;
            this.ibill = ibill;
            this.ibakery = ibakery;
            this.imatrix = imatrix;
        }


        public List<Bakery> Reconmended_bought_also_bought(string IDBakery,int number)
        {
            var listBill = this.ibill.getlist();
            var ListBillhaveAAndBItem = new List<Bill>();
            var ListBakery = this.ibakery.getlist();
            var listHaveAItem =( from u in listBill
                                where u.billdetails.Where(p => p.Bakery.ID == IDBakery).Count() > 0
                                select u).ToList();
            if (listHaveAItem.Count == 0)
                return null;
       
          
            var listscore = new List<double>();
            foreach (var b in ListBakery)
            {
                double point = 0;
                // billl have A item and B item
                var listHaveAItemAndBitem = (from u in listHaveAItem
                                     where u.billdetails.Where(p => p.Bakery.ID == b.ID).Count() > 0
                                     select u).ToList();
                point=listHaveAItemAndBitem.Count/ listHaveAItem.Count;
                listscore.Add(point);


            }
            int n = listscore.Count;
            List<int> pos = new List<int>();
            if (number < n)
                n = number;
            var BakeryReconmended = this.imatrix.getHighNbyonvalue(ListBakery,ref listscore, n,ref pos);
            
            //
            return BakeryReconmended;
        }
    }
}