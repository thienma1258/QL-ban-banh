using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
  public  class Bill
    {
        public string Id { get; set; } 
        public string Nameuser { get; set; }
        public string Addressuser { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string confirmEmail { get; set; }
        public int Totalprice { get; set; }

        public virtual ICollection<Billdetails> billdetails { get; set; }
    }
}
