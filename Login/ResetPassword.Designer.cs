namespace DoAn01
{
    partial class ResetPassword
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
            this.verify1 = new DoAn01.Verify();
            this.changePassword1 = new DoAn01.ChangePassword();
            this.panelContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.SuspendLayout();
            // 
            // verify1
            // 
            this.verify1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.verify1.Location = new System.Drawing.Point(11, 308);
            this.verify1.Name = "verify1";
            this.verify1.Size = new System.Drawing.Size(542, 355);
            this.verify1.TabIndex = 1;
            // 
            // changePassword1
            // 
            this.changePassword1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.changePassword1.Email = null;
            this.changePassword1.Location = new System.Drawing.Point(0, 12);
            this.changePassword1.Name = "changePassword1";
            this.changePassword1.Size = new System.Drawing.Size(542, 355);
            this.changePassword1.TabIndex = 0;
            // 
            // panelContainer
            // 
            this.panelContainer.Location = new System.Drawing.Point(1, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(542, 355);
            this.panelContainer.TabIndex = 0;
            // 
            // ResetPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 355);
            this.Controls.Add(this.panelContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ResetPassword";
            this.Text = "ResetPassword";
            this.Load += new System.EventHandler(this.ResetPassword_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Verify verify1;
        private ChangePassword changePassword1;
        private Guna.UI2.WinForms.Guna2Panel panelContainer;
    }
}