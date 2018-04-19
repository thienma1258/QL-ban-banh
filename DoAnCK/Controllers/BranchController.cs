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
    public class BranchController : Controller
    {
       
      
        public readonly IBranchReposibitory branchresponsibility;
        public BranchController(BakeryContext db, IBranchReposibitory branchresponsibility)
        {
        
            this.branchresponsibility = branchresponsibility;
        }

        // GET: /Branch/
        public ActionResult Index()
        {
            ViewBag.test = "Index";
            return View(branchresponsibility.getlist());
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
                branchresponsibility.AddBranch(newshop);
               
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
            Shop shop = branchresponsibility.find(id);
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
                branchresponsibility.EditBranch(shop);
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
            Shop shop = branchresponsibility.find(id);
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
            
            branchresponsibility.DeleteBranch(id);
            
            return RedirectToAction("Index");
        }
    }
}
