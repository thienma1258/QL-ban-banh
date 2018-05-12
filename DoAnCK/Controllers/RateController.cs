using DAL.Context;
using DAL.Interface;
using DAL.Models;
using DoAnCK.Models;
using DoAnCK.Resposibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCK.Controllers
{
    public class RateController : Controller
    {

        public readonly IRateResposibitory ratereposibitory;
        public readonly IBakeryReposibitory ibakeryrepository;
        public readonly IUserRepository iuser;


        public RateController(     IUserRepository iuser,IBakeryReposibitory ibakeryrepository, IRateResposibitory ratereposibitory)
        {

            this.ratereposibitory = ratereposibitory;
            this.ibakeryrepository = ibakeryrepository;
            this.iuser = iuser;
        }

        // GET: Rate
      
        [HttpPost]
        public String AddRate(string idbake,int valuerate)
        {
            string ip = HelperClass.GetIPHelper();
            //current user;
            var user = iuser.getcurrentUser(User);
            if (user == null)
            {
                // please login
                return "Please login";
            }
            var check = this.ratereposibitory.getlist().Where(p => p.bakery.ID == idbake && p.User == user).SingleOrDefault();
            if (check == null)
            {

                Rating rate = new Rating
                {
                    User = user,
                    IPADDRESS = ip,
                    bakery = this.ibakeryrepository.find(idbake),
                    ratestar = valuerate
                };
                ratereposibitory.AddRates(rate);
                return "Success";
            }
            else
            {
                return "False";

            }
        }
    }
}