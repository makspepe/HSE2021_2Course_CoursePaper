using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Bank
{
    //Для ограничения длины ввода
   public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public class LogPas
        {
            public int sid { get; set; } //id 
            public string slogin { get; set; } //логин
            public string spass { get; set; } //пароль
            public bool sactive { get; set; } //активный ли аккаунт
        }

        public string replace(string s) //все попытки sql injections убираем
        {
            return Regex.Replace(s, "[-']", "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 2 || textBox2.Text.Length < 2)
            {
                label1.Text = ("Слишком короткий логин или пароль");
                label1.Visible = true;
                return;
            }


            var conn = new SqlConnection();
            conn.ConnectionString = Program.str;

            replace(textBox1.Text);
            replace(textBox2.Text);

            LogPas[] slogpass;
            //Login
            using (SqlCommand StrQuer = new SqlCommand("SELECT * FROM Login WHERE login=@login AND password=@password", conn)) //берем всю запись
            {
                StrQuer.Parameters.AddWithValue("@login", textBox1.Text);
                StrQuer.Parameters.AddWithValue("@password", textBox2.Text);
                conn.Open();

                
                SqlDataReader dr = StrQuer.ExecuteReader();

                using (dr) //для проверки на регистр в логине/пароле
                {
                    var list = new List<LogPas>();
                    while (dr.Read())
                        list.Add(new LogPas {sid = dr.GetInt32(0), slogin = dr.GetString(1), spass = dr.GetString(2), sactive = dr.GetBoolean(3) });
                    slogpass = list.ToArray();
                }
                
            }
            if (!(slogpass.GetLength(0) == 0) && String.Equals(textBox1.Text, slogpass[0].slogin) && String.Equals(textBox2.Text, slogpass[0].spass))
            {
                if (!slogpass[0].sactive) //не активен
                {
                    label1.Text = ("Этот аккаунт закрыт");
                    label1.Visible = true;
                    textBox2.Clear();
                    conn.Close();
                    return;
                }
                //Задаем ФИО и права
                using (SqlCommand StrQuer = new SqlCommand("SELECT emppas, fam, name, sname, job FROM emp WHERE logid=@logid", conn))
                {
                    StrQuer.Parameters.AddWithValue("@logid", slogpass[0].sid);
                    SqlDataReader dr = StrQuer.ExecuteReader();
                    using (dr)
                    {
                        while (dr.Read())
                        {
                            Program.emppas = dr.GetString(0);
                            Program.fam = dr.GetString(1);
                            Program.name = dr.GetString(2);
                            Program.sname = dr.GetString(3);
                            Program.isgovernor = (dr.GetInt32(4) == 1 ? true : false); //управляющий ли?
                        }
                    }

                }
                    this.Hide();
                //MessageBox.Show($"loginned as {textBox1.Text}!");
                Menu tmp = new Menu();
                tmp.Show();
            }
            else
            {
                label1.Text = ("Неверное имя пользователя или пароль");
                label1.Visible = true;
                textBox2.Clear();
            }

            conn.Close();
        }

        //Для проверки ввода
        void press (object sender, KeyPressEventArgs e) 
        {
            label1.Visible = false;
            base.OnKeyPress(e);
            char c = e.KeyChar;
            if (e.KeyChar == 22) //запрет на вставку чего-либо в текстбокс
            {
                e.Handled = true;
            }
            if (!(char.IsDigit(c) || char.IsControl(c) || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c == '_')))
            {
                e.Handled = true;
            }
            if (textBox1.Text.Length > 32 || textBox2.Text.Length > 32) //проверка на длину 
            {
                label1.Text = ("Слишком длинный ввод");
                label1.Visible = true;
                e.Handled = true;
            }
        }

        //VISUAL
        private void textBox1_Leave(object sender, EventArgs e)
        {
            Regex.Replace(textBox1.Text, @"\s+", "");
            if (textBox1.Text == "")
            {
                textBox1.Text = "Имя пользователя";
                textBox1.ForeColor = Color.DarkGray;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            Regex.Replace(textBox2.Text, @"\s+", "");
            if (textBox2.Text == "")
            {
                textBox2.Text = "Пароль";
                textBox2.UseSystemPasswordChar = false;
                textBox2.ForeColor = Color.DarkGray;
            }
            
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Имя пользователя")
            {
                textBox1.Text = null;
            }
            textBox1.ForeColor = Color.Black;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Пароль")
            {
                textBox2.UseSystemPasswordChar = true;
                textBox2.Text = null;
            }
            textBox2.ForeColor = Color.Black;
        }

        //Remove char if count  > 32
        private void removeChar(object sender, KeyEventArgs e)
        {
            if (textBox1.Text.Length > 32 || textBox2.Text.Length > 32)
            {
                textBox1.Text = textBox1.Text.Truncate(32);
                textBox2.Text = textBox2.Text.Truncate(32);
            }

        }
    }

    //Truncate
    public static class StringExt
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}
