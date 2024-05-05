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
    public partial class UC_Medicine : UserControl
    {
        public UC_Medicine()
        {
            InitializeComponent();
        }

        Medicine medicine = new Medicine();
        private void fillGrid(SqlCommand cmd)
        {
          
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = medicine.getMedicines(cmd);
            dataGridView1.AllowUserToAddRows = false;
   
        }

        private void UC_Medicine_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from medicine");
            fillGrid(cmd);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            medicine.id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            medicine.name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            medicine.price =Convert.ToDouble(dataGridView1.CurrentRow.Cells[2].Value.ToString());
            InfoMedicine infomedicine = new InfoMedicine(medicine) { StartPosition = FormStartPosition.CenterScreen };
            infomedicine.Show();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from medicine where name like'%" + txtSearch.Text + "%'");
            fillGrid(cmd);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddMedicine addmedicine = new AddMedicine() { StartPosition = FormStartPosition.CenterScreen };
            addmedicine.Show();
        }

        private void medicineBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void patientBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
