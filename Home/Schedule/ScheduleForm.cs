using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn01.Home.Schedule
{
    public partial class ScheduleForm : Form
    {
        public ScheduleForm()
        {
            InitializeComponent();
        }
        Schedule schedule = new Schedule();
        string ca;
        string dentistid;
        string date;
        internal ScheduleForm(Schedule schedule,string ca, string dentistid,string date)
        {
            InitializeComponent();
            this.schedule = schedule;
            this.ca = ca;
            this.dentistid = dentistid;
            this.date = date;
        }

        private void ScheduleForm_Load(object sender, EventArgs e)
        {

            txtID.Text = schedule.Id;
            txtDentistID.Text = dentistid;
            txtPatientID.Text = schedule.PatientID;
            guna2DateTimePicker1.Value = Convert.ToDateTime(date);
            txtCa.Text = ca;
            if(schedule.Tinhtrang=="false")
            {
                RadioButtonFalse.Checked = true;
            }    
            else 
                RadioButtonTrue.Checked = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string patientid = txtPatientID.Text;
            string tinhtrang;
            if (RadioButtonFalse.Checked)
            {
                tinhtrang = "false";
            }
            else tinhtrang = "true";
            if(schedule.insertSchedule(txtID.Text,dentistid,patientid, guna2DateTimePicker1.Value,tinhtrang,ca))
            {
                MessageBox.Show("ADD thanh cong");
            }    
        }

        private void btnPhieuDieuTri_Click(object sender, EventArgs e)
        {

        }
    }
}
