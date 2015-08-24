using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Sleek_Bill_Company;

namespace Sleek_Bill
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SleekBillParentContainer());
        }
    }
}
