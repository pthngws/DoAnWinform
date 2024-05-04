using DoAn01.Home.Manage.Dentist;
using Guna.UI2.WinForms;
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
    public partial class InfoStaff : Form
    {
        public InfoStaff()
        {
            InitializeComponent();
        }

        Staff staff = new Staff();
        internal InfoStaff(Staff staff)
        {
            InitializeComponent();
            this.staff = staff;
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
                errorProvider2.SetError(txtName, "Name can't contain number");
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
                errorProvider4.SetError(txtPhone, "Phone can't contain characters or exceeds 11 numbers");
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
                MemoryStream picture = new MemoryStream();

                if (pictureBox1.Image != null)
                {
                    // Nếu hình ảnh trong PictureBox tồn tại, lưu vào MemoryStream

                    pictureBox1.Image.Save(picture, pictureBox1.Image.RawFormat);
                    if (staff.UpdateStaff(staff.id, name, address, phone, dob, gender, picture))
                        MessageBox.Show("Edit Success");
                    // Sử dụng biến picture ở đây cho mục đích khác nếu cần
                }
                else
                {
                    // Nếu không có hình ảnh, bạn có thể thông báo lỗi hoặc thực hiện xử lý khác tùy thuộc vào yêu cầu của bạn
                    MessageBox.Show("Không có hình ảnh để lưu.");
                    picture = null;
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult result;
            if (staff.DeleteStaff(staff.id))
            {
                MessageBox.Show("Remove Success");
            }
        }
        MY_DB mydb = new MY_DB();
        private void InfoStaff_Load(object sender, EventArgs e)
        {
            txtID.Text = staff.id;
            txtName.Text = staff.name;
            txtPhone.Text = staff.phone;
            txtAddress.Text = staff.address;
            if (staff.Gender == "Male")
                RadioButtonMale.Checked = true;
            else
                RadioButtonMale.Checked = false;
            if (staff.Dob != DateTime.MinValue)
            {
                guna2DateTimePicker1.Value = staff.Dob;
            }
            else
            {
                guna2DateTimePicker1.Value = new DateTime(2000, 1, 1);
            }

            // Assuming you have already executed the SQL query and filled the DataTable


            // Assign the value to guna2RatingStar1

            if (staff.picture != null)
            {
                Image image = Image.FromStream(staff.picture);

                // Gán hình ảnh cho PictureBox
                pictureBox1.Image = image;
            }


        }

        private void buttonUpIMG_Click(object sender, EventArgs e)
        {

            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if ((opf.ShowDialog() == DialogResult.OK))
            {
                pictureBox1.Image = Image.FromFile(opf.FileName);
            }
        }
    }
}
