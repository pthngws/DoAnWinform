using DoAn01.Home.Manage.Dentist;
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

namespace DoAn01
{
    public partial class UC_Staff : UserControl
    {
        public UC_Staff()
        {
            InitializeComponent();
        }
        Staff staff = new Staff();
        private void fillGrid(SqlCommand cmd)
        {
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = staff.getStaffs(cmd);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void UC_Staff_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from staff");
            fillGrid(cmd);
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from staff where concat(name,phone) like'%" + txtSearch.Text + "%'");
            fillGrid(cmd);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            staff.id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            staff.name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            staff.address = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[3].Value != null && dataGridView1.CurrentRow.Cells[3].Value != DBNull.Value)
            {
                staff.Dob = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;
            }
            else
            {
                staff.Dob = new DateTime(2000, 1, 1);
            }

            staff.Gender = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            staff.phone = dataGridView1.CurrentRow.Cells[5].Value.ToString();

            InfoStaff infostaff = new InfoStaff(staff) { StartPosition = FormStartPosition.CenterScreen };
            infostaff.Show();
        }
    }
}
