using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Rating
    {
        [Key]
        public string ID { get; set; } = new Guid().ToString();
        public string IPADDRESS { get; set; }
        public virtual  Bakery bakery {get;set;}
        [Range(0,10)]
        public int ratestar { get; set; }

    }
}
