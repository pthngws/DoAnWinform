using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAn01.Home.Manage.Dentist;

namespace DoAn01
{
    public partial class SignUp : UserControl
    {

        Random random = new Random();
        int otp;

        public SignUp()
        {
            InitializeComponent();
        }

        

        private void btnSendOTP_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                errorProvider1.SetError(txtEmail, "Vui lòng nhập Email");
            }
            else
            {
                otp = /*random.Next(100000, 1000000)*/ 1;
                /*
                                String SendMailFrom = "pthocwinform@gmail.com";
                                String SendMailTo = txtEmail.Text;
                                String SendMailSubject = "OTP COde";
                                String SendMailBody = otp.ToString();

                                try
                                {
                                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587)
                                    {
                                        DeliveryMethod = SmtpDeliveryMethod.Network
                                    };
                                    MailMessage email = new MailMessage
                                    {
                                        // START
                                        From = new MailAddress(SendMailFrom)
                                    };
                                    email.To.Add(SendMailTo);
                                    email.CC.Add(SendMailFrom);
                                    email.Subject = SendMailSubject;
                                    email.Body = SendMailBody;
                                    //END
                                    SmtpServer.Timeout = 10000;
                                    SmtpServer.EnableSsl = true;
                                    SmtpServer.UseDefaultCredentials = false;
                                    SmtpServer.Credentials = new NetworkCredential(SendMailFrom, "wuoq bkqm avnu ixvf");
                                    SmtpServer.Send(email);*/
/*
                MessageBox.Show("OTP đã được gửi.");
            }
                catch (Exception ex)
                {
                MessageBox.Show(ex.Message);
            }*/
        }

    }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            

        }

        private void btnSignUP_Click_1(object sender, EventArgs e)
        {
            string role;

            // Kiểm tra trạng thái của checkbox
            if (guna2CustomRadioButton1.Checked)
            {
                role = "staff";

            }
            else if (guna2CustomRadioButton2.Checked)
            {
                role = "dentist";
            }
            else
            {
                role = "user";
            }
            Global.setRole(role);
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            bool flag = true;

            if (txtUsername.Text == "")
            {
                errorProvider1.SetError(txtUsername, "Vui lòng nhập tên đăng nhập");
                flag = false;
            }
            if (txtPassword.Text == "")
            {
                errorProvider2.SetError(txtPassword, "Vui lòng nhập mật khẩu");
                flag = false;
            }
            if (txtRepass.Text == "")
            {
                errorProvider3.SetError(txtRepass, "Vui lòng nhập mật khẩu");
                flag = false;
            }
            if (txtEmail.Text == "")
            {
                errorProvider4.SetError(txtEmail, "Vui lòng nhập email");
                flag = false;
            }
            if (txtOTP.Text == "")
            {
                flag = false;
            }

            if (flag)
            {
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                string confirmPassword = txtRepass.Text;
                string email = txtEmail.Text;

                if (password == confirmPassword)
                {
                    try
                    {
                        
                        if (otp.ToString().Equals(txtOTP.Text))
                        {
                            if (role == "user")
                            {
                                User user = new User(username, password, email);
                                user.CreateUser();
                            }
                            else if (role == "staff")
                            {
                                Staff user = new Staff(username, password, email);
                                user.CreateStaff();
                            }
                            else if (role == "dentist")
                            {
                                Dentist user = new Dentist(username, password, email);
                                user.CreateDentist();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mã OTP không chính xác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại hoặc có lỗi xảy ra.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu và xác nhận mật khẩu không trùng khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }

        bool passwordChar = true;
        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            passwordChar = !passwordChar;
            if (passwordChar)
            {
                txtPassword.UseSystemPasswordChar = true;
                txtRepass.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
                txtRepass.UseSystemPasswordChar = false;
            }
        }

    }
}
