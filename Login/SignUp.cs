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
using System.Data.SqlClient;
using DoAn01.Home.Manage.Medicine;

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
                errorProvider1.SetError(txtEmail, "Please enter Email");
            }
            else
            {
                otp = random.Next(100000, 1000000) ;

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
                    SmtpServer.Send(email);

                    MessageBox.Show("OTP has been sent.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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
                errorProvider1.SetError(txtUsername, "Please enter your username");
                flag = false;
            }
            if (txtPassword.Text == "")
            {
                errorProvider2.SetError(txtPassword, "Please enter your password");
                flag = false;
            }
            if (txtRepass.Text == "")
            {
                errorProvider3.SetError(txtRepass, "Please confirm your password");
                flag = false;
            }
            if (txtEmail.Text == "")
            {
                errorProvider4.SetError(txtEmail, "Please enter your Email");
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
                            MessageBox.Show("OTP code is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Username already exists or an error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Password and confirm password do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            }

        MY_DB mydb = new MY_DB();
        Staff staff = new Staff();
        Dentist dentist = new Dentist();

        private void SignUp_Load(object sender, EventArgs e)
        {
            if (guna2CustomRadioButton2.Checked)
            {
                txtUsername.Enabled = false;
                txtUsername.Text = dentist.id;
                if (string.IsNullOrEmpty(txtUsername.Text))
                {
                    
                    // Thực hiện truy vấn SQL để lấy ID lớn nhất từ cột "id" trong bảng "schedule"
                    string query = "SELECT MAX(CAST(SUBSTRING(id, 2, LEN(id)) AS INT)) FROM dentist";

                    SqlCommand command = new SqlCommand(query, mydb.getConnection);

                    mydb.openConnection();

                    object result = command.ExecuteScalar();
                    int nextID = 1;

                    if (result != DBNull.Value)
                    {
                        // Nếu có kết quả, tăng giá trị lên một
                        nextID = Convert.ToInt32(result) + 1;
                    }

                    mydb.closeConnection();

                    // Tạo ID mới với định dạng "S" + số, ví dụ: S01, S02, vv
                    string newID = "D" + nextID.ToString("00");
                    txtUsername.Text = newID;

                }
                else
                {
                    // Nếu txtID.Text không rỗng, hiển thị giá trị của txtID.Text
                    txtUsername.Text = dentist.id;
                }
            }
            if (guna2CustomRadioButton1.Checked)
            {
                txtUsername.Enabled = false;

                txtUsername.Text = staff.id;
                if (string.IsNullOrEmpty(txtUsername.Text))
                {
                    // Thực hiện truy vấn SQL để lấy ID lớn nhất từ cột "id" trong bảng "schedule"
                    string query = "SELECT MAX(CAST(SUBSTRING(id, 3, LEN(id)) AS INT)) FROM staff";

                    SqlCommand command = new SqlCommand(query, mydb.getConnection);

                    mydb.openConnection();

                    object result = command.ExecuteScalar();
                    int nextID = 1;

                    if (result != DBNull.Value)
                    {
                        // Nếu có kết quả, tăng giá trị lên một
                        nextID = Convert.ToInt32(result) + 1;
                    }

                    mydb.closeConnection();

                    // Tạo ID mới với định dạng "S" + số, ví dụ: S01, S02, vv
                    string newID = "NV" + nextID.ToString("00");

                    txtUsername.Text = newID;
                }
                else
                {
                    // Nếu txtID.Text không rỗng, hiển thị giá trị của txtID.Text
                    txtUsername.Text = staff.id;
                }
            }
            if (guna2CustomRadioButton3.Checked)
            {
                txtUsername.Enabled = true;
                txtUsername.Text = "";
            }
        }

        private void guna2CustomRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SignUp_Load(sender, e);
        }

        private void guna2CustomRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SignUp_Load(sender, e);
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

        private void guna2CustomRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            SignUp_Load(sender, e);
        }
    }
}
