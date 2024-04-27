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


        Verify verify = new Verify();
        public void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }
        public ResetPassword()
        {
            InitializeComponent();
            verify.VerifySuccess += Verify_VerifySuccess; // Gắn sự kiện cho VerifySuccess
            addUserControl(verify);
        }

        // Xử lý sự kiện khi xác thực thành công
        private void Verify_VerifySuccess(object sender, EventArgs e)
        {
            ChangePassword changePassword = new ChangePassword(verify.email);
            MessageBox.Show(verify.email);
            addUserControl(changePassword); // Mở form ChangePassword
        }

    }
}
