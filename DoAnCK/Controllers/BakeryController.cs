using DAL.Context;
using DAL.Enum;
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
    public class BakeryController : Controller
    {
         public BakeryContext db = new BakeryContext();
        //
        // GET: /Bakery/
        public ActionResult Index()
        {
            return View(db.Bakerys.ToList());
        }
        
        public ActionResult AddBakery()
        {
           
            var listcategory = db.Categorys.ToList();

            ViewBag.listcategory = listcategory;
            ViewBag.test = "abc";
            return View("AddBakery");
        }
        [AllowAnonymous]
        public ActionResult Confirm(string id)
        {
            var bill = db.bill.SingleOrDefault(p => p.confirmEmail==id);
            if (bill == null)
                return HttpNotFound();
            bill.confirmEmail = "true";
            db.SaveChanges();
            return View();
        }
        [HttpPost]
        public ActionResult AddBakery(AddBakeryViewModel bakery)
        {

                //   var path = Path.Combine(Server.MapPath("~/HinhAnh"), imageselected.FileName);

                ImageModel newimage = db.images.SingleOrDefault(p => p.Id == bakery.nameimage);
              //  imageselected.SaveAs(path);
                Category category = db.Categorys.SingleOrDefault(p => p.Id == bakery.category_id);
                Bakery newbakery = new Bakery
                {
                    ID = Guid.NewGuid().ToString(),
                    ngaypost = DateTime.Now,
                    Name = bakery.Bakeryname,
                    category = category,
                    Price = bakery.Price,
                    images = newimage,
                    count=bakery.quantity

                };
            LogResposibility.Log(User, "Thêm bánh" + newbakery.Name);

                db.Bakerys.Add(newbakery);
                db.SaveChanges();
                return RedirectToAction("Index");
            

            //return View(bakery);
        }
        public ActionResult EditBakery(AddBakeryViewModel bakery)
        {
            if (bakery.ID == null)
            {
                return HttpNotFound();
            }
            Bakery newbakery = db.Bakerys.Find(bakery.ID);
            //   Category category = db.Categorys.Find(bakery.category_id);
            ImageModel image = db.images.Find(bakery.nameimage);
            if (newbakery == null)
            {
                return HttpNotFound();
            }
       
            db.SaveChanges();
            return View(newbakery);
        }
        [HttpPost]
        public ActionResult EditBakery(Bakery bakery,string nameimage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bakery).State = EntityState.Modified;
                var bajeryedit = db.Bakerys.SingleOrDefault(p => p.ID == bakery.ID);
                var image = db.images.SingleOrDefault(p => p.Id == nameimage);
                bajeryedit.images = image;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bakery);
        }
        public ActionResult DeleteBakery(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bakery bakery = db.Bakerys.Find(id);
            if (bakery == null)
            {
                return HttpNotFound();
            }
            return View(bakery);
        }
        [HttpPost, ActionName("DeleteBakery")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Bakery bakery = db.Bakerys.Find(id);
            bakery.ngaypost = DateTime.Now;
            db.Bakerys.Remove(bakery);
       
            db.SaveChanges();
            return RedirectToAction("Index");
        }
	}
}