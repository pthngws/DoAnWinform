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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            Login login = new Login();
            addUserControl(login);
        }

        private void LoginButtonClickedEventHandler(object sender, EventArgs e)
        {
            // Đóng form hiện tại (Form1)
            this.Hide() ;
        }

        private void LoginButtonFaceIDClickedEventHandler(object sender, EventArgs e)
        {
            // Đóng form hiện tại (Form1)
            this.Hide();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
        }



        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }



        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            addUserControl(login);
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            SignUp sign = new SignUp();
            addUserControl(sign);
        }
    }
}
