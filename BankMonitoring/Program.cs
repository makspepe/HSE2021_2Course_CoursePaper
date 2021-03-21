using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank
{

    static class Program
    {
        public static string appPath = Application.StartupPath.Replace("\\bin\\Debug", "\\Data");
        public static string str = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={appPath}\Bank.mdf;Integrated Security=True;";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());

        }
    }
}
