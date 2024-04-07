using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn01
{
    public partial class ResetPassword : Form
    {
        public ResetPassword()
        {
            InitializeComponent();


            addUserControl(verify);


        }
        ChangePassword changePassword = new ChangePassword();
        Verify verify = new Verify();
        private void verify_VisibleChanged(object sender, EventArgs e)
        {
            if (!verify.Visible)
            {
                changePassword.Email = verify.email;
                addUserControl(changePassword);
            }
        }
        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }
        private void ResetPassword_Load(object sender, EventArgs e)
        {

        }
    }
}
