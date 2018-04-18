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
    public class BranchResponsibility : IBranchResponsibility
    {
        private BakeryContext db;
        public BranchResponsibility(BakeryContext db)
        {
            this.db = db;
        }

        public void AddBranch(Shop branch)
        {

            db.shops.Add(branch);
            db.SaveChanges();
        }


        public void DeleteBranch(string id)
        {
            var branch = db.shops.Find(id);
            db.shops.Remove(branch);
            db.SaveChanges();
        }

        public void EditBranch(Shop branch)
        {
            var branchedit = db.shops.SingleOrDefault(p => p.Id == branch.Id);
            branchedit.address = branch.address;
            branchedit.gmail = branch.gmail;
            branchedit.SDT = branch.SDT;
            db.SaveChanges();
        }

        public Shop find(string id)
        {
            var Shop = db.shops.Find(id);
            return Shop;
        }

        public List<Shop> getlist(int countnumber = 0)
        {
            var list = db.shops.OrderByDescending(p => p.Id);
            if (countnumber != 0)
                return list.Take(countnumber).ToList();
            return list.ToList();
        }

    }
}
