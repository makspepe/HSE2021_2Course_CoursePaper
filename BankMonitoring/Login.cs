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
            //SqlCommand command = new SqlCommand($"SELECT EXISTS (SELECT* FROM Login WHERE login = {textBox1.Text} AND password = {textBox2.Text})", conn);

            /*

           private void ValidateUser()
            {
                string query = "SELECT role from tbl_login WHERE Username = @username and password=@password";
                string returnValue = "";
                using (SqlConnection con = new SqlConnection("YourConnectionString"))
                {
                    using (SqlCommand sqlcmd = new SqlCommand(query, con))
                    {
                        sqlcmd.Parameters.Add("@username", SqlDbType.VarChar).Value = tbusername.Text;
                        sqlcmd.Parameters.Add("@password", SqlDbType.VarChar).Value = tbpswlog.Text;
                        con.Open();
                        returnValue = (string)sqlcmd.ExecuteScalar();
                    }
                }
                //EDIT to avoid NRE 
                if (String.IsNullOrEmpty(returnValue))
                {
                    MessageBox.Show("Incorrect username or password");
                    return;
                }
                returnValue = returnValue.Trim();
                if (returnValue == "Admin")
                {
                    MessageBox.Show("You are logged in as an Admin");
                    AdminHome fr1 = new AdminHome();
                    fr1.Show();
                    this.Hide();
                }
                else if (returnValue == "User")
                {
                    MessageBox.Show("You are logged in as a User");
                    UserHome fr2 = new UserHome();
                    fr2.Show();
                    this.Hide();
                }
            }






            */
            //SELECT EXISTS (SELECT* FROM login_details WHERE username = ? AND password = ?)
            //search for login in login.login
            // if found - search for pass in login.pass where login = login
            // if match -> messagebox.Show($"login password found!")

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
