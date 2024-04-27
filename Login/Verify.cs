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
using static System.Net.WebRequestMethods;
using System.Web.UI.Design.WebControls;

namespace DoAn01
{
    public partial class Verify : UserControl
    {
        public Verify()
        {
            InitializeComponent();
        }
        public string email;
        private void btnVerify_Click(object sender, EventArgs e)
        {

            
        }
        Random random = new Random();
        int otp;

        public Action<object, FormClosedEventArgs> FormClosed { get; internal set; }

        private void btnSendOTP_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSendOTP_Click_1(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                errorProvider1.SetError(txtEmail, "Vui lòng nhập Email");
            }
            else
            {
                otp = /*random.Next(100000, 1000000)*/1;

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

                    MessageBox.Show("OTP đã được gửi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public bool checks = false;
        public event EventHandler VerifySuccess;
        private void btnVerify_Click_1(object sender, EventArgs e)
        {
            if (otp.ToString() == txtOTP.Text)
            {
                email = txtEmail.Text;
                VerifySuccess?.Invoke(this, EventArgs.Empty);

            }




        }

    }
}
