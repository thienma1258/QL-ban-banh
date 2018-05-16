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
using DAL.Interface;

namespace DoAnCK.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        public readonly INewsResponsibility newsResponsibility;
        public NewsController(BakeryContext db, INewsResponsibility newsResponsibility)
        {

            this.newsResponsibility = newsResponsibility;
        }


        // GET: /News/
        public ActionResult Index()
        {
            ViewBag.test = "Index";
            return View(newsResponsibility.getlist());
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
                Body = news.Body,
                DatePost = DateTime.Now,
                Title = news.Title
            };
            newsResponsibility.AddNews(thongtin);
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
            News news = newsResponsibility.find(id);
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
                newsResponsibility.EditNews(news);
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
            News news = newsResponsibility.find(id);
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
            News news = newsResponsibility.find(id);
            newsResponsibility.DeleteNews(id);

            return RedirectToAction("Index");
        }
    }
}