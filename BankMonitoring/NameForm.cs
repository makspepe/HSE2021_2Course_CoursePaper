using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeView
{
    public partial class NameForm : Form //обновляем название
    {
        public string NameAtte = "";
        private CRUD mode;
        public bool child = false;
        public NameForm(CRUD mode)
        {
            this.mode = mode;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NameAtte = textBox1.Text;
        }

        private void NameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            child = childCheckBox.Checked;
        }

        private void NameForm_Load(object sender, EventArgs e)
        {
            if (mode != CRUD.Create) //создать потомка нельзя при обновлении
            {
                childCheckBox.Hide();
            }
        }
    }
}
