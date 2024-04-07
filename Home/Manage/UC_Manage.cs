using DoAn01.Home.Manage.Dentist;
using DoAn01.Home.Manage.Medicine;
using DoAn01.Home.Manage.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn01.Home
{
    public partial class UC_Manage : UserControl
    {
        public UC_Manage()
        {
            InitializeComponent();
            UC_Patient uC_Patient = new UC_Patient();
            addUserControl(uC_Patient);
        }
        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }
        private void btnPatient_Click(object sender, EventArgs e)
        {
            UC_Patient uC_Patient = new UC_Patient();
            addUserControl(uC_Patient);
        }

        private void btnMedicine_Click(object sender, EventArgs e)
        {
            UC_Medicine medicine = new UC_Medicine();
            addUserControl(medicine);
        }

        private void btnDentist_Click(object sender, EventArgs e)
        {
            UC_Dentist dentist = new UC_Dentist();
            addUserControl(dentist);
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            UC_Service uC_Service = new UC_Service();
            addUserControl(uC_Service);
        }
    }
}
