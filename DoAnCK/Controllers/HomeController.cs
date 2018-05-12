using DAL.Context;
using DAL.Interface;
using DAL.Models;
using DoAnCK.Models;
using DoAnCK.Resposibility;
using DoAnCK.RS.Interface;
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
        IMatrixParse imatrix;
        IUserRepository iuser;
        IRateResposibitory iratel;
        int numberperonepage = 6;
        public HomeController(BakeryContext db,IRateResposibitory irate, IUserRepository iuser, IMatrixParse imatrix,IBranchReposibitory ibranchrepository,ICategoryResponsibility categoryrepository, IBakeryReposibitory bkf,IBillDetailsReposibility billdetailsreposibility,IBillReposibility billreposibility)
        {
            this.db = db;
            this.ibranchrepository = ibranchrepository;
            bakeryreposibitory = bkf;
            this.iratel = irate;
            this.imatrix = imatrix;
            this.categorysponsibility = categoryrepository;
            this.iuser = iuser;
            this.billdetailsreposibility = billdetailsreposibility;
            this.billreposibility = billreposibility;
       


        }
        public ActionResult Index()
        {
            var list = imatrix.UserItemMatrix();
        
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
                var role = UserReposibility.getrolesuser(User);
                ViewBag.Username = username;
                ViewBag.role = role;
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

            var user = iuser.getcurrentUser(User);
      
            Bakery bakery = bakeryreposibitory.find(id);
            var check = iratel.check(bakery, user);
            if (check == null)
            {
                ViewBag.ratestar = -1;
            }
            else
            {
                ViewBag.ratestar = check.ratestar;
            }
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