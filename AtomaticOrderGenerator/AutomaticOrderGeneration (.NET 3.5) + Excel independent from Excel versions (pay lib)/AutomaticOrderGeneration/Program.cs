using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GemBox.Spreadsheet;

namespace AutomaticOrderGeneration
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            //String[] keys = {"9FEKG-9XGLJ-2QD64-57GOU"
            //                 };
            //foreach (String key in keys)
            //{
            //    try
            //    {
                    SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            //        OrderFileGenerator.Register = ExcelFile.Load(Properties.Settings.Default.RegisterPath);

            //    }
            //    catch (System.Exception exc)
            //    {
            //        MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
