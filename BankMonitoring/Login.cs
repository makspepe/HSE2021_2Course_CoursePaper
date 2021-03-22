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
            public string slogin { get; set; }
            public string spass { get; set; }
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
            

            //Login
            using (SqlCommand StrQuer = new SqlCommand("SELECT * FROM Login WHERE login=@login AND password=@password", conn))
            {
                StrQuer.Parameters.AddWithValue("@login", textBox1.Text);
                StrQuer.Parameters.AddWithValue("@password", textBox2.Text);
                conn.Open();

                LogPas[] slogpass;
                SqlDataReader dr = StrQuer.ExecuteReader();

                using (dr) //для проверки на регистр в логине/пароле
                {
                    var list = new List<LogPas>();
                    while (dr.Read())
                        list.Add(new LogPas { slogin = dr.GetString(1), spass = dr.GetString(2) });
                    slogpass = list.ToArray();
                }
                //TODO нужна привязка к logid в emp, и приветствие как фио от logid
                //проверка на active 1/0 если не active => сообщение об ошибке return
                if (!(slogpass.GetLength(0) == 0) && String.Equals(textBox1.Text, slogpass[0].slogin) && String.Equals(textBox2.Text, slogpass[0].spass))
                {
                    this.Hide();
                    //MessageBox.Show($"loginned as {textBox1.Text}!");
                    Меню tmp = new Меню();
                    tmp.Show();
                    return;
                }
                else
                {
                    label1.Text = ("Неверное имя пользователя или пароль");
                    label1.Visible = true;
                    textBox2.Clear();
                }
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
