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
    public partial class AddDentist : Form
    {
        public AddDentist()
        {
            InitializeComponent();
        }
        bool verify(string id, string name, string address, string phone)
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
            else if (ContainsNumbers(name))
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
            else if (!IsNumeric(phone))
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

        private bool IsNumeric(string input)
        {
            foreach (char c in input)
            {
                // Kiểm tra nếu ký tự không phải là số
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
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
                Dentist dentist = new Dentist();
                if (dentist.insertDentist(id, name, address, phone, dob, gender))
                {
                    MessageBox.Show("Add Success");
                }
            }
            
        }
    }
}
