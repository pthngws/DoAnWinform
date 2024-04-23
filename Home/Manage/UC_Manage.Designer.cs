namespace DoAn01.Home
{
    partial class UC_Manage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnService = new Guna.UI2.WinForms.Guna2Button();
            this.btnDentist = new Guna.UI2.WinForms.Guna2Button();
            this.btnPatient = new Guna.UI2.WinForms.Guna2Button();
            this.panelContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.Controls.Add(this.btnService);
            this.guna2Panel1.Controls.Add(this.btnDentist);
            this.guna2Panel1.Controls.Add(this.btnPatient);
            this.guna2Panel1.Font = new System.Drawing.Font("Century Gothic", 7.8F);
            this.guna2Panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(26)))), ((int)(((byte)(150)))));
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(225, 572);
            this.guna2Panel1.TabIndex = 0;
            // 
            // btnService
            // 
            this.btnService.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnService.CheckedState.CustomBorderColor = System.Drawing.Color.RoyalBlue;
            this.btnService.CheckedState.FillColor = System.Drawing.Color.Lavender;
            this.btnService.CustomBorderColor = System.Drawing.Color.White;
            this.btnService.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnService.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnService.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnService.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnService.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnService.FillColor = System.Drawing.Color.White;
            this.btnService.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btnService.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(26)))), ((int)(((byte)(150)))));
            this.btnService.HoverState.CustomBorderColor = System.Drawing.Color.RoyalBlue;
            this.btnService.Image = global::DoAn01.Properties.Resources.icons8_service_90__1_;
            this.btnService.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnService.ImageOffset = new System.Drawing.Point(-5, -3);
            this.btnService.ImageSize = new System.Drawing.Size(45, 45);
            this.btnService.Location = new System.Drawing.Point(0, 79);
            this.btnService.Name = "btnService";
            this.btnService.Size = new System.Drawing.Size(222, 44);
            this.btnService.TabIndex = 4;
            this.btnService.Text = "Service";
            this.btnService.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnService.Click += new System.EventHandler(this.btnService_Click);
            // 
            // btnDentist
            // 
            this.btnDentist.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnDentist.CheckedState.CustomBorderColor = System.Drawing.Color.RoyalBlue;
            this.btnDentist.CheckedState.FillColor = System.Drawing.Color.Lavender;
            this.btnDentist.CustomBorderColor = System.Drawing.Color.White;
            this.btnDentist.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnDentist.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDentist.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDentist.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDentist.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDentist.FillColor = System.Drawing.Color.White;
            this.btnDentist.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btnDentist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(26)))), ((int)(((byte)(150)))));
            this.btnDentist.HoverState.CustomBorderColor = System.Drawing.Color.RoyalBlue;
            this.btnDentist.Image = global::DoAn01.Properties.Resources.icons8_doctor_90;
            this.btnDentist.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDentist.ImageOffset = new System.Drawing.Point(-5, -3);
            this.btnDentist.ImageSize = new System.Drawing.Size(45, 45);
            this.btnDentist.Location = new System.Drawing.Point(0, 38);
            this.btnDentist.Name = "btnDentist";
            this.btnDentist.Size = new System.Drawing.Size(222, 44);
            this.btnDentist.TabIndex = 3;
            this.btnDentist.Text = "Dentist";
            this.btnDentist.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDentist.Click += new System.EventHandler(this.btnDentist_Click);
            // 
            // btnPatient
            // 
            this.btnPatient.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnPatient.Checked = true;
            this.btnPatient.CheckedState.CustomBorderColor = System.Drawing.Color.RoyalBlue;
            this.btnPatient.CheckedState.FillColor = System.Drawing.Color.Lavender;
            this.btnPatient.CustomBorderColor = System.Drawing.Color.White;
            this.btnPatient.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnPatient.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPatient.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPatient.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPatient.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPatient.FillColor = System.Drawing.Color.White;
            this.btnPatient.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btnPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(26)))), ((int)(((byte)(150)))));
            this.btnPatient.HoverState.CustomBorderColor = System.Drawing.Color.RoyalBlue;
            this.btnPatient.Image = global::DoAn01.Properties.Resources.icons8_client_90;
            this.btnPatient.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnPatient.ImageOffset = new System.Drawing.Point(-5, -3);
            this.btnPatient.ImageSize = new System.Drawing.Size(45, 45);
            this.btnPatient.Location = new System.Drawing.Point(0, 0);
            this.btnPatient.Name = "btnPatient";
            this.btnPatient.Size = new System.Drawing.Size(222, 44);
            this.btnPatient.TabIndex = 1;
            this.btnPatient.Text = "Patient";
            this.btnPatient.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnPatient.Click += new System.EventHandler(this.btnPatient_Click);
            // 
            // panelContainer
            // 
            this.panelContainer.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelContainer.Location = new System.Drawing.Point(228, -3);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1032, 575);
            this.panelContainer.TabIndex = 1;
            // 
            // UC_Manage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.guna2Panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UC_Manage";
            this.Size = new System.Drawing.Size(1260, 572);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel panelContainer;
        private Guna.UI2.WinForms.Guna2Button btnPatient;
        private Guna.UI2.WinForms.Guna2Button btnDentist;
        private Guna.UI2.WinForms.Guna2Button btnService;
    }
}
