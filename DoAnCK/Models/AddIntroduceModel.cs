using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCK.Models
{
    public class AddIntroduceModel
    {
        public string Id { get; set; }
          [AllowHtml] 
        public string details { get; set; }
        public string title { get; set; }
  
    }
}