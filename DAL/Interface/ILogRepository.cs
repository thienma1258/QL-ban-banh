using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ILogRepository
    {
        void AddLog(LogUser log);
        void AddLog(System.Security.Principal.IPrincipal user, string Log);
      
        void DeleteAllLog();
        List<LogUser> GetList(); 
    }
}
