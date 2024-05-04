using DoAn01.Home.Schedule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DoAn01.Home
{
    public partial class AddPatient : Form
    {
        public AddPatient()
        {
            InitializeComponent();
        }

        bool verify(string id, string name,string address,string phone)
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            if (string.IsNullOrEmpty(id))
            {
                errorProvider1.SetError(txtID, "Please enter ID");
                return false;
            }

            if (string.IsNullOrEmpty(name))
            {
                errorProvider2.SetError(txtName, "Please enter name");
                return false;
            }
            else if(ContainsNumbers(name))
            {
                errorProvider2.SetError(txtName, "Name can't contain numbers");
                return false;
            }

            if (string.IsNullOrEmpty(address))
            {
                errorProvider3.SetError(txtAddress, "Please enter address");
                return false;
            }

            if (string.IsNullOrEmpty(phone))
            {
                errorProvider4.SetError(txtPhone, "Please enter phone");
                return false;
            }
            else if(!IsNumeric(phone))
            {
                errorProvider4.SetError(txtPhone, "Phone can't contain characters");
                return false;
            }    
            return true;

            // Tiếp tục xử lý nếu tất cả các ràng buộc đều được đáp ứng
            // Ví dụ: gọi hàm xử lý lưu dữ liệu vào cơ sở dữ liệu
        }
        bool ContainsNumbers(string input)
        {
            return input.Any(char.IsDigit);
        }

        bool IsNumeric(string input)
        {
            return int.TryParse(input, out _);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string id = txtID.Text.Trim();
            string name = txtName.Text.Trim();
            string address = txtAddress.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string gender;
            if (RadioButtonMale.Checked)
                gender = "Male";
            else
                gender = "Female";
            DateTime dob = guna2DateTimePicker1.Value;
            if (verify(id, name, address, phone))
            {
                Patient patient = new Patient();
                if (patient.insertPatient(id, name, address, phone, dob, gender))
                {
                    MessageBox.Show("Add Success");
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void RadioButtonMale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RadioButtonFemale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        MY_DB mydb = new MY_DB();
        Patient patient = new Patient();
        private void AddPatient_Load(object sender, EventArgs e)
        {
            txtID.Text = patient.id;
            if (string.IsNullOrEmpty(txtID.Text))
            {
                // Thực hiện truy vấn SQL để lấy ID lớn nhất từ cột "id" trong bảng "schedule"
                string query = "SELECT MAX(CAST(SUBSTRING(id, 3, LEN(id)) AS INT)) FROM patient";

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
                string newID = "BN" + nextID.ToString("00");

                txtID.Text = newID;
            }
            else
            {
                // Nếu txtID.Text không rỗng, hiển thị giá trị của txtID.Text
                txtID.Text = patient.id;
            }
        }
    }
}
