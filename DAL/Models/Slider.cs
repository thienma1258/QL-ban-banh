using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Slider
    {
        public string Id { get; set; } 
        public virtual ImageModel image { get; set; }
        public int  position {get;set;}
    }
}
