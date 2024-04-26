using DoAn01.Home;
using DoAn01.Home.Report;
using DoAn01.Home.Schedule;
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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            UC_Home uC_Home = new UC_Home();
            addUserControl(uC_Home);
        }
        User user = new User();

        public Main(User user)
        {
            InitializeComponent();
            UC_Home uC_Home = new UC_Home();
            addUserControl(uC_Home);
            this.user = user;
            label2.Text = user.Username;
        }
        private void Main_Load(object sender, EventArgs e)
        {
        }
        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            UC_Home uC_Home = new UC_Home();
            addUserControl(uC_Home);
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            UC_Report uC_Report = new UC_Report();
            addUserControl(uC_Report);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            UC_Manage uC_Manage = new UC_Manage();
            addUserControl(uC_Manage);
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            UC_Schedule uC_Schedule = new UC_Schedule();
            addUserControl(uC_Schedule);
        }
    }
}
