using DoAnCK.RS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnCK.RS.Implement
{
    public class ExcelRepository: IExcelrepository
    {
        public void writeExcelFile(double[][] a)
        {
            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;
            Microsoft.Office.Interop.Excel.Range oRng;
            object misvalue = System.Reflection.Missing.Value;
            try
            {
                //Start Excel and get Application object.
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

                //Get a new workbook.
                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                //Add table headers going cell by cell.

                for (int i = 0; i < a.GetLength(0); i++)
                {
                    for (int j = 0; j < a[i].GetLength(0); j++)
                    {
                        oSheet.Cells[i + 1, j + 1] = a[i][j];
                    }
                }

                oXL.Visible = false;
                oXL.UserControl = false;
                oWB.SaveAs("To_Excel.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oWB.Close();

                //...
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }
    }
}