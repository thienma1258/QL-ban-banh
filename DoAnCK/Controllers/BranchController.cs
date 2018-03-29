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
    public class BranchController : Controller
    {
        private BakeryContext db = new BakeryContext();

        // GET: /Branch/
        public ActionResult Index()
        {
            return View(db.shops.ToList());
        }


        public ActionResult AddBranch()
        {
            return View("AddBranch");
        }
        [HttpPost]
        public ActionResult AddBranch(Shop shop)
        {
            if (ModelState.IsValid)
            {
                Shop newshop = new Shop
                {
                    Id = Guid.NewGuid().ToString(),
                    address = shop.address,
                    SDT = shop.SDT,
                    gmail = shop.gmail,
                    Googlemapembded = shop.Googlemapembded

                };
                db.shops.Add(newshop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shop);
        }

        // GET: /Branch/Details/5
        public ActionResult EditBranch(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Shop shop = db.shops.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }
        [HttpPost]
        public ActionResult EditBranch(Shop shop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shop);
        }
        public ActionResult DeleteBranch(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shop = db.shops.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }
        [HttpPost, ActionName("DeleteBranch")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Shop shop = db.shops.Find(id);
            db.shops.Remove(shop);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
