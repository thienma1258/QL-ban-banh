using DAL.Context;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
   public  class CategoryReposibility
    {
        private BakeryContext db;
        public CategoryReposibility(BakeryContext db)
        {
            this.db = db;
        }
        public List<Category> getlist()
        {
            return db.Categorys.ToList();
        }


    }
}
