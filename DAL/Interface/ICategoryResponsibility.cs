using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ICategoryResponsibility
    {
        List<Category> getlist(int countnumber=0);
        Category find(string id);
        void AddCate(Category name);
        void Delete(string id);
        void Edit(Category id); 
    }

}
