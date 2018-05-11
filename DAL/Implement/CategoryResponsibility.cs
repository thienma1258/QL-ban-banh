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
            //db.Categorys.Add(name);
            //db.SaveChanges();
            string query = String.Format("insert into qlbb.\"Category\" values('{0}','{1}')", name.Id, name.Name);
            var result = db.Database.ExecuteSqlCommand(query);
        }

        public void Delete(string id)
        {
            //var cate = db.Categorys.Find(id);
            //db.Categorys.Remove(cate);
            //db.SaveChanges();
            //return list.ToList();
            string query = "delete from qlbb.\"Category\" where\"Id\"='" + id + "'";
            var result = db.Database.ExecuteSqlCommand(query);

        }

        public void Edit(Category id)
        {
            //var cate = db.Categorys.SingleOrDefault(p => p.Id == id.Id);
            string query = "select * from qlbb.\"Category\" where\"Id\"='" + id.Id + "'";
            var cate = db.Database.SqlQuery<Category>(query).SingleOrDefault();
            //cate.Name = id.Name;
            string query1 = "Update qlbb.\"Category\" Set \"Name\"='" + id.Name + "'where \"Id\" ='" + id.Id + "'";
            //b.SaveChanges();
            var result = db.Database.ExecuteSqlCommand(query1);
        }

        public Category find(string id)
        {

            var cate = db.Categorys.Find(id);
            //string query = "select * from qlbb.\"Category\" where\"Id\"='" + id + "'";
            //var cate = db.Database.SqlQuery<Category>(query).SingleOrDefault();
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
            //string query = "select * from qlbb.\"Category\"";
            //var results = db.Database.SqlQuery<Category>(query).ToList();
            //return results;
        }
        public Category SearchByName(string name) {

            var categorys = db.Categorys.ToList();
            
            foreach(var category in categorys)
            {
                if (category.Name==name)
                {
                    return category;
                }
            }
            return null;


            //string query = "select * from qlbb.\"Category\" where\"Name\" like '" + name + "'";
            //var cate = db.Database.SqlQuery<Category>(query).ToList().SingleOrDefault();
            //return cate;
        }
    }
}
