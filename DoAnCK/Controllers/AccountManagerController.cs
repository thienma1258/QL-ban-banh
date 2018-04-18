 using DAL.Context;
using DoAnCK.Resposibility;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCK.Controllers
{
    [Authorize]
    public class AccountManagerController : Controller
    {
        public  BakeryContext Bakerycontext
        {
            get
            {
                return HttpContext.GetOwinContext().Get<BakeryContext>();
            }

        }
        // GET: AccountManager
        [HttpGet]
        public ActionResult Index()
        {
            var currentuser = UserReposibility.getcurrentUser(User);
          
            return View(currentuser);
        }
        [HttpPost]
        public ActionResult Index(string nameimage)
        {
           UserReposibility.setImageuser(User,nameimage);
            var currentuser = UserReposibility.getcurrentUser(User);
            return View(currentuser);
        }
        [Authorize(Roles ="Admin")]
        public ActionResult Log()
        {
            var listlog = Bakerycontext.logs.OrderByDescending(p=>p.datetime).ToList();
            return View(listlog);
        }
    }
}