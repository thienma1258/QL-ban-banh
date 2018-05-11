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
        IBillDetailsReposibility billdetailsreposibility;
        IBillReposibility billreposibility;
        ICategoryResponsibility categorysponsibility;
        IBranchReposibitory ibranchrepository;
        
        int numberperonepage = 6;
        public HomeController(BakeryContext db, IBranchReposibitory ibranchrepository,ICategoryResponsibility categoryrepository, IBakeryReposibitory bkf,IBillDetailsReposibility billdetailsreposibility,IBillReposibility billreposibility)
        {
            this.db = db;
            this.ibranchrepository = ibranchrepository;
            bakeryreposibitory = bkf;
            this.categorysponsibility = categoryrepository;
            this.billdetailsreposibility = billdetailsreposibility;
            this.billreposibility = billreposibility;
       


        }
        public ActionResult Index()
        {

           string ip= HelperClass.GetIPHelper();
            return View(this.bakeryreposibitory.getlist(6));


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
                shop = ibranchrepository.getlist(),
                news = db.news.ToList()

            };
            return View(Footer);
        }
        public ActionResult Header()
        {
            
            ViewBag.Authentication = false;
            var header = new HeaderModel
            {
                category = categorysponsibility.getlist(),
                shop = ibranchrepository.getlist()
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
            return View(categorysponsibility.getlist());
        }
        public ActionResult Shop(string listcate=null,int page=1)
        {
            List<Bakery> listbakery=new List<Bakery>() ;
            if (listcate!=null)
            {
                Category category = null;
                category = categorysponsibility.SearchByName(listcate);
                listbakery = category.bakerys.ToList();
             
            }
            else {
                listbakery = bakeryreposibitory.getlist(0);

            }
            var count = listbakery.Count;
            ViewBag.numberpage = count / numberperonepage;
            if (count % numberperonepage!=0)
            {
                ViewBag.numberpage += 1;
            }
          
            ViewBag.currentpage = page;
            ViewBag.listcate = listcate;
            listbakery = listbakery.Skip(this.numberperonepage *(page - 1)).Take(numberperonepage).ToList();

            //List<Bakery> bakerys = bakeryreposibitory.getlist();
            //List<Bakery> results = new List<Bakery>();
            //if (category == null)
            //    return HttpNotFound();
           
            //    foreach(var i in bakerys)
            //    {
            //        if (i.category == category)
            //            results.Add(i);
            //    }
               
                return View(listbakery);
            
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Bakery bakery = bakeryreposibitory.find(id);
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
           
                    Bakery bakery = bakeryreposibitory.find(item.ID);
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
                billreposibility.AddBill(bill);
                //db.SaveChanges();
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
                Bakery bakery = bakeryreposibitory.find(id);
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