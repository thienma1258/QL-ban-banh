using DAL.Context;
using DAL.Models;
using DoAnCK.Models;
using DoAnCK.Resposibility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace DoAnCK.Controllers
{
    [Authorize]
    public class IntroduceController : Controller
    {
        // GET: Introduce
        public BakeryContext db = new BakeryContext();
        public ActionResult Index()
        {
            return View(db.introduction.ToList());
        }
        public ActionResult AddIntroduce()
        {
            return View("AddIntroduce");
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AddIntroduce(Introduction introduces,string nameimage)
        {
            if (ModelState.IsValid)
            {
                var image = db.images.SingleOrDefault(p => p.Id == nameimage);
                if (image == null)
                    return HttpNotFound();
                Introduction introducess = new Introduction
                {
                    Id = Guid.NewGuid().ToString(),    
                    title = introduces.title,
                    details=introduces.details,
                    image =image       ,
                    Author= db.Users.SingleOrDefault(p=>p.UserName==User.Identity.Name)
                };
                db.introduction.Add(introducess);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(introduces);
        }
        public ActionResult EditIntroduce(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Introduction introduces = db.introduction.Find(id);
            if (introduces == null)
            {
                return HttpNotFound();
            }
            return View(introduces);
        }
        [HttpPost]
       
        public ActionResult EditIntroduce(Introduction introduces,string nameimage)
        {
            if (ModelState.IsValid)
            {
                var image = db.images.SingleOrDefault(p => p.Id == nameimage);
                if (image == null)
                {
                    return HttpNotFound();
                }
                db.Entry(introduces).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(introduces);
        }
        public ActionResult DeleteIntroduce(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Introduction introduces = db.introduction.Find(id);
            if (introduces == null)
            {
                return HttpNotFound();
            }
            return View(introduces);
        }
        [HttpPost, ActionName("DeleteIntroduce")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirme(string id)
        {
            Introduction introduces = db.introduction.Find(id);
            db.introduction.Remove(introduces);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Introduction intro = db.introduction.Find(id);
            if (intro == null)
            {
                return HttpNotFound();
            }
            intro.details = HttpUtility.HtmlDecode(intro.details);
            return View(intro);
        }
    }
}