namespace BS.Client
{
    partial class frmClientFilter
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
            this.ctrlClientInfoWithFIlter1 = new BS.Client.Controls.ctrlClientInfoWithFIlter();
            this.lbTitle = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlClientInfoWithFIlter1
            // 
            this.ctrlClientInfoWithFIlter1.AutoSize = true;
            this.ctrlClientInfoWithFIlter1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctrlClientInfoWithFIlter1.Location = new System.Drawing.Point(13, 75);
            this.ctrlClientInfoWithFIlter1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlClientInfoWithFIlter1.Name = "ctrlClientInfoWithFIlter1";
            this.ctrlClientInfoWithFIlter1.Size = new System.Drawing.Size(775, 356);
            this.ctrlClientInfoWithFIlter1.TabIndex = 0;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.lbTitle.ForeColor = System.Drawing.Color.Red;
            this.lbTitle.Location = new System.Drawing.Point(311, 33);
            this.lbTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(178, 37);
            this.lbTitle.TabIndex = 74;
            this.lbTitle.Text = "Client Filter";
            // 
            // btnSelect
            // 
            this.btnSelect.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSelect.Image = global::BS.Properties.Resources.select;
            this.btnSelect.Location = new System.Drawing.Point(675, 439);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(113, 53);
            this.btnSelect.TabIndex = 75;
            this.btnSelect.Text = "Select";
            this.btnSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // frmClientFilter
            // 
            this.AcceptButton = this.btnSelect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 501);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.ctrlClientInfoWithFIlter1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmClientFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ctrlClientInfoWithFIlter ctrlClientInfoWithFIlter1;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Button btnSelect;
    }
}