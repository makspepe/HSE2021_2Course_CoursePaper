
namespace Bank
{
    partial class Меню
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.выходИзСистемыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ФИО = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.addempb = new System.Windows.Forms.Button();
            this.addclientb = new System.Windows.Forms.Button();
            this.searchb = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.выходИзСистемыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(107, 20);
            this.toolStripMenuItem1.Text = "Личные данные";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // выходИзСистемыToolStripMenuItem
            // 
            this.выходИзСистемыToolStripMenuItem.Name = "выходИзСистемыToolStripMenuItem";
            this.выходИзСистемыToolStripMenuItem.Size = new System.Drawing.Size(120, 20);
            this.выходИзСистемыToolStripMenuItem.Text = "Выход из системы";
            this.выходИзСистемыToolStripMenuItem.Click += new System.EventHandler(this.выходИзСистемыToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // ФИО
            // 
            this.ФИО.AutoSize = true;
            this.ФИО.Location = new System.Drawing.Point(341, 9);
            this.ФИО.Name = "ФИО";
            this.ФИО.Size = new System.Drawing.Size(34, 13);
            this.ФИО.TabIndex = 5;
            this.ФИО.Text = "ФИО";
            this.ФИО.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(506, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Поиск существующего клиента/сотр/дог";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(572, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Добавление нового клиента";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(555, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Добавление нового сотрудника";
            // 
            // addempb
            // 
            this.addempb.BackgroundImage = global::Bank.Properties.Resources.employee;
            this.addempb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.addempb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addempb.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.addempb.FlatAppearance.BorderSize = 0;
            this.addempb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addempb.ForeColor = System.Drawing.Color.Black;
            this.addempb.Location = new System.Drawing.Point(729, 118);
            this.addempb.Name = "addempb";
            this.addempb.Size = new System.Drawing.Size(59, 47);
            this.addempb.TabIndex = 3;
            this.addempb.UseVisualStyleBackColor = true;
            this.addempb.Visible = false;
            this.addempb.Click += new System.EventHandler(this.addempb_Click);
            // 
            // addclientb
            // 
            this.addclientb.BackgroundImage = global::Bank.Properties.Resources.client;
            this.addclientb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.addclientb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addclientb.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.addclientb.FlatAppearance.BorderSize = 0;
            this.addclientb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addclientb.ForeColor = System.Drawing.Color.Black;
            this.addclientb.Location = new System.Drawing.Point(729, 65);
            this.addclientb.Name = "addclientb";
            this.addclientb.Size = new System.Drawing.Size(59, 47);
            this.addclientb.TabIndex = 2;
            this.addclientb.UseVisualStyleBackColor = true;
            // 
            // searchb
            // 
            this.searchb.BackgroundImage = global::Bank.Properties.Resources.search_flat;
            this.searchb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.searchb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.searchb.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.searchb.FlatAppearance.BorderSize = 0;
            this.searchb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchb.ForeColor = System.Drawing.Color.Black;
            this.searchb.Location = new System.Drawing.Point(729, 12);
            this.searchb.Name = "searchb";
            this.searchb.Size = new System.Drawing.Size(59, 47);
            this.searchb.TabIndex = 1;
            this.searchb.UseVisualStyleBackColor = true;
            this.searchb.Click += new System.EventHandler(this.searchb_Click);
            // 
            // Меню
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ФИО);
            this.Controls.Add(this.addempb);
            this.Controls.Add(this.addclientb);
            this.Controls.Add(this.searchb);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Меню";
            this.Text = "Меню";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Меню_FormClosed);
            this.VisibleChanged += new System.EventHandler(this.Меню_VisibleChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button searchb;
        private System.Windows.Forms.Button addclientb;
        private System.Windows.Forms.Button addempb;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem выходИзСистемыToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label ФИО;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}