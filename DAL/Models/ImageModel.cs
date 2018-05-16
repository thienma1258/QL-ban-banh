using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ImageModel
    {
        public string Id { get; set; } 
        public string nameImage { get; set; }
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }             
    }
}
