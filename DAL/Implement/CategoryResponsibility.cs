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
    public class CategoryResponsibility : ICategoryResponsibility
    {
        private BakeryContext db;
        public CategoryResponsibility(BakeryContext db)
        {
            this.db = db;
        }

        public void AddCate(Category name)
        {
            db.Categorys.Add(name);
            db.SaveChanges();
        }

        public void Delete(string id)
        {
            var cate = db.Categorys.Find(id);
            db.Categorys.Remove(cate);
            db.SaveChanges();
        }

        public void Edit(Category id)
        {
            var cate = db.Categorys.SingleOrDefault(p => p.Id == id.Id);
            db.SaveChanges();
        }

        public Category find(string id)
        {
            var cate = db.Categorys.Find(id);
            return cate;
        }

        public List<Category> getlist(int countnumber = 0)
        {
            var list = db.Categorys.OrderByDescending(p => p.Id);
            if (countnumber != 0)
            {
                return list.Take(countnumber).ToList();
            }
            return list.ToList();
        }
    }
}
