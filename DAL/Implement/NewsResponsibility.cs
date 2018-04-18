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
    public class NewsResponsibility : INewsResponsibility
    {
        private BakeryContext db;
        public NewsResponsibility(BakeryContext db)
        {
            this.db = db;
        }
        public void AddNews(News news)
        {
            db.news.Add(news);
            db.SaveChanges();
        }

        public void DeleteNews(string id)
        {
            var news = db.news.Find(id);
            db.news.Remove(news);
            db.SaveChanges();
        }

        public void EditNews(News news)
        {
            var newsedit = db.news.SingleOrDefault(p => p.ID == news.ID);

            db.SaveChanges();
        }

        public News find(string id)
        {
            var news = db.news.Find(id);
            return news;
        }

        public List<News> getlist(int countnumber = 0)
        {

            var list = db.news.OrderByDescending(p => p.ID);
            if (countnumber != 0)
               return list.Take(countnumber).ToList();
            return list.ToList();
        }
    }
}
