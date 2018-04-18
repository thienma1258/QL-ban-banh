using DAL.Implement;
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
    public class IntroductionResponsibility : IIntroduceResponsibility
    {
        private BakeryContext db;
        public IntroductionResponsibility(BakeryContext db)
        {
            this.db = db;
        }
        public void AddIntroduction(Introduction introduce)
        {
            db.introduction.Add(introduce);
            db.SaveChanges();
        }

        public void DeleteIntroduction(string id)
        {
            var introduce = db.introduction.Find(id);
            db.introduction.Remove(introduce);
            db.SaveChanges();
        }

        public void EditIntroduction(Introduction introduce, string nameimage)
        {
            var introedit = db.introduction.SingleOrDefault(p => p.Id == introduce.Id);
            var image = db.images.SingleOrDefault(p => p.Id == nameimage);
            introedit.Author = introduce.Author;
            introedit.details = introduce.details;
            introedit.title = introduce.details;
            if (image != null)
                introedit.image = image;
            db.SaveChanges();
        }

        public Introduction find(string id)
        {
            var introduce = db.introduction.Find(id);
            return introduce;
        }

        public List<Introduction> getlist(int countnumber = 0)
        {
            var list = db.introduction.OrderByDescending(p => p.Id);
            if (countnumber != 0)
                return list.Take(countnumber).ToList();
            return list.ToList();
        }
    }
}
