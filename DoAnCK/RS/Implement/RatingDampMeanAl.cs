using DAL.Interface;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DoAnCK.RS.Implement
{
    public class RatingDampMeanAl
    {
        IRateResposibitory irate;
        public RatingDampMeanAl(IRateResposibitory irate)
        {
            this.irate = irate;
        }
        public double damp_mean(int k,double rui,double ui,double average)
        {
            
            return rui + k* average / Math.Abs(ui) + k;
        }
        public List<Bakery> topxephang()
        {
            var listrating = this.irate.getlist();
            var average = listrating.Average(p => p.ratestar);
            var rui = listrating.Sum(p => p.ratestar);
            //double ui = listrating.Count(p=> p.IPADDRESS);
            //int  k = listrating.Count(p => p.ID);

            return null;
        }
    }
} 