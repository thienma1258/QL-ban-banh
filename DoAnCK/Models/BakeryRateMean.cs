using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnCK.Models
{
    public class BakeryRateMean
    {
        public Bakery bakery { get; set; }
        public double damp_mean {get;set;}
    }
}