using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
        string status;
        internal ScheduleForm(Schedule schedule, string ca, string dentistid, string date)
        {
            InitializeComponent();
            this.schedule = schedule;
            this.ca = ca;
            this.dentistid = dentistid;
            this.date = date;
          
            // Lấy trạng thái từ bảng schedule dựa trên id
            if (!string.IsNullOrEmpty(schedule.Id))
            {
                 status = schedule.GetStatusFromSchedule(schedule.Id);
            };
            // Đặt RadioButton tương ứng dựa trên giá trị của status
            if (status == "true")
            {
                RadioButtonTrue.Checked = true;
            }
            else
            {
                RadioButtonFalse.Checked = true;
            }
        }

        


        private void ScheduleForm_Load(object sender, EventArgs e)
        {

            txtID.Text = schedule.Id;
            txtPatientID.Text = schedule.PatientID;

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
            if(schedule.insertSchedule(txtID.Text,dentistid,patientid, Convert.ToDateTime(date), tinhtrang,ca))
            {
                MessageBox.Show("ADD thanh cong");
                
            }    
        }

        private void btnPhieuDieuTri_Click(object sender, EventArgs e)
        {
                PhieuDieuTri phieuDieuTri = new PhieuDieuTri(txtPatientID.Text, dentistid, txtID.Text);
                phieuDieuTri.Show();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ChonDichVu chonDichVu = new ChonDichVu(txtID.Text);
            chonDichVu.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if(schedule.deleteSchedule(txtID.Text))
            {
                MessageBox.Show("Xoa thanh cong");
            }
        }
    }
}
