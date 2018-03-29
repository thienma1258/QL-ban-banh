using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
     public class News    
     {
         public string ID { get; set; }
         public string Title { get; set; }
         public string Body { get; set; }
         public DateTime DatePost { get; set; }

    }
}
