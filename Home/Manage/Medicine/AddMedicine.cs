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

namespace DoAn01.Home.Manage.Medicine
{
    public partial class AddMedicine : Form
    {
        public AddMedicine()
        {
            InitializeComponent();
        }
        bool verify(string id, string name, string Price)
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
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


            if (string.IsNullOrEmpty(Price))
            {
                errorProvider3.SetError(txtPrice, "Please enter price");
                return false;
            }
            else if (!IsNumeric(Price))
            {
                errorProvider3.SetError(txtPrice, "Price can't contain characters");
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
            string id = txtID.Text;
            string name = txtName.Text;

            Medicine medicine = new Medicine();
            if (verify(id, name, txtPrice.Text))
            {
                double price = Convert.ToDouble(txtPrice.Text);
                if (medicine.addMedicine(id, name, price))
                {
                    MessageBox.Show("Add Success");
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        MY_DB mydb = new MY_DB();
        Medicine medicine = new Medicine();

        private void AddMedicine_Load(object sender, EventArgs e)
        {
            txtID.Text = medicine.id;
            if (string.IsNullOrEmpty(txtID.Text))
            {
                // Thực hiện truy vấn SQL để lấy ID lớn nhất từ cột "id" trong bảng "schedule"
                string query = "SELECT MAX(CAST(SUBSTRING(id, 2, LEN(id)) AS INT)) FROM medicine";

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
                string newID = "M" + nextID.ToString("00");

                txtID.Text = newID;
            }
            else
            {
                // Nếu txtID.Text không rỗng, hiển thị giá trị của txtID.Text
                txtID.Text = medicine.id;
            }
        }
    }
}
