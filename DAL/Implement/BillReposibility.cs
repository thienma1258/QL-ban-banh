using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Context;

namespace DAL.Implement
{
    public class BillReposibility : IBillReposibility
    {
        public BakeryContext db;
        public BillReposibility(BakeryContext db)
        {
            this.db = db;
        }

        public void AddBill(Bill bill)
        {
            db.bill.Add(bill);
            db.SaveChanges();
        }

        public bool ConfirmBill(string id)
        {
            var bill = db.bill.ToList().SingleOrDefault(p => p.confirmEmail == id);
            if (bill==null)
                return false;
            bill.confirmEmail = "true";
            db.SaveChanges();
            return true;
        }

        public void DeleteBill(Bill bill)
        {
            db.bill.Remove(bill);
            db.SaveChanges();
        }

        public void DeleteBill(string id)
        {
            var bill = db.bill.Find(id);
            db.bill.Remove(bill);
            db.SaveChanges();
        }

        public Bill getbill(string id)
        {
           return  db.bill.Find(id);
        }

        public List<Bill> getlist(int countnumber = 0)
        {
            if(countnumber==0)
            return db.bill.ToList();
            return db.bill.OrderByDescending(p => p.Id).Take(countnumber).ToList();
        }
    }
}
