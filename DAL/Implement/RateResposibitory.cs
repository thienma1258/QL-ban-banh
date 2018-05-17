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
    public class RateResposibitory : IRateResposibitory
    {
        private BakeryContext db;
        public RateResposibitory(BakeryContext db)
        {
            this.db = db;
        }
        public void AddRates(Rating rates)
        {
            db.rates.Add(rates);
            db.SaveChanges(); 
        }

        public Rating find(string id)
        {
            var news = db.rates.Find(id);
            return news;
        }

        public List<Rating> getlist(int countnumber = 0)
        {
            var list = db.rates.OrderByDescending(p => p.ID);
            if (countnumber != 0)
                return list.Take(countnumber).ToList();
            return list.ToList();
        }
        //public double countrate(string IDrate)
        //{
        //    //IDrate= "SELECT bakery_ID from QLBB.RatingUser";
        //    var listbakeryid = db.rates.Select(p => p.bakery.ID).ToList();
        //    return db.rates.Where(p => p.bakery.ID == IDrate).ToList().Count();
        //}
        //public double sumrate(string ID)
        //{       
        //    return db.rates.Where(p => p.bakery.ID == ID).ToList().Sum(p=>p.ratestar);
        //}
       
    }
}
