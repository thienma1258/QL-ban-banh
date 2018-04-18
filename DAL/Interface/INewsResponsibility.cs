using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface INewsResponsibility
    {
        List<News> getlist(int countnumber = 0);
        News find(string id);
        void AddNews(News news);
        void EditNews(News news);

        void DeleteNews(string id);
    }
}
