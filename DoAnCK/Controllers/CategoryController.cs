using DAL.Context;
using DAL.Implement;
using DAL.Interface;
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
        public readonly BakeryContext db;
        public readonly ICategoryResponsibility categoryresponsibility;
        //
        // GET: /Category/
        public CategoryController(BakeryContext db, ICategoryResponsibility categoryresponsibility)
        {
            this.db = db;
            this.categoryresponsibility = categoryresponsibility;
        }
        public ActionResult Index()
        {
            return View(categoryresponsibility.getlist());
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
                categoryresponsibility.AddCate(newcate);
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
                categoryresponsibility.Edit(cate);
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
           
            categoryresponsibility.Delete(id);
            return RedirectToAction("Index");
        }
	}
}