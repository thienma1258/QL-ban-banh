using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL.Models;
using DAL.Context;

namespace DoAnCK.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private BakeryContext db = new BakeryContext();

        // GET: /News/
        public ActionResult Index()
        {
            return View(db.news.ToList());
        }


        public ActionResult AddNews()
        {
            return View("AddNews");
        }
        [HttpPost]
        public ActionResult AddNews(News news)
        {
           
                News thongtin = new News
                {
                    ID = Guid.NewGuid().ToString(),
                    Body=news.Body,
                    DatePost=DateTime.Now,
                    Title=news.Title
                };
                db.news.Add(thongtin);
                db.SaveChanges();
                return RedirectToAction("Index");
            
            return View(news);
        }

        // GET: /Branch/Details/5
        public ActionResult EditNews(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            News news = db.news.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }
        [HttpPost]
        public ActionResult EditNews(News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }
        public ActionResult DeleteNews(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.news.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }
        [HttpPost, ActionName("DeleteNews")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            News news = db.news.Find(id);
            db.news.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
