using DAL.Context;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCK.Controllers
{
    public class BillController : Controller
    {
        BakeryContext db = new BakeryContext(); 
        //
        // GET: /Bill/
        public ActionResult Index()
        {
            return View(db.bill.ToList());
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Bill bill = db.bill.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Bill bill = db.bill.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            foreach (var item in bill.billdetails.ToList())
            {
                db.billdetails.Remove(item);
                db.SaveChanges();
            }
            db.bill.Remove(bill);
                db.SaveChanges();
            return RedirectToAction("Index");
        }
	}
}