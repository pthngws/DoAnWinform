using DoAn01.Home;
using DoAn01.Home.Manage.Dentist;
using DoAn01.Home.Report;
using DoAn01.Home.Schedule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
            if (Global.GlobalRole != "user")
            {
                getName();
                linkLabel1.Visible = true;
            }
            if(Global.GlobalRole == "dentist")
            {
                linkLabel2.Visible = false;
            }
            if(Global.GlobalRole == "user")
            {
                linkLabel2.Location = new Point(6, 38);
            }
        }
        User user = new User();

        MY_DB mydb = new MY_DB();
        public void getName()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string tableName = Global.GlobalRole; // Assuming GlobalRole contains the table name
            string username = Global.GlobalID; // Assuming GlobalID contains the username

            // Constructing the SQL query dynamically
            string query = $"SELECT name FROM {tableName} WHERE id = @username";

            SqlCommand cmd = new SqlCommand(query, mydb.getConnection);
            cmd.Parameters.AddWithValue("@username", username);
            sqlDataAdapter.SelectCommand = cmd;
            sqlDataAdapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string fullname = dt.Rows[0]["name"].ToString();
            
                label2.Text = "Hello, " + fullname;
            }
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Global.GlobalRole == "dentist")
            {
                Dentist dentist = new Dentist();
                InfoDentist infoDentist = new InfoDentist(dentist.GetDentistById(Global.GlobalID));
                    infoDentist.Show();
            }
            else if(Global.GlobalRole =="staff")
            {
                Staff staff = new Staff();
                InfoStaff infoStaff = new InfoStaff(staff.GetStaffById(Global.GlobalID));
                infoStaff.Show();
            }    
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChiaCa chiaCa = new ChiaCa();
            chiaCa.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ResetPassword resetPassword = new ResetPassword();
            resetPassword.Show();
        }
    }
}
