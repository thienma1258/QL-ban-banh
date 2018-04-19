using DAL.Interface;
using DAL.Models;
using DoAnCK.Resposibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCK.Controllers
{
    public class BackupRestoreController : Controller
    {
        // GET: BackupRestore
        IBackup_RestoreRepository backup_restorerepository;
        ILogRepository ilogpository;
        public BackupRestoreController(IBackup_RestoreRepository backup_restorerepository,ILogRepository ilog)
        {
            this.backup_restorerepository = backup_restorerepository;
            this.ilogpository = ilog;
        }
        public ActionResult Index()
        {
            var result = backup_restorerepository.getallRestorefile();
            return View(result);
        }
        public ActionResult Backup()
        {
            backup_restorerepository.Backup(DateTime.Now);
            LogUser log = new LogUser();
            var user = UserReposibility.getcurrentUser(User);

            LogResposibility.Log(User, "Vừa backup vào lúc" + DateTime.Now);
                return RedirectToAction("Index");

        }
        public ActionResult Restore(string id)
        {
            backup_restorerepository.Restore(id);
            
            LogResposibility.Log(User, "Vừa restore vào lúc" + DateTime.Now);
            return RedirectToAction("", "Home");
        }
    }
}