﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DoAn01
{
    public partial class ChangePassword : UserControl
    {
        public string Email { get; set; }
        public ChangePassword()
        {
            InitializeComponent();
        }
        string email;
        public ChangePassword(string email)
        {
            InitializeComponent();
            this.email = email;
        }
        public bool checkPassword(string a, string b)
        {
            if (a == null || b == null)
            {
                return false;
            }
            if (a != b)
            {
                return false;
            }
            return true;
        }
        bool passwordChar = true;
        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            passwordChar = !passwordChar;
            if (passwordChar)
            {
                txtPassword.UseSystemPasswordChar = true;
                txtRepass.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
                txtRepass.UseSystemPasswordChar = false;
            }
        }
        MY_DB mydb = new MY_DB();
        private void btnChange_Click(object sender, EventArgs e)
        {

        }

        private void btnChange_Click_1(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            string repass = txtRepass.Text;
            if (checkPassword(password, repass))
            {
                SqlCommand cmd = new SqlCommand("UPDATE [User] SET password = @password WHERE email = @email", mydb.getConnection);
                mydb.openConnection();
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email); // Sử dụng giá trị email thay thế cho @email

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Password has been updated successfully.");
                }
                else
                {
                    MessageBox.Show("No account found with the given email.");
                }
            }
            else
            {
                MessageBox.Show("Enter again password!");
            }
        }
    }
}
