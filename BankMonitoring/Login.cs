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

namespace Bank
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 2 || textBox2.Text.Length < 2)
            {
                MessageBox.Show("Слишком короткий логин или пароль");
                return;
            }
            var conn = new SqlConnection();
            conn.ConnectionString = Program.str;
            conn.Open();

            //Login
            using (SqlCommand StrQuer = new SqlCommand("SELECT * FROM Login WHERE login=@userid AND password=@password", conn))
            {
                StrQuer.Parameters.AddWithValue("@userid", textBox1.Text);
                StrQuer.Parameters.AddWithValue("@password", textBox2.Text);
                SqlDataReader dr = StrQuer.ExecuteReader();

                if (dr.HasRows) //TODO закрытие формы - показ главной формы
                {
                    MessageBox.Show($"loginned as {textBox1.Text}!");
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                    textBox2.Clear();
                }
            }
        }
        void press (object sender, KeyPressEventArgs e) //Для проверки ввода
        {
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
                MessageBox.Show("Слишком длинный ввод");
                e.Handled = true;
            }
        }
    }
}
