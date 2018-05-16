using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
  public  interface IBackup_RestoreRepository
    {
        void Backup(DateTime now);
        List<string> getallRestorefile();
        void Restore(string id);

    }
}
