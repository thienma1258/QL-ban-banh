﻿using System;
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
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string IPADDRESS { get; set; }
        public virtual BakeryUser User { get; set; }
        public virtual  Bakery bakery {get;set;}
        [Range(0,10)]
        public double ratestar { get; set; }
        public DateTime ngayrate { get; set; } = DateTime.Now;


    }
}
