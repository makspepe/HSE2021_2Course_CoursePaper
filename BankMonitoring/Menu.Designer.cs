
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
            this.addempb = new System.Windows.Forms.Button();
            this.addclientb = new System.Windows.Forms.Button();
            this.searchb = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.выходИзСистемыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(341, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Меню
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
    }
}