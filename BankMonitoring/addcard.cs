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

        public void loadInfo (string contnum) //загрузка данных по ид договора
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
                //надо удалить из contract //TODO
                //надо удалить из contcard , вроде можно объединить
                // where contract.Id = "ид выбранного"

                ronly();
                wipe();
                edit.Enabled = true;
                create.Enabled = true;
                save.Visible = false;
                revert.Visible = false;
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
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox8.ReadOnly = true;
        }

        //сохранение
        private void button3_Click(object sender, EventArgs e)
        {
            if (edit.Enabled == true)  //редакт
            {
                //проверка номера договора, карты на повторение (кроме себя самого)
                //проверка на существование паспорта кли, сотруд
                //проверка кому принадлежит счет, если себе(clientid)/null - добавить

                bool k = true;
                var conn = new SqlConnection();
                conn.ConnectionString = Program.str;


                //Данные текущего пользователя
                string emppas = "0", inn = "0", snils = "0", login = "0", password = "0", logid = "-1";
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
                            string tpas = "0", tinn = "0", tsnils = "0", tlogin = "0", tpassword = "0";
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
                int citycode = getcitycode(textBox8.Text);
                string jobcode = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key.ToString();
                string actcode = ((KeyValuePair<bool, string>)comboBox1.SelectedItem).Key.ToString();

                if (citycode == -1)
                {
                    MessageBox.Show("Город не найден");
                    return;
                }

                conn.Open();
                DateTime myDateTime1 = Convert.ToDateTime(textBox5.Text);
                string sqlFormattedDate1 = myDateTime1.ToString("yyyy-MM-dd HH:mm:ss.fff");
                DateTime myDateTime2 = DateTime.Now;
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
                MessageBox.Show("Изменения сохранены");





                loadInfo(textBox2.Text);
            }
            else //добавляем
            {
                textBox7.ReadOnly = true;
                //TODO
                //нужна проверка на наличие паспорта клиента в бд, если нет - ошибка
                //проверка кому принадлежит счет, если себе(clientid)/null - добавить


                //В конце обновляем выбранный контракт
                loadInfo(textBox2.Text);
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
                loadInfo(Program.curcont);
            else
                wipe();

            save.Visible = false;
            revert.Visible = false;
            ronly();
            edit.Enabled = true;
            create.Enabled = true;
        }

        //создать
        private void button5_Click(object sender, EventArgs e)
        {
            Program.curcont = null;
            wipe();
            write();
            edit.Enabled = false;
            save.Visible = true;
            revert.Visible = true;
            textBox7.Text = Program.emppas;
        }
    }
}
