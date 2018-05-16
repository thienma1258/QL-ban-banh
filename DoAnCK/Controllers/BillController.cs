using DAL.Context;
using DAL.Interface;
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
    
        IBillReposibility billreposibility;
        IBillDetailsReposibility billdetailsreposibility;
        public BillController( IBillReposibility billreposibility, IBillDetailsReposibility billdetailsreposibility)
        {

            this.billreposibility = billreposibility;
            this.billdetailsreposibility=billdetailsreposibility;
        }
        //
        // GET: /Bill/
        public ActionResult Index()
        {
            return View(billreposibility.getlist());
        }

        [AllowAnonymous]
        public ActionResult Confirm(string id)
        {
          if( billreposibility.ConfirmBill(id))
            return View();
            return HttpNotFound();
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Bill bill = billreposibility.getbill(id);
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
            Bill bill = billreposibility.getbill(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            foreach (var item in bill.billdetails.ToList())
            {
                billdetailsreposibility.delete(item);
            }
            billreposibility.DeleteBill(bill);
            return RedirectToAction("Index");
        }
	}
}