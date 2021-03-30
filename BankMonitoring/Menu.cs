using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace Bank
{

    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void searchb_Click(object sender, EventArgs e)
        {
            Поиск tmp = new Поиск();
            tmp.Show();
            return;
        }

        private void выходИзСистемыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Form> openForms = new List<Form>();

            foreach (Form f in Application.OpenForms)
                openForms.Add(f);

            foreach (Form f in openForms)
            {
                if (!(String.Equals(f.Name, "Login") || String.Equals(f.Name, "Menu")))
                    f.Close();
            }

            Login tmp = new Login();
            tmp.Show();
            this.Hide();
        }

        private void Меню_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Меню_VisibleChanged(object sender, EventArgs e) //показ имени пользователя
        {
            ФИО.Text = $"{Program.fam} {Program.name} {Program.sname}";
            if (Program.isgovernor)
            {
                ФИО.Text = ФИО.Text + ", Управляющий";
                addempb.Visible = true;
                label3.Visible = true;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) //личные данные
        {
            Program.curepas = Program.emppas; //чтобы при отмене не зануляло
            empinfo tmp = new empinfo();
            tmp.Show();
            tmp.empdata(Program.emppas);
            tmp.button2.PerformClick();
        }

        private void addempb_Click(object sender, EventArgs e) //добавление сотрудника
        {
            empinfo tmp = new empinfo();
            tmp.Show();
            Program.curepas = null;
            tmp.button5.PerformClick();
        }

        private void addclientb_Click(object sender, EventArgs e) //добавление клиента
        {
            clieinfo tmp = new clieinfo();
            tmp.Show();
            Program.curcpas = null;
            tmp.button5.PerformClick();

        }
    }
}
