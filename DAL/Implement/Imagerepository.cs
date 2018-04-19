using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Context;

namespace DAL.Implement
{
   public class Imagerepository : IImageRepository
    {
        BakeryContext db;
        public Imagerepository(BakeryContext db)
        {
            this.db = db;
        }
        public void addImage(ImageModel image)
        {
            throw new NotImplementedException();
        }

        public ImageModel findimage(string id)
        {
            return db.images.SingleOrDefault(p => p.Id == id);
        }
    }
}
