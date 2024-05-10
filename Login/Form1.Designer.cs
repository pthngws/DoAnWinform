namespace DoAn01
{
    partial class Form1
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
            this.btnManage = new Guna.UI2.WinForms.Guna2Button();
            this.btnLogin = new Guna.UI2.WinForms.Guna2Button();
            this.panelContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnManage
            // 
            this.btnManage.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnManage.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(26)))), ((int)(((byte)(150)))));
            this.btnManage.CheckedState.FillColor = System.Drawing.Color.White;
            this.btnManage.CustomBorderColor = System.Drawing.Color.White;
            this.btnManage.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.btnManage.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnManage.DisabledState.BorderColor = System.Drawing.Color.White;
            this.btnManage.DisabledState.CustomBorderColor = System.Drawing.Color.White;
            this.btnManage.DisabledState.FillColor = System.Drawing.Color.White;
            this.btnManage.DisabledState.ForeColor = System.Drawing.Color.White;
            this.btnManage.FillColor = System.Drawing.Color.White;
            this.btnManage.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btnManage.ForeColor = System.Drawing.Color.Black;
            this.btnManage.HoverState.BorderColor = System.Drawing.Color.White;
            this.btnManage.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btnManage.HoverState.FillColor = System.Drawing.Color.White;
            this.btnManage.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnManage.ImageOffset = new System.Drawing.Point(-5, -3);
            this.btnManage.ImageSize = new System.Drawing.Size(45, 45);
            this.btnManage.Location = new System.Drawing.Point(939, 81);
            this.btnManage.Name = "btnManage";
            this.btnManage.Size = new System.Drawing.Size(186, 39);
            this.btnManage.TabIndex = 25;
            this.btnManage.Text = "Sign Up";
            this.btnManage.Click += new System.EventHandler(this.btnManage_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.AliceBlue;
            this.btnLogin.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnLogin.Checked = true;
            this.btnLogin.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(26)))), ((int)(((byte)(150)))));
            this.btnLogin.CheckedState.FillColor = System.Drawing.Color.White;
            this.btnLogin.CustomBorderColor = System.Drawing.Color.White;
            this.btnLogin.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.btnLogin.DisabledState.BorderColor = System.Drawing.Color.White;
            this.btnLogin.DisabledState.CustomBorderColor = System.Drawing.Color.White;
            this.btnLogin.DisabledState.FillColor = System.Drawing.Color.White;
            this.btnLogin.DisabledState.ForeColor = System.Drawing.Color.White;
            this.btnLogin.FillColor = System.Drawing.Color.White;
            this.btnLogin.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.Black;
            this.btnLogin.HoverState.BorderColor = System.Drawing.Color.White;
            this.btnLogin.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btnLogin.HoverState.FillColor = System.Drawing.Color.White;
            this.btnLogin.HoverState.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnLogin.ImageOffset = new System.Drawing.Point(-5, -3);
            this.btnLogin.ImageSize = new System.Drawing.Size(45, 45);
            this.btnLogin.Location = new System.Drawing.Point(732, 81);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(205, 39);
            this.btnLogin.TabIndex = 24;
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // panelContainer
            // 
            this.panelContainer.BorderRadius = 10;
            this.panelContainer.Location = new System.Drawing.Point(723, 126);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(409, 436);
            this.panelContainer.TabIndex = 26;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackgroundImage = global::DoAn01.Properties.Resources.login;
            this.guna2Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 81);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(697, 481);
            this.guna2Panel1.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(26)))), ((int)(((byte)(150)))));
            this.label1.Location = new System.Drawing.Point(772, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 43);
            this.label1.TabIndex = 28;
            this.label1.Text = "DENTAL SMILE";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1148, 590);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.btnManage);
            this.Controls.Add(this.btnLogin);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnManage;
        private Guna.UI2.WinForms.Guna2Button btnLogin;
        private Guna.UI2.WinForms.Guna2Panel panelContainer;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label1;
    }
}

