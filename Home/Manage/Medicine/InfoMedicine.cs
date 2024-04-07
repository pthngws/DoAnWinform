﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn01.Home.Manage.Medicine
{
    public partial class InfoMedicine : Form
    {
        public InfoMedicine()
        {
            InitializeComponent();
        }
        Medicine medicine = new Medicine();
        internal InfoMedicine(Medicine medicine)
        {
            InitializeComponent();
            this.medicine = medicine;
        }
        private void InfoMedicine_Load(object sender, EventArgs e)
        {
            txtID.Text = medicine.id;
            txtName.Text = medicine.name;
            txtPrice.Text =medicine.price.ToString();
        }
        bool verify(string id, string name, string Price)
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
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


            if (string.IsNullOrEmpty(Price))
            {
                errorProvider3.SetError(txtPrice, "Vui lòng nhập giá");
                return false;
            }
            else if (!IsNumeric(Price))
            {
                errorProvider3.SetError(txtPrice, "Giá không được chứa chữ");
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
                if (medicine.updateMedicine(id, name, price))
                {
                    MessageBox.Show("Add thanh cong");
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if(medicine.DeleteMedicine(medicine.id))
            {
                MessageBox.Show("Delete thanh cong");
            }
        }
    }
}