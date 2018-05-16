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
    public class RateReposibitory : IRateReposibitory

    {
        private BakeryContext db;
        public RateReposibitory (BakeryContext db)
        {
            this.db = db;
        }
        public List<Rating> getlist()
        {
            return this.db.rates.ToList();
        }
        public void AddRate(Rating rate)
        {
            db.rates.Add(rate);
            db.SaveChanges();
        }
    }
}
