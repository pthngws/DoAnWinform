namespace DoAn01
{
    partial class PhieuDieuTri
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
            this.comboboxService = new Guna.UI2.WinForms.Guna2ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.guna2NumericUpDown1 = new Guna.UI2.WinForms.Guna2NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.guna2NumericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboboxService
            // 
            this.comboboxService.BackColor = System.Drawing.Color.Transparent;
            this.comboboxService.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboboxService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxService.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboboxService.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboboxService.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboboxService.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comboboxService.ItemHeight = 30;
            this.comboboxService.Location = new System.Drawing.Point(153, 104);
            this.comboboxService.Name = "comboboxService";
            this.comboboxService.Size = new System.Drawing.Size(262, 36);
            this.comboboxService.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(511, 104);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 36);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // guna2NumericUpDown1
            // 
            this.guna2NumericUpDown1.BackColor = System.Drawing.Color.Transparent;
            this.guna2NumericUpDown1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2NumericUpDown1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2NumericUpDown1.Location = new System.Drawing.Point(421, 104);
            this.guna2NumericUpDown1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2NumericUpDown1.Name = "guna2NumericUpDown1";
            this.guna2NumericUpDown1.Size = new System.Drawing.Size(84, 36);
            this.guna2NumericUpDown1.TabIndex = 2;
            // 
            // PhieuDieuTri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 655);
            this.Controls.Add(this.guna2NumericUpDown1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboboxService);
            this.Name = "PhieuDieuTri";
            this.Text = "PhieuDieuTri";
            this.Load += new System.EventHandler(this.PhieuDieuTri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.guna2NumericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ComboBox comboboxService;
        private System.Windows.Forms.Button button1;
        private Guna.UI2.WinForms.Guna2NumericUpDown guna2NumericUpDown1;
    }
}