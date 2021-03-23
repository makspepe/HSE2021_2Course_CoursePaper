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

        private void Меню_VisibleChanged(object sender, EventArgs e)
        {
            label1.Text = $"{Program.fam} {Program.name} {Program.sname}";
            if (Program.isgovernor)
                label1.Text = label1.Text + ", Управляющий";
            //обновление штук после логина
        }
    }
}
