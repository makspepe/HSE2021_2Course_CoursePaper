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
    public partial class addcard : Form
    {
        public addcard()
        {
            InitializeComponent();
        }

        Dictionary<int, string> test = new Dictionary<int, string>();

        private void addcard_VisibleChanged(object sender, EventArgs e)
        {
            if (Program.isgovernor)
            {
                deletecon.Visible = true; //управляющий может удалять
            }

        }

        public void loadinfocard (string contnum) //загрузка данных по ид договора
        {
            Program.curcont = contnum;
            var conn = new SqlConnection();
            conn.ConnectionString = Program.str;

            using (SqlCommand StrQuer = new SqlCommand("SELECT * " +
                "FROM contcard INNER JOIN prcard ON contcard.prcard = prcard.Id " +
                $"WHERE contcard.contid = {contnum}", conn))
            {
                conn.Open();
                SqlDataReader dr = StrQuer.ExecuteReader();

                using (dr) //для проверки на регистр в логине/пароле
                {
                    while (dr.Read())
                    {  //вставка полей в textbox
                        textBox1.Text = dr.GetInt32(0).ToString(); //ид карты
                        textBox2.Text = dr.GetInt32(1).ToString(); //ид договора
                        textBox3.Text = dr.GetDateTime(2).Date.ToString("d"); //заканчивается
                        comboBox1.SelectedIndex = comboBox1.FindStringExact(test[dr.GetInt32(3)]); //код программы карты
                        textBox5.Text = dr.GetDateTime(4).Date.ToString("d"); //заключение
                        textBox6.Text = dr.GetString(5); //паспорт клиента
                        textBox7.Text = dr.GetString(6); //паспорт сотрудника
                        textBox8.Text = dr.GetInt32(7).ToString(); // номер счета
                        textBox9.Text = dr.GetDecimal(10).ToString(); // 10 loan lim
                        textBox10.Text = dr.GetDecimal(11).ToString(); // 11 loan max
                    }
                }
                conn.Close();
            }
            
        }
        private void addcard_Load(object sender, EventArgs e) //загрузка словаря для комбобокса из ДБ
        {

            var conn = new SqlConnection();
            conn.ConnectionString = Program.str;

            using (SqlCommand StrQuer = new SqlCommand("SELECT * " +
                "FROM prcard ", conn))
            {
                conn.Open();
                SqlDataReader dr = StrQuer.ExecuteReader();

                using (dr) //для проверки на регистр в логине/пароле
                {
                    while (dr.Read())
                    {  //вставка полей в textbox
                        int tmp;
                        string tmps;
                        tmp = dr.GetInt32(0); //ид программы
                        tmps = dr.GetString(1); //ид договора
                        test.Add(tmp, tmps);
                    }
                }
                conn.Close();
            }
            comboBox1.DataSource = new BindingSource(test, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
        }

        #region texbox
        //Для блока текстбоксов
        public void ronly()
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            comboBox1.Enabled = false;
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox8.ReadOnly = true;
        }
        public void write()
        {
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            comboBox1.Enabled = true;
            textBox5.ReadOnly = false;
            textBox6.ReadOnly = false;
            textBox7.ReadOnly = false;
            textBox8.ReadOnly = false;
            
        }

        public void wipe()
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox7.Text = null;
            textBox8.Text = null;
            textBox9.Text = null;
            textBox10.Text = null;
        }
        #endregion

        //удаление
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Удаление договора {Program.curcont}, уверены?", "Внимание", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var conn = new SqlConnection();
                conn.ConnectionString = Program.str;

                using (SqlCommand StrQuer = new SqlCommand("BEGIN TRANSACTION " +
                    $"DELETE FROM contract WHERE Id = '{Program.curcont}'; " +
                    $"DELETE FROM contcard WHERE contid = '{Program.curcont}';" +
                    $"COMMIT", conn))
                {
                    conn.Open();
                    SqlDataReader dr = StrQuer.ExecuteReader();

                }
                ronly();
                wipe();
                edit.Enabled = true;
                create.Enabled = true;
                save.Visible = false;
                revert.Visible = false;
                MessageBox.Show("Удаление завершено");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }

        }

        //Редактирование
        private void button1_Click(object sender, EventArgs e)
        {
            create.Enabled = false;
            save.Visible = true;
            revert.Visible = true;
            write();
        }

        //сохранение
        private void button3_Click(object sender, EventArgs e)
        {
            var conn = new SqlConnection();
            conn.ConnectionString = Program.str;
            if (edit.Enabled == true)  //редакт
            {
                
                //Получение ном дог и карты текущ договора
                string cardid = "-1", conid = "-1";
                using (SqlCommand StrQuer = new SqlCommand("SELECT Id, contid " +
                    "FROM contcard " +
                    $"WHERE contid = {Program.curcont}", conn))
                {
                    conn.Open();
                    SqlDataReader dr = StrQuer.ExecuteReader();

                    using (dr)
                    {
                        while (dr.Read())
                        {  //вставка полей в textbox
                            cardid = dr.GetInt32(0).ToString(); //ид карты
                            conid = dr.GetInt32(1).ToString(); //ид договора
                        }
                    }
                    conn.Close();
                }
                //проверка текущего номера договора и карты на повторение (кроме себя самого)
                bool k = true; ;
                using (SqlCommand StrQuer = new SqlCommand("SELECT contcard.Id, contract.Id " +
                    "FROM contcard INNER JOIN contract ON contcard.contid = contract.Id " +
                    $"WHERE contcard.contid = '{textBox1.Text}' " +
                    $"OR contract.Id = '{textBox2.Text}'", conn))
                {
                    conn.Open();
                    SqlDataReader dr = StrQuer.ExecuteReader();

                    using (dr)
                    {
                        bool found = false;
                        while (dr.Read())
                        {
                            found = true;
                            string tcardid = "-1", tid = "-1";
                            tcardid = dr.GetInt32(0).ToString(); //номер карты
                            tid = dr.GetInt32(1).ToString(); //номер договора
                            //Если нашли старую запись - скип
                            if (String.Equals(cardid, tcardid) && String.Equals(conid, tid)) 
                            {
                                continue;
                            }
                            if (String.Equals(textBox1.Text, tcardid))
                            {
                                k = false;
                                textBox1.Text = null;
                            }
                            if (String.Equals(textBox2.Text, tid))
                            {
                                k = false;
                                textBox2.Text = null;
                            }
                        }
                        conn.Close();
                        if (!k)
                        {
                            MessageBox.Show("Не могут повторяться: номера счетов");
                            return;
                        }
                        if (!found)
                        {
                            MessageBox.Show("Номера договора карты не существует");
                            textBox1.Text = null;
                            textBox2.Text = null;
                            return;
                        }
                    }

                }
                k = false;
                //проверка на существование паспорта сотруд
                using (SqlCommand StrQuer = new SqlCommand("SELECT emppas FROM emp " +
                    $"WHERE emppas = '{textBox7.Text}'", conn))
                {
                    conn.Open();
                    SqlDataReader dr = StrQuer.ExecuteReader();
                    using (dr)
                    {
                        string empid = "-1";
                        while (dr.Read())
                        {
                            
                            empid = dr.GetString(0); //пасп сотруд
                            //Если нашли запись - существует
                            if (String.Equals(textBox7.Text, empid))
                            {
                                k = true;
                            }


                        }
                        conn.Close();
                        if (!k)
                        {
                            MessageBox.Show("Сотрудника не существует");
                            return;
                        }
                    }

                }

                //существует ли счет
                bool t = true; //счет не существует?
                using (SqlCommand StrQuer = new SqlCommand("SELECT Id, clipas FROM acc " +
                   $"WHERE Id = '{textBox8.Text}'", conn))
                {
                    conn.Open();
                    SqlDataReader dr = StrQuer.ExecuteReader();
                    using (dr)
                    {
                        k = true;
                        
                        while (dr.Read())
                        {
                            string tid = "-1", tpas = "-1";
                            tid = dr.GetInt32(0).ToString(); //счет
                            tpas = dr.GetString(1); //паспорт кли
                            //Если нашли запись - существует
                            if (String.Equals(textBox8.Text, tid))
                            {
                                t = false;
                            }
                            if (!String.Equals(tpas, textBox6.Text))
                                k = false;

                        }
                        conn.Close();
                    }
                }
                if (!k)
                {
                    MessageBox.Show("Данный счет уже занят");
                    return;

                }
                
                //проверка на существование паспорта клиента
                using (SqlCommand StrQuer = new SqlCommand("SELECT clipas FROM cli " +
                   $"WHERE clipas = '{textBox6.Text}'", conn))
                {
                    string tcli = "-1";
                    conn.Open();
                    SqlDataReader dr = StrQuer.ExecuteReader();
                    using (dr)
                    {
                        while (dr.Read())
                        {
                            string empid = "-1";
                            empid = dr.GetString(0); //пасп клиента
                            //Если нашли запись - существует
                            if (String.Equals(textBox6.Text, empid))
                            {
                                tcli = empid;
                                k = true;
                            }
                        }
                        conn.Close();
                        if (!k)
                        {
                            MessageBox.Show("Клиента не существует");
                            return;
                        }
                    }

                }

                //Добавляем счет, если нет
                if (t)
                {
                    conn.Open();
                    DateTime myDateTime1 = DateTime.Now;
                    DateTime myDateTime2 = DateTime.Now.AddYears(4);
                    
                    string sqlFormattedDate1 = myDateTime1.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    string sqlFormattedDate2 = myDateTime2.ToString("yyyy-MM-dd HH:mm:ss.fff");

    
                    using (SqlCommand StrQuer = new SqlCommand("INSERT INTO acc (Id, opens, closes, clipas) " +
                        $"VALUES ('{textBox8.Text}', '{sqlFormattedDate1}', '{sqlFormattedDate2}', '{textBox6.Text}');", conn))
                    {
                        SqlDataReader dr = StrQuer.ExecuteReader();
                        conn.Close();
                    }
                }

                DateTime myDateTime3 = Convert.ToDateTime(textBox3.Text);
                DateTime myDateTime5 = Convert.ToDateTime(textBox5.Text);
                string sqlFormattedDate3 = myDateTime3.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string sqlFormattedDate5 = myDateTime5.ToString("yyyy-MM-dd HH:mm:ss.fff");

                //Апдейт 
                conn.Open();
                int prcode = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
                using (SqlCommand StrQuer = new SqlCommand
                        (
                        $"UPDATE contcard SET Id = N'{textBox1.Text}', contid = N'{textBox2.Text}', closes = N'{sqlFormattedDate3}', prcard = N'{prcode}', " +
                        $"contdate = '{sqlFormattedDate5}', clipas = N'{textBox6.Text}', emppas = N'{textBox7.Text}', accid = N'{textBox8.Text}' " +
                        $" WHERE (contid = '{textBox2.Text}');", conn))
                {
                    SqlDataReader dr = StrQuer.ExecuteReader();
                    conn.Close();
                }
                MessageBox.Show("Изменения сохранены");

                loadinfocard(textBox2.Text);
            }
            else //добавляем
            {
                textBox7.ReadOnly = true;
                bool k = false;
                //нужна проверка на наличие паспорта клиента в бд, если нет - ошибка
                using (SqlCommand StrQuer = new SqlCommand("SELECT clipas FROM cli " +
                  $"WHERE clipas = '{textBox6.Text}'", conn))
                {
                    string tcli = "-1";
                    conn.Open();
                    SqlDataReader dr = StrQuer.ExecuteReader();
                    using (dr)
                    {
                        while (dr.Read())
                        {
                            string empid = "-1";
                            empid = dr.GetString(0); //пасп клиента
                            //Если нашли запись - существует
                            if (String.Equals(textBox6.Text, empid))
                            {
                                tcli = empid;
                                k = true;
                            }
                        }
                        conn.Close();
                        if (!k)
                        {
                            MessageBox.Show("Клиента не существует");
                            return;
                        }
                    }

                }

                //существует ли счет
                bool t = true; //счет не существует?
                using (SqlCommand StrQuer = new SqlCommand("SELECT Id, clipas FROM acc " +
                   $"WHERE Id = '{textBox8.Text}'", conn))
                {
                    conn.Open();
                    SqlDataReader dr = StrQuer.ExecuteReader();
                    using (dr)
                    {
                        k = true;

                        while (dr.Read())
                        {
                            string tid = "-1", tpas = "-1";
                            tid = dr.GetInt32(0).ToString(); //счет
                            tpas = dr.GetString(1); //паспорт кли
                            //Если нашли запись - существует
                            if (String.Equals(textBox8.Text, tid))
                            {
                                t = false;
                            }
                            if (!String.Equals(tpas, textBox6.Text))
                                k = false;

                        }
                        conn.Close();
                    }
                }
                if (!k)
                {
                    MessageBox.Show("Данный счет уже занят");
                    return;

                }
                //Добавляем счет, если нет
                if (t)
                {
                    conn.Open();
                    DateTime myDateTime1 = DateTime.Now;
                    DateTime myDateTime2 = DateTime.Now.AddYears(4);

                    string sqlFormattedDate1 = myDateTime1.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    string sqlFormattedDate2 = myDateTime2.ToString("yyyy-MM-dd HH:mm:ss.fff");


                    using (SqlCommand StrQuer = new SqlCommand("INSERT INTO acc (Id, opens, closes, clipas) " +
                        $"VALUES ('{textBox8.Text}', '{sqlFormattedDate1}', '{sqlFormattedDate2}', '{textBox6.Text}');", conn))
                    {
                        SqlDataReader dr = StrQuer.ExecuteReader();
                        conn.Close();
                    }
                }

                DateTime myDateTime3 = Convert.ToDateTime(textBox3.Text);
                DateTime myDateTime5 = Convert.ToDateTime(textBox5.Text);
                string sqlFormattedDate3 = myDateTime3.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string sqlFormattedDate5 = myDateTime5.ToString("yyyy-MM-dd HH:mm:ss.fff");


                conn.Open();
                int prcode = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
                using (SqlCommand StrQuer = new SqlCommand
                        (
                        $"INSERT INTO contcard (Id, contid, closes, prcard, contdate, clipas,  emppas, accid) " +
                        $"VALUES ('{textBox1.Text}', '{textBox2.Text}', '{sqlFormattedDate3}', '{prcode}', '{sqlFormattedDate5}', '{textBox6.Text}', '{textBox7.Text}', '{textBox8.Text}'); " +
                        $" ", conn))
                {
                    SqlDataReader dr = StrQuer.ExecuteReader();
                    conn.Close();
                }
                MessageBox.Show("Изменения сохранены");


                //В конце обновляем выбранный контракт
                loadinfocard(textBox2.Text);
            }
            ronly();
            edit.Enabled = true;
            create.Enabled = true;
            save.Visible = false;
            revert.Visible = false;
        }

        //отмена
        private void button4_Click(object sender, EventArgs e)
        {
            if (Program.curcont != null)
                loadinfocard(Program.curcont);
            else
                wipe();

            save.Visible = false;
            revert.Visible = false;
            ronly();
            edit.Enabled = true;
            create.Enabled = true;
        }

        //создать
        public void button5_Click(object sender, EventArgs e)
        {
            Program.curcont = null;
            wipe();
            write();
            edit.Enabled = false;
            save.Visible = true;
            revert.Visible = true;
            textBox7.Text = Program.emppas;

            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox5.Text = DateTime.Now.ToShortDateString();

            var conn = new SqlConnection();
            conn.ConnectionString = Program.str;
            conn.Open();
            int maxloc = -1, maxc = -1;
            using (SqlCommand StrQuer = new SqlCommand($"SELECT MAX(contid) FROM contcard", conn))
            {
                SqlDataReader dr = StrQuer.ExecuteReader();
                using (dr)
                    while (dr.Read())
                    {
                        maxloc = dr.GetInt32(0);
                    }
            }
            using (SqlCommand StrQuer = new SqlCommand($"SELECT MAX(Id) FROM contract", conn))
            {
                SqlDataReader dr = StrQuer.ExecuteReader();
                using (dr)
                    while (dr.Read())
                    {
                        maxc = dr.GetInt32(0);
                    }
            }
            textBox1.Text = (maxloc + 1).ToString();
            textBox2.Text = (maxc + 1).ToString();
        }
    }
}
