using DAL.Context;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCK.Controllers
{
    public class SliderController : Controller
    {
        BakeryContext db = new BakeryContext();
        //
        // GET: /Slider/
        public ActionResult Index()
        {
            return View(db.sliders.ToList());
        }
        public ActionResult AddSlider()
        {
            return View("AddSlider");
        }
        [HttpPost]
        public ActionResult AddSlider(ImageModel newslider, int position)
        {
            ImageModel newimage = db.images.SingleOrDefault(p => p.Id == newslider.nameImage);
            Slider slider = new Slider
            {
                Id = Guid.NewGuid().ToString(),
                image = newimage,
                position = position
            };
            db.sliders.Add(slider);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditSlider(ImageModel newimage)
        {
            if (newimage.Id == null)
            {
                return HttpNotFound();
            }
            Slider newslider = db.sliders.Find(newimage.Id);
            if (newslider == null)
            {
                return HttpNotFound();
            }

            return View(newslider);
        }
        [HttpPost]
        public ActionResult EditSlider(Slider slider, string nameimage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slider).State = EntityState.Modified;
                var slidereditor = db.sliders.SingleOrDefault(p => p.Id == slider.Id);
                if (slidereditor == null) return HttpNotFound();
                ImageModel image = db.images.Find(nameimage);
                slidereditor.image = image;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(slider);
        }
        public ActionResult DeleteSlider(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Slider sliders = db.sliders.Find(id);
            if (sliders == null)
            {
                return HttpNotFound();
            }
            return View(sliders);
        }
        [HttpPost, ActionName("DeleteSlider")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Slider sliders = db.sliders.Find(id);
            db.sliders.Remove(sliders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}