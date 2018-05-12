
using DAL.Context;
using DAL.Models;
using DoAnCK.Models;
using DoAnCK.Resposibility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project.Controllers
{
    [Authorize(Roles = "Admin, Editor")]
    public class AdminController : Controller
    {
        // GET: Admin
        public BakeryContext db = new BakeryContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Menuadmin()
        {
            var currentuser = UserReposibility.getcurrentUser(User);
            ViewBag.role = UserReposibility.getrolesuser(User);
            return PartialView(currentuser);
        }
        public ActionResult Header()
        {
            var currentuser = UserReposibility.getcurrentUser(User);

            return PartialView(currentuser);
        }
        public ActionResult GalleryPicker()
        {
            var listimage = db.images.ToList();


            return PartialView(listimage);
        }
        [HttpPost]
        public JsonResult UploadImage(HttpPostedFileBase file)
        {
            if (file == null) return Json(new
            {
                status = false
            });
            var path = Path.Combine(Server.MapPath("~/HinhAnh/"), file.FileName);
            ImageModel image = new ImageModel
            {
                Id=Guid.NewGuid().ToString(),
                nameImage = file.FileName,
                width = 0,
                height = 0,
                url = path,
            };
            file.SaveAs(path);
            db.images.Add(image);
            db.SaveChanges();
            var listimage = db.images.ToList();
            return Json(new
            {
                status = 20,
                images=listimage
            });
        }
      
    }
}