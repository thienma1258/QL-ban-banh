using DAL.Context;
using DAL.Interface;
using DAL.Models;
using DoAnCK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCK.Controllers
{
    public class RateController : Controller
    {

        public readonly IRateReposibitory ratereposibitory;
        public readonly IBakeryReposibitory ibakeryrepository; 
        public RateController(IBakeryReposibitory ibakeryrepository, IRateReposibitory ratereposibitory)
        {

            this.ratereposibitory = ratereposibitory;
            this.ibakeryrepository = ibakeryrepository;
        }

        // GET: Rate
      
        [HttpPost]
        public String AddRate(string idbake,int valuerate)
        {
            string ip = HelperClass.GetIPHelper();
            var check = this.ratereposibitory.getlist().Where(p => p.bakery.ID == idbake && p.IPADDRESS == ip).SingleOrDefault();
            if (check == null)
            {

                Rating rate = new Rating
                {

                    IPADDRESS = ip,
                    bakery = this.ibakeryrepository.find(idbake),
                    ratestar = valuerate
                };
                ratereposibitory.AddRate(rate);
                return "Success";
            }
            else
            {
                return "False";

            }
        }
    }
}