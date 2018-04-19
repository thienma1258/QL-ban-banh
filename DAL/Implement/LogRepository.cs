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
    public class LogRepository : ILogRepository
    {
        BakeryContext db;
        public LogRepository(BakeryContext db)
        {
            this.db = db;
        }
        public void AddLog(LogUser log)
        {
            this.db.logs.Add(log);
            this.db.SaveChanges();
        }
    
    public void DeleteAllLog()
        {
           foreach(var log in this.db.logs)
            {
                this.db.logs.Remove(log);
                this.db.SaveChanges();
            }
        }

      
        public LogUser Find(string id)
        {
            var log = this.db.logs.SingleOrDefault(p => p.Id == id);
            return log;
        }
        public List<LogUser> GetList()
        {
            return db.logs.OrderByDescending(p => p.datetime).ToList();
        }

      public  void AddLog(System.Security.Principal.IPrincipal user, string Log)
        {
            var username = user.Identity.Name;
        var userlog = db.Users.SingleOrDefault(p => p.UserName == username);
        LogUser loguser = new LogUser()
        {
            Id = Guid.NewGuid().ToString(),
            bakeryuser = userlog,
            datetime = DateTime.Now,
            logstring = username + " Vừa " + Log
        };
        db.logs.Add(loguser);
            db.SaveChanges();


        }
}
}
