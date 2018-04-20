using DAL.Context;
using DAL.Interface;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
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
        public List<Bakery> getlist(int countnumber = 0,int partiotion=1)
        {
            var list = db.Bakerys.OrderByDescending(p => p.ID);
            if (countnumber != 0)
            {
                           return list.Take(countnumber).ToList();
            }


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
        
    
       //     string query = String.Format("insert into qlbb.\"Bakery\" values('{0}','{1}',{2},to_date('{3}','dd-mm-yy hh:mi:ss am'),{4},{5},'{6}','{7}')",bakery.ID,bakery.Name,bakery.Price,bakery.ngaypost,bakery.VAT,bakery.count,bakery.category.Id,bakery.images.Id) ;
         //   var result = db.Database.ExecuteSqlCommand(query);
        }
        public void EditBakery(Bakery bakery,string Id)
        {
            var bajeryedit = db.Bakerys.SingleOrDefault(p => p.ID == bakery.ID);
            var image = db.images.SingleOrDefault(p => p.Id == Id);
            bajeryedit.Name = bakery.Name;
            bajeryedit.Price = bakery.Price;
            bajeryedit.VAT = bakery.VAT;
            bajeryedit.count = bakery.count;
            if (bakery.category!=null)
            bajeryedit.category = bakery.category;
           if(image!=null)
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

        public bool AddBakery(string name, Category category, ImageModel image, int price, float VAT, int count)
        {
            Bakery newbakery = new Bakery
            {
                ID = Guid.NewGuid().ToString(),
                ngaypost = DateTime.Now,
                Name = name,
                category = category,
                Price = price,
                VAT=VAT,
                images = image,
                count = count

            };
            this.db.Bakerys.Add(newbakery);
            this.db.SaveChanges();
            return true;
        }
    }
}
