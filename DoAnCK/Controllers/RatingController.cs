using DAL.Context;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCK.Controllers
{
    public class RatingController : Controller
    {
        // GET: Rating
        public readonly IRateResposibitory rateResposibitory;
        public RatingController(BakeryContext db, IRateResposibitory rateResposibitory)
        {

            this.rateResposibitory = rateResposibitory;
        }
        public ActionResult Index()
        {
            ViewBag.test = "Index";
            return View(rateResposibitory.getlist());
        }
    }
}