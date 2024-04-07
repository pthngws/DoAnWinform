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
            this.SuspendLayout();
            // 
            // btnManage
            // 
            this.btnManage.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnManage.CheckedState.CustomBorderColor = System.Drawing.Color.RoyalBlue;
            this.btnManage.CheckedState.FillColor = System.Drawing.Color.White;
            this.btnManage.CustomBorderColor = System.Drawing.Color.White;
            this.btnManage.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.btnManage.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnManage.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnManage.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnManage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnManage.FillColor = System.Drawing.Color.White;
            this.btnManage.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btnManage.ForeColor = System.Drawing.Color.Black;
            this.btnManage.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnManage.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnManage.ImageOffset = new System.Drawing.Point(-5, -3);
            this.btnManage.ImageSize = new System.Drawing.Size(45, 45);
            this.btnManage.Location = new System.Drawing.Point(566, 42);
            this.btnManage.Name = "btnManage";
            this.btnManage.Size = new System.Drawing.Size(192, 39);
            this.btnManage.TabIndex = 25;
            this.btnManage.Text = "Sign Up";
            this.btnManage.Click += new System.EventHandler(this.btnManage_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnLogin.Checked = true;
            this.btnLogin.CheckedState.CustomBorderColor = System.Drawing.Color.RoyalBlue;
            this.btnLogin.CheckedState.FillColor = System.Drawing.Color.White;
            this.btnLogin.CustomBorderColor = System.Drawing.Color.White;
            this.btnLogin.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.btnLogin.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLogin.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLogin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLogin.FillColor = System.Drawing.Color.White;
            this.btnLogin.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.Black;
            this.btnLogin.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLogin.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnLogin.ImageOffset = new System.Drawing.Point(-5, -3);
            this.btnLogin.ImageSize = new System.Drawing.Size(45, 45);
            this.btnLogin.Location = new System.Drawing.Point(359, 42);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(211, 39);
            this.btnLogin.TabIndex = 24;
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // panelContainer
            // 
            this.panelContainer.Location = new System.Drawing.Point(339, 87);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(409, 436);
            this.panelContainer.TabIndex = 26;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 590);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.btnManage);
            this.Controls.Add(this.btnLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnManage;
        private Guna.UI2.WinForms.Guna2Button btnLogin;
        private Guna.UI2.WinForms.Guna2Panel panelContainer;
    }
}

