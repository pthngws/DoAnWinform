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

namespace DoAn01.Home.Manage.Dentist
{
    public partial class UC_Dentist : UserControl
    {
        public UC_Dentist()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddDentist adddentist = new AddDentist();
            adddentist.Show();
        }
        Dentist dentist = new Dentist();
        private void fillGrid(SqlCommand cmd)
        {
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = dentist.getDentists(cmd);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void UC_Dentist_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from dentist");
            fillGrid(cmd);
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from dentist where concat(name,phone) like'%" + txtSearch.Text + "%'");
            fillGrid(cmd);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dentist.id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            dentist.name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            dentist.address = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dentist.Dob = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;
            dentist.Gender = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            dentist.phone = dataGridView1.CurrentRow.Cells[5].Value.ToString();

            InfoDentist infodentist = new InfoDentist(dentist) { StartPosition = FormStartPosition.CenterScreen };
            infodentist.Show();
        }
    }
}
