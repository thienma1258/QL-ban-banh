using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IIntroduceResponsibility
    {
        List<Introduction> getlist(int countnumber = 0);
        Introduction find(string id);
        void AddIntroduction(Introduction introduce);
        void EditIntroduction(Introduction introduce, string nameimage);

        void DeleteIntroduction(string id);
    }
}
