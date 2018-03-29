using DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Bakery
    {
        public string ID { get; set; } 
        public string Name { get; set; }
        public int Price { get; set; }
        public DateTime ngaypost { get; set; }
        public BakerySize size;
        public double VAT { get; set; }
        public int count { get; set; }
        public virtual Category category { get; set; }
        public virtual ImageModel images { get; set; }
       
    }
}
