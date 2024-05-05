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

namespace DoAn01.Home
{
    public partial class UC_Patient : UserControl
    {
        public UC_Patient()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddPatient addPatient = new AddPatient() { StartPosition = FormStartPosition.CenterScreen };
            addPatient.Show();
        }

        private void btnFaceID_Click(object sender, EventArgs e)
        {
            LoginFaceID loginFaceID = new LoginFaceID
            {
                // Thiết lập StartPosition để form mới mở ở giữa màn hình
                StartPosition = FormStartPosition.CenterScreen
            };
            loginFaceID.Show(); // Sử dụng Show() thay vì ShowDialog()
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Patient patient = new Patient();
            patient.id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            patient.name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            patient.address = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            patient.Dob = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;
            patient.Gender = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            patient.phone = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            InfoPatient infoPatient = new InfoPatient(patient) { StartPosition = FormStartPosition.CenterScreen};
            infoPatient.Show();
        }
        Patient patient = new Patient();
        private void UC_Patient_Load(object sender, EventArgs e)
        {
           
            SqlCommand cmd = new SqlCommand("select * from patient");
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = patient.getPatients(cmd);
            dataGridView1.AllowUserToAddRows = false;
           
            dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
        }
        private void fillGrid(SqlCommand cmd)
        {
     
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = patient.getPatients(cmd);
            dataGridView1.AllowUserToAddRows = false;
       
            dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2TextBox1_KeyUp(object sender, KeyEventArgs e)
        {

            SqlCommand cmd = new SqlCommand("select * from patient where concat(name,phone) like'%" + txtSearch.Text + "%'");
            fillGrid(cmd);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
