﻿using DAL.Context;
using DAL.Interface;
using DoAnCK.Resposibility;
using DoAnCK.RS.Implement;
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
        IAppraiseAlgorthim iappraise;
        IUserRepository iuser;
        IBakeryReposibitory ibakery;
        public AccountManagerController(IBakeryReposibitory ibakery,IUserRepository iuser,ILogRepository ilog,IAppraiseAlgorthim iappraise)
        {
            this.ilog = ilog;
            this.iappraise = iappraise;
            this.iuser = iuser;
            this.ibakery = ibakery;
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

        public ActionResult AppraiseAlgorthim()
        {
            var user = this.iuser.getcurrentUser(User);
            var bakery = this.ibakery.getlist();
            ViewBag.GlobalAverage = this.iappraise.GlobalAvarage();
            ViewBag.UserAverage = this.iappraise.UserAverage(user.Id);

            ViewBag.ItemAverage = this.iappraise.ItemAvaeage(bakery[0].ID);
            return View();
        }
    }
}