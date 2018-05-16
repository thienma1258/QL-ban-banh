using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnCK.Models
{
    public class AddBakeryViewModel
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string Bakeryname { get; set; }
        
        [Required]
        public int Price { get; set; }
        [Required]
        public int quantity { get; set; }
        public string category_id { get; set; }
        public string nameimage { get; set; }



    }
}