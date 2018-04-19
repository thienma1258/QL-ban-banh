using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
   public  class Category
    {
   
       public string Id { get; set; }
    
       public  string Name { get; set; }

        public virtual ICollection<Bakery> bakerys { get; set; }
    }
}
