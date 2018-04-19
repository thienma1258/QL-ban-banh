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

            //string query = String.Format("insert into qlbb.\"News\" values('{0}','{1}','{2}',to_date('{3}','dd-mm-yy hh:mi:ss am')", news.ID, news.Title,news.Body,news.DatePost);
            //var result = db.Database.ExecuteSqlCommand(query)
        }

        public void DeleteNews(string id)
        {
            //var news = db.news.Find(id);
            //db.news.Remove(news);
            //db.SaveChanges();
         //   string query = "delete from qlbb.\"News\" where\"ID\"='" + id + "'";
           // var result = db.Database.ExecuteSqlCommand(query);

            var tt = db.news.Find(id);
            db.news.Remove(tt);
            db.SaveChanges();
        }

        public void EditNews(News news)
        {
            //var newsedit = db.news.SingleOrDefault(p => p.ID == news.ID);

            //db.SaveChanges();

            //var cate = db.Categorys.SingleOrDefault(p => p.Id == id.Id);
            string query = "select * from qlbb.\"News\" where\"ID\"='" + news.ID + "'";
            var cate = db.Database.SqlQuery<News>(query).SingleOrDefault();
            //cate.Name = id.Name;
            string query1 = "Update qlbb.\"News\" Set \"Title\"='" + news.Title + "', \"Body\" = '"+news.Body+"' where \"ID\" ='" + news.ID + "'";
            //b.SaveChanges();
            var result = db.Database.ExecuteSqlCommand(query1);
            var newsedit = db.news.SingleOrDefault(p => p.ID == news.ID);
            newsedit.Body = news.Body;
            newsedit.Title = news.Title;
            db.SaveChanges();
        }

        public News find(string id)
        {
            var news = db.news.Find(id);
            return news;
            //string query = "select * from qlbb.\"News\" where\"ID\"='" + id + "'";
            //var news = db.Database.SqlQuery<News>(query).SingleOrDefault();
            //return news;
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
