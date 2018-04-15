using DAL.Context;
using DAL.Interface;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implement
{
    public class BillDetailsReposibility : IBillDetailsReposibility
    {
        BakeryContext db;
        public BillDetailsReposibility(BakeryContext db)
        {
            this.db = db;
        }

        public void add(Billdetails bill)
        {
            db.billdetails.Add(bill);
            db.SaveChanges();
        }

        public void delete(Billdetails billdetails)
        {
            db.billdetails.Remove(billdetails);
            db.SaveChanges();
        }
    }
}
