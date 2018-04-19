using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
   public interface IImageRepository
    {
         ImageModel findimage(string id);
        void addImage(ImageModel image);

    }
}
