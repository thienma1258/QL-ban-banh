using DAL.Context;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implement
{
    public class Backup_RestoreRepository : IBackup_RestoreRepository
    {
        BakeryContext db;
        public Backup_RestoreRepository(BakeryContext db)
        {
            this.db = db;
        }
        public  void Backup(DateTime now)
        {
              string command = "/C expdp SYSTEM/ SCHEMAS=qlbb  DUMPFILE=qlbb" + now.ToFileTimeUtc() + ".dmp";
            // System.Diagnostics.Process.Start("CMD.exe", command);
            //String command = @"/k java -jar myJava.jar";
            ProcessStartInfo cmdsi = new ProcessStartInfo("cmd.exe");
            cmdsi.Arguments = command;
            cmdsi.RedirectStandardOutput = true;
            cmdsi.RedirectStandardError = true;
            cmdsi.UseShellExecute = false;
      //      startInfo.CreateNoWindow = true;
            cmdsi.CreateNoWindow = true;
            Process cmd = Process.Start(cmdsi);
           
            cmd.WaitForExit();
            cmd.Close();
            // this.db.Database.ExecuteSqlCommand("expdp SYSTEM/9406715 SCHEMAS=qlbb  DUMPFILE=qlbb"+now.ToString()+".dmp");

        }

        public void Restore(string id)
        {
            string command = "/C impdp SYSTEM/Manga24h.com SCHEMAS=qlbb  DUMPFILE=" + id+ ".dmp TABLE_EXISTS_ACTION=REPLACE";
            // System.Diagnostics.Process.Start("CMD.exe", command);
            //String command = @"/k java -jar myJava.jar";
            ProcessStartInfo cmdsi = new ProcessStartInfo("cmd.exe");
            cmdsi.Arguments = command;
            cmdsi.RedirectStandardOutput = true;
            cmdsi.RedirectStandardError = true;
            cmdsi.UseShellExecute = false;
             
            cmdsi.CreateNoWindow = true;
            Process cmd = Process.Start(cmdsi);

            cmd.WaitForExit();
            //cmd.Close();
        }
        public List<string> getallRestorefile()
        {
            //get dictonary;
            string directoru = this.db.Database.SqlQuery<string>("SELECT directory_path FROM all_directories where directory_name = 'DATA_PUMP_DIR' ").SingleOrDefault();
            List<FileInfo> files = new List<FileInfo>();  // List that will hold the files and subfiles in path
            List<string> result = new List<string>();
            DirectoryInfo di = new DirectoryInfo(directoru);                           // list the files
            try
            {
                foreach (FileInfo f in di.GetFiles("*"))
                {
                    //Console.WriteLine("File {0}", f.FullName);
                    files.Add(f);
              //      result.Add(f.Name);
                    if (f.Name.Contains("QLBB"))
                        result.Add(f.Name);
                    
                }
                return result;
            }
            catch
            {
                //Console.WriteLine("Directory {0}  \n could not be accessed!!!!", dir.FullName);
                return null;  // We alredy got an error trying to access dir so dont try to access it again
            }
        }
    }
}
