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
    public partial class clieinfo : Form
    {
        public clieinfo()
        {
            InitializeComponent();
        }

        Dictionary<int, string> test2 = new Dictionary<int, string>();
        Dictionary<int, string> test3 = new Dictionary<int, string>();

        public void cliinfo_Load(object sender, EventArgs e)
        {

            // Загрузка возможных счетов ид закр
            var conn = new SqlConnection();
            conn.ConnectionString = Program.str;

            // Город
            using (SqlCommand StrQuer = new SqlCommand("SELECT * FROM city", conn))
            {
                conn.Open();
                SqlDataReader dr = StrQuer.ExecuteReader();

                using (dr) 
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

                comboBox3.DataSource = new BindingSource(test3, null); // города
                comboBox3.DisplayMember = "Value";
                comboBox3.ValueMember = "Key";



                //((KeyValuePair<int, string>)comboBox1.SelectedItem).Key.ToString(); //вот так получать 0 1 из комбобокса
            }
        }

        public void clidata(string ppas) //парс даты по паспорту
        {
            var conn = new SqlConnection();
            conn.ConnectionString = Program.str;

            test2.Clear();
            using (SqlCommand StrQuer = new SqlCommand("SELECT * " +
          $"FROM acc WHERE clipas = '{Program.curcpas}'", conn))
            {
                conn.Open();
                SqlDataReader dr = StrQuer.ExecuteReader();

                using (dr)
                {
                    while (dr.Read())
                    {  //вставка полей в textbox
                        int tmp;
                        string tmps;
                        tmp = dr.GetInt32(0); //ид 
                        tmps = dr.GetDateTime(2).ToString(); //закрывается
                        test2.Add(tmp, tmps);
                    }
                }
                conn.Close();
            }
            comboBox2.DataSource = new BindingSource(test2, null); //счета
            comboBox2.DisplayMember = "Key";
            comboBox2.ValueMember = "Key";

            using (SqlCommand StrQuer = new SqlCommand("SELECT * " +
                $"FROM cli WHERE clipas = '{ppas}' ", conn)) //берем всю запись
            {
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
                        textBox5.Text = dr.GetDateTime(4).Date.ToString("d"); //др
                        textBox6.Text = dr.GetDecimal(5).ToString(); //доход
                        textBox10.Text = dr.GetString(6); //инн
                        textBox9.Text = dr.GetString(7); //снилс
                        comboBox3.SelectedIndex = comboBox3.FindStringExact(test3[dr.GetInt32(8)]);  //город
                        textBox7.Text = dr.GetString(9); //адрес
                        textBox13.Text = dr.GetString(10); //тек адрес
                        textBox12.Text = dr.GetString(11); //тел личный
                        textBox11.Text = dr.GetString(12); //тел рабоч
                        textBox17.Text = dr.GetString(13); //емейл
                        comboBox2.SelectedIndex = comboBox2.FindStringExact(test2[dr.GetInt32(14)]);  //ид счета
                        textBox15.Text = dr.GetDateTime(15).Date.ToString("d"); //updated
                    }
                }
                conn.Close();
            }

        }

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
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            comboBox2.Enabled = false;
            textBox9.ReadOnly = true;
            textBox10.ReadOnly = true;
            textBox11.ReadOnly = true;
            textBox12.ReadOnly = true;
            textBox13.ReadOnly = true;
            textBox15.ReadOnly = true;
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
            textBox6.ReadOnly = false;
            textBox7.ReadOnly = false;
            comboBox3.Enabled = true;
            textBox9.ReadOnly = false;
            textBox10.ReadOnly = false;
            textBox11.ReadOnly = false;
            textBox12.ReadOnly = false;
            textBox13.ReadOnly = false;
            textBox15.ReadOnly = false;
            textBox17.ReadOnly = false;
        }

        public void wipe()
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox7.Text = null;
            textBox9.Text = null;
            textBox10.Text = null;
            textBox11.Text = null;
            textBox12.Text = null;
            textBox13.Text = null;
            textBox15.Text = null;
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

        private void clieinfo_VisibleChanged(object sender, EventArgs e)
        { 

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

            var conn = new SqlConnection();
            conn.ConnectionString = Program.str;
            //comboBox2.Enabled = false;
            int maxid = 0;
            conn.Open();
            using (SqlCommand StrQuer = new SqlCommand($"SELECT MAX(Id) FROM acc", conn)) //добавляем счет сразу
            {
                SqlDataReader dr = StrQuer.ExecuteReader();
                using (dr)
                    while (dr.Read())
                    {
                        maxid = 1 + dr.GetInt32(0);
                    }
            }
            textBox8.Text = maxid.ToString();

            textBox15.Text = DateTime.Now.ToShortDateString();
            textBox15.ReadOnly = true;


            textBox8.Visible = true;
            comboBox2.Enabled = false;
            comboBox2.Visible = false;
            button2.PerformClick();


        }

        // Отмена
        private void button1_Click(object sender, EventArgs e)
        {
            textBox8.Visible = false;
            changeBut(0);
            if (Program.curcpas == null) //при добавлении нового сотрудника - зануляем
                wipe();
            else
            {
                clidata(Program.curcpas);
                button2.PerformClick();
            }
            ronly();

        }

        // Сохранение
        private void button6_Click(object sender, EventArgs e)
        {
            var conn = new SqlConnection();
            conn.ConnectionString = Program.str;
            if (button4.Enabled == true)  //редакт
            {
                bool k = true;

                //Данные текущего клиента
                string clipas = "0", inn = "0", snils = "0";
                using (SqlCommand StrQuer = new SqlCommand($"SELECT clipas, inn, snils " +
                    $"FROM cli WHERE clipas = N'{Program.curcpas}'", conn))
                {
                    conn.Open();
                    SqlDataReader dr = StrQuer.ExecuteReader();

                    using (dr)
                    {
                        while (dr.Read())
                        {
                            clipas = dr.GetString(0); //пасс
                            inn = dr.GetString(1); //инн
                            snils = dr.GetString(2); //снилс
                        }
                    }
                    conn.Close();
                }

                //Проверка данных текущего клиента с тем, что найдено в дб
                using (SqlCommand StrQuer = new SqlCommand($"SELECT clipas, inn, snils " +
                $"FROM cli WHERE clipas LIKE '{textBox1.Text}%' OR inn LIKE '{textBox10.Text}%' " +
                $"OR snils LIKE '{textBox9.Text}%' ", conn))
                {
                    conn.Open();
                    SqlDataReader dr = StrQuer.ExecuteReader();

                    using (dr)
                    {
                        while (dr.Read())
                        {
                            string tpas = "0", tinn = "0", tsnils = "0";
                            tpas = dr.GetString(0); //пасс
                            tinn = dr.GetString(1); //инн
                            tsnils = dr.GetString(2); //снилс
                            //Если нашли старую запись - скип
                            if (String.Equals(clipas, tpas) && String.Equals(inn, tinn) && String.Equals(snils, tsnils))
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
                        }
                        conn.Close();
                        if (!k)
                        {
                            MessageBox.Show("Не могут повторяться: паспорт, ИНН, СНИЛС");
                            return;
                        }
                    }

                }
                int citycode = ((KeyValuePair<int, string>)comboBox3.SelectedItem).Key;
                string acccode = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key.ToString();

                conn.Open();
                DateTime myDateTime1 = Convert.ToDateTime(textBox5.Text);
                string sqlFormattedDate1 = myDateTime1.ToString("yyyy-MM-dd HH:mm:ss.fff");
                DateTime myDateTime2 = DateTime.Now;
                string sqlFormattedDate2 = myDateTime2.ToString("yyyy-MM-dd HH:mm:ss.fff");
                using (SqlCommand StrQuer = new SqlCommand
                        ("BEGIN TRANSACTION " +
                        $"UPDATE contcard SET contcard.clipas = '{textBox1.Text}' WHERE(((contcard.clipas) = '{Program.curcpas}'));" +
                        $"UPDATE contdepo SET contdepo.clipas = '{textBox1.Text}' WHERE(((contdepo.clipas) = '{Program.curcpas}'));" +
                        $"UPDATE contloan SET contloan.clipas = '{textBox1.Text}' WHERE(((contloan.clipas) = '{Program.curcpas}')); " +
                        $"UPDATE cli SET cli.clipas = N'{textBox1.Text}', cli.fam = N'{textBox2.Text}', cli.name = N'{textBox3.Text}', " +
                        $"cli.sname = N'{textBox4.Text}', cli.birth = '{sqlFormattedDate1}', " +
                        $"cli.income = N'{textBox6.Text.Replace(",0000", "")}', cli.inn = '{textBox10.Text}', cli.snils = '{textBox9.Text}', cli.city = '{citycode}', " +
                        $"cli.adr = N'{textBox7.Text}', cli.cadr = N'{textBox13.Text}', cli.phone = N'{textBox12.Text}', " +
                        $"cli.jphone = N'{textBox11.Text}', cli.email = N'{textBox17.Text}', cli.updated = '{sqlFormattedDate2}', cli.accnum = '{acccode}'" +
                        $" WHERE (cli.clipas = '{Program.curcpas}'); " +
                        $" COMMIT", conn))
                {
                    SqlDataReader dr = StrQuer.ExecuteReader();
                    conn.Close();
                }
                MessageBox.Show("Изменения сохранены");
            }


            else //INSERT
            {
                //проверка на повторение emppas, inn, snils
                if (checkcdata())
                {
                    //Получаем код города
                    int citycode = ((KeyValuePair<int, string>)comboBox3.SelectedItem).Key;
                    
                    string acccode = textBox8.Text; //код аккаунта

                    DateTime myDateTime1 = DateTime.Now;
                    DateTime myDateTime2 = DateTime.Now.AddYears(4);
                    

                    string sqlFormattedDate1 = myDateTime1.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    string sqlFormattedDate2 = myDateTime2.ToString("yyyy-MM-dd HH:mm:ss.fff");

                    DateTime myDateTime3 = Convert.ToDateTime(textBox5.Text);
                    string sqlFormattedDate3 = myDateTime3.ToString("yyyy-MM-dd HH:mm:ss.fff");


                    conn.Open();
                    using (SqlCommand StrQuer = new SqlCommand("BEGIN TRANSACTION " +
                        "INSERT INTO cli (clipas, fam, name, sname, birth, income, inn, snils, city," +
                        " adr, cadr, phone, jphone, email, accnum, updated) " +
                        $"VALUES ('{textBox1.Text}', N'{textBox2.Text}', N'{textBox3.Text}', N'{textBox3.Text}', '{sqlFormattedDate3}', '{textBox6.Text}', " +
                        $"'{textBox10.Text}', '{textBox9.Text}', '{citycode}', N'{textBox7.Text}', N'{textBox13.Text}', '{textBox12.Text}', '{textBox11.Text}', " +
                        $"'{textBox17.Text}', '{acccode}', '{sqlFormattedDate1}'); " +
                        $"INSERT INTO acc (Id, opens, closes, clipas) " +
                        $"VALUES ('{acccode}', '{sqlFormattedDate1}', '{sqlFormattedDate2}', '{textBox1.Text}'); " +
                        $"COMMIT", conn))
                    {
                        SqlDataReader dr = StrQuer.ExecuteReader();
                        conn.Close();
                    }
                    MessageBox.Show("Изменения сохранены");

                    test2.Clear();
                    int tmp = Int32.Parse(acccode);
                    test2.Add(tmp, myDateTime2.ToShortDateString());

                }
            }
            textBox8.Visible = false;
            changeBut(0);
            ronly();
            Program.curcpas = textBox1.Text;
            clidata(Program.curcpas);
            button2.PerformClick();
            comboBox2.Visible = true;
        }


        public bool checkcdata()  //проверка на повторение clipas, inn, snils
        {
            bool k = true;
            var conn = new SqlConnection();
            conn.ConnectionString = Program.str;

            using (SqlCommand StrQuer = new SqlCommand($"SELECT clipas, inn, snils " +
                $"FROM cli WHERE clipas LIKE '{textBox1.Text}%' AND inn LIKE '{textBox10.Text}%' " +
                $"AND snils LIKE '{textBox9.Text}%'", conn))
            {
                conn.Open();
                SqlDataReader dr = StrQuer.ExecuteReader();

                using (dr)
                {
                    while (dr.Read())
                    {
                        string tpas, tinn, tsnils;
                        string res = "Не могут повторяться:\n";
                        tpas = dr.GetString(0); //пасс
                        tinn = dr.GetString(1); //инн
                        tsnils = dr.GetString(2); //снилс
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
                        if (!k)
                            MessageBox.Show(res);
                    }
                }
                conn.Close();
            }
            return k;
        }


        //Обновление информации о человеке чей паспорт в textbox1
        public void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            var conn = new SqlConnection();
            conn.ConnectionString = Program.str;
            List<string[]> data = new List<string[]>();
            conn.Open();
            using (SqlCommand StrQuer = new SqlCommand($"SELECT contdepo.contid, contdate FROM contdepo WHERE(((contdepo.clipas) = '{textBox1.Text}'));", conn))
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
            using (SqlCommand StrQuer = new SqlCommand($"SELECT contcard.contid, contdate FROM contcard WHERE(((contcard.clipas) = '{textBox1.Text}'));", conn))
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
            using (SqlCommand StrQuer = new SqlCommand($"SELECT contloan.contid, contdate FROM contloan WHERE(((contloan.clipas) = '{textBox1.Text}'));", conn))
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
            try //при даблклике по названию колонки
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

            catch (Exception)
            { }
        }

        private void button3_Click(object sender, EventArgs e) //на карту
        {
            addcard tmp = new addcard();
            tmp.Show();
            tmp.create.PerformClick();
            tmp.textBox6.Text = textBox1.Text;

        }

        private void button7_Click(object sender, EventArgs e) //на вклад
        {
            adddepo tmp = new adddepo();
            tmp.Show();
            tmp.create.PerformClick();
            tmp.textBox6.Text = textBox1.Text;

        }

        private void button8_Click(object sender, EventArgs e) //на кредит
        {
            addloan tmp = new addloan();
            tmp.Show();
            tmp.create.PerformClick();
            tmp.textBox6.Text = textBox1.Text;

        }
    }
}
