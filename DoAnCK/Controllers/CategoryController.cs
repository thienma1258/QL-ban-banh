using DAL.Context;
using DAL.Models;
using DoAnCK.Models;
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
    public class CategoryController : Controller
    {
        BakeryContext db = new BakeryContext();
        //
        // GET: /Category/
        public ActionResult Index()
        {
            return View(db.Categorys.ToList());
        }
        public ActionResult AddCategory()
        {
            return View("AddCategory");
        }
        [HttpPost]
        public ActionResult AddCategory(Category cate)
        {
            if (ModelState.IsValid)
            {
                Category newcate = new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = cate.Name
                };
                db.Categorys.Add(newcate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cate);
        }
        public ActionResult EditCategory(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Category cate = db.Categorys.Find(id);
            if (cate == null)
            {
                return HttpNotFound();
            }
            return View(cate);
        }
        [HttpPost]
        public ActionResult EditCategory(Category cate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cate);
        }
        public ActionResult DeleteCategory(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category cate = db.Categorys.Find(id);
            if (cate == null)
            {
                return HttpNotFound();
            }
            return View(cate);
        }
        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Category cate = db.Categorys.Find(id);
            db.Categorys.Remove(cate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
	}
}