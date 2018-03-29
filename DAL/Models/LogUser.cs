using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
   public  class LogUser
    {
        public string Id { get; set; }
        public virtual BakeryUser bakeryuser { get; set; }
        public string logstring { get; set; }
        public DateTime datetime { get; set; } //= DateTime.Now;
    }
}
