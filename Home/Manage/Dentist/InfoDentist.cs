using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn01.Home.Manage.Dentist
{
    public partial class InfoDentist : Form
    {
        public InfoDentist()
        {
            InitializeComponent();
        }
        Dentist dentist = new Dentist();
        internal InfoDentist(Dentist dentist)
        {
            InitializeComponent( );
            this.dentist = dentist;
        }
        bool verify(string id, string name, string address, string phone)
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            if (string.IsNullOrEmpty(id))
            {
                errorProvider1.SetError(txtID, "Vui lòng nhập ID");
                return false;
            }

            if (string.IsNullOrEmpty(name))
            {
                errorProvider2.SetError(txtName, "Vui lòng nhập tên");
                return false;
            }
            else if (ContainsNumbers(name))
            {
                errorProvider2.SetError(txtName, "Tên không được chứa số");
                return false;
            }

            if (string.IsNullOrEmpty(address))
            {
                errorProvider3.SetError(txtAddress, "Vui lòng nhập địa chỉ");
                return false;
            }

            if (string.IsNullOrEmpty(phone))
            {
                errorProvider4.SetError(txtPhone, "Vui lòng nhập số điện thoại");
                return false;
            }
            else if (!IsNumeric(phone))
            {
                errorProvider4.SetError(txtPhone, "Số điện thoại không được chứa chữ");
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
        private void btnEdit_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string phone = txtPhone.Text;
            string address = txtAddress.Text;
            string gender;
            if (RadioButtonMale.Checked)
                gender = "Male";
            else gender = "Female";
            DateTime dob = guna2DateTimePicker1.Value;
            if (verify(txtID.Text, name, address, phone))
            {
                if (dentist.UpdateDentist(dentist.id, name, address, phone, dob, gender))
                    MessageBox.Show("Update thanh cong");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult result;
            if (dentist.DeleteDentist(dentist.id))
            {
                MessageBox.Show("Delete thanh cong");
            }
        }

        private void InfoDentist_Load(object sender, EventArgs e)
        {
            txtID.Text = dentist.id;
            txtName.Text = dentist.name;
            txtPhone.Text = dentist.phone;
            txtAddress.Text = dentist.address;
            if (dentist.Gender == "Male")
                RadioButtonMale.Checked = true;
            else
                RadioButtonMale.Checked = false;
            guna2DateTimePicker1.Value = dentist.Dob;
            guna2RatingStar1.Value = Convert.ToInt32(dentist.rating);
        }
    }
}
