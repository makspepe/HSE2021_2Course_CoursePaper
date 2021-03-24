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
        public static bool isgovernor = false; //TODO TEST
        public static string emppas, fam, name, sname, curepas, curcpas, curcont;

        public static bool FIO(string s)
        {
            return s.All(c => Char.IsLetterOrDigit(c) || c == '_');
        }
        public static bool date(string s)
        {
            return true;//TODO
        }

        public static string trun(string s) //rem last char typed
        {
            return s = s.Truncate(s.Length-1);
        }
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
