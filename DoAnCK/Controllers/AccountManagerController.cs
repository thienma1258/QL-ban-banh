using DAL.Context;
using DAL.Interface;
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
        ILogRepository ilog;
        public AccountManagerController(ILogRepository ilog)
        {
            this.ilog = ilog;
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
            var listlog = this.ilog.GetList();
            return View(listlog);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAll()
        {
            this.ilog.DeleteAllLog();
            return RedirectToAction("Log");
        }
    }
}