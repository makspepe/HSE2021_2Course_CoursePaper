using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank
{

    public partial class Меню : Form
    {
        public Меню()
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
            Login tmp = new Login();
            tmp.Show();
            this.Hide();
        }

        private void Меню_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Меню_VisibleChanged(object sender, EventArgs e) //смена имени пользователя
        {
            ФИО.Text = $"{Program.fam} {Program.name} {Program.sname}";
            if (Program.isgovernor)
            {
                ФИО.Text = ФИО.Text + ", Управляющий";
                addempb.Visible = true;
            }
            else
            {
                addempb.Visible = false;
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

        private void addclientb_Click(object sender, EventArgs e)
        {
            clieinfo tmp = new clieinfo();
            tmp.Show();
            //Program.curcpas = null;
            tmp.clidata(Program.curcpas); //TEST TODO
            tmp.button2.PerformClick(); //TEST
            //tmp.button5.PerformClick();

        }
    }
}
