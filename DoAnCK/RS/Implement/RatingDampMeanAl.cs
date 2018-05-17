using DAL.Interface;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DoAnCK.RS.Interface;
using DAL.Context;
using DoAnCK.Models;

namespace DoAnCK.RS.Implement
{
    public class RatingDampMeanAl: IRateDampMeanAl
    {
        BakeryContext db;
        IRateResposibitory irate;
        IBakeryReposibitory ibakery;
        int k = 5;
        public RatingDampMeanAl(IRateResposibitory irate,IBakeryReposibitory ibakery)
        {
            this.irate = irate;
            this.ibakery = ibakery;

        }
        public  double damp_mean(double rui,double ui,double average,int k)
        {
            if (rui == 0 || ui == 0||average==0 ) return 0;
        
                return ( rui + k * average) / (Math.Abs(ui) + k);
        }
        public List<BakeryRateMean> topxephangngay()
        {
            List<BakeryRateMean> bakeryRateMean = new List<BakeryRateMean>();
            var listrating = this.irate.getlist().Where(p => p.ngayrate.Day == DateTime.Now.Day && p.ngayrate.Month==DateTime.Now.Month); ;
            var listbakery = this.ibakery.getlist();
            foreach (var a in listbakery)
            {
                var test = listrating.Where(p => p.bakery.ID == a.ID).ToList() ;

                double average = 0;
                if (test.Count() != 0&&test.Count>k)
                {
                    average = test.Average(p => p.ratestar); 
                }
                var rui = test.Sum(p => p.ratestar);
                var ui = test.Count;
                var dampmean = this.damp_mean(rui, ui, average, k);

                bakeryRateMean.Add(new BakeryRateMean
                {
                    bakery = a,
                    damp_mean = dampmean,
                    
                });
            }


            return bakeryRateMean.OrderByDescending(p => p.damp_mean).Take(5).ToList();
        }

        public List<BakeryRateMean> topxephangthang()
        {
            List<BakeryRateMean> bakeryRateMean = new List<BakeryRateMean>();
           
            var listrating = this.irate.getlist().Where(p => p.ngayrate.Month == DateTime.Now.Month);
            
            var listbakery = this.ibakery.getlist();
           
            foreach (var a in listbakery)
            {
                //var average = listrating.Average(p => p.ratestar);
                //var rui = irate.sumrate(a.ID);
                //var ui = irate.countrate(a.ID);
                var test = listrating.Where(p => p.bakery.ID == a.ID).ToList();

                double average = 0;
                if (test.Count() != 0 && test.Count > k)
                {
                    average = test.Average(p => p.ratestar);
                }
                var rui = test.Sum(p => p.ratestar);
                var ui = test.Count;
                var dampmean = this.damp_mean(rui, ui, average, k);

                bakeryRateMean.Add(new BakeryRateMean
                {
                    bakery = a,
                    damp_mean = dampmean,
                });
            }


            return bakeryRateMean.OrderByDescending(p => p.damp_mean).Take(5).ToList();
        }

        public List<BakeryRateMean> topxephangnam()
        {
          
            List<BakeryRateMean> bakeryRateMean = new List<BakeryRateMean>();
            var listrating = this.irate.getlist();
            var listbakery = this.ibakery.getlist();
           
                foreach (var a in listbakery)
                {
                    var test = listrating.Where(p => p.bakery.ID == a.ID).ToList();

                    double average = 0;
                    if (test.Count() != 0 && test.Count > k)
                    {
                        average = test.Average(p => p.ratestar);
                    }
                    var rui = test.Sum(p => p.ratestar);
                    var ui = test.Count;
                    var dampmean = this.damp_mean(rui, ui, average, k);

                    bakeryRateMean.Add(new BakeryRateMean
                    {
                        bakery = a,
                        damp_mean = dampmean,

                    });
                }
          

            return bakeryRateMean.OrderByDescending(p => p.damp_mean).Take(5).ToList();
        }

        public List<BakeryRateMean> topxephang()
        {
            List<BakeryRateMean> bakeryRateMean = new List<BakeryRateMean>();
            
            var listbakery = this.ibakery.getlist();
            var listrating = this.irate.getlist();
            foreach (var a in listbakery)
            {
                //var average = listrating.Average(p => p.ratestar);
                //var rui = irate.sumrate(a.ID);
                //var ui = irate.countrate(a.ID);
                var test = listrating.Where(p => p.bakery.ID == a.ID).ToList();

                double average = 0;
                if (test.Count() != 0 && test.Count > k)
                {
                    average = test.Average(p => p.ratestar);
                }
                var rui = test.Sum(p => p.ratestar);
                var ui = test.Count;
                var dampmean = this.damp_mean(rui, ui, average, k);

                bakeryRateMean.Add(new BakeryRateMean
                {
                    bakery = a,
                    damp_mean = dampmean,
                    });
            } 
           
           
            return bakeryRateMean.OrderByDescending(p=>p.damp_mean).Take(4).ToList();
        }
        public List<BakeryRateMean> topxephangtatca()
        {
            List<BakeryRateMean> bakeryRateMean = new List<BakeryRateMean>();

            var listbakery = this.ibakery.getlist();
            var listrating = this.irate.getlist();
            foreach (var a in listbakery)
            {
                //var average = listrating.Average(p => p.ratestar);
                //var rui = irate.sumrate(a.ID);
                //var ui = irate.countrate(a.ID);
                var test = listrating.Where(p => p.bakery.ID == a.ID).ToList();

                double average = 0;
                if (test.Count() != 0 && test.Count > k)
                {
                    average = test.Average(p => p.ratestar);
                }
                var rui = test.Sum(p => p.ratestar);
                var ui = test.Count;
                var dampmean = this.damp_mean(rui, ui, average, k);

                bakeryRateMean.Add(new BakeryRateMean
                {
                    bakery = a,
                    damp_mean = dampmean,
                });
            }


            return bakeryRateMean.OrderByDescending(p => p.damp_mean).ToList();
        }


    }
} 