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

namespace DoAn01.Home.Manage.Service
{
    public partial class UC_Service : UserControl
    {
        public UC_Service()
        {
            InitializeComponent();
        }

        Service service = new Service();
        private void fillGrid(SqlCommand cmd)
        {
            this.serviceTableAdapter.Fill(this.dentalSmileDataSet4.Service);
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = service.getServices(cmd);
            dataGridView1.AllowUserToAddRows = false;
            this.serviceTableAdapter.Fill(this.dentalSmileDataSet4.Service);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddService addService = new AddService();
            addService.Show();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            service.id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            service.name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            service.price = Convert.ToDouble(dataGridView1.CurrentRow.Cells[2].Value.ToString());
            InfoService infoservice = new InfoService(service) { StartPosition = FormStartPosition.CenterScreen };
            infoservice.Show();

        }
        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from service where name like'%" + txtSearch.Text + "%'");
            fillGrid(cmd);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            service.id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            service.name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            service.price = Convert.ToDouble(dataGridView1.CurrentRow.Cells[2].Value.ToString());
            InfoService infoservice = new InfoService(service) { StartPosition = FormStartPosition.CenterScreen };
            infoservice.Show();
        }

        private void UC_Service_Load_1(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("select * from service");
            fillGrid(sqlCommand);
        }
    }


}
