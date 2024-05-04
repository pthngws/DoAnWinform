using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn01.Home.Manage.Service
{
    public partial class InfoService : Form
    {
        public InfoService()
        {
            InitializeComponent();
        }
        Service service = new Service();
        internal InfoService(Service service)
        {
            InitializeComponent();
            this.service = service;
        }

        bool verify(string id, string name, string Price)
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            if (string.IsNullOrEmpty(id))
            {
                errorProvider1.SetError(txtID, "Please enter  ID");
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

        bool IsNumeric(string input)
        {
            return int.TryParse(input, out _);
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string name = txtName.Text;

            if (verify(id, name, txtPrice.Text))
            {
                double price = Convert.ToDouble(txtPrice.Text);
                if (service.updateService(id, name, price))
                {
                    MessageBox.Show("Edit Success");
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (service.DeleteService(service.id))
            {
                MessageBox.Show("Delete Success");
            }
        }

        private void InfoService_Load(object sender, EventArgs e)
        {
            txtID.Text = service.id;
            txtName.Text = service.name;
            txtPrice.Text = service.price.ToString();
        }
    }
}
