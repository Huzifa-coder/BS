﻿namespace BS.Invoice
{
    partial class frmInvoicesList
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
            this.tbFilterValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbRecords = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvInvoices = new System.Windows.Forms.DataGridView();
            this.cmsInvoices = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddNewInvoice = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).BeginInit();
            this.cmsInvoices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbFilterValue
            // 
            this.tbFilterValue.Location = new System.Drawing.Point(243, 230);
            this.tbFilterValue.Margin = new System.Windows.Forms.Padding(2);
            this.tbFilterValue.Name = "tbFilterValue";
            this.tbFilterValue.Size = new System.Drawing.Size(139, 26);
            this.tbFilterValue.TabIndex = 87;
            this.tbFilterValue.Visible = false;
            this.tbFilterValue.TextChanged += new System.EventHandler(this.tbFilterValue_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label4.Location = new System.Drawing.Point(11, 597);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 24);
            this.label4.TabIndex = 86;
            this.label4.Text = "# Records : ";
            // 
            // lbRecords
            // 
            this.lbRecords.AutoSize = true;
            this.lbRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lbRecords.Location = new System.Drawing.Point(122, 597);
            this.lbRecords.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbRecords.Name = "lbRecords";
            this.lbRecords.Size = new System.Drawing.Size(30, 24);
            this.lbRecords.TabIndex = 85;
            this.lbRecords.Text = "00";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "ID",
            "First Name",
            "Last Name"});
            this.cbFilterBy.Location = new System.Drawing.Point(100, 230);
            this.cbFilterBy.Margin = new System.Windows.Forms.Padding(2);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(139, 28);
            this.cbFilterBy.TabIndex = 83;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label2.Location = new System.Drawing.Point(11, 228);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 24);
            this.label2.TabIndex = 82;
            this.label2.Text = "Filter By :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(370, 187);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 31);
            this.label1.TabIndex = 81;
            this.label1.Text = "Manage Invoices";
            // 
            // dgvInvoices
            // 
            this.dgvInvoices.AllowUserToAddRows = false;
            this.dgvInvoices.AllowUserToDeleteRows = false;
            this.dgvInvoices.AllowUserToResizeColumns = false;
            this.dgvInvoices.AllowUserToResizeRows = false;
            this.dgvInvoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInvoices.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvInvoices.BackgroundColor = System.Drawing.Color.White;
            this.dgvInvoices.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvInvoices.ColumnHeadersHeight = 29;
            this.dgvInvoices.ContextMenuStrip = this.cmsInvoices;
            this.dgvInvoices.Location = new System.Drawing.Point(15, 268);
            this.dgvInvoices.Margin = new System.Windows.Forms.Padding(2);
            this.dgvInvoices.MultiSelect = false;
            this.dgvInvoices.Name = "dgvInvoices";
            this.dgvInvoices.ReadOnly = true;
            this.dgvInvoices.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvInvoices.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvInvoices.RowTemplate.Height = 24;
            this.dgvInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInvoices.Size = new System.Drawing.Size(929, 308);
            this.dgvInvoices.StandardTab = true;
            this.dgvInvoices.TabIndex = 80;
            // 
            // cmsInvoices
            // 
            this.cmsInvoices.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsInvoices.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editInfoToolStripMenuItem,
            this.toolStripMenuItem1,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.refreshToolStripMenuItem});
            this.cmsInvoices.Name = "contextMenuStrip1";
            this.cmsInvoices.Size = new System.Drawing.Size(157, 106);
            // 
            // editInfoToolStripMenuItem
            // 
            this.editInfoToolStripMenuItem.Image = global::BS.Properties.Resources.edit;
            this.editInfoToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editInfoToolStripMenuItem.Name = "editInfoToolStripMenuItem";
            this.editInfoToolStripMenuItem.Size = new System.Drawing.Size(156, 30);
            this.editInfoToolStripMenuItem.Text = "Edit Invoice";
            this.editInfoToolStripMenuItem.Click += new System.EventHandler(this.editInfoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(153, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::BS.Properties.Resources.delete;
            this.deleteToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(156, 30);
            this.deleteToolStripMenuItem.Text = "Delete Invoice";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(153, 6);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Image = global::BS.Properties.Resources.refresh;
            this.refreshToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(156, 30);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnClose.Image = global::BS.Properties.Resources.close;
            this.btnClose.Location = new System.Drawing.Point(820, 583);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(123, 48);
            this.btnClose.TabIndex = 88;
            this.btnClose.Text = " Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAddNewInvoice
            // 
            this.btnAddNewInvoice.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAddNewInvoice.Image = global::BS.Properties.Resources.add;
            this.btnAddNewInvoice.Location = new System.Drawing.Point(885, 218);
            this.btnAddNewInvoice.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddNewInvoice.Name = "btnAddNewInvoice";
            this.btnAddNewInvoice.Size = new System.Drawing.Size(59, 45);
            this.btnAddNewInvoice.TabIndex = 0;
            this.btnAddNewInvoice.UseVisualStyleBackColor = true;
            this.btnAddNewInvoice.Click += new System.EventHandler(this.btnAddNewInvoice_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::BS.Properties.Resources.Invoice_List_100;
            this.pictureBox1.Location = new System.Drawing.Point(395, 11);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(168, 174);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 79;
            this.pictureBox1.TabStop = false;
            // 
            // frmInvoicesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(954, 636);
            this.Controls.Add(this.tbFilterValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbRecords);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvInvoices);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddNewInvoice);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmInvoicesList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invoice";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInvoicesList_FormClosing);
            this.Load += new System.EventHandler(this.frmInvoicesList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).EndInit();
            this.cmsInvoices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFilterValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbRecords;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvInvoices;
        private System.Windows.Forms.ContextMenuStrip cmsInvoices;
        private System.Windows.Forms.ToolStripMenuItem editInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAddNewInvoice;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}