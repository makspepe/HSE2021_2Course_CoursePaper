﻿using System;
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
    }
}
