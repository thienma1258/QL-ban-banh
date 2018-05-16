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
    public class BranchReposibitory : IBranchReposibitory
    {
        private BakeryContext db;
        public BranchReposibitory(BakeryContext db)
        {
            this.db = db;
        }

        public void AddBranch(Shop branch)
        {
            string query = String.Format("insert into QLBB.\"Shop\" values('{0}','{1}','{2}','{3}','{4}')", branch.Id, branch.address,branch.gmail,branch.Googlemapembded,branch.SDT);
            var result = db.Database.ExecuteSqlCommand(query);
        }


        public void DeleteBranch(string id)
        {
            //var branch = db.shops.Find(id);
            //db.shops.Remove(branch);
            //db.SaveChanges();
           // var list = db.shops.OrderByDescending(p => p.Id);"
            string query = "delete from QLBB.\"Shop\" where \"Id\" = '"+id+"'";
            var result = db.Database.ExecuteSqlCommand(query);

        }

        public void EditBranch(Shop branch)
        {
            //var branchedit = db.shops.SingleOrDefault(p => p.Id == branch.Id);
            //branchedit.address = branch.address;
            //branchedit.gmail = branch.gmail;
            //branchedit.SDT = branch.SDT;
            //db.SaveChanges();
            string find = "select * from QLBB.\"Shop\" where \"Id\"='" + branch.Id + "'";
            var test = db.Database.SqlQuery<Shop>(find).SingleOrDefault();
            string query = " update QLBB.\"Shop\" set \"address\"='" + branch.address+"',\"SDT\" ='"+branch.SDT+"',\"gmail\"='"+branch.gmail+"'where \"Id\"='"+branch.Id+"'";
            var result = db.Database.ExecuteSqlCommand(query);


        }

        public Shop find(string id)
        {
            //var Shop = db.shops.Find(id);
            //return Shop;

            string query="select * from qlbb.\"Shop\" where \"Id\"='"+id+"'";
            var branch = db.Database.SqlQuery<Shop>(query).SingleOrDefault();
            return branch;
        }

        public List<Shop> getlist(int countnumber = 0)
        {
            
            var list = db.shops.OrderByDescending(p => p.Id);
            string query = "select * from QLBB.\"Shop\"";
            var results = db.Database.SqlQuery<Shop>(query).ToList();
            return results;
        }

    }
}
