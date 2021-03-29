using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace Bank
{


    static class Program
    {
        public static string appPath = Application.StartupPath.Replace("\\bin\\Debug", "\\Data");
        public static string str = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={appPath}\Bank.mdf;Integrated Security=True;";
        public static bool isgovernor = false;
        public static string emppas, fam, name, sname, curepas, curcpas, curcont;

        public static string trun(string s) //rem last char typed
        {
            return s = s.Truncate(s.Length-1);
        }

        public static bool notempty(string s) //пустая ли строка?
        {
            s = Regex.Replace(s, @"\s+", "");
            if (s.Length == 0)
                return false;
            return true;
        }

        public static bool digit(string s) //только digit
        {
            bool k = true;
            Regex rgx = new Regex(@"^(\d{1,})$");
            if (!rgx.IsMatch(s))
                k = false;
            return k;
        }

        public static string FIO(string s) //без цифр 
        {
            s = Regex.Replace(s, @"[\d]", string.Empty);
            return s;
        }

        public static bool incmask(string s)
        {
            bool k = true;
            Regex rgx = new Regex(@"^(\d{1,}([,]){1,}\d{1,})|(\d{1,})$");
            if (!rgx.IsMatch(s))
                k = false;
            return k;
        }

        public static string income(string s) //доход, изменяем запятую
        {
            if (notempty(s))
            {
                s = s.Replace(",", ".");
            }
            return s;
        }

        public static bool pasmask(string s) //проверка на длину и только цифры
        {
            bool k = true;
            if (s.Length != 11)
            {
                k = false;
                return k;
            }
            Regex rgx = new Regex(@"^\d{4} \d{6}$");
            if (!rgx.IsMatch(s))
                k = false;
            return k;
        }

        public static bool innmask(string s) //проверка на длину и только цифры
        {
            bool k = true;
            if (s.Length != 12)
            {
                k = false;
                return k;
            }
            Regex rgx = new Regex(@"^\d{12}$");
            if (!rgx.IsMatch(s))
                k = false;
            return k;
        }
        public static bool snilsmask(string s) //проверка на длину и только цифры
        {
            bool k = true;
            if (s.Length != 14)
            {
                k = false;
                return k;
            }
            Regex rgx = new Regex(@"^(\d{3}-){2}\d{3} \d{2}$");
            if (!rgx.IsMatch(s))
                k = false;
            return k;
        }

        public static bool phonemask(string s) //проверка на длину и маску
        {
            bool k = true;
            if (s.Length != 14)
            {
                k = false;
                return k;
            }
            Regex rgx = new Regex(@"^[(]\d{3}[)] \d{3}-\d{4}$");
            if (!rgx.IsMatch(s))
                k = false;
            return k;
        }

        public static bool emailmask(string s)
        {
            bool k = true;
            Regex rgx = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                    + "@"
                                    + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            if (!rgx.IsMatch(s))
                k = false;
            return k;
        }

        public static bool datemask(string s) //дата
        {
            bool k = true;
            if (s.Length != 10)
            {
                k = false;
                return k;
            }
            DateTime temp;
            if (!DateTime.TryParse(s, out temp))
                k = false;
            return k;
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
