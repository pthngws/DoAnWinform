using DoAn01.Home.Manage.Dentist;
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
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }
        public event EventHandler LoginButtonClicked;
        public event EventHandler LoginButtonFaceIDClicked;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string role;

            // Kiểm tra trạng thái của checkbox
            if (guna2CustomRadioButton1.Checked)
            {
                role = "staff";

            }
            else if(guna2CustomRadioButton2.Checked)
            {
                role = "dentist";
            }
            else
            {
                role = "user";
            }
            Global.setRole(role);
            Global.setID(txtUsername.Text);
            errorProvider1.Clear();
            errorProvider2.Clear();
            bool flag = true;
            if (txtUsername.Text == "")
            {
                errorProvider1.SetError(txtUsername, "Please enter your username");
                flag = false;
            }
            if (txtPassword.Text == "")
            {
                errorProvider2.SetError(txtPassword, "Please enter your password");
                flag = false;
            }
            if (flag)
            {
                switch (role)
                {
                    case "staff":
                        Staff staff = new Staff(txtUsername.Text, txtPassword.Text);
                        if (staff.LoginStaff()!=null)
                        {

                            Main main = new Main();
                            main.StartPosition = FormStartPosition.CenterParent;
                            main.ShowDialog();
                            LoginButtonClicked?.Invoke(this, EventArgs.Empty);
                        }
                        break;

                    case "user":
                        User user = new User(txtUsername.Text, txtPassword.Text);
                        if (user.LoginUser() != null)
                        {
                            Main main = new Main();
                            main.StartPosition = FormStartPosition.CenterParent;
                            main.ShowDialog();
                            LoginButtonClicked?.Invoke(this, EventArgs.Empty);
                        }
                        break;

                    case "dentist":
                        Dentist dentist = new Dentist(txtUsername.Text, txtPassword.Text, null);
                        if (dentist.LoginDentist() != null)
                        {
                            Main main = new Main();
                            main.StartPosition = FormStartPosition.CenterParent;
                            main.ShowDialog();
                            LoginButtonClicked?.Invoke(this, EventArgs.Empty);
                        }
                        break;

                    default:
                        // Xử lý nếu role không phù hợp với bất kỳ trường hợp nào
                        break;
                }

            }
        }
        bool passwordChar = true;
        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            passwordChar = !passwordChar;
            if (passwordChar)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
            }
        }




        private void Login_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }



        private void txtPassword_MouseClick(object sender, MouseEventArgs e)
        {

            guna2CheckBox1.Checked = false;
        }
        LoginFaceID loginFaceID;
        private void btnFaceID_Click(object sender, EventArgs e)
        {
            // Tạo và hiển thị form LoginFaceID
            LoginFaceID loginFaceID = new LoginFaceID();

            // Thiết lập StartPosition để form mới mở ở giữa màn hình
            loginFaceID.StartPosition = FormStartPosition.CenterScreen;

            loginFaceID.FormClosed += LoginFaceID_FormClosing; // Gắn sự kiện FormClosed
            loginFaceID.Show(); // Sử dụng Show() thay vì ShowDialog()
        }

        // Xử lý sự kiện FormClosed của form LoginFaceID
        private void LoginFaceID_FormClosing(object sender, FormClosedEventArgs e)
        {

            // Kiểm tra nếu form cha của Login là form
            if (this.ParentForm != null && this.ParentForm is Form parentForm)
            {
                parentForm.Hide(); // Ẩn form cha của Login khi LoginFaceID ẩn
            }
        }

        private void linkForgotPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ResetPassword resetPassword = new ResetPassword();
            resetPassword.StartPosition = FormStartPosition.CenterScreen;
            resetPassword.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
