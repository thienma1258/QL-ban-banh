using DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL.Models
{
   public class Introduction
    {
        public string Id { get; set; } 
        public virtual BakeryUser Author { get; set; }
       
        public string details { get; set; }
        public string title { get; set; }
        public virtual ImageModel image { get; set; }
        public IntroductionType type { get; set; }
    }
}
