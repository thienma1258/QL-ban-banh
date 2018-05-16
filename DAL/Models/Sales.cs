using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
   public  class Sales
    {
        public string ID { get; set; } 
        public DateTime time1 { get; set; }
        public DateTime time2 { get; set; }
        public double sale { get; set; }

    }
}
