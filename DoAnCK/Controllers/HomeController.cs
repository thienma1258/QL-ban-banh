using DAL.Context;
using DAL.Interface;
using DAL.Models;
using DoAnCK.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;


namespace project.Controllers
{

    public class HomeController : Controller
    {
        public BakeryContext db;
        IBakeryReposibitory bakeryreposibitory;
        public HomeController(BakeryContext db, IBakeryReposibitory bkf)
        {
            this.db = db;
            bakeryreposibitory = bkf;
        }
        public ActionResult Index()
        {
       
            return View(bakeryreposibitory.getlist(6));


        }
        public ActionResult About()
        {
            return View(db.introduction.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Slider()
        {
            return View(db.sliders.OrderBy(p=>p.position).ToList());
        }
        public ActionResult Footer()
        {
            var Footer = new FooterModel()
            {
                shop = db.shops.ToList(),
                news = db.news.ToList()

            };
            return View(Footer);
        }
        public ActionResult Header()
        {
            
            ViewBag.Authentication = false;
            var header = new HeaderModel
            {
                category = db.Categorys.ToList(),
                shop = db.shops.ToList()
            };
            if (User.Identity.IsAuthenticated) {
                var opl = (ClaimsIdentity)User.Identity;
                var username = User.Identity.Name;
                ViewBag.Username = username;
                ViewBag.Authentication = true;
            IEnumerable<Claim> claims = opl.Claims;
            }
            return View(header);
        }
        public ActionResult Menu()
        {
            return View(db.Categorys.ToList());
        }
        public ActionResult Shop(string listcate)
        {
           
                var bakelist = new List<Bakery>();
                var bakelist1 = db.Bakerys.ToList();
                bakelist = bakelist1;
                var bakelist2 = new List<Bakery>();
                var cateList = new List<string>();
                var cateQuer = from c in db.Categorys orderby c.Name select c.Name;
                cateList.AddRange(cateQuer.Distinct());
                ViewBag.listCate = new SelectList(cateList);
                var bakery = from b in db.Bakerys select b;
                if (!string.IsNullOrEmpty(listcate))
                {
                    bakery = bakery.Where(b => b.category.Name == listcate);
                    bakelist2.AddRange(bakery);
                    bakelist = bakelist2;
                }        
                return View(bakelist);
            
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Bakery bakery = db.Bakerys.Find(id);
            if (bakery == null)
            {
                return HttpNotFound();
            }
            return View(bakery);
        }
        public ActionResult Checkout()
        {
            
            return View();
        }
        public ActionResult News()
        {
            return View(db.news.ToList());
        }
        public ActionResult Thanks()
        {
            return View();
        }
        public ActionResult AddBill()
        {
            return View("AddBill");
        }
        [HttpPost]
        public ActionResult AddBill(Bill newbill)
        {
            var listbakery = Session["listCheckout"] as List<AddBakeryViewModel>;
            if (listbakery==null)
            {            
                return RedirectToAction("Shop", "Home");
            }
            else
            {
                Bill bill = new Bill
                {
                    Id = Guid.NewGuid().ToString(),
                    Nameuser = newbill.Nameuser,
                    Addressuser = newbill.Addressuser,
                    Email = newbill.Email,
                    SDT=newbill.SDT
                };

                var billdetails = new List<Billdetails>();
                var dem = 0;
                var tong = 0;
                foreach (var item in listbakery)
                {
                    Bakery bakery = db.Bakerys.Find(item.ID);
                    var billdetail = new Billdetails()
                    {
                        Bakery = bakery,
                        Bill = bill,
                        iddetails = Guid.NewGuid().ToString(),
                        quality=item.quantity
                    };
                    billdetails.Add(billdetail);
                    dem = item.Price * item.quantity;
                    tong += dem;
                }

                bill.billdetails = billdetails;
                bill.Totalprice = tong;
                bill.confirmEmail = bill.Id;
                db.bill.Add(bill);
                db.SaveChanges();
                if (EmailService.sendEmailTocustomer(bill, Server))
                {
                    return RedirectToAction("Thanks", "Home");
                }
                else
                {
                    return RedirectToAction("Shop", "Home");
                }
            }
        }
       
        public ActionResult Search(string listcate)
        {
            var cateList = new List<string>();
            var cateQuer = from c in db.Categorys orderby c.Name select c.Name;
            cateList.AddRange(cateQuer.Distinct());
            ViewBag.listCate = new SelectList(cateList);
            var bakery = from b in db.Bakerys select b;
            if (!string.IsNullOrEmpty(listcate))
            {
                bakery = bakery.Where(b => b.category.Name == listcate);
            }
            return View(bakery);
        }
        public ActionResult demoCheckout(){
            var listbakery = Session["listCheckout"] as List<AddBakeryViewModel>;
            if (listbakery == null)
            {
                listbakery = new List<AddBakeryViewModel>();
            }
            return View(listbakery);
        }
        [HttpPost]
        public JsonResult AddItem(string id)
        {

            var listbakery=Session["listCheckout"] as List<AddBakeryViewModel>;
            if (listbakery == null)
            {
                listbakery = new List<AddBakeryViewModel>();
            }

        
           var CheckExisit = listbakery.SingleOrDefault(p => p.ID == id);
           if (CheckExisit == null)
           {
               Bakery bakery = db.Bakerys.SingleOrDefault(p => p.ID == id);
               if (bakery == null)
               {
                   return Json(new
                   {

                       status = 0,

                   });

               }
               AddBakeryViewModel newBakeryadd = new AddBakeryViewModel()
               {
                   ID = bakery.ID,
                   Bakeryname = bakery.Name,
                   nameimage = bakery.images.nameImage,
                   quantity = 1,
                   Price = bakery.Price
               };

               listbakery.Add(newBakeryadd);
           }
           else
           {
               CheckExisit.quantity += 1;
               
           }
           Session["listCheckout"] = listbakery;
           var tongslhang = listbakery.Sum(p => p.quantity);
           return Json(new
           {
               status = 1,
               data = listbakery,
               tongsl=tongslhang

           });
          





        }
     

    }
}