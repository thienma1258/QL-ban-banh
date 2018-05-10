using DAL.Context;
using DAL.Implement;
using DAL.Interface;
using DAL.Models;
using DoAnCK.Models;
using DoAnCK.Resposibility;
using Ganss.XSS;
using Microsoft.Security.Application;
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
        public readonly BakeryContext db;
        public readonly IIntroductionReposibitory introduceResponsibility;
        public IntroduceController(BakeryContext db, IIntroductionReposibitory introduceResponsibility)
        {
            this.db = db;
            this.introduceResponsibility = introduceResponsibility;
        }
        public ActionResult Index()
        {
            ViewBag.test = "Index";
            return View(introduceResponsibility.getlist());
        }
        public ActionResult AddIntroduce()
        {
            return View("AddIntroduce");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddIntroduce(AddIntroduceModel introduces,string nameimage)
        {
            if (ModelState.IsValid)
            {
                var sanitizer = new HtmlSanitizer();
                var image = db.images.SingleOrDefault(p => p.Id == nameimage);
                var details = sanitizer.Sanitize(Server.HtmlDecode(introduces.details));
                Introduction introducess = new Introduction
                {
                    Id = Guid.NewGuid().ToString(),    
                    title = introduces.title,
                    details=details,
                    Author= db.Users.SingleOrDefault(p=>p.UserName==User.Identity.Name)
                };
                if (image != null)
                    introducess.image = image;
                introduceResponsibility.AddIntroduction(introducess);
             
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
            Introduction introduces = introduceResponsibility.find(id);
            if (introduces == null)
            {
                return HttpNotFound();
            }
            return View(introduces);
        }
        [HttpPost]
       
        public ActionResult EditIntroduce(AddIntroduceModel introduces,string nameimage)
        {
            if (ModelState.IsValid)
            {
                var image = db.images.SingleOrDefault(p => p.Id == nameimage);
                if (image == null)
                {
                    return HttpNotFound();
                }
                //db.Entry(introduces).State = EntityState.Modified;
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
            Introduction introduces = introduceResponsibility.find(id);
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
            introduceResponsibility.DeleteIntroduction(id);
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Introduction intro = introduceResponsibility.find(id);
            if (intro == null)
            {
                return HttpNotFound();
            }
            intro.details = HttpUtility.HtmlDecode(intro.details);
            return View(intro);
        }
    }
}