using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Data.SqlClient;

namespace Bank
{
    
    public partial class empinfo : Form
    {
        
        public empinfo()
        {
            InitializeComponent();
        }

        Dictionary<int, string> test2 = new Dictionary<int, string>();
        Dictionary<bool, string> test = new Dictionary<bool, string>();
        Dictionary<int, string> test3 = new Dictionary<int, string>();
        #region texbox
        //Для блока текстбоксов
        public void ronly()
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            comboBox2.Enabled = false;
            comboBox1.Enabled = false;
            textBox7.ReadOnly = true;
            comboBox2.Enabled = false;
            textBox9.ReadOnly = true;
            textBox10.ReadOnly = true;
            textBox11.ReadOnly = true;
            textBox12.ReadOnly = true;
            textBox13.ReadOnly = true;
            textBox14.ReadOnly = true;
            textBox15.ReadOnly = true;
            textBox16.ReadOnly = true;
            textBox17.ReadOnly = true;
        }
        public void write()
        {
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            textBox4.ReadOnly = false;
            textBox5.ReadOnly = false;
            comboBox2.Enabled = true;
            comboBox1.Enabled = true;
            textBox7.ReadOnly = false;
            comboBox3.Enabled = true;
            textBox9.ReadOnly = false;
            textBox10.ReadOnly = false;
            textBox11.ReadOnly = false;
            textBox12.ReadOnly = false;
            textBox13.ReadOnly = false;
            textBox14.ReadOnly = false;
            textBox15.ReadOnly = false;
            textBox16.ReadOnly = false;
           textBox17.ReadOnly = false;
        }

        public void wipe()
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox7.Text = null;
            textBox9.Text = null;
            textBox10.Text = null;
            textBox11.Text = null;
            textBox12.Text = null;
            textBox13.Text = null;
            textBox14.Text = null;
            textBox15.Text = null;
            textBox16.Text = null;
            textBox17.Text = null;
        }

        //Включение отключение кнопок
        public void changeBut(byte i)
        {
            if (i == 1) //вкл
            {
                write();
                button1.Visible = true;
                button6.Visible = true;
            }
            else
            {
                button1.Visible = false;
                button6.Visible = false;
                button4.Enabled = true;
                button5.Enabled = true;
            }
        }
        #endregion

        private void empinfo_VisibleChanged(object sender, EventArgs e)
        {
            if (Program.isgovernor)
            {
                label21.Visible = true;
                label17.Visible = true;
                textBox14.Visible = true; //упр показывается логин и пароль 
                textBox16.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                label22.Visible = true;
                comboBox1.Visible = true;
            }

        }

        //редактирование
        public void button4_Click_1(object sender, EventArgs e)
        {
            Program.curepas = textBox1.Text;
            write();
            button5.Enabled = false;
            changeBut(1);

        }

        //добавление
        public void button5_Click(object sender, EventArgs e)
        {
            wipe();
            button4.Enabled = false;
            changeBut(1);
            textBox15.Text = DateTime.Now.ToShortDateString();

        }

        // Отмена
        private void button1_Click(object sender, EventArgs e)
        {
            changeBut(0);
            if (Program.curepas == null) //при добавлении нового сотрудника - зануляем
                wipe();
            else
                empdata(Program.curepas);
            ronly();

        }

        // Сохранение
        private void button6_Click(object sender, EventArgs e) 
        {
            bool k = true;

            string errdia = "Ошибки при вводе:\n";
            //Проверки и парс штук в правильный вид
            if (!Program.pasmask(textBox1.Text))
            {
                errdia += "Номера паспорта\n";
                k = false;
            }
            if (!Program.notempty(textBox2.Text) || !Program.notempty(textBox3.Text) || !Program.notempty(textBox4.Text))
            {
                errdia += "ФИО\n";
                k = false;

            }
            if (!Program.datemask(textBox5.Text) || !Program.datemask(textBox15.Text))
            {
                errdia += "Полей даты\n";
                k = false;
            }

            if (!Program.innmask(textBox10.Text))
            {
                errdia += "ИНН\n";
                k = false;
            }

            if (!Program.snilsmask(textBox9.Text))
            {
                errdia += "СНИЛС\n";
                k = false;
            }
            if (!Program.emailmask(textBox17.Text))
            {
                errdia += "email\n";
                k = false;
            }
            if (!Program.notempty(textBox7.Text) || !Program.notempty(textBox13.Text))
            {
                errdia += "Адресов\n";
                k = false;

            }
            if (!Program.phonemask(textBox12.Text) || !Program.notempty(textBox11.Text))
            {
                errdia += "Номеров телефонов\n";
                k = false;

            }

            if (!k)
            {
                MessageBox.Show(errdia);
                return;
            }
            textBox2.Text = Program.FIO(textBox2.Text);
            textBox3.Text = Program.FIO(textBox3.Text);
            textBox4.Text = Program.FIO(textBox4.Text);

            if (button4.Enabled == true)  //редакт
            {
                var conn = new SqlConnection();
                conn.ConnectionString = Program.str;

                //Данные текущего пользователя
                string emppas="0", inn="0", snils="0", login="0", password="0", logid="-1";
                using (SqlCommand StrQuer = new SqlCommand($"SELECT emppas, inn, snils, Login.login, Login.password, Login.Id " +
                    $"FROM emp INNER JOIN Login ON emp.logid = Login.Id WHERE emppas = N'{Program.curepas}'", conn))
                {
                    conn.Open();
                    SqlDataReader dr = StrQuer.ExecuteReader();

                    using (dr)
                    {
                        while (dr.Read())
                        {
                            emppas = dr.GetString(0); //пасс
                            inn = dr.GetString(1); //инн
                            snils = dr.GetString(2); //снилс
                            login = dr.GetString(3); // login
                            password = dr.GetString(4); //pass
                            logid = dr.GetInt32(5).ToString();
                        }
                    }
                    conn.Close();
                }

                //Проверка данных текущего пользователя с тем, что найдено в дб
                    using (SqlCommand StrQuer = new SqlCommand($"SELECT emppas, inn, snils, Login.login, Login.password " +
                    $"FROM emp INNER JOIN Login ON emp.logid = Login.ID WHERE emppas LIKE '{textBox1.Text}%' OR inn LIKE '{textBox10.Text}%' " +
                    $"OR snils LIKE '{textBox9.Text}%' OR Login.login LIKE '{textBox16.Text}%' OR Login.password LIKE '{textBox14.Text}%'", conn))
                {
                    conn.Open();
                    SqlDataReader dr = StrQuer.ExecuteReader();

                    using (dr)
                    {
                        while (dr.Read())
                        {
                            string tpas="0", tinn="0", tsnils="0", tlogin="0", tpassword="0";
                            tpas = dr.GetString(0); //пасс
                            tinn = dr.GetString(1); //инн
                            tsnils = dr.GetString(2); //снилс
                            tlogin = dr.GetString(3);
                            tpassword = dr.GetString(4); //pass
                            //Если нашли старую запись - скип
                            if (String.Equals(emppas, tpas) && String.Equals(inn, tinn) && String.Equals(snils, tsnils) && (String.Equals(password, tpassword) && String.Equals(login, tlogin)))
                            {
                                continue;
                            }
                            if (String.Equals(textBox1.Text, tpas))
                            {
                                k = false;
                                textBox1.Text = null;
                            }
                            if (String.Equals(textBox10.Text, tinn))
                            {
                                k = false;
                                textBox10.Text = null;
                            }
                            if (String.Equals(textBox9.Text, tsnils))
                            {
                                k = false;
                                textBox9.Text = null;
                            }
                            if ((String.Equals(textBox16.Text, tpassword) && String.Equals(textBox14.Text, tlogin)))
                            {
                                k = false;
                                textBox14.Text = null;
                                textBox16.Text = null;
                            }
                        }
                        conn.Close();
                        if (!k)
                        {
                            MessageBox.Show("Не могут повторяться: паспорт, ИНН, СНИЛС, имя пользователи или пароль");
                            return;
                        }
                    }
                    
                }
                int citycode = ((KeyValuePair<int, string>)comboBox3.SelectedItem).Key;
                string jobcode = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key.ToString();
                string actcode = ((KeyValuePair<bool, string>)comboBox1.SelectedItem).Key.ToString();

                conn.Open();
                DateTime myDateTime1 = Convert.ToDateTime(textBox5.Text);
                string sqlFormattedDate1 = myDateTime1.ToString("yyyy-MM-dd HH:mm:ss.fff");
                DateTime myDateTime2 = Convert.ToDateTime(textBox15.Text);
                string sqlFormattedDate2 = myDateTime2.ToString("yyyy-MM-dd HH:mm:ss.fff");
                using (SqlCommand StrQuer = new SqlCommand
                        ("BEGIN TRANSACTION " +
                        $"UPDATE contcard SET contcard.emppas = '{textBox1.Text}' WHERE(((contcard.emppas) = '{Program.curepas}'));" +
                        $"UPDATE contdepo SET contdepo.emppas = '{textBox1.Text}' WHERE(((contdepo.emppas) = '{Program.curepas}'));" +
                        $"UPDATE contloan SET contloan.emppas = '{textBox1.Text}' WHERE(((contloan.emppas) = '{Program.curepas}')); " +
                        $"UPDATE emp SET emp.emppas = N'{textBox1.Text}', emp.fam = N'{textBox2.Text}', emp.name = N'{textBox3.Text}', emp.sname = N'{textBox4.Text}', emp.birth = '{sqlFormattedDate1}', " +
                        $"emp.job = N'{jobcode}', emp.inn = '{textBox10.Text}', emp.snils = '{textBox9.Text}', emp.city = '{citycode}', emp.adr = N'{textBox7.Text}', emp.cadr = N'{textBox13.Text}', emp.phone = N'{textBox12.Text}', " +
                        $"emp.jphone = N'{textBox11.Text}', emp.email = N'{textBox17.Text}', emp.updated = '{sqlFormattedDate2}' " +
                        $" WHERE (emp.emppas = '{Program.curepas}'); " +
                        $"UPDATE Login SET Login.login = N'{textBox16.Text}', Login.password = N'{textBox14.Text}', Login.active = N'{actcode}' " +
                        $" WHERE (Login.Id = '{logid}');" +
                        $" COMMIT", conn))
                {
                    SqlDataReader dr = StrQuer.ExecuteReader();
                    conn.Close();
                }
                Program.curepas = textBox1.Text;
                empdata(Program.curepas);
                button2.PerformClick();
                MessageBox.Show("Изменения сохранены");
            }


            else //INSERT
            {
                var conn = new SqlConnection();
                conn.ConnectionString = Program.str;

                //проверка на повторение emppas, inn, snils, login.password
                if (checkedata())
                {
                    //Получаем код города
                    int citycode = ((KeyValuePair<int, string>)comboBox3.SelectedItem).Key;
                    int maxid = 0;
                    string jobcode = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key.ToString();
                    string actcode = ((KeyValuePair<bool, string>)comboBox1.SelectedItem).Key.ToString();
                    conn.Open();
                    using (SqlCommand StrQuer = new SqlCommand($"SELECT MAX(Id) FROM Login", conn))
                    {
                        SqlDataReader dr = StrQuer.ExecuteReader();
                        using (dr)
                            while (dr.Read())
                            {
                                maxid = dr.GetInt32(0);
                            }
                    }

                    DateTime myDateTime3 = Convert.ToDateTime(textBox5.Text);
                    string sqlFormattedDate3 = myDateTime3.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    DateTime myDateTime1 = Convert.ToDateTime(textBox15.Text);
                    string sqlFormattedDate1 = myDateTime1.ToString("yyyy-MM-dd HH:mm:ss.fff");

                    using (SqlCommand StrQuer = new SqlCommand("BEGIN TRANSACTION " +
                        "INSERT INTO emp (emppas, fam, emp.name, sname, birth, job, inn, snils, emp.city," +
                        " adr, cadr, phone, jphone, email, updated, logid) " +
                        $"VALUES ('{textBox1.Text}', N'{textBox2.Text}', N'{textBox3.Text}', N'{textBox3.Text}', '{sqlFormattedDate3}', '{jobcode}', " +
                        $"'{textBox10.Text}', '{textBox9.Text}', '{citycode}', N'{textBox7.Text}', N'{textBox13.Text}', '{textBox12.Text}', '{textBox11.Text}', " +
                        $"'{textBox17.Text}', '{sqlFormattedDate1}', '{maxid + 1}'); " +
                        $"INSERT INTO Login (login, password, active) " +
                        $"VALUES ('{textBox16.Text}', '{textBox14.Text}', '{actcode}'); " +
                        $"COMMIT", conn))
                    {
                        SqlDataReader dr = StrQuer.ExecuteReader();
                        conn.Close();
                    }
                    Program.curepas = textBox1.Text;
                    empdata(Program.curepas);
                    button2.PerformClick();
                    MessageBox.Show("Изменения сохранены");
                }
            }
            changeBut(0);
            ronly();
        }

        public void empinfo_Load(object sender, EventArgs e)
        {
            //Показ Да Нет для активности аккаунта

            test.Add(false, "Нет");
            test.Add(true, "Да");
            comboBox1.DataSource = new BindingSource(test, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";

            // Загрузка возможных должностей
            var conn = new SqlConnection();
            conn.ConnectionString = Program.str;

            using (SqlCommand StrQuer = new SqlCommand("SELECT * " +
                "FROM job ", conn))
            {
                conn.Open();
                SqlDataReader dr = StrQuer.ExecuteReader();

                using (dr) //для проверки на регистр в логине/пароле
                {
                    while (dr.Read())
                    {  //вставка полей в textbox
                        int tmp;
                        string tmps;
                        tmp = dr.GetInt32(0); //ид должности
                        tmps = dr.GetString(1); //название
                        test2.Add(tmp, tmps);
                    }
                }
                conn.Close();
            }

            using (SqlCommand StrQuer = new SqlCommand("SELECT * FROM city", conn))
            {
                conn.Open();
                SqlDataReader dr = StrQuer.ExecuteReader();

                using (dr) //для проверки на регистр в логине/пароле
                {
                    while (dr.Read())
                    {  //вставка полей в textbox
                        int tmp;
                        string tmps;
                        tmp = dr.GetInt32(0); //ид города
                        tmps = dr.GetString(1); //название
                        test3.Add(tmp, tmps);
                    }
                }
                conn.Close();

                comboBox3.DataSource = new BindingSource(test3, null);
                comboBox3.DisplayMember = "Value";
                comboBox3.ValueMember = "Key";

                //test2.Add(0, "Менеджер");
                //test2.Add(1, "Управляющий");
                comboBox2.DataSource = new BindingSource(test2, null);
                comboBox2.DisplayMember = "Value";
                comboBox2.ValueMember = "Key";

                //((KeyValuePair<int, string>)comboBox1.SelectedItem).Key.ToString(); //вот так получать 0 1 из комбобокса
            }
        }

        public bool checkedata()  //проверка на повторение emppas, inn, snils, login.password
        {
            bool k = true;
            var conn = new SqlConnection();
            conn.ConnectionString = Program.str;

            using (SqlCommand StrQuer = new SqlCommand($"SELECT emppas, inn, snils, Login.login, Login.password " +
                $"FROM emp INNER JOIN Login ON emp.logid = Login.ID WHERE emppas LIKE '{textBox1.Text}%' AND inn LIKE '{textBox10.Text}%' " +
                $"AND snils LIKE '{textBox9.Text}%' AND Login.login LIKE '{textBox16.Text}%' AND Login.password LIKE '{textBox14.Text}%'", conn))
            {
                //StrQuer.Parameters.AddWithValue("@emppas", textBox1.Text);

                conn.Open();
                SqlDataReader dr = StrQuer.ExecuteReader();

                using (dr) 
                {
                    while (dr.Read())
                    {
                        string tpas, tinn, tsnils, tlogin, tpassword;
                        string res = "Не могут повторяться:\n";
                        tpas = dr.GetString(0); //пасс
                        tinn = dr.GetString(1); //инн
                        tsnils = dr.GetString(2); //снилс
                        tlogin = dr.GetString(3);
                        tpassword = dr.GetString(4); //pass
                        if (String.Equals(textBox1.Text, tpas))
                        {
                            k = false;
                            res = res + "паспорт\n";
                            textBox1.Text = null;
                        }
                        if (String.Equals(textBox10.Text, tinn))
                        {
                            k = false;
                            res = res + "ИНН\n";
                            textBox10.Text = null;
                        }
                        if (String.Equals(textBox9.Text, tsnils))
                        {
                            k = false;
                            res = res + "СНИЛС\n";
                            textBox9.Text = null;
                        }
                        if (String.Equals(textBox14.Text, tpassword) && String.Equals(textBox16.Text, tlogin))
                        {
                            k = false;
                            res = res + "имя пользователи или пароль";
                            textBox14.Text = null;
                            textBox16.Text = null;
                        }
                        if (!k)
                            MessageBox.Show(res);
                    }
                }
                conn.Close();
            }
            return k;
        }

        public void empdata(string ppas) //парс даты по паспорту
        {
            var conn = new SqlConnection();
            conn.ConnectionString = Program.str;

            using (SqlCommand StrQuer = new SqlCommand("SELECT emppas, fam, emp.name, sname, birth, job, inn, snils, city.Id," +
                " adr, cadr, phone, jphone, email, updated, Login.login, Login.password, Login.active  " +
                "FROM emp INNER JOIN city ON emp.city = city.Id INNER JOIN Login ON emp.logid = Login.ID " +
                "WHERE emppas=@emppas", conn)) //берем всю запись
            {
                StrQuer.Parameters.AddWithValue("@emppas", ppas);

                conn.Open();
                SqlDataReader dr = StrQuer.ExecuteReader();

                using (dr) //для проверки на регистр в логине/пароле
                {
                    while (dr.Read())
                    {  //вставка полей в textbox
                        textBox1.Text = dr.GetString(0); //пасс
                        textBox2.Text = dr.GetString(1); //ФИО
                        textBox3.Text = dr.GetString(2);
                        textBox4.Text = dr.GetString(3);
                        textBox5.Text = dr.GetDateTime(4).Date.ToString("d");
                        comboBox2.SelectedIndex = comboBox2.FindStringExact(test2[dr.GetInt32(5)]);  //ид должности
                        textBox10.Text = dr.GetString(6); //инн
                        textBox9.Text = dr.GetString(7); //снилс
                        comboBox3.SelectedIndex = comboBox3.FindStringExact(test3[dr.GetInt32(8)]);  //город
                        textBox7.Text = dr.GetString(9); //адрес
                        textBox13.Text = dr.GetString(10); //тек адрес
                        textBox12.Text = dr.GetString(11); //тел личный
                        textBox11.Text = dr.GetString(12); //тел рабоч
                        textBox17.Text = dr.GetString(13); //емейл
                        textBox15.Text = dr.GetDateTime(14).Date.ToString("d"); //updated
                        textBox16.Text = dr.GetString(15); //login
                        textBox14.Text = dr.GetString(16); //pass
                        comboBox1.SelectedIndex = comboBox1.FindStringExact(test[dr.GetBoolean(17)]);  //активен
                    }
                }
                conn.Close();
            }

        }

        //Обновление информации о человеке чей паспорт в textbox1
        public void button2_Click(object sender, EventArgs e) 
        {
            dataGridView1.Rows.Clear();
            var conn = new SqlConnection();
            conn.ConnectionString = Program.str;
            List<string[]> data = new List<string[]>();
            conn.Open();
            using (SqlCommand StrQuer = new SqlCommand($"SELECT contdepo.contid, contdate FROM contdepo WHERE(((contdepo.emppas) = '{Program.curepas}'));", conn))
            {
                SqlDataReader dr = StrQuer.ExecuteReader();
                using (dr)
                {
                    while (dr.Read())
                    {
                        data.Add(new string[2]); 
                        data[data.Count - 1][0] = dr[0].ToString();
                        data[data.Count - 1][1] = dr[1].ToString().Replace(" 0:00:00", "");

                    }
                }
            }
            using (SqlCommand StrQuer = new SqlCommand($"SELECT contcard.contid, contdate FROM contcard WHERE(((contcard.emppas) = '{Program.curepas}'));", conn))
            {
                SqlDataReader dr = StrQuer.ExecuteReader();
                using (dr)
                {
                    while (dr.Read())
                    {
                        data.Add(new string[2]);
                        data[data.Count - 1][0] = dr[0].ToString();
                        data[data.Count - 1][1] = dr[1].ToString().Replace(" 0:00:00", "");

                    }
                }
            }
            using (SqlCommand StrQuer = new SqlCommand($"SELECT contloan.contid, contdate FROM contloan WHERE(((contloan.emppas) = '{Program.curepas}'));", conn))
            {
                SqlDataReader dr = StrQuer.ExecuteReader();
                using (dr)
                {
                    while (dr.Read())
                    {
                        data.Add(new string[2]); 
                        data[data.Count - 1][0] = dr[0].ToString();
                        data[data.Count - 1][1] = dr[1].ToString().Replace(" 0:00:00", "");

                    }
                    foreach (string[] s in data)
                        dataGridView1.Rows.Add(s);
                }
            }
            conn.Close();
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) //получаем id из клетки и переходим в форму договора
        {
            try
            {

                var conn = new SqlConnection();
                conn.ConnectionString = Program.str;
                var item = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                int loan, depo, card;
                conn.Open();
                using (SqlCommand StrQuer = new SqlCommand($"SELECT contid FROM contcard WHERE contid=N'{item}'", conn))
                {
                    SqlDataReader dr = StrQuer.ExecuteReader();
                    using (dr)
                        while (dr.Read())
                        {
                            card = dr.GetInt32(0);
                            addcard tmp = new addcard();
                            tmp.Show();
                            tmp.loadinfocard(card.ToString());
                            conn.Close();
                            return;
                        }
                }
                using (SqlCommand StrQuer = new SqlCommand($"SELECT contid FROM contdepo WHERE contid=N'{item}'", conn))
                {
                    SqlDataReader dr = StrQuer.ExecuteReader();
                    using (dr)
                        while (dr.Read())
                        {
                            depo = dr.GetInt32(0);
                            adddepo tmp = new adddepo();
                            tmp.Show();
                            tmp.loadInfodepo(depo.ToString());
                            conn.Close();
                            return;
                        }
                }
                using (SqlCommand StrQuer = new SqlCommand($"SELECT contid FROM contloan WHERE contid=N'{item}'", conn))
                {
                    SqlDataReader dr = StrQuer.ExecuteReader();
                    using (dr)
                        while (dr.Read())
                        {
                            loan = dr.GetInt32(0);
                            addloan tmp = new addloan();
                            tmp.Show();
                            tmp.loadInfoloan(loan.ToString());
                            conn.Close();
                            return;
                        }
                }
            }

            catch (Exception )
            { }
        }
    }
}
