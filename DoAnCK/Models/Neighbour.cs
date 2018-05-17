using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnCK.Models
{
    public class Neighbour
    {
        public string id { get; set; }
        public double correl { get; set; }
        public double avarage { get; set; }

        public int[] ratemovie { get; set; }
    }
}