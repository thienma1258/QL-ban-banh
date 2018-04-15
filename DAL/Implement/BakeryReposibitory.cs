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
    public class BakeryReposibitory: IBakeryReposibitory
    {
        private BakeryContext db;
        public BakeryReposibitory(BakeryContext db)
        {
            this.db = db;
        }
        public List<Bakery> getlist(int countnumber = 0)
        {
            var list = db.Bakerys.OrderByDescending(p => p.ID);
            if (countnumber != 0)
            return list.Take(countnumber).ToList();
            return list.ToList();

        }
        public Bakery find(string id)
        {
            var Bakery = db.Bakerys.Find(id);
            return Bakery;
        }
        public void AddBakery(Bakery bakery)
        {
            db.Bakerys.Add(bakery);
            db.SaveChanges();
        }
        public void EditBakery(Bakery bakery,string nameimage)
        {
            var bajeryedit = db.Bakerys.SingleOrDefault(p => p.ID == bakery.ID);
            var image = db.images.SingleOrDefault(p => p.Id == nameimage);
            bajeryedit.images = image;
            db.SaveChanges();
        }
        public void DeleteBakery(string id)
        {
            Bakery bakery = db.Bakerys.Find(id);
            bakery.ngaypost = DateTime.Now;
            db.Bakerys.Remove(bakery);

            db.SaveChanges();
        }

        public List<Bakery> getlist(Category category,int countnumber)
        {
            var list = db.Bakerys.OrderByDescending(p => p.ID).ToList();
            //return list;
            var listbakerynesscary = new List<Bakery>();
           
                foreach(var item in list)
                {
                    if (item.category == category)
                    {
                        listbakerynesscary.Add(item);
                    }
                if (listbakerynesscary.Count != 0 || listbakerynesscary.Count == countnumber)
                {
                    break;
                }
                }


            ///  return list.Where(p=>p.category==category).Take(countnumber).ToList();
            //return list.Where(p => p.category == category).ToList();
            return listbakerynesscary;



        }
    }
}
