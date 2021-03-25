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
    public partial class Поиск : Form
    {
        public Поиск()
        {
            InitializeComponent();
        }
        Dictionary<int, string> test2 = new Dictionary<int, string>();
        Dictionary<int, string> test3 = new Dictionary<int, string>();

        //Поиск
        private void button1_Click(object sender, EventArgs e)
        {
            var conn = new SqlConnection();
            conn.ConnectionString = Program.str;
            bool found = false;
            int citycode = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
            int jobcode = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key;
            conn.Open();
            if (radioButton1.Checked)
            {
                dataGridView1.Rows.Clear();
                List<string[]> data = new List<string[]>();

                using (SqlCommand StrQuer = new SqlCommand($"SELECT * " +
                    $"FROM cli INNER JOIN city ON cli.city = city.Id " +
                    $"WHERE cli.clipas LIKE '{textBox1.Text}%' AND fam LIKE '{textBox2.Text}%' AND cli.name LIKE '{textBox3.Text}%' " +
                    $"AND sname LIKE '{textBox4.Text}%' AND birth LIKE '%{datereverse(textBox5.Text)}%' AND income LIKE '{textBox6.Text}%' " +
                    $"AND inn LIKE '{textBox10.Text}%' AND snils LIKE '{textBox9.Text}%' AND cli.city LIKE '{citycode}%' " +
                    $"AND updated LIKE '%{datereverse(textBox15.Text)}%' AND adr LIKE '{textBox7.Text}%' AND cadr LIKE '{textBox13.Text}%' " +
                    $"AND phone LIKE '{textBox12.Text}%' AND jphone LIKE '{textBox11.Text}%' AND email LIKE '{textBox17.Text}%' ", conn))
                { 
                    SqlDataReader dr = StrQuer.ExecuteReader();
                    using (dr)
                    {
                        
                        while (dr.Read())
                        {
                            found = true;
                            data.Add(new string[15]);
                            data[data.Count - 1][0] = dr[0].ToString(); //пас
                            data[data.Count - 1][1] = dr[1].ToString(); // фам       
                            data[data.Count - 1][2] = dr[2].ToString(); //имя
                            data[data.Count - 1][3] = dr[3].ToString(); //от
                            data[data.Count - 1][4] = dr[4].ToString().Replace("0:00:00",""); //др
                            data[data.Count - 1][5] = dr[5].ToString().Replace(",0000", ""); //доход
                            data[data.Count - 1][6] = dr[6].ToString(); //ИНН
                            data[data.Count - 1][7] = dr[7].ToString(); //снилс
                            data[data.Count - 1][8] = dr[17].ToString(); //Город
                            data[data.Count - 1][9] = dr[9].ToString(); //адр
                            data[data.Count - 1][10] = dr[10].ToString(); //тадр
                            data[data.Count - 1][11] = dr[11].ToString(); //телефон
                            data[data.Count - 1][12] = dr[12].ToString(); //рабтелефон
                            data[data.Count - 1][13] = dr[13].ToString(); //емейл
                            data[data.Count - 1][14] = dr[15].ToString().Replace("0:00:00", ""); //обновлено
                        }
                       
                    }
                }
                foreach (string[] s in data)
                    dataGridView1.Rows.Add(s);
            }

            else if (radioButton2.Checked)
            {
                dataGridView2.Rows.Clear();
                List<string[]> data = new List<string[]>();

                using (SqlCommand StrQuer = new SqlCommand($"SELECT * " +
                    $"FROM emp INNER JOIN city ON emp.city = city.Id INNER JOIN job ON emp.job = job.Id " +
                    $"WHERE emp.emppas LIKE '{textBox1.Text}%' AND fam LIKE '{textBox2.Text}%' AND emp.name LIKE '{textBox3.Text}%' " +
                    $"AND sname LIKE '{textBox4.Text}%' AND birth LIKE '%{datereverse(textBox5.Text)}%' AND job.Id LIKE '{jobcode}%' " +
                    $"AND inn LIKE '{textBox10.Text}%' AND snils LIKE '{textBox9.Text}%' AND emp.city LIKE '{citycode}%' " +
                    $"AND updated LIKE '%{datereverse(textBox15.Text)}%' AND adr LIKE '{textBox7.Text}%' AND cadr LIKE '{textBox13.Text}%' " +
                    $"AND phone LIKE '{textBox12.Text}%' AND jphone LIKE '{textBox11.Text}%' AND email LIKE '{textBox17.Text}%' ", conn))
                {
                    SqlDataReader dr = StrQuer.ExecuteReader();
                    using (dr)
                    {

                        while (dr.Read())
                        {
                            found = true;
                            data.Add(new string[15]);
                            data[data.Count - 1][0] = dr[0].ToString(); //пас
                            data[data.Count - 1][1] = dr[1].ToString(); // фам       
                            data[data.Count - 1][2] = dr[2].ToString(); //имя
                            data[data.Count - 1][3] = dr[3].ToString(); //от
                            data[data.Count - 1][4] = dr[4].ToString().Replace("0:00:00", ""); //др
                            data[data.Count - 1][5] = dr[19].ToString().Replace(",0000", ""); //job 19
                            data[data.Count - 1][6] = dr[6].ToString(); //ИНН
                            data[data.Count - 1][7] = dr[7].ToString(); //снилс
                            data[data.Count - 1][8] = dr[17].ToString(); //Город
                            data[data.Count - 1][9] = dr[9].ToString(); //адр
                            data[data.Count - 1][10] = dr[10].ToString(); //тадр
                            data[data.Count - 1][11] = dr[11].ToString(); //телефон
                            data[data.Count - 1][12] = dr[12].ToString(); //рабтелефон
                            data[data.Count - 1][13] = dr[13].ToString(); //емейл
                            data[data.Count - 1][14] = dr[14].ToString().Replace("0:00:00", ""); //обновлено
                        }

                    }
                }
                foreach (string[] s in data)
                    dataGridView2.Rows.Add(s);
            }
            else
            {
                dataGridView3.Rows.Clear();
                List<string[]> data = new List<string[]>();

                using (SqlCommand StrQuer = new SqlCommand($"SELECT * " +
                    $"FROM contract " +
                    $"WHERE Id LIKE '{textBox7.Text}%' ", conn))
                {
                    SqlDataReader dr = StrQuer.ExecuteReader();
                    using (dr)
                    {

                        while (dr.Read())
                        {
                            found = true;
                            data.Add(new string[1]);
                            data[data.Count - 1][0] = dr[0].ToString(); //ид
                        }
                    }
                }
                foreach (string[] s in data)
                    dataGridView3.Rows.Add(s);
            }
            conn.Close();
            if (!found)
                MessageBox.Show("Ничего не найдено");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        // При клике на ячейку - переход на форму просмотра
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var item = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            Program.curcpas = item.ToString(); //чтобы при отмене не зануляло
            clieinfo tmp = new clieinfo();
            tmp.Show();
            tmp.clidata(Program.curcpas);
            tmp.button2.PerformClick();

        }

        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var item = dataGridView2.Rows[e.RowIndex].Cells[0].Value;
            Program.curepas = item.ToString(); //чтобы при отмене не зануляло
            empinfo tmp = new empinfo();
            tmp.Show();
            tmp.empdata(Program.curepas);
            tmp.button2.PerformClick();
        }
        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var conn = new SqlConnection();
                conn.ConnectionString = Program.str;
                var item = dataGridView3.Rows[e.RowIndex].Cells[0].Value;
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

        // Change view when radio clicked
        private void radioButton1_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            textBox6.Visible = true;
            comboBox1.Visible = true;
            comboBox2.Visible = false;
            textBox9.Visible = true;
            textBox10.Visible = true;
            textBox11.Visible = true;
            textBox12.Visible = true;
            textBox13.Visible = true;
            textBox15.Visible = true;
            textBox17.Visible = true;

            label2.Visible = true;
            label2.Text = "Паспорт клиента";
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label7.Text = "Доход";
            label8.Text = "Адрес прописки";
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            label12.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label16.Visible = true;
            label18.Visible = true;
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;


        }

        private void radioButton2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            textBox6.Visible = false;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            textBox9.Visible = true;
            textBox10.Visible = true;
            textBox11.Visible = true;
            textBox12.Visible = true;
            textBox13.Visible = true;
            textBox15.Visible = true;
            textBox17.Visible = true;

            label2.Visible = true;
            label2.Text = "Паспорт сотрудника";
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label7.Text = "Должность";
            label8.Text = "Адрес прописки";
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            label12.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label16.Visible = true;
            label18.Visible = true;

            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            dataGridView3.Visible = false;
        }

        private void radioButton3_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            textBox9.Visible = false;
            textBox10.Visible = false;
            textBox11.Visible = false;
            textBox12.Visible = false;
            textBox13.Visible = false;
            textBox15.Visible = false;
            textBox17.Visible = false;

            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Text = "Номер договора";
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label16.Visible = false;
            label18.Visible = false;

            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            dataGridView3.Visible = true;
        }

        private void Поиск_VisibleChanged(object sender, EventArgs e)
        {
        }

        private void Поиск_Load(object sender, EventArgs e)
        {

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
            //город
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

                comboBox1.DataSource = new BindingSource(test3, null);
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";

                comboBox2.DataSource = new BindingSource(test2, null);
                comboBox2.DisplayMember = "Value";
                comboBox2.ValueMember = "Key";
            }
        }

        private string datereverse(string s)
        {
            s.Trim();
            if (s.Length > 0)
            {
                string[] sa = s.Split('.');
                s = $"{sa[2]}-{sa[1]}-{sa[0]}";
            }
            return s;
        }
    }
}
