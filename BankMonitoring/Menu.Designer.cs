﻿
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
            this.addempb = new System.Windows.Forms.Button();
            this.addclientb = new System.Windows.Forms.Button();
            this.searchb = new System.Windows.Forms.Button();
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
            this.addempb.Location = new System.Drawing.Point(729, 125);
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
            this.addclientb.Location = new System.Drawing.Point(729, 72);
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
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.addempb);
            this.Controls.Add(this.addclientb);
            this.Controls.Add(this.searchb);
            this.Name = "Меню";
            this.Text = "Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button searchb;
        private System.Windows.Forms.Button addclientb;
        private System.Windows.Forms.Button addempb;
    }
}