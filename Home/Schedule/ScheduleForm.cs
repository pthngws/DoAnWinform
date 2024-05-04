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
                status = "false";
                RadioButtonFalse.Checked = true;
            }
        }

        


        private void ScheduleForm_Load(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            txtID.Text = schedule.Id;
            if (string.IsNullOrEmpty(txtID.Text))
            {
                // Thực hiện truy vấn SQL để lấy ID lớn nhất từ cột "id" trong bảng "schedule"
                string query = "SELECT MAX(CAST(SUBSTRING(id, 2, LEN(id)) AS INT)) FROM schedule";

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
                string newID = "S" + nextID.ToString("00");

                txtID.Text = newID;
            }
            else
            {
                // Nếu txtID.Text không rỗng, hiển thị giá trị của txtID.Text
                txtID.Text = schedule.Id;
                btnAdd.Enabled = false;
                txtPatientID.Enabled = false;

            }

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
            errorProvider6.Clear();
            if(txtPatientID.Text =="")
            {
                errorProvider6.SetError(txtPatientID, "Please enter PatientID");
            }
            else 
            {
                if (schedule.insertSchedule(txtID.Text, dentistid, patientid, Convert.ToDateTime(date), tinhtrang, ca))
                {
                    MessageBox.Show("ADD Success");
                    btnAdd.Enabled = false;
                }
            }

               
        }
        MY_DB mydb = new MY_DB();
        private void btnPhieuDieuTri_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;

            // Tạo một truy vấn SQL để kiểm tra xem giá trị txtID.Text có tồn tại trong bảng "schedule" hay không
            string query = "SELECT COUNT(*) FROM schedule WHERE id = @id";

            SqlCommand command = new SqlCommand(query, mydb.getConnection);
            command.Parameters.AddWithValue("@id", id);

            mydb.openConnection();

            int count = (int)command.ExecuteScalar(); // Lấy số lượng bản ghi trả về từ truy vấn

            mydb.closeConnection();

            if (count > 0)
            {
                // Nếu tồn tại, mở form PhieuDieuTri
                PhieuDieuTri phieuDieuTri = new PhieuDieuTri(txtPatientID.Text, dentistid, txtID.Text);
                phieuDieuTri.Show();
            }
            else
            {
                // Nếu không tồn tại, hiển thị thông báo
                MessageBox.Show("There is no information for this ID in the Schedule table.", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }




        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (status == "false")
            {
                if (schedule.deleteSchedule(txtID.Text))
                {
                    ScheduleForm_Load(null,null);
                    MessageBox.Show("Remove Success");

                }
            }
            else
            {
                MessageBox.Show("Can't remove");
            }
        }
    }
}
