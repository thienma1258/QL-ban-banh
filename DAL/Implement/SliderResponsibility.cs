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
    public class SliderResponsibility : ISliderResponsibility
    {
        private BakeryContext db;
        public SliderResponsibility(BakeryContext db)
        {
            this.db = db;
        }

        public void AddSlider(Slider slider)
        {
            db.sliders.Add(slider);
            db.SaveChanges();

        }

        public void DeleteSlider(string id)
        {
            Slider slider = db.sliders.Find(id);
          
            db.sliders.Remove(slider);

            db.SaveChanges();
        }
        public void EditSlider(Slider slider, string nameimage)
        {
            var slideredit = db.sliders.SingleOrDefault(p => p.Id == slider.Id);
            var image = db.images.SingleOrDefault(p => p.Id == nameimage);
            slideredit.Id = slider.Id;
            slideredit.position = slider.position;
           
            if (image != null)
                slideredit.image = image;
            db.SaveChanges();
        }

        public Slider find(string id)
        {
            var slider = db.sliders.Find(id);
            return slider;
        }

        public List<Slider> getlist(int countnumber = 0)
        {
            var list = db.sliders.OrderByDescending(p => p.Id);
            if (countnumber != 0)
                return list.Take(countnumber).ToList();
            return list.ToList();
        }
    }
}
