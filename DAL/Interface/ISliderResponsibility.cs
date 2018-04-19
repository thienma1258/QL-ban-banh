using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ISliderResponsibility
    {
        List<Slider> getlist(int countnumber = 0);
        Slider find(string id);
        void AddSlider(Slider slider);
        void EditSlider(Slider slider,string nameimage);
        void DeleteSlider(string id );
      
    }
}
